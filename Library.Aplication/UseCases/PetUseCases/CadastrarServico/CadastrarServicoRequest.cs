using System.Text.Json.Serialization;
using MediatR;

namespace Library.Aplication.UseCases.PetUseCases.CadastrarServico;

public sealed record CadastrarServicoRequest(
    string NomePet,
    string TipoServico,
    DateTime DataServico,
    string Observacao,
    [property: JsonIgnore] string? Token = null) : IRequest<CadastrarServicoResponse>
{
    public CadastrarServicoRequest AtualizarToken(string Token) =>
        this with { Token = Token };
}
