namespace SecurityProvider.Domain.Entities.Contract;

public interface IRebuildable<R, O, S, E>
    where R : RequiredFields
    where O : OptionalFields
    where S : SelfGeneratedFields
    where E : IEntity<R, O, S>
{
    E Rebuild(R requiredFields, S selfGeneratedFields);
}