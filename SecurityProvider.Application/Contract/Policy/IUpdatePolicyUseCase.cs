using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IUpdatePolicyUseCase
{
    Task<UpdatePolicyOutput> Execute(UpdatePolicyInput input);
}