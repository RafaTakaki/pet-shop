using Library.Domain.Entities;

namespace Library.Domain.Interface;

public interface IPetRepositorory
{
    Task<bool> CadastroPet(PetEntity cadastroPet);
    Task<List<PetEntity>> ListarTodosPetsCadastrados();
    Task<List<PetEntity>> ListarPorRaca(string raca);
    Task<List<PetEntity>> ListarCachorrosCadastrados();
    Task<List<PetEntity>> ListarGatosCadastrados();

    Task<List<string>> BuscaPetUsuario(int idUsuario);
    Task<PetEntity> BuscaObjetoPetUsuario(int idUsuario, string nome);

    Task<bool> Delete(int id);

}