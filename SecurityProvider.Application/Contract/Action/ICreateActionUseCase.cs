using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface ICreateActionUseCase
{
    Task<CreateActionOutput> Execute(CreateActionInput input);
}