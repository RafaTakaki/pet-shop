using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Aplication.UseCases.PetUseCases.ListarPetsPorUsuario;
using Library.Aplication.UseCases.PetUseCases.BuscaPetPorNome;
using Library.Aplication.UseCases.PetUseCases.CadastrarServico;
using Library.Aplication.UseCases.PetUseCases.ListarServicosPorPet;
using Library.Aplication.UseCases.PetUseCases.ListarTodosServicos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuidadoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CuidadoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("PetsCadastrados")]
    [Authorize(Roles = "usuario_comum")]
    [SwaggerOperation(
    Summary = "Lista os pets cadastrados do usuário",
    Description = "Este método retorna a lista de pets cadastrados de um usuário específico. A solicitação exige que o usuário esteja autenticado e tenha o papel 'usuario_comum'. O token JWT deve ser incluído no cabeçalho da requisição para a autenticação."
    )]
    public async Task<IActionResult> PetsCadastrados()
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return BadRequest("Token JWT não fornecido no header Authorization");
        }

        var jwtToken = authHeader.Replace("Bearer ", "").Trim();

        try
        {
            var request = new ListarPetsPorUsuarioRequest().AtualizarToken(jwtToken);
            var resultado = await _mediator.Send(request);
            
            if (resultado?.NomesPets != null && resultado.NomesPets.Count > 0)
            {
                return Ok(resultado.NomesPets);
            }
            
            return Ok(new List<string> { });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CuidadoController.PetsCadastrados ERROR] {ex.Message}");
            Console.Error.WriteLine($"[Stack Trace] {ex.StackTrace}");
            return BadRequest(new { mensagem = $"Não foi possível localizar os pets: {ex.Message}" });
        }
    }


    [HttpGet("BuscaPetPorNome")]
    [Authorize(Roles = "usuario_comum")]
    [SwaggerOperation(
    Summary = "Busca um pet pelo nome",
    Description = "Este método permite que o usuário busque um pet pelo nome. O usuário deve estar autenticado e ter o papel 'usuario_comum'. O token JWT deve ser incluído no cabeçalho da requisição para a autenticação."
    )]
    public async Task<IActionResult> BuscarPet([FromQuery] string nome)
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return BadRequest("Token JWT não fornecido no header Authorization");
        }

        var jwtToken = authHeader.Replace("Bearer ", "").Trim();

        try
        {
            var request = new BuscaPetPorNomeRequest(nome).AtualizarToken(jwtToken);
            var resultado = await _mediator.Send(request);
            
            if (resultado?.Pet != null)
            {
                return Ok(resultado.Pet);
            }
            
            return NotFound(new { mensagem = "Pet não encontrado" });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CuidadoController.BuscarPet ERROR] {ex.Message}");
            Console.Error.WriteLine($"[Stack Trace] {ex.StackTrace}");
            return BadRequest(new { mensagem = $"Não foi possível localizar o pet: {ex.Message}" });
        }
    }

    [HttpPost("CadastrarServico")]
    [Authorize(Roles = "usuario_comum")]
    [SwaggerOperation(
    Summary = "Cadastra um serviço para um pet",
    Description = "Este método permite que o usuário cadastre um serviço para um pet específico. O usuário deve estar autenticado e ter o papel 'usuario_comum'. O token JWT deve ser incluído no cabeçalho da requisição para autenticação."
    )]
    public async Task<IActionResult> CadastroServico([FromQuery] string nomePet, [FromBody] CadastrarServicoDTO cuidadoDTO)
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return BadRequest("Token JWT não fornecido no header Authorization");
        }

        var jwtToken = authHeader.Replace("Bearer ", "").Trim();

        try
        {
            var request = new CadastrarServicoRequest(
                nomePet,
                cuidadoDTO.TipoServico,
                cuidadoDTO.DataServico,
                cuidadoDTO.Observacao
            ).AtualizarToken(jwtToken);

            var resultado = await _mediator.Send(request);
            
            if (resultado.Sucesso)
            {
                return Ok(new { mensagem = resultado.Mensagem });
            }
            
            return BadRequest(new { mensagem = resultado.Mensagem });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CuidadoController.CadastroServico ERROR] {ex.Message}");
            Console.Error.WriteLine($"[Stack Trace] {ex.StackTrace}");
            return BadRequest(new { mensagem = $"Não foi possível cadastrar o serviço: {ex.Message}" });
        }
    }

    [HttpGet("ListarAgendamentosPorPet")]
    [Authorize(Roles = "usuario_comum")]
    [SwaggerOperation(
    Summary = "Lista todos os serviços/agendamentos do pet",
    Description = "Este método lista todos os serviços que o usuário tenha cadastrado para um pet específico. O usuário deve estar autenticado e ter o papel 'usuario_comum'. O token JWT deve ser incluído no cabeçalho da requisição para autenticação."
    )]
    public async Task<IActionResult> ListarAgendamentos([FromQuery] string nomePet)
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return BadRequest("Token JWT não fornecido no header Authorization");
        }

        var jwtToken = authHeader.Replace("Bearer ", "").Trim();

        try
        {
            var request = new ListarServicosPorPetRequest(nomePet).AtualizarToken(jwtToken);
            var resultado = await _mediator.Send(request);
            
            if (resultado?.Servicos != null && resultado.Servicos.Count > 0)
            {
                return Ok(resultado.Servicos);
            }
            
            return Ok(new List<object>());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CuidadoController.ListarAgendamentos ERROR] {ex.Message}");
            Console.Error.WriteLine($"[Stack Trace] {ex.StackTrace}");
            return BadRequest(new { mensagem = $"Não foi possível localizar os agendamentos: {ex.Message}" });
        }
    }

    [HttpGet("ListarTodosAgendamentos")]
    [SwaggerOperation(
    Summary = "Lista todos os agendamentos cadastrados",
    Description = "Este método lista todos os agendamentos/serviços cadastrados no sistema. Sem restrição de autenticação para fins de consulta pública."
    )]
    public async Task<IActionResult> ListarTodosAgendamentos()
    {
        try
        {
            var resultado = await _mediator.Send(new ListarTodosServicosRequest());
            
            if (resultado?.Servicos != null && resultado.Servicos.Count > 0)
            {
                return Ok(resultado.Servicos);
            }
            
            return Ok(new List<object>());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CuidadoController.ListarTodosAgendamentos ERROR] {ex.Message}");
            Console.Error.WriteLine($"[Stack Trace] {ex.StackTrace}");
            return BadRequest(new { mensagem = $"Não foi possível localizar os agendamentos: {ex.Message}" });
        }
    }
}