using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    public async Task<UpdateUserOutput> Execute(UpdateUserInput input)
    {
        throw new Exception();
    }
}