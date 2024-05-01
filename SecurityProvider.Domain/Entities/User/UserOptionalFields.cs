using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.User;

public class UserOptionalFields: IOptionalFields
{
    public string? Department { get; set; }
}