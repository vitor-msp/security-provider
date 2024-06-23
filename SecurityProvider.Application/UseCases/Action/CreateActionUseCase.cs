using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class CreateActionUseCase : ICreateActionUseCase
{
    public async Task<CreateActionOutput> Execute(CreateActionInput input)
    {
        throw new Exception();
    }
}