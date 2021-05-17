using PokemonDomain.Models;
using PokemonDomain.Services;
using System.Threading.Tasks;

namespace PokemonDomain.Handlers
{
    /// <inheritdoc/>
    public class PokemonHandler : IPokemonHandler
    {
        private readonly IPokemonService _pokemonService;
        private readonly IPokemonTranslationService _pokemonTranslationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonHandler"/> class.
        /// </summary>
        /// <param name="pokemonService">Pokemon Service.</param>
        /// <param name="pokemonTranslationService">Translate Pokemon Service.</param>
        public PokemonHandler(IPokemonService pokemonService, IPokemonTranslationService pokemonTranslationService)
        {
            _pokemonService = pokemonService;
            _pokemonTranslationService = pokemonTranslationService;
        }

        /// <inheritdoc/>
        public async Task<GetPokemonResponse> GetPokemonDetails(string pokemonName)
        {
            return await _pokemonService.GetPokemonDetails(pokemonName);
        }

        /// <inheritdoc/>
        public async Task<GetPokemonResponse> GetTranslatedPokemonDetails(string pokemonName)
        {
            return await _pokemonTranslationService.GetTranslatedPokemonDetails(pokemonName);
        }
    }
}
