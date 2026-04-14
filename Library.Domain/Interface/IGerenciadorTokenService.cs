using Library.Domain.Entities;

namespace Library.Domain.Interface
{
    public interface IGerenciadorTokenService
    {
        Task<string> GerarToken(Usuario usuario);
        Task<(string, string)> BuscarGuidTokenENome(string token);
    }
}