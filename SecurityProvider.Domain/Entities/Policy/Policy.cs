using SecurityProvider.Domain.Entities.Action;
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

    public PolicyEffect Effect { get; set; }

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

    private readonly List<IAction> _permissions = new();
    public List<IAction> Permissions
    {
        get { return new(_permissions); }
    }

    public Policy(PolicyRequiredFields fields) : base(fields)
    {
        Name = fields.Name!;
        Effect = (PolicyEffect)fields.Effect!;
    }

    public Policy(PolicyRequiredFields requiredFields, PolicySelfGeneratedFields selfGeneratedFields)
        : base(requiredFields, selfGeneratedFields)
    {
        Name = requiredFields.Name!;
        Effect = (PolicyEffect)requiredFields.Effect!;
    }

    public override void HydrateRequiredFields(PolicyRequiredFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted policy.");

        if (fields.Name != null) Name = fields.Name;
        if (fields.Effect != null) Effect = (PolicyEffect)fields.Effect;
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
            other.Effect == Effect,
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
            requiredFields.Name,
            requiredFields.Effect
        };
        if (fields.Any(field => field == null))
            throw new DomainException("Missing required fields.");
    }

    public IPolicy AddPermission(IAction action)
    {
        if (!_permissions.Contains(action))
            _permissions.Add(action);
        return this;
    }

    public void RemovePermission(IAction action)
    {
        _permissions.Remove(action);
    }
}