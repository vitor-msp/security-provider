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

    public bool Deleted { get; private set; }

    private string? _department;
    public string? Department
    {
        get { return _department; }
        private set
        {
            if (value == null) return;
            value = value.Trim();
            bool invalid = value == "";
            if (invalid) throw new DomainException("Department is invalid.");
            _department = value;
        }
    }

    public User(UserRequiredFields fields)
    {
        if (fields.Username == null || fields.Name == null)
            throw new DomainException("Missing required fields.");

        Username = fields.Username;
        Name = fields.Name;
        Id = new Guid();
        CreatedAt = DateTime.Now;
        Deleted = false;
    }

    public void HydrateRequiredFields(UserRequiredFields fields)
    {
        if (fields.Name != null) Name = fields.Name;
    }

    public void HydrateOptionalFields(UserOptionalFields fields)
    {
        if (fields.Department != null) Department = fields.Department;
    }

    public void Delete()
    {
        Deleted = true;
    }
}