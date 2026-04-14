using Library.Domain.Interface;
using MediatR;

namespace Library.Aplication.UseCases.UsuarioUseCases.UsuarioLogin
{
    public class UsuarioLoginHandler : IRequestHandler<UsuarioLoginRequest, UsuarioLoginResponse>
    {
        IUsuarioRepository _usuarioRepository;
        IGerenciadorTokenService _gerenciadorTokenService;

        public UsuarioLoginHandler(IUsuarioRepository usuarioRepository, IGerenciadorTokenService gerenciadorTokenService)
        {
            _usuarioRepository = usuarioRepository;
            _gerenciadorTokenService = gerenciadorTokenService;
        }

        public async Task<UsuarioLoginResponse> Handle(UsuarioLoginRequest request, CancellationToken cancellationToken)
        {
            var usuario = _usuarioRepository.ValidarEmail(request.Email).Result;
            if (usuario.Senha == request.Senha)
            {
                var token = await _gerenciadorTokenService.GerarToken(usuario);
                return new UsuarioLoginResponse
                {
                    IdUsuario = usuario.Id,
                    Nome = usuario.Nome,
                    Token = token,
                    TipoUsuario =  usuario.TipoUsuario,
                    Email = usuario.Email
                };
            }
            throw new Exception("Senha inválida");
        }
    }
}