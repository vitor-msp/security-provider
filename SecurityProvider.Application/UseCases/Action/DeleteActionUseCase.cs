using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class DeleteActionUseCase : IDeleteActionUseCase
{
    public async Task<DeleteActionOutput> Execute(DeleteActionInput input)
    {
        throw new Exception();
    }
}