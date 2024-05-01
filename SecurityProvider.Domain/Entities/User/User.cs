namespace SecurityProvider.Domain.Entities.User;

public class User : IUser
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool Deleted { get; private set; }

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
        ValidateRequiredFields(fields);
        Username = fields.Username!;
        Name = fields.Name!;
        Id = new Guid();
        CreatedAt = DateTime.Now;
        Deleted = false;
    }

    public User(UserRequiredFields requiredFields, UserSelfGeneratedFields selfGeneratedFields)
    {
        ValidateRequiredFields(requiredFields);
        Username = requiredFields.Username!;
        Name = requiredFields.Name!;

        ValidateSelfGeneratedFields(selfGeneratedFields);
        Id = (Guid)selfGeneratedFields.Id!;
        CreatedAt = (DateTime)selfGeneratedFields.CreatedAt!;
        Deleted = (bool)selfGeneratedFields.Deleted!;
    }

    public void HydrateRequiredFields(UserRequiredFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted user.");

        if (fields.Name != null) Name = fields.Name;
    }

    public void HydrateOptionalFields(UserOptionalFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted user.");

        if (fields.Department != null) Department = fields.Department;
    }

    public void Delete()
    {
        Deleted = true;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        var otherUser = (User)obj;
        if (otherUser.Id != Id) return false;

        var assertions = new List<bool>(){
            otherUser.CreatedAt == CreatedAt,
            otherUser.Deleted == Deleted,
            otherUser.Username == Username,
            otherUser.Name == Name,
            otherUser.Department == Department,
        };
        return !assertions.Any(assertion => !assertion);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    private static void ValidateRequiredFields(UserRequiredFields requiredFields)
    {
        var fields = new List<object?>()
        {
            requiredFields.Username,
            requiredFields.Name
        };
        if (fields.Any(field => field == null))
            throw new DomainException("Missing required fields.");
    }

    private static void ValidateSelfGeneratedFields(UserSelfGeneratedFields selfGeneratedFields)
    {
        var fields = new List<object?>()
        {
            selfGeneratedFields.Id,
            selfGeneratedFields.CreatedAt,
            selfGeneratedFields.Deleted,

        };
        if (fields.Any(field => field == null))
            throw new DomainException("Missing self generated fields.");
    }
}