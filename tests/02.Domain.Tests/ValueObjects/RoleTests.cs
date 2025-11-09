using FluentAssertions;
using MyApp.Domain.Models;
using Xunit;

namespace MyApp.Domain.Tests.ValueObjects;

public class RoleTests
{
    [Fact]
    public void Role_Should_Initialize_With_Correct_Values()
    {
        // Arrange
        var roleName = "Administrator";
        var description = "System administrator role";

        // Act
        var role = new Role
        {
            Name = roleName,
            Description = description
        };

        // Assert
        role.Name.Should().Be(roleName);
        role.Description.Should().Be(description);
    }

    [Fact]
    public void Role_Should_Initialize_Collections()
    {
        // Arrange & Act
        var role = new Role();

        // Assert
        role.UserRoles.Should().NotBeNull();
        role.RoleClaims.Should().NotBeNull();
        role.UserRoles.Should().BeEmpty();
        role.RoleClaims.Should().BeEmpty();
    }

    [Fact]
    public void UserRole_Should_Assign_User_To_Role()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        var role = new Role { Name = "User" };

        // Act
        var userRole = new UserRole
        {
            User = user,
            Role = role
        };

        // Assert
        userRole.User.Should().Be(user);
        userRole.Role.Should().Be(role);
    }

    [Fact]
    public void UserClaim_Should_Assign_Claim_To_User()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        var claimType = "permission";
        var claimValue = "read:users";

        // Act
        var userClaim = new UserClaim
        {
            User = user,
            ClaimType = claimType,
            ClaimValue = claimValue
        };

        // Assert
        userClaim.User.Should().Be(user);
        userClaim.ClaimType.Should().Be(claimType);
        userClaim.ClaimValue.Should().Be(claimValue);
    }

    [Fact]
    public void RoleClaim_Should_Assign_Claim_To_Role()
    {
        // Arrange
        var role = new Role { Name = "Admin" };
        var claimType = "permission";
        var claimValue = "delete:users";

        // Act
        var roleClaim = new RoleClaim
        {
            Role = role,
            ClaimType = claimType,
            ClaimValue = claimValue
        };

        // Assert
        roleClaim.Role.Should().Be(role);
        roleClaim.ClaimType.Should().Be(claimType);
        roleClaim.ClaimValue.Should().Be(claimValue);
    }

    [Fact]
    public void AuditLog_Should_Record_Changes()
    {
        // Arrange
        var action = "CREATE";
        var entityName = "User";
        var entityId = 1;
        var oldValues = "{}";
        var newValues = "{\"name\":\"John Doe\"}";
        var ipAddress = "192.168.1.1";
        var userAgent = "TestAgent/1.0";

        // Act
        var auditLog = new AuditLog
        {
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            OldValues = oldValues,
            NewValues = newValues,
            IpAddress = ipAddress,
            UserAgent = userAgent
        };

        // Assert
        auditLog.Action.Should().Be(action);
        auditLog.EntityName.Should().Be(entityName);
        auditLog.EntityId.Should().Be(entityId);
        auditLog.OldValues.Should().Be(oldValues);
        auditLog.NewValues.Should().Be(newValues);
        auditLog.IpAddress.Should().Be(ipAddress);
        auditLog.UserAgent.Should().Be(userAgent);
    }

    [Fact]
    public void AuditLog_Should_Handle_Null_Values()
    {
        // Arrange
        var action = "DELETE";
        var entityName = "User";
        var entityId = 1;

        // Act
        var auditLog = new AuditLog
        {
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            OldValues = null,
            NewValues = null
        };

        // Assert
        auditLog.Action.Should().Be(action);
        auditLog.EntityName.Should().Be(entityName);
        auditLog.EntityId.Should().Be(entityId);
        auditLog.OldValues.Should().BeNull();
        auditLog.NewValues.Should().BeNull();
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData("User")]
    [InlineData("Manager")]
    [InlineData("Guest")]
    public void Role_Should_Accept_Valid_Names(string roleName)
    {
        // Arrange & Act
        var role = new Role { Name = roleName };

        // Assert
        role.Name.Should().Be(roleName);
    }

    [Theory]
    [InlineData("System administrator with full access")]
    [InlineData("Basic user role with limited permissions")]
    [InlineData("Temporary role for contractors")]
    public void Role_Should_Accept_Valid_Descriptions(string description)
    {
        // Arrange & Act
        var role = new Role { Name = "TestRole", Description = description };

        // Assert
        role.Description.Should().Be(description);
    }
}