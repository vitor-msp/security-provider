using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class RoleBasedOneOppositeAndNoneDefaultEffect : RoleBasedOneOppositeEffect, ISecurityAnalyzer
{
    public override bool UserCanAccessAction(IUser user, IAction action, PolicyEffect defaultEffect)
    {
        var someDefaultEffect = user.Role?.Permissions.Any(policy =>
        {
            return policy.Permissions.Any(addedAction => addedAction.Equals(action) && policy.Effect == defaultEffect);
        }) ?? false;
        if (someDefaultEffect)
            return defaultEffect == PolicyEffect.Allow;
        return base.UserCanAccessAction(user, action, defaultEffect);
    }
}