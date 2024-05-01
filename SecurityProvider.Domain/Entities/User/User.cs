namespace SecurityProvider.Domain.Entities.User;

public class User : IUser
{
    public string Id { get; private set; }
    public string Username { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User(UserRequiredFields fields)
    {
        Username = fields.Username;
        Id = "123";
        CreatedAt = DateTime.Now;
    }
}