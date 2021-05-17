using AutoMapper;
using PokemonDomain.Models;
using System.Net.Http;
using System.Threading.Tasks;
using PokemonDomain.Extensions;

namespace PokemonDomain.Services
{
    /// <inheritdoc/>
    public class PokemonService : IPokemonService
    {
        private const string PokemonApiUrl = "https://pokeapi.co/api/v2/pokemon-species";
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonService"/> class.
        /// </summary>
        /// <param name="httpClient">Http Client.</param>
        /// <param name="mapper">AutoMapper.</param>
        public PokemonService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<GetPokemonResponse> GetPokemonDetails(string pokemonName)
        {
            var response = await _httpClient.GetAsync<GetPokemonDtoResponse>($"{PokemonApiUrl}/{pokemonName}");
            if (response.Content != null)
            {
                return _mapper.Map<GetPokemonResponse>(response.Content);
            }

            return null;            
        }
    }
}
