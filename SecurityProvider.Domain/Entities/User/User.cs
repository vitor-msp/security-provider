using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.User;

public class User : Entity<UserRequiredFields, UserOptionalFields, UserSelfGeneratedFields>, IUser
{
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

    public User(UserRequiredFields fields) : base(fields)
    {
        Username = fields.Username!;
        Name = fields.Name!;
    }

    public User(UserRequiredFields requiredFields, UserSelfGeneratedFields selfGeneratedFields) :
        base(requiredFields, selfGeneratedFields)
    {
        Username = requiredFields.Username!;
        Name = requiredFields.Name!;
    }

    public override void HydrateRequiredFields(UserRequiredFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted user.");

        if (fields.Name != null) Name = fields.Name;
    }

    public override void HydrateOptionalFields(UserOptionalFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted user.");

        if (fields.Department != null) Department = fields.Department;
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

    protected override void ValidateRequiredFields(UserRequiredFields requiredFields)
    {
        var fields = new List<object?>()
        {
            requiredFields.Username,
            requiredFields.Name
        };
        if (fields.Any(field => field == null))
            throw new DomainException("Missing required fields.");
    }
}