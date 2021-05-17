using NSubstitute;
using NUnit.Framework;
using PokemonDomain.Handlers;
using PokemonDomain.Models;
using PokemonDomain.Services;
using Shouldly;
using System.Threading.Tasks;

namespace PokemonDomainTests.HandlerTests
{
    public class PokemonHandlerTests
    {
        private IPokemonService _pokemonService;
        private IPokemonTranslationService _pokemonTranslationService;
        private IPokemonHandler _pokemonHandler;

        [SetUp]
        public void Setup()
        {
            _pokemonService = Substitute.For<IPokemonService>();
            _pokemonTranslationService = Substitute.For<IPokemonTranslationService>();
            _pokemonHandler = new PokemonHandler(_pokemonService, _pokemonTranslationService);
        }

        public class PokemonDetailsHandlerTests : PokemonHandlerTests
        {
            [Test]
            public async Task GivenAValidRequestReceived_ToGetPokemonDetails_ShouldCallPokemonService()
            {
                //Arrange
                var pokemonResponse = new GetPokemonResponse()
                {
                    Name = "test",
                    Description = "This is the english description",
                    Habitat = null,
                    IsLegendary = true,
                };
                _pokemonService.GetPokemonDetails(Arg.Any<string>()).Returns(pokemonResponse);

                //Act
                var handlerResponse = await _pokemonHandler.GetPokemonDetails("test");

                //Assert
                await _pokemonService.Received(1).GetPokemonDetails(Arg.Is("test"));
                handlerResponse.Name.ShouldBe("test");
                handlerResponse.Description.ShouldBe("This is the english description");
                handlerResponse.IsLegendary.ShouldBeTrue();
                handlerResponse.Habitat.ShouldBeNull();
            }


            [Test]
            public async Task GivenAValidRequestReceived_ToGetPokemonTranslatedDetails_ShouldCallPokemonTranslationService()
            {
                //Arrange
                var pokemonResponse = new GetPokemonResponse()
                {
                    Name = "test",
                    Description = "This is the shakespeare description",
                    Habitat = "cave",
                    IsLegendary = true,
                };
                _pokemonTranslationService.GetTranslatedPokemonDetails(Arg.Any<string>()).Returns(pokemonResponse);

                //Act
                var handlerResponse = await _pokemonHandler.GetTranslatedPokemonDetails("test");

                //Assert
                await _pokemonTranslationService.Received(1).GetTranslatedPokemonDetails(Arg.Is("test"));
                handlerResponse.Name.ShouldBe("test");
                handlerResponse.Description.ShouldBe("This is the shakespeare description");
                handlerResponse.IsLegendary.ShouldBeTrue();
                handlerResponse.Habitat.ShouldBe("cave");
            }
        }
    }
}
    
