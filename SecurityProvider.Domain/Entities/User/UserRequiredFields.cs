using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.User;

public class UserRequiredFields: IRequiredFields
{
    public string? Username { get; set; }
    public string? Name { get; set; }
}