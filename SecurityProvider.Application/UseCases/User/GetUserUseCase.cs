using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class GetUserUseCase : IGetUserUseCase
{
    public async Task<GetUserOutput> Execute(GetUserInput input)
    {
        throw new Exception();
    }
}