using Library.Domain.Entities;

namespace Library.Aplication.UseCases.PetUseCases.ListarTodosServicos;

public sealed record ListarTodosServicosResponse(List<CuidadoEntity> Servicos);
