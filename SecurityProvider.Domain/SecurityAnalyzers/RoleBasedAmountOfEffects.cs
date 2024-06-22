using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class RoleBasedAmountOfEffects : ISecurityAnalyzer
{
    public bool UserCanAccessAction(IUser user, IAction action, PolicyEffect defaultEffect)
    {
        var oppositeEffect = GetOppositeEffect(defaultEffect);
        var defaultEffectAmount = FindEffect(user, action, defaultEffect);
        var oppositeEffectAmount = FindEffect(user, action, oppositeEffect);

        if (defaultEffect == PolicyEffect.Deny)
            return defaultEffectAmount < oppositeEffectAmount;

        return defaultEffectAmount >= oppositeEffectAmount;
    }

    private static PolicyEffect GetOppositeEffect(PolicyEffect effect)
        => effect == PolicyEffect.Allow ? PolicyEffect.Deny : PolicyEffect.Allow;

    private static int FindEffect(IUser user, IAction action, PolicyEffect effect)
        => user.Role?.Permissions.Sum(policy =>
        {
            var actionFinded = policy.Permissions.Any(actionAdded => actionAdded.Equals(action)) && policy.Effect == effect;
            return actionFinded ? 1 : 0;
        }) ?? 0;
}