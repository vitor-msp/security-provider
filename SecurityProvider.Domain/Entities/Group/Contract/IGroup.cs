using SecurityProvider.Domain.Entities.Contract;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.Entities.Group;

public interface IGroup : IEntity<GroupRequiredFields, GroupOptionalFields, GroupSelfGeneratedFields>
{
    void AddUser(IUser user);
    void RemoveUser(IUser user);
    void AddPolicy(IPolicy policy);
    void RemovePolicy(IPolicy policy);
}