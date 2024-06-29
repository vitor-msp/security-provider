using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.Contract;

public interface IUpdateUserUseCase
{
    Task<UpdateUserOutput> Execute(UpdateUserInput input);
}