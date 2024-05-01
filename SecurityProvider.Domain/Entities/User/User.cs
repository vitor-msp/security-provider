namespace SecurityProvider.Domain.Entities.User;

public class User : IUser
{
    public string Id { get; private set; }

    private string _username;
    public string Username
    {
        get { return _username; }
        private set
        {
            value = value.Trim();
            bool invalid = string.IsNullOrEmpty(value);
            if (invalid) throw new DomainException("Username is invalid.");
            _username = value;
        }
    }

    public DateTime CreatedAt { get; private set; }

    public User(UserRequiredFields fields)
    {
        Username = fields.Username;
        Id = "123";
        CreatedAt = DateTime.Now;
    }
}