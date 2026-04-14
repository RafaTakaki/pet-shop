using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarCachorrosCadastrados;

public class ListarCachorrosCadastradosHandler : IRequestHandler<ListarCachorrosCadastradosRequest, ListarCachorrosCadastradosResponse>
{
    private readonly IPetRepositorory _petRepository;

    public ListarCachorrosCadastradosHandler(IPetRepositorory petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<ListarCachorrosCadastradosResponse> Handle(ListarCachorrosCadastradosRequest request, CancellationToken cancellationToken)
    {
        var pets = await _petRepository.ListarCachorrosCadastrados();
        return new ListarCachorrosCadastradosResponse(pets);
    }
}
