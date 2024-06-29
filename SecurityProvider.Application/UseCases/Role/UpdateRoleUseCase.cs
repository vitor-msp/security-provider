using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class UpdateRoleUseCase : IUpdateRoleUseCase
{
    public async Task<UpdateRoleOutput> Execute(UpdateRoleInput input)
    {
        throw new Exception();
    }
}