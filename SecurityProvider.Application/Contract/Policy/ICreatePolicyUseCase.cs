using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface ICreatePolicyUseCase
{
    Task<CreatePolicyOutput> Execute(CreatePolicyInput input);
}