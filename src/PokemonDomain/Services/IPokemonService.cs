using PokemonDomain.Models;
using System.Threading.Tasks;

namespace PokemonDomain.Services
{
    /// <summary>
    /// Interface definition for Pokemon Service. This service get the basic pokemon details without any fun translation.
    /// </summary>
    public interface IPokemonService
    {
        /// <summary>
        /// Get basic pokemon details without any fun translation of description.
        /// </summary>
        /// <param name="pokemonName">Pokemon Name.</param>
        /// <returns>Name, Description, Habitat and Legendary status.</returns>
        Task<GetPokemonResponse> GetPokemonDetails(string pokemonName);
    }
}
