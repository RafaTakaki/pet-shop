using System.Text.Json.Serialization;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.ListarPetsPorUsuario;

public sealed record ListarPetsPorUsuarioRequest(
    [property: JsonIgnore] string? Token = null) : IRequest<ListarPetsPorUsuarioResponse>
{
    public ListarPetsPorUsuarioRequest AtualizarToken(string Token) =>
        this with { Token = Token };
}
