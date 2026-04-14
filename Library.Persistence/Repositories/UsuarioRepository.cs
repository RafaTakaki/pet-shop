using Library.Domain.Entities;
using Library.Domain.Interface;
using MongoDB.Driver;

namespace Library.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;
        public UsuarioRepository(IMongoDatabase database)
        {
            _usuarios = database.GetCollection<Usuario>("Usuarios");
        }

        public async Task<bool> CriarUsuario(Usuario usuario)
        {
            try
            {
                await _usuarios.InsertOneAsync(usuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Ao criar usuário: {ex.Message}");
                Console.WriteLine($"[STACK TRACE] {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[INNER EXCEPTION] {ex.InnerException.Message}");
                }
                return false;
            }
        }

        public async Task<Usuario?> ValidarEmail(string email)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Email, email);
            return await _usuarios.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Dictionary<string, string>> BuscarNomesUsuarios(IEnumerable<string> usuariosIds)
        {
            var filter = Builders<Usuario>.Filter.In(u => u.Id, usuariosIds.ToList());
            var usuariosList = await _usuarios.Find(filter).ToListAsync();
            return usuariosList.ToDictionary(u => u.Id, u => u.Nome);
        }
    }
}