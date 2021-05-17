using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokemonDomain.Handlers;
using PokemonDomain.Models;

namespace PokemonWebService.Controllers
{
    /// <summary>
    /// Controller to get Pokemon details(basic and translated descriptions).
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonHandler _pokemonHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonController"/> class.
        /// </summary>
        /// <param name="pokemonHandler">Pokemon Handler.</param>
        public PokemonController(IPokemonHandler pokemonHandler)
        {
            _pokemonHandler = pokemonHandler;
        }

        /// <summary>
        /// Get Endpoint to return basic pokemon details.
        /// </summary>
        /// <param name="name">Pokemon name.</param>
        /// <returns>Name, Description, Habitat and Legendary status.</returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDetails(string name)
        {
            var response = await _pokemonHandler.GetPokemonDetails(name);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Endpoint to return fun translated pokemon details.
        /// </summary>
        /// <param name="name">Pokemon name.</param>
        /// <returns>Name, Translated Description, Habitat and Legendary status.</returns>
        [HttpGet]
        [Route("translated")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTranslatedDetails(string name)
        {
            var response = await _pokemonHandler.GetTranslatedPokemonDetails(name);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}