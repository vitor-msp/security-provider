using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Action;

public interface IAction :
    IEntity<ActionRequiredFields, ActionOptionalFields, ActionSelfGeneratedFields>
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    bool Deleted { get; }
    string Name { get; }
    string? Description { get; }
}