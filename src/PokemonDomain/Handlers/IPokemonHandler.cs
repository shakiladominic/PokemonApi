using PokemonDomain.Models;
using System.Threading.Tasks;

namespace PokemonDomain.Handlers
{
    /// <summary>
    /// Interface for Pokemon handler class to orchestrate Pokemon services.
    /// </summary>
    public interface IPokemonHandler
    {
        /// <summary>
        /// Get basic pokemon details without any fun translation of description.
        /// </summary>
        /// <param name="pokemonName">Pokemon Name.</param>
        /// <returns>Name, Description, Habitat and Legendary status.</returns>
        Task<GetPokemonResponse> GetPokemonDetails(string pokemonName);

        /// <summary>
        /// Get basic pokemon details with fun translation of description.
        /// </summary>
        /// <param name="pokemonName">Pokemon Name.</param>
        /// <returns>Name, Fun Translated Description, Habitat and Legendary status.</returns>
        Task<GetPokemonResponse> GetTranslatedPokemonDetails(string pokemonName);
    }
}
