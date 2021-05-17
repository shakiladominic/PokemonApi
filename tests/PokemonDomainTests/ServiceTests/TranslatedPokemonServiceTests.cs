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
    public class PokemonTranslationServiceTests
    {
        private IPokemonService _pokemonService;
        private PokemonTranslationService _pokemonTranslationService;
        private PostTranslationResponse _shakespeareTranslationResponse;
        private PostTranslationResponse _yodaTranslationResponse;
        private HttpClient _httpClient;

        private const string PokemonName = "testpokemon";
        private const string ShakespeareTranslationApi = "https://api.funtranslations.com/translate/shakespeare.json";
        private const string YodaTranslationApi = "https://api.funtranslations.com/translate/yoda.json";

        [SetUp]
        public void Setup()
        {
            var messages = new Dictionary<string, HttpResponseMessage>();
            var shakespeareTranslation = new Dictionary<string, string>
            {
                { "translated", "This is Shakespeare translation" }
            };
            _shakespeareTranslationResponse = new PostTranslationResponse()
            {
                Contents = shakespeareTranslation
            };
            messages.Add(ShakespeareTranslationApi, new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_shakespeareTranslationResponse))
            });

            var yodaTranslation = new Dictionary<string, string>
            {
                { "translated", "This is Yoda translation" }
            };
            _yodaTranslationResponse = new PostTranslationResponse()
            {
                Contents = yodaTranslation
            };
            messages.Add(YodaTranslationApi, new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_yodaTranslationResponse))
            });

            _httpClient = new HttpClient(new TestMessageHandler(messages));
            _pokemonService = Substitute.For<IPokemonService>();
        }

        public class PokemonTranslatedDetailServiceTests : PokemonTranslationServiceTests
        {
            [Test]
            public async Task GivenAValidRequestReceived_ToTranslationService_WhenLegendaryStatusFalse_ShouldReturnShakespeareTranslation()
            {
                //Arrange
                var pokemonDetailsResponse = new GetPokemonResponse()
                {
                    Name = PokemonName,
                    IsLegendary = false,
                    Habitat = null,
                    Description = "This is the english pokemon Description",
                };

                _pokemonTranslationService = new PokemonTranslationService(_httpClient, _pokemonService);
                _pokemonService.GetPokemonDetails(Arg.Is(PokemonName)).Returns(pokemonDetailsResponse);

                //Act
                var response = await _pokemonTranslationService.GetTranslatedPokemonDetails(PokemonName);

                //Assert
                response.ShouldNotBeNull();
                response.Description.ShouldBe("This is Shakespeare translation");
                response.Habitat.ShouldBeNull();
                response.IsLegendary.ShouldBeFalse();
            }

            [Test]
            public async Task GivenAValidRequestReceived_ToTranslationService_WhenHabitatIsCave_ShouldReturnYodaTranslation()
            {
                //Arrange
                var pokemonDetailsResponse = new GetPokemonResponse()
                {
                    Name = PokemonName,
                    IsLegendary = false,
                    Habitat = "cave",
                    Description = "This is the english pokemon Description",
                };

                _pokemonTranslationService = new PokemonTranslationService(_httpClient, _pokemonService);
                _pokemonService.GetPokemonDetails(Arg.Is(PokemonName)).Returns(pokemonDetailsResponse);

                //Act
                var response = await _pokemonTranslationService.GetTranslatedPokemonDetails(PokemonName);

                //Assert
                response.ShouldNotBeNull();
                response.Description.ShouldBe("This is Yoda translation");
                response.Habitat.ShouldBe("cave");
                response.IsLegendary.ShouldBeFalse();
            }

            [Test]
            public async Task GivenAValidRequestReceived_WhenTranslateServiceReturnsNull_ShouldReturnStandardDescription()
            {
                //Arrange
                var pokemonDetailsResponse = new GetPokemonResponse()
                {
                    Name = PokemonName,
                    IsLegendary = false,
                    Habitat = "cave",
                    Description = "This is the english pokemon Description",
                };
                var messages = new Dictionary<string, HttpResponseMessage>
                {
                    {
                        YodaTranslationApi,
                        new HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new StringContent(JsonConvert.SerializeObject(new PostTranslationResponse()))
                        }
                    }
                };
                var httpClientToTestNull = new HttpClient(new TestMessageHandler(messages));

                _pokemonTranslationService = new PokemonTranslationService(httpClientToTestNull, _pokemonService);
                _pokemonService.GetPokemonDetails(Arg.Is(PokemonName)).Returns(pokemonDetailsResponse);
                

                //Act
                var response = await _pokemonTranslationService.GetTranslatedPokemonDetails(PokemonName);

                //Assert
                response.ShouldNotBeNull();
                response.Description.ShouldBe("This is the english pokemon Description");
                response.Habitat.ShouldBe("cave");
                response.IsLegendary.ShouldBeFalse();
            }
        }
    }
}

