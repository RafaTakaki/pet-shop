using Library.Domain.Entities;

namespace Library.Aplication.UseCases.PetUseCases.ListarTodosPetsCadastrados;

public sealed record ListarTodosPetsCadastradosResponse(List<PetEntity> Pets);
