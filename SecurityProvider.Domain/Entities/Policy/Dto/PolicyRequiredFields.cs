using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Policy;

public class PolicyRequiredFields : RequiredFields
{
    public string? Name { get; set; }
    public PolicyEffect? Effect { get; set; }
}