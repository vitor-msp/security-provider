using SecurityProvider.Domain.Entities.Action;
using SecurityProvider.Domain.Entities.Policy;
using SecurityProvider.Domain.Entities.User;

namespace SecurityProvider.Domain.SecurityAnalyzers;

public interface ISecurityAnalyzer
{
    bool UserCanAccessAction(IUser user, IAction action, PolicyEffect defaultEffect);
}