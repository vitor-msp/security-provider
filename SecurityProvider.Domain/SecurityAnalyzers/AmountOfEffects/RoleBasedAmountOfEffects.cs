using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public class RoleBasedAmountOfEffects : AmountOfEffects
{
    protected override int SumEffects(IUser user, IAction action, PolicyEffect effect)
        => user.Role?.Permissions.Sum(policy =>
        {
            var actionFinded = policy.Permissions.Any(actionAdded => actionAdded.Equals(action)) && policy.Effect == effect;
            return actionFinded ? 1 : 0;
        }) ?? 0;
}