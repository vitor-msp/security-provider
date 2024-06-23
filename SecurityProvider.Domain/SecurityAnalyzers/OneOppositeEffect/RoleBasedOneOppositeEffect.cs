using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class RoleBasedOneOppositeEffect : OneOppositeEffect
{
    protected override bool ExistsActionToTheOppositeEffect(IUser user, IAction action, PolicyEffect oppositeEffect)
        => user.Role?.Permissions.Any(policy =>
        {
            return policy.Permissions.Any(actionAdded => actionAdded.Equals(action)) && policy.Effect == oppositeEffect;
        }) ?? false;
}