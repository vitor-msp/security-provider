using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class GetPolicyUseCase : IGetPolicyUseCase
{
    public async Task<GetPolicyOutput> Execute(GetPolicyInput input)
    {
        throw new Exception();
    }
}