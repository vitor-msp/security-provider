using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class UpdatePolicyUseCase : IUpdatePolicyUseCase
{
    public async Task<UpdatePolicyOutput> Execute(UpdatePolicyInput input)
    {
        throw new Exception();
    }
}