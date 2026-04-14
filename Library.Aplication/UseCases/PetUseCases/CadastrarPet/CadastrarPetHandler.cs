
using System.Collections;
using System.Net.Http.Json;
using Library.Aplication.Models;
using Library.Aplication.Settings;
using Library.Domain.Entities;
using Library.Domain.Interface;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Aplication.UseCases.PetUseCases.CadastrarPet;


public class CadastrarPetHandler : IRequestHandler<CadastrarPetRequest, CadastrarPetResponse>
{
    private readonly IPetRepositorory _petRepository;
    private readonly HttpClient _httpClient;
    private readonly IGerenciadorTokenService _gerenciadorTokenService;
    private readonly PetApiSettings _petApiSettings;

    public CadastrarPetHandler(IGerenciadorTokenService gerenciadorTokenService, HttpClient httpClient, IPetRepositorory petRepository, IOptions<PetApiSettings> petApiSettings)
    {
        _gerenciadorTokenService = gerenciadorTokenService;
        _httpClient = httpClient;
        _petRepository = petRepository;
        _petApiSettings = petApiSettings.Value;
    }

    public async Task<CadastrarPetResponse> Handle(CadastrarPetRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Token))
                throw new Exception("Token não fornecido");

            var (idUsuario, email) = await _gerenciadorTokenService.BuscarGuidTokenENome(request.Token!);

            string tipoApiPet = request.TipoPet == "CACHORRO" ? "dog" : "cat";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _petApiSettings.ApiKey);
            _httpClient.BaseAddress = new Uri($"https://api.the{tipoApiPet}api.com/");
            
            var responseApi = await _httpClient.GetAsync("v1/breeds");

            if (!responseApi.IsSuccessStatusCode)
                throw new Exception($"Erro ao buscar raças: {responseApi.StatusCode}");

            var racas = await responseApi.Content.ReadFromJsonAsync<List<Breed>>();
            var matchingBreed = racas?.FirstOrDefault(b =>
                string.Equals(b.Name, request.Raca, StringComparison.OrdinalIgnoreCase));
            
            if (matchingBreed == null)
                throw new Exception($"Raça '{request.Raca}' não encontrada na API");

            var response2 = await _httpClient.GetAsync($"v1/images/search?limit=1&breed_ids={matchingBreed.Id}");
            var imagem = await response2.Content.ReadFromJsonAsync<List<DogImageResponse>>();

            PetEntity petEntity = new PetEntity(
                Id: Guid.NewGuid().ToString(),
                IdUsuario: idUsuario,
                NomePet: request.NomePet,
                TipoPet: tipoApiPet,
                IdadePet: request.IdadePet,
                Sexo: request.Sexo,
                Raca: request.Raca,
                Imagem: imagem?.FirstOrDefault()?.Url ?? "Sem imagem"
            );

            var responseRepository = await _petRepository.CadastroPet(petEntity);
            if (responseRepository)
            {
                return new CadastrarPetResponse(true, "Pet cadastrado com sucesso");
            }
            throw new Exception("Falha ao salvar pet no banco de dados");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CadastrarPetHandler ERROR] {ex.Message}");
            Console.Error.WriteLine($"[Stack Trace] {ex.StackTrace}");
            return new CadastrarPetResponse(false, $"Erro ao cadastrar pet: {ex.Message}");
        }
    }
}