using MediatR;

namespace Library.Aplication.UseCases.UsuarioUseCases.UsuarioLogin
{
    public sealed record UsuarioLoginRequest(
    
        string Email,
        string Senha 
    ) : IRequest<UsuarioLoginResponse>;
}