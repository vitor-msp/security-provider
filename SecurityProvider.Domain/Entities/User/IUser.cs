namespace SecurityProvider.Domain.Entities.User;

public interface IUser
{
    void HydrateRequiredFields(UserRequiredFields fields);
    void HydrateOptionalFields(UserOptionalFields fields);
    void Delete();
}