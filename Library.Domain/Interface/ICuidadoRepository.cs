using Library.Domain.Entities;

namespace Library.Domain.Interface;

public interface ICuidadoRepository
{
    Task<bool> CadastrarServico(CuidadoEntity servico);
    Task<List<CuidadoEntity>> ListarServicosPorPetId(string idPet);
    Task<List<CuidadoEntity>> ListarTodosServicos();
}

