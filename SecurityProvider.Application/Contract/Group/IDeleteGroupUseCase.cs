using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IDeleteGroupUseCase
{
    Task<DeleteGroupOutput> Execute(DeleteGroupInput input);
}