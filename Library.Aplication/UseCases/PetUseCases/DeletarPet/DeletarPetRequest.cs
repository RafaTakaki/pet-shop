using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.DeletarPet;

public sealed record DeletarPetRequest(int Id) : IRequest<DeletarPetResponse>;
