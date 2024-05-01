namespace SecurityProvider.Domain.Entities.User;

public class User : IUser
{
    public Guid Id { get; private set; }

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

    private string _name;
    public string Name
    {
        get { return _name; }
        private set
        {
            value = value.Trim();
            bool invalid = string.IsNullOrEmpty(value);
            if (invalid) throw new DomainException("Name is invalid.");
            _name = value;
        }
    }

    public DateTime CreatedAt { get; private set; }

    public User(UserRequiredFields fields)
    {
        Username = fields.Username;
        Name = fields.Name;
        Id = new Guid();
        CreatedAt = DateTime.Now;
    }

    public void HydrateRequiredFields(UserRequiredFields fields)
    {
        Name = fields.Name;
    }
}