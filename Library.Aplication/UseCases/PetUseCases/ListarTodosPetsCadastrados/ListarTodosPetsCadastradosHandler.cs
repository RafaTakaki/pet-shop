using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarTodosPetsCadastrados;

public class ListarTodosPetsCadastradosHandler : IRequestHandler<ListarTodosPetsCadastradosRequest, ListarTodosPetsCadastradosResponse>
{
    private readonly IPetRepositorory _petRepository;

    public ListarTodosPetsCadastradosHandler(IPetRepositorory petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<ListarTodosPetsCadastradosResponse> Handle(ListarTodosPetsCadastradosRequest request, CancellationToken cancellationToken)
    {
        var pets = await _petRepository.ListarTodosPetsCadastrados();
        return new ListarTodosPetsCadastradosResponse(pets);
    }
}
