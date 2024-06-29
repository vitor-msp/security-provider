using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IUpdateGroupUseCase
{
    Task<UpdateGroupOutput> Execute(UpdateGroupInput input);
}