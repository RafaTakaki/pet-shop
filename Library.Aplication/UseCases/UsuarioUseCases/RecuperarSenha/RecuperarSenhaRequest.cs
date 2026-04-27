using MediatR;

namespace Library.Aplication.UseCases.UsuarioUseCases.RecuperarSenha;

public sealed record RecuperarSenhaRequest(string Email) : IRequest<RecuperarSenhaResponse>;
