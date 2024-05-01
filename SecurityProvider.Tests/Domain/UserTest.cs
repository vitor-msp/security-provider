using System;
using System.Text.Json;
using SecurityProvider.Domain;
using SecurityProvider.Domain.Entities.User;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class UserTest
{
    private readonly string _username = "fulano";
    private readonly string _name = "Fulano de Tal";
    private readonly string _department = "development";

    private User GetUser()
    {
        return new User(new UserRequiredFields()
        {
            Username = _username,
            Name = _name
        });
    }

    private User GetHydratedUser()
    {
        var user = GetUser();
        user.HydrateOptionalFields(new UserOptionalFields() { Department = _department });
        return user;
    }

    private User GetDeletedUser()
    {
        var user = GetUser();
        user.Delete();
        return user;
    }

    [Fact]
    public void Create_Valid()
    {
        DateTime minDate = DateTime.Now;
        var userRequiredFields = new UserRequiredFields()
        {
            Username = _username,
            Name = _name
        };

        var user = new User(userRequiredFields);
        DateTime maxDate = DateTime.Now;

        Assert.Equal(_username, user.Username);
        Assert.Equal(_name, user.Name);
        Assert.IsType<Guid>(user.Id);
        Assert.IsType<DateTime>(user.CreatedAt);
        Assert.True(user.CreatedAt >= minDate);
        Assert.True(user.CreatedAt <= maxDate);
        Assert.False(user.Deleted);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "fulano")]
    [InlineData("    ", "fulano")]
    [InlineData("fulano", "")]
    [InlineData("fulano", "     ")]
    public void Create_Invalid(string username, string name)
    {
        var userRequiredFields = new UserRequiredFields() { Username = username, Name = name };
        var action = new Action(() => new User(userRequiredFields));
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void HydrateRequiredFields_SetNewFieldValue()
    {
        string newName = "Ciclano Santos";
        var requiredFields = new UserRequiredFields() { Name = newName };
        var user = GetUser();
        var userId = user.Id;
        var userCreatedAt = user.CreatedAt;

        user.HydrateRequiredFields(requiredFields);

        Assert.Equal(newName, user.Name);
        Assert.Equal(_username, user.Username);
        Assert.Equal(userId, user.Id);
        Assert.Equal(userCreatedAt, user.CreatedAt);
        Assert.False(user.Deleted);
    }

    [Fact]
    public void HydrateRequiredFields_IgnoreFieldNotInformed()
    {
        var requiredFields = new UserRequiredFields() { };
        var user = GetUser();
        var userId = user.Id;
        var userCreatedAt = user.CreatedAt;

        user.HydrateRequiredFields(requiredFields);

        Assert.Equal(_name, user.Name);
        Assert.Equal(_username, user.Username);
        Assert.Equal(userId, user.Id);
        Assert.Equal(userCreatedAt, user.CreatedAt);
        Assert.False(user.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HydrateRequiredFields_Invalid(string name)
    {
        var user = GetUser();
        var userId = user.Id;
        var userRequiredFields = new UserRequiredFields() { Name = name };

        var action = new Action(() => user.HydrateRequiredFields(userRequiredFields));

        Assert.Throws<DomainException>(action);
        Assert.Equal(userId, user.Id);
        Assert.False(user.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_SetNewFieldValue()
    {
        var optionalFields = new UserOptionalFields() { Department = _department };
        var user = GetUser();
        var userId = user.Id;

        user.HydrateOptionalFields(optionalFields);

        Assert.Equal(_department, user.Department);
        Assert.Equal(userId, user.Id);
        Assert.False(user.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_IgnoreFieldNotInformed()
    {
        var optionalFields = new UserOptionalFields() { };
        var user = GetHydratedUser();

        user.HydrateOptionalFields(optionalFields);

        Assert.Equal(_department, user.Department);
        Assert.False(user.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void HydrateOptionalFields_Invalid(string department)
    {
        var user = GetUser();
        var userId = user.Id;
        var userOptionalFields = new UserOptionalFields() { Department = department };

        var action = new Action(() => user.HydrateOptionalFields(userOptionalFields));

        Assert.Throws<DomainException>(action);
        Assert.Equal(userId, user.Id);
        Assert.False(user.Deleted);
    }

    [Fact]
    public void Delete()
    {
        var user = GetUser();
        user.Delete();
        Assert.True(user.Deleted);
    }


    [Fact]
    public void HydrateRequiredFields_Deleted()
    {
        var user = GetDeletedUser();
        var userRequiredFields = new UserRequiredFields() { Name = "Ciclano" };

        var action = new Action(() => user.HydrateRequiredFields(userRequiredFields));

        Assert.Throws<DomainException>(action);
        Assert.True(user.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_Deleted()
    {
        var user = GetDeletedUser();
        var userOptionalFields = new UserOptionalFields() { Department = _department };

        var action = new Action(() => user.HydrateOptionalFields(userOptionalFields));

        Assert.Throws<DomainException>(action);
        Assert.True(user.Deleted);
    }

    [Fact]
    public void Rebuild()
    {
        var savedUser = GetUser();
        var requiredFields = new UserRequiredFields()
        {
            Username = savedUser.Username,
            Name = savedUser.Name
        };
        var selfGeneratedFields = new UserSelfGeneratedFields()
        {
            Id = savedUser.Id,
            CreatedAt = savedUser.CreatedAt,
            Deleted = savedUser.Deleted
        };

        var user = new UserBuilder().Rebuild(requiredFields, selfGeneratedFields);

        Assert.Equal(savedUser, user);
    }
}