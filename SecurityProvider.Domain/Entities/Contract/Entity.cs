namespace SecurityProvider.Domain.Entities.Contract;

public abstract class Entity<R, O, S> : IEntity<R, O, S>
    where R : RequiredFields
    where O : OptionalFields
    where S : SelfGeneratedFields
{
    public abstract void HydrateRequiredFields(R fields);
    public abstract void HydrateOptionalFields(O fields);
    public abstract void Delete();

}