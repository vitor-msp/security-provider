using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IUpdateActionUseCase
{
    Task<UpdateActionOutput> Execute(UpdateActionInput input);
}