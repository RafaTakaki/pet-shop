using Library.Domain.Entities;

namespace Library.Aplication.UseCases.PetUseCases.ListarCachorrosCadastrados;

public sealed record ListarCachorrosCadastradosResponse(List<PetEntity> Pets);
