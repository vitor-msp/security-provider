namespace SecurityProvider.Domain.Entities.Contract;

public interface IEntity<R, O, S>
    where R : RequiredFields
    where O : OptionalFields
    where S : SelfGeneratedFields
{
    void HydrateRequiredFields(R fields);
    void HydrateOptionalFields(O fields);
    void Delete();
}