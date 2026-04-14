using Library.Domain.Entities;
using Library.Domain.Interface;
using MongoDB.Driver;

namespace Library.Persistence.Repositories;

public class PetRepository : IPetRepositorory
{
    private readonly IMongoCollection<PetEntity> _pets;
    public PetRepository(IMongoDatabase database)
    {
        _pets = database.GetCollection<PetEntity>("Pets");
    }

    public async Task<PetEntity> BuscaObjetoPetUsuario(int idUsuario, string nome)
    {
        var filter = Builders<PetEntity>.Filter.And(
            Builders<PetEntity>.Filter.Eq(p => p.IdUsuario, idUsuario.ToString()),
            Builders<PetEntity>.Filter.Eq(p => p.NomePet, nome)
        );
        return await _pets.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<string>> BuscaPetUsuario(int idUsuario)
    {
        var filter = Builders<PetEntity>.Filter.Eq(p => p.IdUsuario, idUsuario.ToString());
        var pets = await _pets.Find(filter).ToListAsync();
        return pets.Select(p => p.NomePet).ToList();
    }

    public async Task<bool> CadastroPet(PetEntity cadastroPet)
    {
        try
        {
            await _pets.InsertOneAsync(cadastroPet);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        var result = await _pets.DeleteOneAsync(Builders<PetEntity>.Filter.Eq(p => p.Id, id.ToString()));
        return result.DeletedCount > 0;
    }

    public async Task<List<PetEntity>> ListarCachorrosCadastrados()
    {
        var filter = Builders<PetEntity>.Filter.Eq(p => p.TipoPet, "Cachorro");
        return await _pets.Find(filter).ToListAsync();
    }

    public async Task<List<PetEntity>> ListarGatosCadastrados()
    {
        var filter = Builders<PetEntity>.Filter.Eq(p => p.TipoPet, "Gato");
        return await _pets.Find(filter).ToListAsync();
    }

    public async Task<List<PetEntity>> ListarPorRaca(string raca)
    {
        var filter = Builders<PetEntity>.Filter.Eq(p => p.Raca, raca);
        return await _pets.Find(filter).ToListAsync();
    }

    public async Task<List<PetEntity>> ListarTodosPetsCadastrados()
    {
        return await _pets.Find(Builders<PetEntity>.Filter.Empty).ToListAsync();
    }
}
