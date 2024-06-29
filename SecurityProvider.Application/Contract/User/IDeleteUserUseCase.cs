using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IDeleteUserUseCase
{
    Task<DeleteUserOutput> Execute(DeleteUserInput input);
}