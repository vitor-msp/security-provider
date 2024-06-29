using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class DeletePolicyUseCase : IDeletePolicyUseCase
{
    public async Task<DeletePolicyOutput> Execute(DeletePolicyInput input)
    {
        throw new Exception();
    }
}