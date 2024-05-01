using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.User;

public interface IUser : IEntity<UserRequiredFields, UserOptionalFields> { }