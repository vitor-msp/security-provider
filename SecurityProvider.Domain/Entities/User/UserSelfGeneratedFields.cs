using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.User;

public class UserSelfGeneratedFields: ISelfGeneratedFields
{
    public Guid? Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool? Deleted { get; set; }
}