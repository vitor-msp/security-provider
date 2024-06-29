using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IDeleteRoleUseCase
{
    Task<DeleteRoleOutput> Execute(DeleteRoleInput input);
}