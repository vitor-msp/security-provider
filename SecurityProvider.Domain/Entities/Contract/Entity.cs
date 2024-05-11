namespace SecurityProvider.Domain.Entities.Contract;

public abstract class Entity<R, O, S> : IEntity<R, O, S>
    where R : RequiredFields
    where O : OptionalFields
    where S : SelfGeneratedFields
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool Deleted { get; private set; }

    public Entity(R fields)
    {
        ValidateRequiredFields(fields);
        Id = new Guid();
        CreatedAt = DateTime.Now;
        Deleted = false;
    }

    public Entity(R requiredFields, S selfGeneratedFields)
    {
        ValidateRequiredFields(requiredFields);
        ValidateCommonSelfGeneratedFields(selfGeneratedFields);
        Id = (Guid)selfGeneratedFields.Id!;
        CreatedAt = (DateTime)selfGeneratedFields.CreatedAt!;
        Deleted = (bool)selfGeneratedFields.Deleted!;
    }

    public abstract void HydrateRequiredFields(R fields);
    public abstract void HydrateOptionalFields(O fields);

    public virtual void Delete()
    {
        Deleted = true;
    }

    public override bool Equals(object? obj)
    {
        var other = (Entity<R, O, S>)obj!;
        if (other.Id != Id) return false;

        var assertions = new List<bool>(){
            other.CreatedAt == CreatedAt,
            other.Deleted == Deleted,
        };
        return assertions.All(assertion => assertion);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected abstract void ValidateRequiredFields(R requiredFields);

    protected void ValidateCommonSelfGeneratedFields(S selfGeneratedFields)
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