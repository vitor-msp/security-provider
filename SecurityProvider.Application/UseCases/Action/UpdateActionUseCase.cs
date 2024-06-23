using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class UpdateActionUseCase : IUpdateActionUseCase
{
    public async Task<UpdateActionOutput> Execute(UpdateActionInput input)
    {
        throw new Exception();
    }
}