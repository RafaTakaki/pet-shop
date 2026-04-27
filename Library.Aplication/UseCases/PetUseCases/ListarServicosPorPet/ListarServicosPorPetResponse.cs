using Library.Domain.Entities;

namespace Library.Aplication.UseCases.PetUseCases.ListarServicosPorPet;

public sealed record ListarServicosPorPetResponse(List<CuidadoEntity> Servicos);
