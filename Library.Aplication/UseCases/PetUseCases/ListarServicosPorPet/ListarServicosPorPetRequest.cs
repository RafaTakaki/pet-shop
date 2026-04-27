using System.Text.Json.Serialization;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarServicosPorPet;

public sealed record ListarServicosPorPetRequest(
    string NomePet,
    [property: JsonIgnore] string? Token = null) : IRequest<ListarServicosPorPetResponse>
{
    public ListarServicosPorPetRequest AtualizarToken(string Token) =>
        this with { Token = Token };
}
