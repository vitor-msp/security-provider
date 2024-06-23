using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class RoleBasedOneOppositeAndNoneDefaultEffect : OneOppositeAndNoneDefaultEffect, ISecurityAnalyzer
{
    public bool UserCanAccessAction(IUser user, IAction action, PolicyEffect defaultEffect)
    {
        return base.UserCanAccessAction(new RoleBasedOneOppositeEffect(), user, action, defaultEffect);
    }

    protected override bool SomeDefaultEffect(IUser user, IAction action, PolicyEffect defaultEffect)
        => user.Role?.Permissions.Any(policy =>
        {
            return policy.Permissions.Any(addedAction => addedAction.Equals(action) && policy.Effect == defaultEffect);
        }) ?? false;
}