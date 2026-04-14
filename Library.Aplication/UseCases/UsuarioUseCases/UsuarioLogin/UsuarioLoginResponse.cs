using Library.Domain.Enums;

namespace Library.Aplication.UseCases.UsuarioUseCases.UsuarioLogin
{
    public class UsuarioLoginResponse
    {
        public string IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Email { get; set; }
    }
}