using SecurityProvider.Domain.Entities.Contract;

namespace SecurityProvider.Domain.Entities.Policy;

public interface IPolicyBuilder :
    IRebuildable<PolicyRequiredFields, PolicyOptionalFields, PolicySelfGeneratedFields, IPolicy>
{ }