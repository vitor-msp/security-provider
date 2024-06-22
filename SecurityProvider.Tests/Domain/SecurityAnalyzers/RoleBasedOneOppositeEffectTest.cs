using System;
using System.Collections.Generic;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;
using SecurityProvider.Domain.SecurityAnalyzers;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class RoleBasedOneOppositeEffectTest
{
    private static IUser MakeSUT(List<PolicyEffect> effects)
    {
        var role = RoleTest.GetRole();
        foreach (var effect in effects)
        {
            var policyName = $"policy-{new Random().Next(0, 100)}";
            var policy = new Policy(new PolicyRequiredFields() { Name = policyName, Effect = effect });
            policy.AddPermission(ActionTest.GetAction()).AddPermission(ActionTest.GetAction());
            role.AddPermission(policy);
        }
        var user = UserTest.GetUser();
        user.Role = role;
        return user;
    }

    [Fact]
    public void DefaultEffectDeny_NoneAllowEffect()
    {
        var user = MakeSUT(new() { PolicyEffect.Deny });
        var action = ActionTest.GetAction();

        var result = new RoleBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneDenyEffect()
    {
        var user = MakeSUT(new() { PolicyEffect.Allow });
        var action = ActionTest.GetAction();

        var result = new RoleBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectDeny_NoneRole()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new RoleBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneRole()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new RoleBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }
}