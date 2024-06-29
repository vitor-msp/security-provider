using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class CreateUserUseCase : ICreateUserUseCase
{
    public async Task<CreateUserOutput> Execute(CreateUserInput input)
    {
        throw new Exception();
    }
}