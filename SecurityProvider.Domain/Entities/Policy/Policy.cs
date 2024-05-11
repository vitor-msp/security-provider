using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Policy;

public class Policy :
    Entity<PolicyRequiredFields, PolicyOptionalFields, PolicySelfGeneratedFields>, IPolicy
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            value = value.Trim();
            bool invalid = string.IsNullOrEmpty(value);
            if (invalid) throw new DomainException("Name is invalid.");
            _name = value;
        }
    }

    private string _description;
    public string? Description
    {
        get { return _description; }
        set
        {
            if (value == null) return;
            value = value.Trim();
            bool invalid = value == "";
            if (invalid) throw new DomainException("Description is invalid.");
            _description = value;
        }
    }

    public Policy(PolicyRequiredFields fields) : base(fields)
    {
        Name = fields.Name!;
    }

    public Policy(PolicyRequiredFields requiredFields, PolicySelfGeneratedFields selfGeneratedFields)
        : base(requiredFields, selfGeneratedFields)
    {
        Name = requiredFields.Name!;
    }

    public override void HydrateRequiredFields(PolicyRequiredFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted policy.");

        if (fields.Name != null) Name = fields.Name;
    }

    public override void HydrateOptionalFields(PolicyOptionalFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted policy.");

        if (fields.Description != null) Description = fields.Description;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        if (!base.Equals(obj)) return false;
        var other = (Policy)obj;

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

    protected override void ValidateRequiredFields(PolicyRequiredFields requiredFields)
    {
        var fields = new List<object?>()
        {
            requiredFields.Name
        };
        if (fields.Any(field => field == null))
            throw new DomainException("Missing required fields.");
    }
}