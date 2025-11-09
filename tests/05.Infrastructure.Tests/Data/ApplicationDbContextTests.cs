using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Data;
using Xunit;

namespace MyApp.Infrastructure.Tests.Data;

public class ApplicationDbContextTests : IDisposable
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void ApplicationDbContext_Should_Initialize_DbSets()
    {
        // Assert
        _context.Users.Should().NotBeNull();
        _context.Roles.Should().NotBeNull();
        _context.UserRoles.Should().NotBeNull();
        _context.UserClaims.Should().NotBeNull();
        _context.RoleClaims.Should().NotBeNull();
        _context.AuditLogs.Should().NotBeNull();
    }

    [Fact]
    public async Task SaveChangesAsync_Should_Set_CreatedAt_For_New_Entities()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };

        // Act
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Assert
        user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        user.UpdatedAt.Should().BeNull(); // Not updated yet
        user.IsDeleted.Should().BeFalse();
    }

    [Fact]
    public async Task SaveChangesAsync_Should_Update_UpdatedAt_Field_For_Modified_Entities()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var originalCreatedAt = user.CreatedAt;

        // Act
        Thread.Sleep(10); // Small delay to ensure different timestamp
        user.Name = "Updated Name";
        await _context.SaveChangesAsync();

        // Assert
        user.CreatedAt.Should().Be(originalCreatedAt); // Should not change
        user.UpdatedAt.Should().NotBeNull();
        user.UpdatedAt!.Value.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task SaveChangesAsync_Should_Soft_Delete_Entities()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        // Assert
        user.IsDeleted.Should().BeTrue();
        user.UpdatedAt.Should().NotBeNull();

        // Should not appear in normal queries due to query filter
        var activeUsers = await _context.Users.ToListAsync();
        activeUsers.Should().BeEmpty();

        // But should appear when ignoring query filters
        var allUsers = await _context.Users.IgnoreQueryFilters().ToListAsync();
        allUsers.Should().HaveCount(1);
    }

    [Fact]
    public async Task OnModelCreating_Should_Allow_Multiple_Users_In_Memory_Database()
    {
        // Arrange
        var user1 = new User
        {
            Email = "user1@example.com",
            Name = "User One",
            PasswordHash = "hash1"
        };

        // Act
        _context.Users.Add(user1);
        await _context.SaveChangesAsync();

        // Assert
        var user2 = new User
        {
            Email = "user2@example.com",
            Name = "User Two",
            PasswordHash = "hash2"
        };
        _context.Users.Add(user2);
        Func<Task> act = async () => await _context.SaveChangesAsync();
        await act.Should().NotThrowAsync<DbUpdateException>();

        var users = await _context.Users.ToListAsync();
        users.Should().HaveCount(2);
    }

    [Fact]
    public async Task SeedData_Should_Create_Default_Roles_And_Users()
    {
        // Act - The seed data should be created when the database is created
        await _context.Database.EnsureCreatedAsync();

        // Assert
        var seededRoles = await _context.Roles.ToListAsync();
        seededRoles.Should().HaveCount(3);
        seededRoles.Should().Contain(r => r.Name == "Administrator");
        seededRoles.Should().Contain(r => r.Name == "User");
        seededRoles.Should().Contain(r => r.Name == "Manager");

        var seededUsers = await _context.Users.ToListAsync();
        seededUsers.Should().HaveCount(1);
        seededUsers.First().Email.Should().Be("admin@myapp.com");
        seededUsers.First().Name.Should().Be("System Administrator");

        var seededUserRoles = await _context.UserRoles.ToListAsync();
        seededUserRoles.Should().HaveCount(1);
        seededUserRoles.First().UserId.Should().Be(1);
        seededUserRoles.First().RoleId.Should().Be(1);
    }

    [Fact]
    public async Task QueryFilter_Should_Filter_Soft_Deleted_Entities()
    {
        // Arrange
        var user1 = new User
        {
            Email = "active@example.com",
            Name = "Active User",
            PasswordHash = "hash1"
        };
        var user2 = new User
        {
            Email = "inactive@example.com",
            Name = "Inactive User",
            PasswordHash = "hash2"
        };

        _context.Users.AddRange(user1, user2);
        await _context.SaveChangesAsync();

        // Soft delete one user
        _context.Users.Remove(user2);
        await _context.SaveChangesAsync();

        // Act
        var activeUsers = await _context.Users.ToListAsync();
        var allUsers = await _context.Users.IgnoreQueryFilters().ToListAsync();

        // Assert
        activeUsers.Should().HaveCount(1);
        activeUsers.First().Email.Should().Be("active@example.com");

        allUsers.Should().HaveCount(2);
    }

    [Fact]
    public async Task UserRole_Relationship_Should_Work_Correctly()
    {
        // Arrange
        await _context.Database.EnsureCreatedAsync(); // Load seed data

        var user = new User
        {
            Email = "newuser@example.com",
            Name = "New User",
            PasswordHash = "hashedPassword"
        };

        // Get existing role
        var userRole = await _context.Roles.FirstAsync(r => r.Name == "User");

        // Act
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var userRoleAssignment = new UserRole
        {
            UserId = user.Id,
            RoleId = userRole.Id
        };
        _context.UserRoles.Add(userRoleAssignment);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedUser = await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstAsync(u => u.Id == user.Id);

        retrievedUser.UserRoles.Should().HaveCount(1);
        retrievedUser.UserRoles.First().Role.Name.Should().Be("User");
    }

    [Fact]
    public async Task DatabaseOperations_Should_Work_Without_Transactions_In_Memory_Database()
    {
        // Arrange
        await _context.Database.EnsureDeletedAsync(); // Start with clean database

        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };

        // Act - In-memory database doesn't support transactions, so we test normal operations
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Add another user
        var user2 = new User
        {
            Email = "test2@example.com",
            Name = "Jane Doe",
            PasswordHash = "hashedPassword2"
        };
        _context.Users.Add(user2);
        await _context.SaveChangesAsync();

        // Assert
        var users = await _context.Users.ToListAsync();
        users.Should().HaveCount(2);
        users.Should().Contain(u => u.Email == "test@example.com");
        users.Should().Contain(u => u.Email == "test2@example.com");
    }

    [Fact]
    public async Task UserClaims_Should_Work_Correctly()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };

        // Act
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var userClaim = new UserClaim
        {
            UserId = user.Id,
            ClaimType = "permission",
            ClaimValue = "read:profile"
        };
        _context.UserClaims.Add(userClaim);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedUser = await _context.Users
            .Include(u => u.UserClaims)
            .FirstAsync(u => u.Id == user.Id);

        retrievedUser.UserClaims.Should().HaveCount(1);
        retrievedUser.UserClaims.First().ClaimType.Should().Be("permission");
        retrievedUser.UserClaims.First().ClaimValue.Should().Be("read:profile");
    }

    [Fact]
    public async Task AuditLog_Should_Store_Correct_Information()
    {
        // Arrange
        var auditLog = new AuditLog
        {
            Action = "CREATE",
            EntityName = "User",
            EntityId = 1,
            OldValues = null,
            NewValues = "{\"name\":\"John Doe\"}",
            IpAddress = "192.168.1.1",
            UserAgent = "TestAgent/1.0"
        };

        // Act
        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedLog = await _context.AuditLogs.FirstAsync();
        retrievedLog.Action.Should().Be("CREATE");
        retrievedLog.EntityName.Should().Be("User");
        retrievedLog.EntityId.Should().Be(1);
        retrievedLog.IpAddress.Should().Be("192.168.1.1");
        retrievedLog.UserAgent.Should().Be("TestAgent/1.0");
        retrievedLog.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}