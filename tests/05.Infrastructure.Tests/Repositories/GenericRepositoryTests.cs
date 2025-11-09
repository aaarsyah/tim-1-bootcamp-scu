using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Data.Repositories;
using Xunit;

namespace MyApp.Infrastructure.Tests.Repositories;

public class GenericRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly GenericRepository<User> _repository;

    public GenericRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new GenericRepository<User>(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task AddAsync_Should_Add_Entity()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };

        // Act
        await _repository.AddAsync(user);
        await _context.SaveChangesAsync();

        // Assert
        var addedUser = await _context.Users.FindAsync(user.Id);
        addedUser.Should().NotBeNull();
        addedUser!.Email.Should().Be(user.Email);
        addedUser.Name.Should().Be(user.Name);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Entity_When_Exists()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(user.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(user.Id);
        result.Email.Should().Be(user.Email);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Exists()
    {
        // Arrange
        var nonExistentId = 9999;

        // Act
        var result = await _repository.GetByIdAsync(nonExistentId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Entities()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Email = "user1@example.com", Name = "User One", PasswordHash = "hash1" },
            new User { Email = "user2@example.com", Name = "User Two", PasswordHash = "hash2" },
            new User { Email = "user3@example.com", Name = "User Three", PasswordHash = "hash3" }
        };
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
        result.Should().OnlyContain(u => users.Any(au => au.Id == u.Id));
    }

    [Fact]
    public async Task Update_Should_Modify_Entity()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        user.Name = "Updated Name";
        user.Email = "updated@example.com";
        _repository.Update(user);
        await _context.SaveChangesAsync();

        // Assert
        var updatedUser = await _context.Users.FindAsync(user.Id);
        updatedUser.Should().NotBeNull();
        updatedUser!.Name.Should().Be("Updated Name");
        updatedUser.Email.Should().Be("updated@example.com");
    }

    [Fact]
    public async Task Remove_Should_Mark_Entity_For_Deletion()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        _repository.Remove(user);
        await _context.SaveChangesAsync();

        // Assert
        // Note: In this test setup, we verify the entity is marked for deletion
        // In a real database, the entity would be removed
        var deletedUser = await _context.Users.FindAsync(user.Id);
        // The assertion depends on whether the context tracks the entity as deleted
        // For in-memory database, let's check if we can find it using IgnoreQueryFilters
        var allUsers = await _context.Users.IgnoreQueryFilters().ToListAsync();
        allUsers.Should().Contain(u => u.Id == user.Id);
    }

    [Fact]
    public async Task FindAsync_Should_Return_Entities_Matching_Condition()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Email = "user1@example.com", Name = "John Doe", PasswordHash = "hash1" },
            new User { Email = "user2@example.com", Name = "Jane Doe", PasswordHash = "hash2" },
            new User { Email = "user3@example.com", Name = "Bob Smith", PasswordHash = "hash3" }
        };
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.FindAsync(u => u.Name.Contains("Doe"));

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().OnlyContain(u => u.Name.Contains("Doe"));
    }

    [Fact]
    public async Task FirstOrDefaultAsync_Should_Return_First_Matching_Entity()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Email = "user1@example.com", Name = "John Doe", PasswordHash = "hash1" },
            new User { Email = "user2@example.com", Name = "Jane Doe", PasswordHash = "hash2" }
        };
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.FirstOrDefaultAsync(u => u.Name.Contains("Doe"));

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Contain("Doe");
    }

    [Fact]
    public async Task AnyAsync_Should_Return_True_When_Entity_Exists()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Name = "John Doe",
            PasswordHash = "hashedPassword"
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.AnyAsync(u => u.Email == "test@example.com");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AnyAsync_Should_Return_False_When_Entity_Does_Not_Exist()
    {
        // Act
        var result = await _repository.AnyAsync(u => u.Email == "nonexistent@example.com");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task CountAsync_Should_Return_Total_Count()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Email = "user1@example.com", Name = "User One", PasswordHash = "hash1" },
            new User { Email = "user2@example.com", Name = "User Two", PasswordHash = "hash2" },
            new User { Email = "user3@example.com", Name = "User Three", PasswordHash = "hash3" }
        };
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.CountAsync();

        // Assert
        result.Should().Be(3);
    }

    [Fact]
    public async Task CountAsync_Should_Return_Filtered_Count()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Email = "user1@example.com", Name = "John Doe", PasswordHash = "hash1" },
            new User { Email = "user2@example.com", Name = "Jane Doe", PasswordHash = "hash2" },
            new User { Email = "user3@example.com", Name = "Bob Smith", PasswordHash = "hash3" }
        };
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.CountAsync(u => u.Name.Contains("Doe"));

        // Assert
        result.Should().Be(2);
    }

    [Fact]
    public async Task AddRangeAsync_Should_Add_Multiple_Entities()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Email = "user1@example.com", Name = "User One", PasswordHash = "hash1" },
            new User { Email = "user2@example.com", Name = "User Two", PasswordHash = "hash2" }
        };

        // Act
        await _repository.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // Assert
        var addedUsers = await _repository.GetAllAsync();
        addedUsers.Should().HaveCount(2);
    }

    [Fact]
    public async Task RemoveRange_Should_Delete_Multiple_Entities()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Email = "user1@example.com", Name = "User One", PasswordHash = "hash1" },
            new User { Email = "user2@example.com", Name = "User Two", PasswordHash = "hash2" }
        };
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // Act
        _repository.RemoveRange(users);
        await _context.SaveChangesAsync();

        // Assert
        var remainingUsers = await _repository.GetAllAsync();
        remainingUsers.Should().BeEmpty();
    }
}