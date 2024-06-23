using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public abstract class OneOppositeEffect : ISecurityAnalyzer
{
    public bool UserCanAccessAction(IUser user, IAction action, PolicyEffect defaultEffect)
    {
        var oppositeEffect = GetOppositeEffect(defaultEffect);
        var existsActionToTheOppositeEffect = ExistsActionToTheOppositeEffect(user, action, oppositeEffect);
        return defaultEffect == PolicyEffect.Deny ? existsActionToTheOppositeEffect : !existsActionToTheOppositeEffect;
    }

    private static PolicyEffect GetOppositeEffect(PolicyEffect effect)
        => effect == PolicyEffect.Allow ? PolicyEffect.Deny : PolicyEffect.Allow;

    protected abstract bool ExistsActionToTheOppositeEffect(IUser user, IAction action, PolicyEffect oppositeEffect);
}