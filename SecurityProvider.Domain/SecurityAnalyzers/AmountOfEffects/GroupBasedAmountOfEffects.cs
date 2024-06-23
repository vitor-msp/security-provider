using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class GroupBasedAmountOfEffects : AmountOfEffects
{
    protected override int SumEffects(IUser user, IAction action, PolicyEffect effect)
        => SumEffectsAttachedToTheUser(user, action, effect) + SumEffectsAttachedToTheGroup(user, action, effect);

    private static int SumEffectsAttachedToTheUser(IUser user, IAction action, PolicyEffect effect)
        => user.Policies.Sum(policy =>
        {
            var actionFinded = policy.Permissions.Any(actionAdded => actionAdded.Equals(action)) && policy.Effect == effect;
            return actionFinded ? 1 : 0;
        });

    private static int SumEffectsAttachedToTheGroup(IUser user, IAction action, PolicyEffect effect)
        => user.Groups.Sum(group =>
        {
            return group.Policies.Sum(policy =>
            {
                var actionFinded = policy.Permissions.Any(actionAdded => actionAdded.Equals(action)) && policy.Effect == effect;
                return actionFinded ? 1 : 0;
            });
        });
}