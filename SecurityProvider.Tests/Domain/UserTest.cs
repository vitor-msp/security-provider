using System;
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

    [Fact]
    public void Create_Invalid()
    {
        var userRequiredFields = new UserRequiredFields() { };
        Assert.Throws<DomainException>(() => new User(userRequiredFields));

        userRequiredFields = new UserRequiredFields() { Username = "", Name = "fulano" };
        Assert.Throws<DomainException>(() => new User(userRequiredFields));

        userRequiredFields = new UserRequiredFields() { Username = "    ", Name = "fulano" };
        Assert.Throws<DomainException>(() => new User(userRequiredFields));

        userRequiredFields = new UserRequiredFields() { Username = "fulano", Name = "" };
        Assert.Throws<DomainException>(() => new User(userRequiredFields));

        userRequiredFields = new UserRequiredFields() { Username = "fulano", Name = "  " };
        Assert.Throws<DomainException>(() => new User(userRequiredFields));
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

    [Fact]
    public void HydrateRequiredFields_Invalid()
    {
        var user = GetUser();
        var userId = user.Id;

        var userRequiredFields = new UserRequiredFields() { Name = "" };
        Assert.Throws<DomainException>(() => user.HydrateRequiredFields(userRequiredFields));

        userRequiredFields = new UserRequiredFields() { Name = "  " };
        Assert.Throws<DomainException>(() => user.HydrateRequiredFields(userRequiredFields));

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

    [Fact]
    public void HydrateOptionalFields_Invalid()
    {
        var user = GetUser();
        var userId = user.Id;

        var userOptionalFields = new UserOptionalFields() { Department = "" };
        Assert.Throws<DomainException>(() => user.HydrateOptionalFields(userOptionalFields));

        userOptionalFields = new UserOptionalFields() { Department = "  " };
        Assert.Throws<DomainException>(() => user.HydrateOptionalFields(userOptionalFields));

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
}