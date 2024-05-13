using SecurityProvider.Domain.Entities.Contract;
using SecurityProvider.Domain.Entities.Policy;

namespace SecurityProvider.Domain.Entities.User;

public interface IUser : IEntity<UserRequiredFields, UserOptionalFields, UserSelfGeneratedFields>
{
    void AttachPolicy(IPolicy policy);
    void DetachPolicy(IPolicy policy);
}