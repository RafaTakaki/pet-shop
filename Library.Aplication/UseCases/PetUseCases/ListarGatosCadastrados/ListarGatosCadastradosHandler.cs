using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarGatosCadastrados;

public class ListarGatosCadastradosHandler : IRequestHandler<ListarGatosCadastradosRequest, ListarGatosCadastradosResponse>
{
    private readonly IPetRepositorory _petRepository;

    public ListarGatosCadastradosHandler(IPetRepositorory petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<ListarGatosCadastradosResponse> Handle(ListarGatosCadastradosRequest request, CancellationToken cancellationToken)
    {
        var pets = await _petRepository.ListarGatosCadastrados();
        return new ListarGatosCadastradosResponse(pets);
    }
}
