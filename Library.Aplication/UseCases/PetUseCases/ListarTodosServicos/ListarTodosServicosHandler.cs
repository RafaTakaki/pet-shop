using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarTodosServicos;

public class ListarTodosServicosHandler : IRequestHandler<ListarTodosServicosRequest, ListarTodosServicosResponse>
{
    private readonly ICuidadoRepository _cuidadoRepository;

    public ListarTodosServicosHandler(ICuidadoRepository cuidadoRepository)
    {
        _cuidadoRepository = cuidadoRepository;
    }

    public async Task<ListarTodosServicosResponse> Handle(ListarTodosServicosRequest request, CancellationToken cancellationToken)
    {
        var servicos = await _cuidadoRepository.ListarTodosServicos();
        return new ListarTodosServicosResponse(servicos);
    }
}
