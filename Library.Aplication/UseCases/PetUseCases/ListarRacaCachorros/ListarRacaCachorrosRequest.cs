using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarRacaCachorros;

public sealed record  ListarRacaCachorrosRequest() : IRequest<ListarRacaCachorrosResponse>;