using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class UpdateGroupUseCase : IUpdateGroupUseCase
{
    public async Task<UpdateGroupOutput> Execute(UpdateGroupInput input)
    {
        throw new Exception();
    }
}