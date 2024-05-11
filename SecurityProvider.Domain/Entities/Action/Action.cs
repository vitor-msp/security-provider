using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Action;

public class Action :
    Entity<ActionRequiredFields, ActionOptionalFields, ActionSelfGeneratedFields>, IAction
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

    public Action(ActionRequiredFields fields) : base(fields)
    {
        Name = fields.Name!;
    }

    public Action(ActionRequiredFields requiredFields, ActionSelfGeneratedFields selfGeneratedFields)
        : base(requiredFields, selfGeneratedFields)
    {
        Name = requiredFields.Name!;
    }

    public override void HydrateRequiredFields(ActionRequiredFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted action.");

        if (fields.Name != null) Name = fields.Name;
    }

    public override void HydrateOptionalFields(ActionOptionalFields fields)
    {
        if (Deleted)
            throw new DomainException("Impossible to update a deleted action.");

        if (fields.Description != null) Description = fields.Description;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        var otherUser = (Action)obj;
        if (otherUser.Id != Id) return false;

        var assertions = new List<bool>(){
            otherUser.CreatedAt == CreatedAt,
            otherUser.Deleted == Deleted,
            otherUser.Name == Name,
            otherUser.Description == Description,
        };
        return !assertions.Any(assertion => !assertion);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected override void ValidateRequiredFields(ActionRequiredFields requiredFields)
    {
        var fields = new List<object?>()
        {
            requiredFields.Name
        };
        if (fields.Any(field => field == null))
            throw new DomainException("Missing required fields.");
    }
}