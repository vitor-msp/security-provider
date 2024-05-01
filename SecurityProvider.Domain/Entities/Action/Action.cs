using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Action;

public class Action :
    Entity<ActionRequiredFields, ActionOptionalFields, ActionSelfGeneratedFields>, IAction
{
    public override void Delete()
    {
        throw new NotImplementedException();
    }

    public override void HydrateOptionalFields(ActionOptionalFields fields)
    {
        throw new NotImplementedException();
    }

    public override void HydrateRequiredFields(ActionRequiredFields fields)
    {
        throw new NotImplementedException();
    }
}