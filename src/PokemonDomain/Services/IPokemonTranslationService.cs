using PokemonDomain.Models;
using System.Threading.Tasks;

namespace PokemonDomain.Services
{
    /// <summary>
    /// Interface definition for Pokemon Translation Service. This service get the basic pokemon details with fun translation.
    /// </summary>
    public interface IPokemonTranslationService
    {
        Task<GetPokemonResponse> GetTranslatedPokemonDetails(string pokemonName);
    }
}
