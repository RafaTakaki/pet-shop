using Library.Domain.Entities;

namespace Library.Aplication.UseCases.PetUseCases.ListarPetsPorRaca;

public sealed record ListarPetsPorRacaResponse(List<PetEntity> Pets);
