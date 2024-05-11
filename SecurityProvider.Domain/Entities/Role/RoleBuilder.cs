namespace SecurityProvider.Domain.Entities.Role;

public class RoleBuilder : IRoleBuilder
{
    public IRole Rebuild(RoleRequiredFields requiredFields, RoleSelfGeneratedFields selfGeneratedFields)
    {
        return new Role(requiredFields, selfGeneratedFields);
    }
}
