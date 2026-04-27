using Library.Aplication.Services;
using Library.Domain.Entities;
using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarServicosPorPet;

public class ListarServicosPorPetHandler : IRequestHandler<ListarServicosPorPetRequest, ListarServicosPorPetResponse>
{
    private readonly IPetRepositorory _petRepository;
    private readonly ICuidadoRepository _cuidadoRepository;
    private readonly IGerenciadorTokenService _gerenciadorTokenService;

    public ListarServicosPorPetHandler(IPetRepositorory petRepository, ICuidadoRepository cuidadoRepository, IGerenciadorTokenService gerenciadorTokenService)
    {
        _petRepository = petRepository;
        _cuidadoRepository = cuidadoRepository;
        _gerenciadorTokenService = gerenciadorTokenService;
    }

    public async Task<ListarServicosPorPetResponse> Handle(ListarServicosPorPetRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Token))
        {
            throw new Exception("Token não fornecido");
        }

        var (idUsuario, _) = await _gerenciadorTokenService.BuscarGuidTokenENome(request.Token);
        
        if (!int.TryParse(idUsuario, out int idUsuarioInt))
        {
            throw new Exception("ID do usuário inválido no token");
        }

        var pet = await _petRepository.BuscaObjetoPetUsuario(idUsuarioInt, request.NomePet);
        
        if (pet == null)
        {
            return new ListarServicosPorPetResponse(new List<CuidadoEntity>());
        }

        var servicos = await _cuidadoRepository.ListarServicosPorPetId(pet.Id);
        return new ListarServicosPorPetResponse(servicos);
    }
}
