using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.SecurityAnalyzers;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class GroupBasedOneOppositeAndNoneDefaultEffectTest
{
    [Fact]
    public void DefaultEffectDeny_NoneRole_GroupAttached()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_NoneAllowEffect_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Deny });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_OneAllowAndNoneDeny_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Allow });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectDeny_OneAllowAndOneDeny_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Allow, PolicyEffect.Deny });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneRole_GroupAttached()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneDenyEffect_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Allow });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_OneDenyAndNoneAllow_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Deny });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_OneDenyAndOneAllow_GroupAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeGroupAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectDeny_NoneRole_UserAttached()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_NoneAllowEffect_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Deny });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_OneAllowAndNoneDeny_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Allow });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectDeny_OneAllowAndOneDeny_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Allow, PolicyEffect.Deny });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneRole_UserAttached()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneDenyEffect_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Allow });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_OneDenyAndNoneAllow_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Deny });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_OneDenyAndOneAllow_UserAttached()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeUserAttachedSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedOneOppositeAndNoneDefaultEffect()
            .UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }
}