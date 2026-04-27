using Library.Aplication.Services;
using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarPetsPorUsuario;

public class ListarPetsPorUsuarioHandler : IRequestHandler<ListarPetsPorUsuarioRequest, ListarPetsPorUsuarioResponse>
{
    private readonly IPetRepositorory _petRepository;
    private readonly IGerenciadorTokenService _gerenciadorTokenService;

    public ListarPetsPorUsuarioHandler(IPetRepositorory petRepository, IGerenciadorTokenService gerenciadorTokenService)
    {
        _petRepository = petRepository;
        _gerenciadorTokenService = gerenciadorTokenService;
    }

    public async Task<ListarPetsPorUsuarioResponse> Handle(ListarPetsPorUsuarioRequest request, CancellationToken cancellationToken)
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

        var nomesPets = await _petRepository.BuscaPetUsuario(idUsuarioInt);
        return new ListarPetsPorUsuarioResponse(nomesPets);
    }
}
