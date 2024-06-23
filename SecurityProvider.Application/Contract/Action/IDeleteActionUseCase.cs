using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IDeleteActionUseCase
{
    Task<DeleteActionOutput> Execute(DeleteActionInput input);
}