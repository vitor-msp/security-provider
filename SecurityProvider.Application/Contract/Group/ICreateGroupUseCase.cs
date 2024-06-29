using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface ICreateGroupUseCase
{
    Task<CreateGroupOutput> Execute(CreateGroupInput input);
}