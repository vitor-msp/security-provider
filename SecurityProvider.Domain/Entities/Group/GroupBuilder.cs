namespace SecurityProvider.Domain.Entities.Group;

public class GroupBuilder : IGroupBuilder
{
    public IGroup Rebuild(GroupRequiredFields requiredFields, GroupSelfGeneratedFields selfGeneratedFields)
    {
        return new Group(requiredFields, selfGeneratedFields);
    }
}