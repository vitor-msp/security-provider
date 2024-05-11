using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Policy;

public interface IPolicy :
    IEntity<PolicyRequiredFields, PolicyOptionalFields, PolicySelfGeneratedFields>
{ }