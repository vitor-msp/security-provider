using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface ICreateRoleUseCase
{
    Task<CreateRoleOutput> Execute(CreateRoleInput input);
}