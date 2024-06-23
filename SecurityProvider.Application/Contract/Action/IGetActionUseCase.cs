using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IGetActionUseCase
{
    Task<GetActionOutput> Execute(GetActionInput input);
}