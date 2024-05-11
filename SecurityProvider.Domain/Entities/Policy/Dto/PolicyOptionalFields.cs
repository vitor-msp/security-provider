using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Policy;

public class PolicyOptionalFields : OptionalFields
{
    public string? Description { get; set; }
}