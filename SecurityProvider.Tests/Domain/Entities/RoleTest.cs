using System;
using SecurityProvider.Domain;
using SecurityProvider.Domain.Entities.Role;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class RoleTest
{
    private static readonly string _name = "s3_writer";
    private static readonly string _description = "allow write in s3";

    public static Role GetRole()
    {
        return new Role(new RoleRequiredFields() { Name = _name });
    }

    private Role GetHydratedRole()
    {
        var role = GetRole();
        role.HydrateOptionalFields(new RoleOptionalFields() { Description = _description });
        return role;
    }

    private Role GetDeletedRole()
    {
        var role = GetRole();
        role.Delete();
        return role;
    }

    [Fact]
    public void Create_Valid()
    {
        DateTime minDate = DateTime.Now;
        var roleRequiredFields = new RoleRequiredFields() { Name = _name };

        var role = new Role(roleRequiredFields);
        DateTime maxDate = DateTime.Now;

        Assert.Equal(_name, role.Name);
        Assert.IsType<Guid>(role.Id);
        Assert.IsType<DateTime>(role.CreatedAt);
        Assert.True(role.CreatedAt >= minDate);
        Assert.True(role.CreatedAt <= maxDate);
        Assert.False(role.Deleted);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Create_Invalid(string name)
    {
        var roleRequiredFields = new RoleRequiredFields() { Name = name };
        var action = new Action(() => new Role(roleRequiredFields));
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void HydrateRequiredFields_SetNewFieldValue()
    {
        string newName = "s3_reader";
        var requiredFields = new RoleRequiredFields() { Name = newName };
        var role = GetRole();
        var roleId = role.Id;
        var roleCreatedAt = role.CreatedAt;

        role.HydrateRequiredFields(requiredFields);

        Assert.Equal(newName, role.Name);
        Assert.Equal(roleId, role.Id);
        Assert.Equal(roleCreatedAt, role.CreatedAt);
        Assert.False(role.Deleted);
    }

    [Fact]
    public void HydrateRequiredFields_IgnoreFieldNotInformed()
    {
        var requiredFields = new RoleRequiredFields() { };
        var role = GetRole();
        var roleId = role.Id;
        var roleCreatedAt = role.CreatedAt;

        role.HydrateRequiredFields(requiredFields);

        Assert.Equal(_name, role.Name);
        Assert.Equal(roleId, role.Id);
        Assert.Equal(roleCreatedAt, role.CreatedAt);
        Assert.False(role.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HydrateRequiredFields_Invalid(string name)
    {
        var role = GetRole();
        var roleId = role.Id;
        var roleRequiredFields = new RoleRequiredFields() { Name = name };

        var action = new Action(() => role.HydrateRequiredFields(roleRequiredFields));

        Assert.Throws<DomainException>(action);
        Assert.Equal(roleId, role.Id);
        Assert.False(role.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_SetNewFieldValue()
    {
        var optionalFields = new RoleOptionalFields() { Description = _description };
        var role = GetRole();
        var roleId = role.Id;

        role.HydrateOptionalFields(optionalFields);

        Assert.Equal(_description, role.Description);
        Assert.Equal(roleId, role.Id);
        Assert.False(role.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_IgnoreFieldNotInformed()
    {
        var optionalFields = new RoleOptionalFields() { };
        var role = GetHydratedRole();

        role.HydrateOptionalFields(optionalFields);

        Assert.Equal(_description, role.Description);
        Assert.False(role.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void HydrateOptionalFields_Invalid(string description)
    {
        var role = GetRole();
        var roleId = role.Id;
        var roleOptionalFields = new RoleOptionalFields() { Description = description };

        var action = new Action(() => role.HydrateOptionalFields(roleOptionalFields));

        Assert.Throws<DomainException>(action);
        Assert.Equal(roleId, role.Id);
        Assert.False(role.Deleted);
    }

    [Fact]
    public void Delete()
    {
        var role = GetRole();
        role.Delete();
        Assert.True(role.Deleted);
    }


    [Fact]
    public void HydrateRequiredFields_Deleted()
    {
        var role = GetDeletedRole();
        var roleRequiredFields = new RoleRequiredFields() { Name = "s3_reader" };

        var action = new Action(() => role.HydrateRequiredFields(roleRequiredFields));

        Assert.Throws<DomainException>(action);
        Assert.True(role.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_Deleted()
    {
        var role = GetDeletedRole();
        var roleOptionalFields = new RoleOptionalFields() { Description = _description };

        var action = new Action(() => role.HydrateOptionalFields(roleOptionalFields));

        Assert.Throws<DomainException>(action);
        Assert.True(role.Deleted);
    }

    [Fact]
    public void Rebuild()
    {
        var savedRole = GetRole();
        var requiredFields = new RoleRequiredFields() { Name = savedRole.Name };
        var selfGeneratedFields = new RoleSelfGeneratedFields()
        {
            Id = savedRole.Id,
            CreatedAt = savedRole.CreatedAt,
            Deleted = savedRole.Deleted
        };

        var role = new RoleBuilder().Rebuild(requiredFields, selfGeneratedFields);

        Assert.Equal(savedRole, role);
    }

    [Fact]
    public void AddPermission()
    {
        var role = GetRole();
        var policy = PolicyTest.GetPolicy();

        role.AddPermission(policy);
        role.AddPermission(policy);

        Assert.Contains(policy, role.Permissions);
        Assert.Single(role.Permissions);
    }

    [Fact]
    public void RemovePermission()
    {
        var role = GetRole();
        var policy = PolicyTest.GetPolicy();
        role.AddPermission(policy);

        role.RemovePermission(policy);

        Assert.DoesNotContain(policy, role.Permissions);
    }
}