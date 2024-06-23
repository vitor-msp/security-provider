using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class GroupBasedOneOppositeEffect : OneOppositeEffect
{
    protected override bool ExistsActionToTheOppositeEffect(IUser user, IAction action, PolicyEffect oppositeEffect)
        => user.Groups.Any(group =>
        {
            return group.Policies.Any(policy =>
            {
                return policy.Permissions.Any(actionAdded => actionAdded.Equals(action)) && policy.Effect == oppositeEffect;
            });
        });
}