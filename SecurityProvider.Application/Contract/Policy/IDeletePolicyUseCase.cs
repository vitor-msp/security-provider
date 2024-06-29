using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IDeletePolicyUseCase
{
    Task<DeletePolicyOutput> Execute(DeletePolicyInput input);
}