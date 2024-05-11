using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Group;

public interface IGroup : IEntity<GroupRequiredFields, GroupOptionalFields, GroupSelfGeneratedFields> { }