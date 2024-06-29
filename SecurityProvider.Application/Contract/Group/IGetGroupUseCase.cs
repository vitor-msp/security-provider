using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IGetGroupUseCase
{
    Task<GetGroupOutput> Execute(GetGroupInput input);
}