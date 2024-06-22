using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.SecurityAnalyzers;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class RoleBasedAmountOfEffectsTest
{
    [Fact]
    public void DefaultEffectDeny_NoneRole()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new RoleBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_NoneEffect()
    {
        var action = ActionTest.GetAction();
        var user = SUTUtil.MakeSUT(action, new() { });

        var result = new RoleBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneRole()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new RoleBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneEffect()
    {
        var action = ActionTest.GetAction();
        var user = SUTUtil.MakeSUT(action, new() { });

        var result = new RoleBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }
}