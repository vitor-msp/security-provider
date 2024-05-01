using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Action;

public class ActionOptionalFields : OptionalFields
{
    public string? Description { get; set; }
}