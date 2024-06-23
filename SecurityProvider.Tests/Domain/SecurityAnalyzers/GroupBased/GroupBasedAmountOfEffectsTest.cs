using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.SecurityAnalyzers;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class GroupBasedAmountOfEffectsTest
{
    [Fact]
    public void DefaultEffectDeny_NoneRole_GroupAttached()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_NoneEffect_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_EqualAmountEffects_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_MoreDenyEffects_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_MoreAllowEffects_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneRole_GroupAttached()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneEffect_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }


    [Fact]
    public void DefaultEffectAllow_EqualAmountEffects_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }


    [Fact]
    public void DefaultEffectAllow_MoreDenyEffects_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_MoreAllowEffects_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectDeny_NoneRole_UserAttached()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_NoneEffect_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_EqualAmountEffects_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_MoreDenyEffects_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_MoreAllowEffects_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneRole_UserAttached()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneEffect_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }


    [Fact]
    public void DefaultEffectAllow_EqualAmountEffects_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }


    [Fact]
    public void DefaultEffectAllow_MoreDenyEffects_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_MoreAllowEffects_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow, PolicyEffect.Allow });

        var result = new GroupBasedAmountOfEffects().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }
}