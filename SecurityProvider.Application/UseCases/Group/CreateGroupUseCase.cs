using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class CreateGroupUseCase : ICreateGroupUseCase
{
    public async Task<CreateGroupOutput> Execute(CreateGroupInput input)
    {
        throw new Exception();
    }
}