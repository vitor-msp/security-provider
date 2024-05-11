using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Role;

public interface IRole :
    IEntity<RoleRequiredFields, RoleOptionalFields, RoleSelfGeneratedFields>
{ }