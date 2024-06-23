using System;
using System.Collections.Generic;
using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Tests.Domain;

public class RoleBasedSUT
{
    public static IUser MakeSUT(IAction action, List<PolicyEffect> effects)
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
}