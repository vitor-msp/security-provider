using SecurityProvider.Application.Contract;
using SecurityProvider.Application.Input;
using SecurityProvider.Application.Output;

namespace SecurityProvider.Application.UseCases;

public class GetGroupUseCase : IGetGroupUseCase
{
    public async Task<GetGroupOutput> Execute(GetGroupInput input)
    {
        throw new Exception();
    }
}