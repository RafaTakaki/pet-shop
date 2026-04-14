using MediatR;

namespace Library.Aplication.UseCases.UsuarioUseCases.CreateUser;

public sealed record CreateUserRequest(
    string Nome,
    string Email,
    string Senha,
    string? Apelido,
    DateTime? DataNascimento
) : IRequest<CreateUserResponse>;
