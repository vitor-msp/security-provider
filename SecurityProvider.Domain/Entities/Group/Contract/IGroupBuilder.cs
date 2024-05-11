using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Group;

public interface IGroupBuilder :
    IRebuildable<GroupRequiredFields, GroupOptionalFields, GroupSelfGeneratedFields, IGroup>
{ }