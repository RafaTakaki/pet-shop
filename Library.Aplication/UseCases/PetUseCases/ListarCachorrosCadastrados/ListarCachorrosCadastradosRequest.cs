using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarCachorrosCadastrados;

public sealed record ListarCachorrosCadastradosRequest() : IRequest<ListarCachorrosCadastradosResponse>;
