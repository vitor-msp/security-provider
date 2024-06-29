using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class DeleteRoleUseCase : IDeleteRoleUseCase
{
    public async Task<DeleteRoleOutput> Execute(DeleteRoleInput input)
    {
        throw new Exception();
    }
}