using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Action;

public interface IActionBuilder :
    IRebuildable<IAction, ActionRequiredFields, ActionSelfGeneratedFields, ActionOptionalFields>
{ }