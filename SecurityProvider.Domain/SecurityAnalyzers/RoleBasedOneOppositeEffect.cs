using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class RoleBasedOneOppositeEffect : ISecurityAnalyzer
{
    public virtual bool UserCanAccessAction(IUser user, IAction action, PolicyEffect defaultEffect)
    {
        var oppositeEffect = GetOppositeEffect(defaultEffect);
        var existsActionToTheOppositeEffect = user.Role?.Permissions.Any(policy =>
        {
            return policy.Permissions.Any(actionAdded => actionAdded.Equals(action)) && policy.Effect == oppositeEffect;
        }) ?? false;
        return defaultEffect == PolicyEffect.Deny ? existsActionToTheOppositeEffect : !existsActionToTheOppositeEffect;
    }

    private static PolicyEffect GetOppositeEffect(PolicyEffect effect)
        => effect == PolicyEffect.Allow ? PolicyEffect.Deny : PolicyEffect.Allow;
}