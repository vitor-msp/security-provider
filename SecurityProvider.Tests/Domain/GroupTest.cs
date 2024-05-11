using System;
using System.Collections.Generic;
using SecurityProvider.Domain;
using SecurityProvider.Domain.Entities.Group;
using SecurityProvider.Domain.Entities.User;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class GroupTest
{
    private readonly string _name = "developers";
    private readonly string _description = "development department group";

    private Group GetGroup()
    {
        return new Group(new GroupRequiredFields() { Name = _name });
    }

    private Group GetHydratedGroup()
    {
        var group = GetGroup();
        group.HydrateOptionalFields(new GroupOptionalFields() { Description = _description });
        return group;
    }

    private Group GetDeletedGroup()
    {
        var group = GetGroup();
        group.Delete();
        return group;
    }

    [Fact]
    public void Create_Valid()
    {
        DateTime minDate = DateTime.Now;
        var groupRequiredFields = new GroupRequiredFields() { Name = _name };

        var group = new Group(groupRequiredFields);
        DateTime maxDate = DateTime.Now;

        Assert.Equal(_name, group.Name);
        Assert.IsType<Guid>(group.Id);
        Assert.IsType<DateTime>(group.CreatedAt);
        Assert.True(group.CreatedAt >= minDate);
        Assert.True(group.CreatedAt <= maxDate);
        Assert.False(group.Deleted);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void Create_Invalid(string name)
    {
        var groupRequiredFields = new GroupRequiredFields() { Name = name };
        var action = new Action(() => new Group(groupRequiredFields));
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void HydrateRequiredFields_SetNewFieldValue()
    {
        string newName = "human_resources";
        var requiredFields = new GroupRequiredFields() { Name = newName };
        var group = GetGroup();
        var groupId = group.Id;
        var groupCreatedAt = group.CreatedAt;

        group.HydrateRequiredFields(requiredFields);

        Assert.Equal(newName, group.Name);
        Assert.Equal(groupId, group.Id);
        Assert.Equal(groupCreatedAt, group.CreatedAt);
        Assert.False(group.Deleted);
    }

    [Fact]
    public void HydrateRequiredFields_IgnoreFieldNotInformed()
    {
        var requiredFields = new GroupRequiredFields() { };
        var group = GetGroup();
        var groupId = group.Id;
        var groupCreatedAt = group.CreatedAt;

        group.HydrateRequiredFields(requiredFields);

        Assert.Equal(_name, group.Name);
        Assert.Equal(groupId, group.Id);
        Assert.Equal(groupCreatedAt, group.CreatedAt);
        Assert.False(group.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HydrateRequiredFields_Invalid(string name)
    {
        var group = GetGroup();
        var groupId = group.Id;
        var groupRequiredFields = new GroupRequiredFields() { Name = name };

        var action = new Action(() => group.HydrateRequiredFields(groupRequiredFields));

        Assert.Throws<DomainException>(action);
        Assert.Equal(groupId, group.Id);
        Assert.False(group.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_SetNewFieldValue()
    {
        var optionalFields = new GroupOptionalFields() { Description = _description };
        var group = GetGroup();
        var groupId = group.Id;

        group.HydrateOptionalFields(optionalFields);

        Assert.Equal(_description, group.Description);
        Assert.Equal(groupId, group.Id);
        Assert.False(group.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_IgnoreFieldNotInformed()
    {
        var optionalFields = new GroupOptionalFields() { };
        var group = GetHydratedGroup();

        group.HydrateOptionalFields(optionalFields);

        Assert.Equal(_description, group.Description);
        Assert.False(group.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void HydrateOptionalFields_Invalid(string description)
    {
        var group = GetGroup();
        var groupId = group.Id;
        var groupOptionalFields = new GroupOptionalFields() { Description = description };

        var action = new Action(() => group.HydrateOptionalFields(groupOptionalFields));

        Assert.Throws<DomainException>(action);
        Assert.Equal(groupId, group.Id);
        Assert.False(group.Deleted);
    }

    [Fact]
    public void Delete()
    {
        var group = GetGroup();
        group.Delete();
        Assert.True(group.Deleted);
    }


    [Fact]
    public void HydrateRequiredFields_Deleted()
    {
        var group = GetDeletedGroup();
        var groupRequiredFields = new GroupRequiredFields() { Name = "human_resources" };

        var action = new Action(() => group.HydrateRequiredFields(groupRequiredFields));

        Assert.Throws<DomainException>(action);
        Assert.True(group.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_Deleted()
    {
        var group = GetDeletedGroup();
        var groupOptionalFields = new GroupOptionalFields() { Description = _description };

        var action = new Action(() => group.HydrateOptionalFields(groupOptionalFields));

        Assert.Throws<DomainException>(action);
        Assert.True(group.Deleted);
    }

    [Fact]
    public void Rebuild()
    {
        var savedGroup = GetGroup();
        var requiredFields = new GroupRequiredFields()
        {
            Name = savedGroup.Name
        };
        var selfGeneratedFields = new GroupSelfGeneratedFields()
        {
            Id = savedGroup.Id,
            CreatedAt = savedGroup.CreatedAt,
            Deleted = savedGroup.Deleted
        };

        var group = new GroupBuilder().Rebuild(requiredFields, selfGeneratedFields);

        Assert.Equal(savedGroup, group);
    }

    [Fact]
    public void AddUser()
    {
        var group = GetGroup();
        var user = UserTest.GetUser();

        group.AddUser(user);

        Assert.Contains(user, group.Users);
    }

    [Fact]
    public void RemoveUser()
    {
        var group = GetGroup();
        var user = UserTest.GetUser();
        group.AddUser(user);

        group.RemoveUser(user);

        Assert.DoesNotContain(user, group.Users);
    }
}