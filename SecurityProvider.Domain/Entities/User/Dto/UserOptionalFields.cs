using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.User;

public class UserOptionalFields: OptionalFields
{
    public string? Department { get; set; }
}