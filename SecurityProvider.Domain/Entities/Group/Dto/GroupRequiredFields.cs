using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Group;

public class GroupRequiredFields : RequiredFields
{
    public string? Name { get; set; }
}