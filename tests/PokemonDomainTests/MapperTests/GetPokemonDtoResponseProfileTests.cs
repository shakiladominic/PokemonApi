using System.Collections.Generic;
using NUnit.Framework;
using PokemonDomain.Mappers;
using PokemonDomain.Models;
using Shouldly;

namespace PokemonDomainTests.MapperTests
{
    [TestFixture]
    public class GetPokemonDtoResponseProfileTests : ProfileTestsBase<GetPokemonDtoResponseProfile>
    {

        [Test]
        public void WhenGetPokemonDTOResponse_ShouldReturnGetPokemonResponse()
        {
            //Arrange
            var expectedGetPokemonResponse = new GetPokemonResponse()
            {
                Name = "test",
                Description = "This is the english pokemon description",
                Habitat = "Cave",
                IsLegendary = true,
            };

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
            var getPokemonDtoResponse = new GetPokemonDtoResponse()
            {
                Name = "test",
                IsLegendary = true,
                Habitat = habitat,
                Flavor_Text_Entries = flavorTextEntries
            };

            //Act
            var destination = Mapper.Map(getPokemonDtoResponse, new GetPokemonResponse());

            //Assert
            destination.ShouldBeOfType<GetPokemonResponse>();
            destination.Name.ShouldBe(expectedGetPokemonResponse.Name);
            destination.Description.ShouldBe(expectedGetPokemonResponse.Description);
            destination.Habitat.ShouldBe(expectedGetPokemonResponse.Habitat);
            destination.IsLegendary.ShouldBe(expectedGetPokemonResponse.IsLegendary);
        }
    }


}
