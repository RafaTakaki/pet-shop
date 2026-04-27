using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarTodosServicos;

public sealed record ListarTodosServicosRequest() : IRequest<ListarTodosServicosResponse>;
