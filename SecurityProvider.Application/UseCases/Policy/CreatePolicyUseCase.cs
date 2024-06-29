using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class CreatePolicyUseCase : ICreatePolicyUseCase
{
    public async Task<CreatePolicyOutput> Execute(CreatePolicyInput input)
    {
        throw new Exception();
    }
}