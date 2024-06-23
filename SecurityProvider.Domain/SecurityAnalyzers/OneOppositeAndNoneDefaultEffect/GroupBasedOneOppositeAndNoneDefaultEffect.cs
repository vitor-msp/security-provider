using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class GroupBasedOneOppositeAndNoneDefaultEffect : OneOppositeAndNoneDefaultEffect, ISecurityAnalyzer
{
    public bool UserCanAccessAction(IUser user, IAction action, PolicyEffect defaultEffect)
    {
        return base.UserCanAccessAction(new GroupBasedOneOppositeEffect(), user, action, defaultEffect);
    }

    protected override bool SomeDefaultEffect(IUser user, IAction action, PolicyEffect defaultEffect)
        => SomeDefaultEffectAttachedToTheUser(user, action, defaultEffect) || SomeDefaultEffectAttachedToTheGroup(user, action, defaultEffect);

    private static bool SomeDefaultEffectAttachedToTheUser(IUser user, IAction action, PolicyEffect defaultEffect)
        => user.Policies.Any(policy =>
        {
            return policy.Permissions.Any(addedAction => addedAction.Equals(action) && policy.Effect == defaultEffect);

        });

    private static bool SomeDefaultEffectAttachedToTheGroup(IUser user, IAction action, PolicyEffect defaultEffect)
        => user.Groups.Any(group =>
        {
            return group.Policies.Any(policy =>
            {
                return policy.Permissions.Any(addedAction => addedAction.Equals(action) && policy.Effect == defaultEffect);

            });
        });
}