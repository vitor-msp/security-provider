namespace SecurityProvider.Domain.Entities.User;

interface IUser
{
    void HydrateRequiredFields(UserRequiredFields fields);
}