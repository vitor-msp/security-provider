using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Group;

public class GroupOptionalFields : OptionalFields
{
    public string? Description { get; set; }
}