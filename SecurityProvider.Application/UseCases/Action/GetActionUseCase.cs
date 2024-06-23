using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class GetActionUseCase : IGetActionUseCase
{
    public async Task<GetActionOutput> Execute(GetActionInput input)
    {
        throw new Exception();
    }
}