using SecurityProvider.Domain.Entities.Contract;
using SecurityProvider.Domain.Entities.Policy;

namespace SecurityProvider.Domain.Entities.Role;

public interface IRole :
    IEntity<RoleRequiredFields, RoleOptionalFields, RoleSelfGeneratedFields>
{
    void AddPermission(IPolicy policy);
    void RemovePermission(IPolicy policy);
}