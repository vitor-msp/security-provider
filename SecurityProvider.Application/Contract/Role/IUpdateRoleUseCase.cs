using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IUpdateRoleUseCase
{
    Task<UpdateRoleOutput> Execute(UpdateRoleInput input);
}