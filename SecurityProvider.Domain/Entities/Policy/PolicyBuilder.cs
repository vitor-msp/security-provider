namespace SecurityProvider.Domain.Entities.Policy;

public class PolicyBuilder : IPolicyBuilder
{
    public IPolicy Rebuild(PolicyRequiredFields requiredFields, PolicySelfGeneratedFields selfGeneratedFields)
    {
        return new Policy(requiredFields, selfGeneratedFields);
    }
}
