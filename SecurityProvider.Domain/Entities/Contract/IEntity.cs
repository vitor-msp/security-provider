namespace SecurityProvider.Domain.Entities.Contract;

public interface IEntity<T, U>
    where T : IRequiredFields
    where U : IOptionalFields
{
    void HydrateRequiredFields(T fields);
    void HydrateOptionalFields(U fields);
    void Delete();
}