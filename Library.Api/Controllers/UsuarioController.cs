using System.Threading.Tasks;
using Library.Aplication.UseCases.UsuarioUseCases.CreateUser;
using Library.Aplication.UseCases.UsuarioUseCases.UsuarioLogin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsuarioController : ControllerBase
{
    IMediator _mediator;

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CriarUsuario")]
    [SwaggerOperation(
        Summary = "Cria um novo usuário",
        Description = "Cria um novo usuário no sistema.")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var usuario = await _mediator.Send(request);
        return Ok(usuario);
    }


    [HttpPost("Login")]
    [SwaggerOperation(
        Summary = "Realiza o login do usuário",
        Description = "Realiza o login do usuário no sistema.")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginRequest request)
    {

        var usuario = await _mediator.Send(request);
        return Ok(usuario);
    }
}
