namespace Library.Domain.Entities;

public sealed record PetEntity(
    string Id,
    string IdUsuario,
    string NomePet,
    string TipoPet,
    int IdadePet,
    string Sexo,
    string Raca,
    string Imagem
);