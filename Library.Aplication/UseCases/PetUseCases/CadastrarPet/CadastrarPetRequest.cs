using System.Text.Json.Serialization;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.CadastrarPet;

public sealed record CadastrarPetRequest(
    string NomePet,
    string TipoPet,
    int IdadePet,
    string Raca,
    string Sexo,
    [property: JsonIgnore] string? IdUsuario = null,
    [property: JsonIgnore] string? Token = null) : IRequest<CadastrarPetResponse>

{
    public CadastrarPetRequest AtualizarId(string Id) =>
        this with { IdUsuario = Id };

    public CadastrarPetRequest AtualizarTipoPet(string TipoPet) =>
        this with { TipoPet = TipoPet };

    public CadastrarPetRequest AtualizarToken(string Token) =>
    this with { Token = Token };
}