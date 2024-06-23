using System;
using System.Collections.Generic;
using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Tests.Domain;

public class GroupBasedSUT
{
    public static IUser MakeGroupAttachedSUT(IAction action, List<PolicyEffect> effects)
    {
        var group = GroupTest.GetGroup();
        foreach (var effect in effects)
        {
            var policy = GeneratePolicy(effect);
            policy.AddPermission(action).AddPermission(ActionTest.GetAction());
            group.AttachPolicy(policy);
        }
        var user = UserTest.GetUser();
        group.AddUser(user);
        return user;
    }

    public static IUser MakeUserAttachedSUT(IAction action, List<PolicyEffect> effects)
    {
        var user = UserTest.GetUser();
        foreach (var effect in effects)
        {
            var policy = GeneratePolicy(effect);
            policy.AddPermission(action).AddPermission(ActionTest.GetAction());
            user.AttachPolicy(policy);
        }
        return user;
    }

    private static IPolicy GeneratePolicy(PolicyEffect effect)
    {
        var policyName = $"policy-{new Random().Next(0, 100)}";
        return new Policy(new PolicyRequiredFields() { Name = policyName, Effect = effect });
    }
}