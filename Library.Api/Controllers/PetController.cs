using System;
using System.Threading.Tasks;
using Library.Aplication.UseCases.PetUseCases.CadastrarPet;
using Library.Aplication.UseCases.PetUseCases.ListarRacaCachorros;
using Library.Aplication.UseCases.PetUseCases.ListarRacaGatos;
using Library.Aplication.UseCases.PetUseCases.ListarTodosPetsCadastrados;
using Library.Aplication.UseCases.PetUseCases.ListarPetsPorRaca;
using Library.Aplication.UseCases.PetUseCases.ListarCachorrosCadastrados;
using Library.Aplication.UseCases.PetUseCases.ListarGatosCadastrados;
using Library.Aplication.UseCases.PetUseCases.DeletarPet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PetController : ControllerBase
{
    IMediator _mediator;

    public PetController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("cadastro")]
    [Authorize(Roles = "usuario_comum")]
    [SwaggerOperation(
    Summary = "Cadastrar um novo pet",
    Description = "Este método permite ao usuário comum cadastrar um novo pet no sistema, fornecendo informações como tipo de pet e outros detalhes. O tipo de pet deve ser 'CACHORRO' ou 'GATO'."
    )]
    public async Task<IActionResult> CadastrarPet([FromBody] CadastrarPetRequest request)
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return BadRequest("Token JWT não fornecido no header Authorization");
        }

        var jwtToken = authHeader.Replace("Bearer ", "").Trim();
        
        if (request.TipoPet != "CACHORRO" && request.TipoPet != "GATO")
        {
            return BadRequest("Tipo de Pet Inválido. Use 'CACHORRO' ou 'GATO'");
        }

        try
        {
            var requestComToken = new CadastrarPetRequest(
                request.NomePet,
                request.TipoPet,
                request.IdadePet,
                request.Raca,
                request.Sexo
            ).AtualizarToken(jwtToken);

            var resultado = await _mediator.Send(requestComToken);
            
            if (resultado.Sucesso)
            {
                return Ok(new { mensagem = "Pet cadastrado com sucesso", resultado });
            }
            
            return BadRequest(new { mensagem = resultado.Mensagem ?? "Não foi possível salvar o pet" });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[PetController.CadastrarPet ERROR] {ex.Message}");
            Console.Error.WriteLine($"[Stack Trace] {ex.StackTrace}");
            return BadRequest(new { mensagem = $"Erro ao cadastrar pet: {ex.Message}" });
        }
    }



    [HttpGet("listarRacaGatos")]
    [SwaggerOperation(
    Summary = "Lista as raças de gatos da API externa",
    Description = "Este método retorna uma lista de todas as raças de gatos cadastradas na API externa."
    )]
    public async Task<ListarRacaGatosResponse> ListarRacaGatos()
    {
        var resultado = await _mediator.Send(new ListarRacaGatosRequest());
        if (resultado != null)
        {
            return resultado;
        }
        throw new Exception("Não foi possivel listar as raças");
    }

    [HttpGet("listarRacaCachorros")]
    [SwaggerOperation(
    Summary = "Lista as raças de cachorros da API externa",
    Description = "Este método retorna uma lista de todas as raças de cachorros cadastradas na API externa."
    )]
    public async Task<IActionResult> ListarRacaCachorros()
    {
        var resultado = await _mediator.Send(new ListarRacaCachorrosRequest());
        if (resultado != null)
        {
            return Ok(resultado);
        }
        return BadRequest("Não foi possivel listar as raças");
    }

    [HttpGet("ListarTodosPetsCadastrados")]
    [SwaggerOperation(
    Summary = "Lista todos os pets cadastrados",
    Description = "Este método retorna uma lista de todos os pets cadastrados no sistema. Caso não haja pets cadastrados, um erro será retornado informando a falha."
    )]
    public async Task<IActionResult> ListarTodosPetsCadastrados()
    {
        var resultado = await _mediator.Send(new ListarTodosPetsCadastradosRequest());
        if (resultado?.Pets != null && resultado.Pets.Count > 0)
        {
            return Ok(resultado.Pets);
        }
        return BadRequest("Não foi possivel listar os pets cadastrados");
    }


    [HttpGet("ListarTodosPetsCadastradosComRacaEspecifica")]
    [SwaggerOperation(
    Summary = "Lista todos os pets cadastrados com uma raça específica",
    Description = "Este método retorna uma lista de pets cadastrados que pertencem a uma raça específica fornecida como parâmetro. Se não houver nenhum pet com a raça informada, será retornado um erro."
    )]
    public async Task<IActionResult> ListarTodosPetsCadastradosComRacaEspecifica([FromQuery] string raca)
    {
        var resultado = await _mediator.Send(new ListarPetsPorRacaRequest(raca));
        if (resultado?.Pets != null && resultado.Pets.Count > 0)
        {
            return Ok(resultado.Pets);
        }
        return BadRequest("Não foi possivel listar os pets cadastrados");
    }


    [HttpGet("ListarCachorrosCadastrados")]
    [SwaggerOperation(
    Summary = "Lista todos os cachorros cadastrados",
    Description = "Este método retorna uma lista de todos os cachorros cadastrados no sistema. Se não houver nenhum cachorro cadastrado, será retornado um erro informando que não foi encontrado nenhum cachorro."
    )]
    public async Task<IActionResult> ListarCachorrosCadastrados()
    {
        var resultado = await _mediator.Send(new ListarCachorrosCadastradosRequest());
        if (resultado?.Pets != null && resultado.Pets.Count > 0)
        {
            return Ok(resultado.Pets);
        }
        return BadRequest("Não foi localizado nenhum cachorro cadastrado");
    }


    [HttpGet("ListarGatosCadastrados")]
    [SwaggerOperation(
    Summary = "Lista todos os gatos cadastrados",
    Description = "Este método retorna uma lista de todos os gatos cadastrados no sistema. Se não houver nenhum gato cadastrado, será retornado um erro informando que não foi encontrado nenhum gato."
    )]
    public async Task<IActionResult> ListarGatosCadastrados()
    {
        var resultado = await _mediator.Send(new ListarGatosCadastradosRequest());
        if (resultado?.Pets != null && resultado.Pets.Count > 0)
        {
            return Ok(resultado.Pets);
        }
        return BadRequest("Não foi localizado nenhum gato cadastrado");
    }


    [HttpDelete("{id}")]
    [SwaggerOperation(
    Summary = "Deleta um pet pelo ID",
    Description = "Este método remove um pet do sistema com base no ID fornecido. Caso o pet não seja encontrado ou não seja possível realizar a exclusão, será retornado um erro informando a falha."
    )]
    public async Task<IActionResult> Deletar(int id)
    {
        try
        {
            var response = await _mediator.Send(new DeletarPetRequest(id));
            if (response.Sucesso)
            {
                return Ok("Deletado com sucesso");
            }
            return BadRequest("Não foi possivel deletar");
        }
        catch
        {
            return BadRequest("Não foi possivel deletar");
        }
    }

}