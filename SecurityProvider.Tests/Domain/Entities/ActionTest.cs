using System;
using SecurityProvider.Domain;
using SecurityProvider.Domain.Entities.Action;
using Xunit;
using Action = SecurityProvider.Domain.Entities.Action.Action;

namespace SecurityProvider.Tests.Domain;

public class ActionTest
{
    private static readonly string _name = "create_s3_object";
    private static readonly string _description = "create a s3 object in a bucket";

    public static Action GetAction()
    {
        return new Action(new ActionRequiredFields() { Name = _name });
    }

    private Action GetHydratedAction()
    {
        var action = GetAction();
        action.HydrateOptionalFields(new ActionOptionalFields() { Description = _description });
        return action;
    }

    private Action GetDeletedAction()
    {
        var action = GetAction();
        action.Delete();
        return action;
    }

    [Fact]
    public void Create_Valid()
    {
        DateTime minDate = DateTime.Now;
        var actionRequiredFields = new ActionRequiredFields() { Name = _name };

        var action = new Action(actionRequiredFields);
        DateTime maxDate = DateTime.Now;

        Assert.Equal(_name, action.Name);
        Assert.IsType<Guid>(action.Id);
        Assert.IsType<DateTime>(action.CreatedAt);
        Assert.True(action.CreatedAt >= minDate);
        Assert.True(action.CreatedAt <= maxDate);
        Assert.False(action.Deleted);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Create_Invalid(string name)
    {
        var actionRequiredFields = new ActionRequiredFields() { Name = name };
        var systemAction = new System.Action(() => new Action(actionRequiredFields));
        Assert.Throws<DomainException>(systemAction);
    }

    [Fact]
    public void HydrateRequiredFields_SetNewFieldValue()
    {
        string newName = "delete_s3_object";
        var requiredFields = new ActionRequiredFields() { Name = newName };
        var action = GetAction();
        var actionId = action.Id;
        var actionCreatedAt = action.CreatedAt;

        action.HydrateRequiredFields(requiredFields);

        Assert.Equal(newName, action.Name);
        Assert.Equal(actionId, action.Id);
        Assert.Equal(actionCreatedAt, action.CreatedAt);
        Assert.False(action.Deleted);
    }

    [Fact]
    public void HydrateRequiredFields_IgnoreFieldNotInformed()
    {
        var requiredFields = new ActionRequiredFields() { };
        var action = GetAction();
        var actionId = action.Id;
        var actionCreatedAt = action.CreatedAt;

        action.HydrateRequiredFields(requiredFields);

        Assert.Equal(_name, action.Name);
        Assert.Equal(actionId, action.Id);
        Assert.Equal(actionCreatedAt, action.CreatedAt);
        Assert.False(action.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HydrateRequiredFields_Invalid(string name)
    {
        var action = GetAction();
        var actionId = action.Id;
        var actionRequiredFields = new ActionRequiredFields() { Name = name };

        var systemAction = new System.Action(() => action.HydrateRequiredFields(actionRequiredFields));

        Assert.Throws<DomainException>(systemAction);
        Assert.Equal(actionId, action.Id);
        Assert.False(action.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_SetNewFieldValue()
    {
        var optionalFields = new ActionOptionalFields() { Description = _description };
        var action = GetAction();
        var actionId = action.Id;

        action.HydrateOptionalFields(optionalFields);

        Assert.Equal(_description, action.Description);
        Assert.Equal(actionId, action.Id);
        Assert.False(action.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_IgnoreFieldNotInformed()
    {
        var optionalFields = new ActionOptionalFields() { };
        var action = GetHydratedAction();

        action.HydrateOptionalFields(optionalFields);

        Assert.Equal(_description, action.Description);
        Assert.False(action.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void HydrateOptionalFields_Invalid(string description)
    {
        var action = GetAction();
        var actionId = action.Id;
        var actionOptionalFields = new ActionOptionalFields() { Description = description };

        var systemAction = new System.Action(() => action.HydrateOptionalFields(actionOptionalFields));

        Assert.Throws<DomainException>(systemAction);
        Assert.Equal(actionId, action.Id);
        Assert.False(action.Deleted);
    }

    [Fact]
    public void Delete()
    {
        var action = GetAction();
        action.Delete();
        Assert.True(action.Deleted);
    }


    [Fact]
    public void HydrateRequiredFields_Deleted()
    {
        var action = GetDeletedAction();
        var actionRequiredFields = new ActionRequiredFields() { Name = "delete_s3_object" };

        var systemAction = new System.Action(() => action.HydrateRequiredFields(actionRequiredFields));

        Assert.Throws<DomainException>(systemAction);
        Assert.True(action.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_Deleted()
    {
        var action = GetDeletedAction();
        var actionOptionalFields = new ActionOptionalFields() { Description = _description };

        var systemAction = new System.Action(() => action.HydrateOptionalFields(actionOptionalFields));

        Assert.Throws<DomainException>(systemAction);
        Assert.True(action.Deleted);
    }

    [Fact]
    public void Rebuild()
    {
        var savedAction = GetAction();
        var requiredFields = new ActionRequiredFields() { Name = savedAction.Name };
        var selfGeneratedFields = new ActionSelfGeneratedFields()
        {
            Id = savedAction.Id,
            CreatedAt = savedAction.CreatedAt,
            Deleted = savedAction.Deleted
        };

        var action = new ActionBuilder().Rebuild(requiredFields, selfGeneratedFields);

        Assert.Equal(savedAction, action);
    }
}