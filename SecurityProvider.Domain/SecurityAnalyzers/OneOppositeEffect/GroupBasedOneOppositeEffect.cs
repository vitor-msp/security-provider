using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class GroupBasedOneOppositeEffect : OneOppositeEffect
{
    protected override bool ExistsActionToTheOppositeEffect(IUser user, IAction action, PolicyEffect oppositeEffect)
        => ExistsActionAttachedToTheUser(user, action, oppositeEffect) || ExistsActionAttachedToTheGroup(user, action, oppositeEffect);

    private static bool ExistsActionAttachedToTheUser(IUser user, IAction action, PolicyEffect oppositeEffect)
        => user.Policies.Any(policy =>
        {
            return policy.Permissions.Any(actionAdded => actionAdded.Equals(action)) && policy.Effect == oppositeEffect;
        });

    private static bool ExistsActionAttachedToTheGroup(IUser user, IAction action, PolicyEffect oppositeEffect)
        => user.Groups.Any(group =>
        {
            return group.Policies.Any(policy =>
            {
                return policy.Permissions.Any(actionAdded => actionAdded.Equals(action)) && policy.Effect == oppositeEffect;
            });
        });
}