using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Role;

public class RoleRequiredFields : RequiredFields
{
    public string? Name { get; set; }
}