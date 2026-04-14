using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarPetsPorRaca;

public class ListarPetsPorRacaHandler : IRequestHandler<ListarPetsPorRacaRequest, ListarPetsPorRacaResponse>
{
    private readonly IPetRepositorory _petRepository;

    public ListarPetsPorRacaHandler(IPetRepositorory petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<ListarPetsPorRacaResponse> Handle(ListarPetsPorRacaRequest request, CancellationToken cancellationToken)
    {
        var pets = await _petRepository.ListarPorRaca(request.Raca);
        return new ListarPetsPorRacaResponse(pets);
    }
}
