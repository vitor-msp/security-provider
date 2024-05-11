using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Role;

public interface IRoleBuilder :
    IRebuildable<RoleRequiredFields, RoleOptionalFields, RoleSelfGeneratedFields, IRole>
{ }