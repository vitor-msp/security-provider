using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class DeleteGroupUseCase : IDeleteGroupUseCase
{
    public async Task<DeleteGroupOutput> Execute(DeleteGroupInput input)
    {
        throw new Exception();
    }
}