using System;
using SecurityProvider.Domain.Entities.User;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class UserTest
{
    [Fact]
    public void CreateUser()
    {
        string username = "username";
        DateTime minDate = DateTime.Now;
        var userRequiredFields = new UserRequiredFields()
        {
            Username = username,
        };

        var user = new User(userRequiredFields);
        DateTime maxDate = DateTime.Now;

        Assert.Equal(username, user.Username);
        Assert.IsType<string>(user.Id);
        Assert.IsType<DateTime>(user.CreatedAt);
        Assert.True(user.CreatedAt >= minDate);
        Assert.True(user.CreatedAt <= maxDate);
    }
}