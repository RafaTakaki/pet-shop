using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarRacaGatos;

public sealed record ListarRacaGatosRequest() : IRequest<ListarRacaGatosResponse>;