using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Role;

public class RoleOptionalFields : OptionalFields
{
    public string? Description { get; set; }
}