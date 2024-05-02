using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Action;

public class Action :
    Entity<ActionRequiredFields, ActionOptionalFields, ActionSelfGeneratedFields>, IAction
{
    public Action(ActionRequiredFields fields) : base(fields) { }

    public override void HydrateOptionalFields(ActionOptionalFields fields)
    {
        throw new NotImplementedException();
    }

    public override void HydrateRequiredFields(ActionRequiredFields fields)
    {
        throw new NotImplementedException();
    }

    protected override void ValidateRequiredFields(ActionRequiredFields requiredFields)
    {
        throw new NotImplementedException();
    }
}