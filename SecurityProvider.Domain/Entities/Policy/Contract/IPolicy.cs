using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Policy;

public interface IPolicy :
    IEntity<PolicyRequiredFields, PolicyOptionalFields, PolicySelfGeneratedFields>
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    bool Deleted { get; }
    string Name { get; }
    PolicyEffect Effect { get; }
    string? Description { get; }
    List<IAction> Permissions { get; }
    void AddPermission(IAction action);
    void RemovePermission(IAction action);
}