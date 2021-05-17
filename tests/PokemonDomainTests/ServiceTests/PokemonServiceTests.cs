using AutoMapper;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using PokemonDomain.Models;
using PokemonDomain.Services;
using Shouldly;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PokemonDomainTests.Helpers;

namespace PokemonDomainTests.ServiceTests
{
    public class PokemonServiceTests
    {
        private IMapper _mapper;
        private IPokemonService _pokemonService;
        private HttpClient _httpClient;
        private GetPokemonDtoResponse _getPokemonDtoResponse;

        private const string PokemonStandardApi = "https://pokeapi.co/api/v2/pokemon-species/testpokemon";

        [SetUp]
        public void Setup()
        {
            _mapper = Substitute.For<IMapper>();
            var flavorTextEntries = new List<FlavorTextEntries>();
            var habitat = new Dictionary<string, string>
            {
                { "name", "Cave" }
            };
            var languageEn = new Dictionary<string, string>
            {
                { "name", "en" }
            };
            var flavorTextEntryEn = new FlavorTextEntries()
            {
                Flavor_Text = "This is the english pokemon description",
                Language = languageEn,
            };
            flavorTextEntries.Add(flavorTextEntryEn);

            var languageFr = new Dictionary<string, string>
            {
                { "name", "fr" }
            };
            var flavorTextEntryFr = new FlavorTextEntries()
            {
                Flavor_Text = "This is the french pokemon description",
                Language = languageFr,
            };

            flavorTextEntries.Add(flavorTextEntryFr);
            _getPokemonDtoResponse = new GetPokemonDtoResponse()
            {
                Name = "testpokemon",
                IsLegendary = true,
                Habitat = habitat,
                Flavor_Text_Entries = flavorTextEntries
            };
            var messages = new Dictionary<string, HttpResponseMessage>
            {
                {
                    PokemonStandardApi,
                    new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new StringContent(JsonConvert.SerializeObject(_getPokemonDtoResponse))
                    }
                }
            };
            _httpClient = new HttpClient(new TestMessageHandler(messages));
            _pokemonService = new PokemonService(_httpClient, _mapper);
        }
        
        [Test]
        public async Task GivenAValidRequestReceived_ShouldReturnBasicPokemonDetails()
        {
            //Arrange
            var name = "testpokemon";
            _mapper.Map<GetPokemonResponse>(Arg.Any<GetPokemonDtoResponse>()).Returns(new GetPokemonResponse()
                {
                    Name = "testpokemon",
                    IsLegendary = true,
                    Habitat = "Cave",
                    Description = "This is the english pokemon description",

                });

            //Act
            var response = await _pokemonService.GetPokemonDetails(name);

            //Assert
            response.ShouldNotBeNull();
            response.Description.ShouldBe("This is the english pokemon description");
            response.Habitat.ShouldBe("Cave");
            response.IsLegendary.ShouldBeTrue();
        }
    }
    
}