using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.User;

public interface IUserBuilder :
    IRebuildable<UserRequiredFields, UserOptionalFields, UserSelfGeneratedFields, IUser>
{ }