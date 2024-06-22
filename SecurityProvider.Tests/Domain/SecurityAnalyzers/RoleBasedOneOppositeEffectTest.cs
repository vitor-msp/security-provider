using System;
using System.Collections.Generic;
using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;
using SecurityProvider.Domain.SecurityAnalyzers;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class RoleBasedOneOppositeEffectTest
{
    private static IUser MakeSUT(IAction action, List<PolicyEffect> effects)
    {
        var role = RoleTest.GetRole();
        foreach (var effect in effects)
        {
            var policy = GeneratePolicy(effect);
            policy.AddPermission(action).AddPermission(ActionTest.GetAction());
            role.AddPermission(policy);
        }
        var user = UserTest.GetUser();
        user.Role = role;
        return user;
    }

    private static IPolicy GeneratePolicy(PolicyEffect effect)
    {
        var policyName = $"policy-{new Random().Next(0, 100)}";
        return new Policy(new PolicyRequiredFields() { Name = policyName, Effect = effect });
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

    [Fact]
    public void DefaultEffectDeny_NoneAllowEffect()
    {
        var action = ActionTest.GetAction();
        var user = MakeSUT(action, new() { PolicyEffect.Deny });

        var result = new RoleBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneDenyEffect()
    {
        var action = ActionTest.GetAction();
        var user = MakeSUT(action, new() { PolicyEffect.Allow });

        var result = new RoleBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectDeny_OneAllowAndNoneDeny()
    {
        var action = ActionTest.GetAction();
        var user = MakeSUT(action, new() { PolicyEffect.Allow });

        var result = new RoleBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_OneDenyAndNoneAllow()
    {
        var action = ActionTest.GetAction();
        var user = MakeSUT(action, new() { PolicyEffect.Deny });

        var result = new RoleBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.False(result);
    }
}