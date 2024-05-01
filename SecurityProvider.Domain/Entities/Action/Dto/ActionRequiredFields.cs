using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Action;

public class ActionRequiredFields : RequiredFields
{
    public string? Name { get; set; }
}