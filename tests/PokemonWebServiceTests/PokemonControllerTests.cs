using NSubstitute;
using NUnit.Framework;
using PokemonDomain.Handlers;
using PokemonDomain.Models;
using PokemonWebService.Controllers;
using Shouldly;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PokemonWebServiceTests
{
    [TestFixture]
    public class PokemonControllerTests
    {
        private IPokemonHandler _pokemonHandler;

        [SetUp]
        public void Setup()
        {
            _pokemonHandler = Substitute.For<IPokemonHandler>();
        }

        [Test]
        public async Task GivenARequestReceived_ToGetEndPoint_WhenIsValid_ShouldCallHandler()
        {
            //Arrange
            _pokemonHandler.GetPokemonDetails("test").Returns(new GetPokemonResponse());
            var controller = new PokemonController(_pokemonHandler);

            //Act
            await controller.GetDetails("test");

            //Assert
            await _pokemonHandler.Received(1).GetPokemonDetails(Arg.Is("test"));
        }

        [Test]
        public async Task GivenARequestReceived_ToGetEndPoint_WhenIsValid_ShouldReturnValidResponse()
        {
            //Arrange
            _pokemonHandler.GetPokemonDetails("test").Returns(new GetPokemonResponse());
            var controller = new PokemonController(_pokemonHandler);
            //Act
            var response = await controller.GetDetails("test");

            //Assert
            response.ShouldBeOfType<OkObjectResult>();
        }

        [Test]
        public async Task GivenARequestReceived_ToGetTranslatedEndPoint_WhenIsValid_ShouldCallHandler()
        {
            //Arrange
            _pokemonHandler.GetTranslatedPokemonDetails("test").Returns(new GetPokemonResponse());
            var controller = new PokemonController(_pokemonHandler);
            
            //Act
            await controller.GetTranslatedDetails("test");

            //Assert
            await _pokemonHandler.Received(1).GetTranslatedPokemonDetails(Arg.Is("test"));
        }

        [Test]
        public async Task GivenARequestReceived_ToGetTranslatedEndPoint_WhenIsValid_ShouldReturnValidResponse()
        {
            //Arrange
            _pokemonHandler.GetTranslatedPokemonDetails("test").Returns(new GetPokemonResponse());
            var controller = new PokemonController(_pokemonHandler);
            //Act
            var response = await controller.GetTranslatedDetails("test");

            //Assert
            response.ShouldBeOfType<OkObjectResult>();
        }
    }
}
