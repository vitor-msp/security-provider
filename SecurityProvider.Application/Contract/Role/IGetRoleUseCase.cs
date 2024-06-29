using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IGetRoleUseCase
{
    Task<GetRoleOutput> Execute(GetRoleInput input);
}