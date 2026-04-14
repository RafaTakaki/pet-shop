using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.DeletarPet;

public class DeletarPetHandler : IRequestHandler<DeletarPetRequest, DeletarPetResponse>
{
    private readonly IPetRepositorory _petRepository;

    public DeletarPetHandler(IPetRepositorory petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<DeletarPetResponse> Handle(DeletarPetRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _petRepository.Delete(request.Id);
        return new DeletarPetResponse(resultado);
    }
}
