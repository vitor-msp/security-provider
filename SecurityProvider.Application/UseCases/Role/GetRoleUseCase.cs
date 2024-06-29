using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class GetRoleUseCase : IGetRoleUseCase
{
    public async Task<GetRoleOutput> Execute(GetRoleInput input)
    {
        throw new Exception();
    }
}