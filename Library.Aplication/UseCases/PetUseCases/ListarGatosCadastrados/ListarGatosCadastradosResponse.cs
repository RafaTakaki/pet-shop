using Library.Domain.Entities;

namespace Library.Aplication.UseCases.PetUseCases.ListarGatosCadastrados;

public sealed record ListarGatosCadastradosResponse(List<PetEntity> Pets);
