using SecurityProvider.Domain.Entities.Contract;
using SecurityProvider.Domain.Entities.Policy;

namespace SecurityProvider.Domain.Entities.Role;

public interface IRole :
    IEntity<RoleRequiredFields, RoleOptionalFields, RoleSelfGeneratedFields>
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    bool Deleted { get; }
    string Name { get; }
    string? Description { get; }
    List<IPolicy> Permissions { get; }
    void AddPermission(IPolicy policy);
    void RemovePermission(IPolicy policy);
}