using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.SecurityAnalyzers;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class GroupBasedOneOppositeEffectTest
{
    [Fact]
    public void DefaultEffectDeny_NoneRole()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_NoneAllowEffect()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeSUT(action, new() { PolicyEffect.Deny });

        var result = new GroupBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectDeny_OneAllowAndNoneDeny()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeSUT(action, new() { PolicyEffect.Allow });

        var result = new GroupBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectDeny_OneAllowAndOneDeny()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeSUT(action, new() { PolicyEffect.Allow, PolicyEffect.Deny });

        var result = new GroupBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Deny);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneRole()
    {
        var user = UserTest.GetUser();
        var action = ActionTest.GetAction();

        var result = new GroupBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_NoneDenyEffect()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeSUT(action, new() { PolicyEffect.Allow });

        var result = new GroupBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.True(result);
    }

    [Fact]
    public void DefaultEffectAllow_OneDenyAndNoneAllow()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeSUT(action, new() { PolicyEffect.Deny });

        var result = new GroupBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.False(result);
    }

    [Fact]
    public void DefaultEffectAllow_OneDenyAndOneAllow()
    {
        var action = ActionTest.GetAction();
        var user = GroupBasedSUT.MakeSUT(action, new() { PolicyEffect.Deny, PolicyEffect.Allow });

        var result = new GroupBasedOneOppositeEffect().UserCanAccessAction(user, action, defaultEffect: PolicyEffect.Allow);

        Assert.False(result);
    }
}