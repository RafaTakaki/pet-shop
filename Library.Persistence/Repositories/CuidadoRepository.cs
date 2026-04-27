using Library.Domain.Entities;
using Library.Domain.Interface;
using MongoDB.Driver;

namespace Library.Persistence.Repositories;

public class CuidadoRepository : ICuidadoRepository
{
    private readonly IMongoCollection<CuidadoEntity> _cuidados;

    public CuidadoRepository(IMongoDatabase database)
    {
        _cuidados = database.GetCollection<CuidadoEntity>("Cuidados");
    }

    public async Task<bool> CadastrarServico(CuidadoEntity servico)
    {
        try
        {
            await _cuidados.InsertOneAsync(servico);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<CuidadoEntity>> ListarServicosPorPetId(string idPet)
    {
        var filter = Builders<CuidadoEntity>.Filter.Eq(c => c.IdPet, idPet);
        return await _cuidados.Find(filter).ToListAsync();
    }

    public async Task<List<CuidadoEntity>> ListarTodosServicos()
    {
        return await _cuidados.Find(Builders<CuidadoEntity>.Filter.Empty).ToListAsync();
    }
}
