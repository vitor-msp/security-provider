using SecurityProvider.Domain.Entities.Contract;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.Role;

namespace SecurityProvider.Domain.Entities.User;

public interface IUser : IEntity<UserRequiredFields, UserOptionalFields, UserSelfGeneratedFields>
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    bool Deleted { get; }
    string Username { get; }
    string Name { get; }
    string? Department { get; }
    List<IPolicy> Policies { get; }
    IRole? Role { get; set; }
    void AttachPolicy(IPolicy policy);
    void DetachPolicy(IPolicy policy);
}