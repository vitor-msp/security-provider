namespace SecurityProvider.Domain.Entities.User;

public class UserBuilder : IUserBuilder
{
    public IUser Rebuild(UserRequiredFields requiredFields, UserSelfGeneratedFields selfGeneratedFields)
    {
        return new User(requiredFields, selfGeneratedFields);
    }
}