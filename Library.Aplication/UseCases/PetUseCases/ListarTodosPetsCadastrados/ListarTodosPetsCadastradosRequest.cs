using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarTodosPetsCadastrados;

public sealed record ListarTodosPetsCadastradosRequest() : IRequest<ListarTodosPetsCadastradosResponse>;
