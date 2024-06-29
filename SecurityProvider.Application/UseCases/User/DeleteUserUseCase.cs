using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    public async Task<DeleteUserOutput> Execute(DeleteUserInput input)
    {
        throw new Exception();
    }
}