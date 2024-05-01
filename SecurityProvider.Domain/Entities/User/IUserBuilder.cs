namespace SecurityProvider.Domain.Entities.User;

public interface IUserBuilder
{
    IUser Rebuild(UserRequiredFields requiredFields, UserSelfGeneratedFields selfGeneratedFields);
}