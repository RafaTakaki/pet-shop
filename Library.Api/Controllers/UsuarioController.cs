using System;
using System.Threading.Tasks;
using Library.Aplication.UseCases.UsuarioUseCases.CreateUser;
using Library.Aplication.UseCases.UsuarioUseCases.UsuarioLogin;
using Library.Aplication.UseCases.UsuarioUseCases.RecuperarSenha;
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

    [HttpGet("RecuperarSenha")]
    [SwaggerOperation(
    Summary = "Recupera a senha do usuário",
    Description = "Envia um link para o email do usuário para recuperação de senha.")]
    public async Task<IActionResult> RecuperarSenha([FromQuery] string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(new { mensagem = "Email não fornecido" });
        }

        try
        {
            var request = new RecuperarSenhaRequest(email);
            var resultado = await _mediator.Send(request);
            
            if (resultado.Sucesso)
            {
                return Ok(new { mensagem = resultado.Mensagem });
            }
            
            return BadRequest(new { mensagem = resultado.Mensagem });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[UsuarioController.RecuperarSenha ERROR] {ex.Message}");
            Console.Error.WriteLine($"[Stack Trace] {ex.StackTrace}");
            return BadRequest(new { mensagem = $"Erro ao recuperar senha: {ex.Message}" });
        }
    }
}
