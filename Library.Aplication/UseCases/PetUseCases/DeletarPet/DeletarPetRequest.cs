using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.DeletarPet;

public sealed record DeletarPetRequest(string Id) : IRequest<DeletarPetResponse>;
