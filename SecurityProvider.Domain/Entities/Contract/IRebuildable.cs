namespace SecurityProvider.Domain.Entities.Contract;

public interface IRebuildable<T, U, V, W>
    where T: IEntity<U, W>
    where U: IRequiredFields
    where V: ISelfGeneratedFields
    where W: IOptionalFields
{
    T Rebuild(U requiredFields, V selfGeneratedFields);
}