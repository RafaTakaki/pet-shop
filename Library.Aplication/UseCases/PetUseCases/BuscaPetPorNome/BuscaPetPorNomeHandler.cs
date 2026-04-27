using Library.Aplication.Services;
using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.BuscaPetPorNome;

public class BuscaPetPorNomeHandler : IRequestHandler<BuscaPetPorNomeRequest, BuscaPetPorNomeResponse>
{
    private readonly IPetRepositorory _petRepository;
    private readonly IGerenciadorTokenService _gerenciadorTokenService;

    public BuscaPetPorNomeHandler(IPetRepositorory petRepository, IGerenciadorTokenService gerenciadorTokenService)
    {
        _petRepository = petRepository;
        _gerenciadorTokenService = gerenciadorTokenService;
    }

    public async Task<BuscaPetPorNomeResponse> Handle(BuscaPetPorNomeRequest request, CancellationToken cancellationToken)
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
        return new BuscaPetPorNomeResponse(pet);
    }
}
