using System.Text.Json.Serialization;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.BuscaPetPorNome;

public sealed record BuscaPetPorNomeRequest(
    string NomePet,
    [property: JsonIgnore] string? Token = null) : IRequest<BuscaPetPorNomeResponse>
{
    public BuscaPetPorNomeRequest AtualizarToken(string Token) =>
        this with { Token = Token };
}
