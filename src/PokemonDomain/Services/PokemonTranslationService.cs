using PokemonDomain.Models;
using System.Net.Http;
using System.Threading.Tasks;
using PokemonDomain.Extensions;
using System;

namespace PokemonDomain.Services
{
    /// <inheritdoc/>
    public class PokemonTranslationService : IPokemonTranslationService
    {
        private const string TranslationApiUrl = "https://api.funtranslations.com/translate";
        private readonly HttpClient _httpClient;
        private readonly IPokemonService _pokemonService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonTranslationService"/> class.
        /// </summary>
        /// <param name="httpClient">Http Client.</param>
        /// <param name="pokemonService">Pokemon Service..</param>
        public PokemonTranslationService(HttpClient httpClient, IPokemonService pokemonService)
        {
            _httpClient = httpClient;
            _pokemonService = pokemonService;
        }

        /// <inheritdoc/>
        public async Task<GetPokemonResponse> GetTranslatedPokemonDetails(string pokemonName)
        {
            var response = await _pokemonService.GetPokemonDetails(pokemonName);
            if (response != null)
            {
                var translationType = GetTranslationType(response);
                var translatedText = await _httpClient.PostAsync<PostTranslationRequest, PostTranslationResponse>($"{TranslationApiUrl}/{translationType}.json",
                                            CreateTranslationRequest(response));
                if (translatedText.Content.Contents != null)
                {
                    response.Description = translatedText.Content.Contents["translated"];
                }

                return response;
            }

            return null;
        }

        private PostTranslationRequest CreateTranslationRequest(GetPokemonResponse response)
        {
            return new PostTranslationRequest()
            {
                Text = response.Description
            };
        }

        private string GetTranslationType(GetPokemonResponse response)
        {
            return (string.Equals(response.Habitat,"Cave", StringComparison.OrdinalIgnoreCase) || response.IsLegendary)
                ? "yoda"
                : "shakespeare";
        }
    }
}
