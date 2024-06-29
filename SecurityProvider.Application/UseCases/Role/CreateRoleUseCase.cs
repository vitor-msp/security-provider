using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class CreateRoleUseCase : ICreateRoleUseCase
{
    public async Task<CreateRoleOutput> Execute(CreateRoleInput input)
    {
        throw new Exception();
    }
}