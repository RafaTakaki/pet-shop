namespace Library.Domain.Entities;

public record CuidadoEntity(
    string Id,
    string IdPet,
    string TipoServico,
    DateTime DataServico,
    string Observacao);
