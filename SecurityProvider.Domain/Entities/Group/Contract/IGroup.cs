using SecurityProvider.Domain.Entities.Contract;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.Entities.Group;

public interface IGroup : IEntity<GroupRequiredFields, GroupOptionalFields, GroupSelfGeneratedFields>
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    bool Deleted { get; }
    string Name { get; }
    string? Description { get; }
    List<IUser> Users { get; }
    List<IPolicy> Policies { get; }
    void AddUser(IUser user);
    void RemoveUser(IUser user);
    void AttachPolicy(IPolicy policy);
    void DetachPolicy(IPolicy policy);
}