using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public abstract class AmountOfEffects : ISecurityAnalyzer
{
    public bool UserCanAccessAction(IUser user, IAction action, PolicyEffect defaultEffect)
    {
        var oppositeEffect = GetOppositeEffect(defaultEffect);
        var defaultEffectAmount = SumEffects(user, action, defaultEffect);
        var oppositeEffectAmount = SumEffects(user, action, oppositeEffect);

        if (defaultEffect == PolicyEffect.Deny)
            return defaultEffectAmount < oppositeEffectAmount;

        return defaultEffectAmount >= oppositeEffectAmount;
    }

    private static PolicyEffect GetOppositeEffect(PolicyEffect effect)
        => effect == PolicyEffect.Allow ? PolicyEffect.Deny : PolicyEffect.Allow;

    protected abstract int SumEffects(IUser user, IAction action, PolicyEffect effect);
}