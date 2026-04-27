using Library.Aplication.Services;
using Library.Domain.Entities;
using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.CadastrarServico;

public class CadastrarServicoHandler : IRequestHandler<CadastrarServicoRequest, CadastrarServicoResponse>
{
    private readonly IPetRepositorory _petRepository;
    private readonly ICuidadoRepository _cuidadoRepository;
    private readonly IGerenciadorTokenService _gerenciadorTokenService;
    private readonly IEmailService _emailService;

    public CadastrarServicoHandler(IPetRepositorory petRepository, ICuidadoRepository cuidadoRepository, IGerenciadorTokenService gerenciadorTokenService, IEmailService emailService)
    {
        _petRepository = petRepository;
        _cuidadoRepository = cuidadoRepository;
        _gerenciadorTokenService = gerenciadorTokenService;
        _emailService = emailService;
    }

    public async Task<CadastrarServicoResponse> Handle(CadastrarServicoRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Token))
        {
            throw new Exception("Token não fornecido");
        }

        var (idUsuario, emailUsuario) = await _gerenciadorTokenService.BuscarGuidTokenENome(request.Token);
        
        if (!int.TryParse(idUsuario, out int idUsuarioInt))
        {
            throw new Exception("ID do usuário inválido no token");
        }

        var pet = await _petRepository.BuscaObjetoPetUsuario(idUsuarioInt, request.NomePet);
        
        if (pet == null)
        {
            return new CadastrarServicoResponse(false, "Pet não encontrado");
        }

        var cuidado = new CuidadoEntity(
            Id: Guid.NewGuid().ToString(),
            IdPet: pet.Id,
            TipoServico: request.TipoServico,
            DataServico: request.DataServico,
            Observacao: request.Observacao
        );

        var resultado = await _cuidadoRepository.CadastrarServico(cuidado);

        if (resultado)
        {
            // Enviar email ao usuário
            await EnviarEmailConfirmacao(emailUsuario, request.NomePet, request.TipoServico, request.DataServico);
            
            return new CadastrarServicoResponse(true, "Serviço cadastrado com sucesso");
        }

        return new CadastrarServicoResponse(false, "Não foi possível cadastrar o serviço");
    }

    private async Task EnviarEmailConfirmacao(string emailUsuario, string nomePet, string tipoServico, DateTime dataServico)
    {
        try
        {
            var corpoEmail = EmailTemplateService.GerarTemplateConfirmacaoServico(nomePet, tipoServico, dataServico);
            await _emailService.EnviarEmailAsync(emailUsuario, corpoEmail);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CadastrarServicoHandler] Erro ao enviar email: {ex.Message}");
            // Não lançar exceção para não quebrar o fluxo de cadastro
        }
    }
}

