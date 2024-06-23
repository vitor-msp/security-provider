using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public abstract class OneOppositeAndNoneDefaultEffect
{
    public bool UserCanAccessAction(OneOppositeEffect oneOppositeEffect, IUser user, IAction action, PolicyEffect defaultEffect)
    {
        var someDefaultEffect = SomeDefaultEffect(user, action, defaultEffect);
        if (someDefaultEffect)
            return defaultEffect == PolicyEffect.Allow;
        return oneOppositeEffect.UserCanAccessAction(user, action, defaultEffect);
    }

    protected abstract bool SomeDefaultEffect(IUser user, IAction action, PolicyEffect defaultEffect);
}