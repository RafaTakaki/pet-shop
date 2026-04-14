using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarPetsPorRaca;

public sealed record ListarPetsPorRacaRequest(string Raca) : IRequest<ListarPetsPorRacaResponse>;
