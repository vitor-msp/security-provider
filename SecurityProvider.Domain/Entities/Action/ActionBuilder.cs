namespace SecurityProvider.Domain.Entities.Action;

public class ActionBuilder : IActionBuilder
{
    public IAction Rebuild(ActionRequiredFields requiredFields, ActionSelfGeneratedFields selfGeneratedFields)
    {
        return new Action(requiredFields, selfGeneratedFields);
    }
}
