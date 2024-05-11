using SecurityProvider.Domain.Entities.Contract;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.Entities.Group;

public class Group : Entity<GroupRequiredFields, GroupOptionalFields, GroupSelfGeneratedFields>, IGroup
{
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

    private string? _description;
    public string? Description
    {
        get { return _description; }
        private set
        {
            if (value == null) return;
            value = value.Trim();
            bool invalid = value == "";
            if (invalid) throw new DomainException("Description is invalid.");
            _description = value;
        }
    }

    private readonly List<IUser> _users = new();
    public List<IUser> Users
    {
        get { return new List<IUser>(_users); }
    }

    public Group(GroupRequiredFields fields) : base(fields)
    {
        Name = fields.Name!;
    }

    public Group(GroupRequiredFields requiredFields, GroupSelfGeneratedFields selfGeneratedFields) :
        base(requiredFields, selfGeneratedFields)
    {
        Name = requiredFields.Name!;
    }

    public override void HydrateRequiredFields(GroupRequiredFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted group.");

        if (fields.Name != null) Name = fields.Name;
    }

    public override void HydrateOptionalFields(GroupOptionalFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted group.");

        if (fields.Description != null) Description = fields.Description;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        if (!base.Equals(obj)) return false;
        var other = (Group)obj;

        var assertions = new List<bool>(){
            other.Name == Name,
            other.Description == Description,
        };
        return assertions.All(assertion => assertion);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected override void ValidateRequiredFields(GroupRequiredFields requiredFields)
    {
        var fields = new List<object?>()
        {
            requiredFields.Name
        };
        if (fields.Any(field => field == null))
            throw new DomainException("Missing required fields.");
    }

    public void AddUser(IUser user)
    {
        _users.Add(user);
    }
}