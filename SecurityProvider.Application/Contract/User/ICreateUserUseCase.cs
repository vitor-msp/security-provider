using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface ICreateUserUseCase
{
    Task<CreateUserOutput> Execute(CreateUserInput input);
}