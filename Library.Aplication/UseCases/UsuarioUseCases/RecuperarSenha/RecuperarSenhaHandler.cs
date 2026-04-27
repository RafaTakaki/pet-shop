using Library.Aplication.Services;
using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.UsuarioUseCases.RecuperarSenha;

public class RecuperarSenhaHandler : IRequestHandler<RecuperarSenhaRequest, RecuperarSenhaResponse>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;

    public RecuperarSenhaHandler(IUsuarioRepository usuarioRepository, IEmailService emailService)
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }

    public async Task<RecuperarSenhaResponse> Handle(RecuperarSenhaRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Email))
        {
            return new RecuperarSenhaResponse(false, "Email não fornecido");
        }

        var usuario = await _usuarioRepository.ValidarEmail(request.Email);
        
        if (usuario == null)
        {
            return new RecuperarSenhaResponse(false, "Email não encontrado no sistema");
        }

        // Gerar token de recuperação (válido por 24 horas)
        var tokenRecuperacao = Guid.NewGuid().ToString();
        var linkRecuperacao = $"https://seu-dominio.com/recuperar-senha?token={tokenRecuperacao}";

        // TODO: Armazenar token no banco de dados com expiração

        // Enviar email com link de recuperação
        var corpoEmail = EmailTemplateService.GerarTemplateRecuperacaoSenha(usuario.Nome, linkRecuperacao);
        var emailEnviado = await _emailService.EnviarEmailAsync(usuario.Email, corpoEmail);

        if (emailEnviado)
        {
            return new RecuperarSenhaResponse(true, "Link para alteração de senha enviado por email");
        }

        return new RecuperarSenhaResponse(false, "Erro ao enviar email de recuperação");
    }
}
