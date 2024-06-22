using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;
using SecurityProvider.Domain.SecurityAnalyzers;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class RoleBasedOneOppositeEffectTest
{
    private IUser MakeSUT_NoneOppositeEffect(PolicyEffect effect)
    {
        var policyDeny1 = new Policy(new PolicyRequiredFields() { Name = "policy1", Effect = effect });
        var policyDeny2 = new Policy(new PolicyRequiredFields() { Name = "policy2", Effect = effect });
        policyDeny1.AddPermission(ActionTest.GetAction());
        policyDeny2.AddPermission(ActionTest.GetAction());
        var role = RoleTest.GetRole();
        role.AddPermission(policyDeny1);
        role.AddPermission(policyDeny2);
        var user = UserTest.GetUser();
        user.Role = role;
        return user;
    }

    [Fact]
    public void DefaultEffectDeny_NoneAllowEffect()
    {
        var user = MakeSUT_NoneOppositeEffect(PolicyEffect.Deny);
        var action = ActionTest.GetAction();

        var result = new RoleBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneDenyEffect()
    {
        var user = MakeSUT_NoneOppositeEffect(PolicyEffect.Allow);
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