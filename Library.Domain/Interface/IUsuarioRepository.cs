using Library.Domain.Entities;

namespace Library.Domain.Interface
{
    public interface IUsuarioRepository
    {
        Task<bool> CriarUsuario(Usuario usuario);
        Task<Usuario?> ValidarEmail(string email);
        Task<Dictionary<string, string>> BuscarNomesUsuarios(IEnumerable<string> usuariosIds);
    }
}