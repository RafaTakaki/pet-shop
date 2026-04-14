using System.Net.Http.Json;
using Library.Aplication.Models;
using Library.Aplication.Settings;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Aplication.UseCases.PetUseCases.ListarRacaCachorros;

public class ListarRacaCachorrosHandler : IRequestHandler<ListarRacaCachorrosRequest, ListarRacaCachorrosResponse>
{
    private readonly HttpClient _httpClient;
    private readonly PetApiSettings _petApiSettings;

    public ListarRacaCachorrosHandler(HttpClient httpClient, IOptions<PetApiSettings> petApiSettings)
    {
        _httpClient = httpClient;
        _petApiSettings = petApiSettings.Value;
    }

    public async Task<ListarRacaCachorrosResponse> Handle(ListarRacaCachorrosRequest request, CancellationToken cancellationToken)
    {
        List<string> racas = new List<string>();

        _httpClient.BaseAddress = new Uri($"https://api.thedogapi.com/");
        _httpClient.DefaultRequestHeaders.Add("x-api-key", _petApiSettings.ApiKey);
        var response = await _httpClient.GetAsync("v1/breeds");

        if (response.IsSuccessStatusCode)
        {
            var breeds = await response.Content.ReadFromJsonAsync<List<Breed>>();

            if (breeds != null)
            {
                foreach (var breed in breeds)
                    racas.Add(breed.Name);
            }

        }
        return new ListarRacaCachorrosResponse(racas!);
    }
}