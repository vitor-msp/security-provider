using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Action;

public interface IAction :
    IEntity<ActionRequiredFields, ActionOptionalFields, ActionSelfGeneratedFields>
{ }