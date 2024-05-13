using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Policy;

public interface IPolicy :
    IEntity<PolicyRequiredFields, PolicyOptionalFields, PolicySelfGeneratedFields>
{
    void AddPermission(IAction action);
    void RemovePermission(IAction action);
}