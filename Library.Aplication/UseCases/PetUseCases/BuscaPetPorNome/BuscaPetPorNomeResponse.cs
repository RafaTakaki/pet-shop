using Library.Domain.Entities;

namespace Library.Aplication.UseCases.PetUseCases.BuscaPetPorNome;

public sealed record BuscaPetPorNomeResponse(PetEntity? Pet);
