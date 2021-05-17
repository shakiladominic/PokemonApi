using System.Collections.Generic;

namespace PokemonDomain.Models
{
    public class GetPokemonDtoResponse
    {
        public string Name { get; set; }

        public IEnumerable<FlavorTextEntries> Flavor_Text_Entries { get; set; }

        public Dictionary<string, string> Habitat { get; set; }

        public bool IsLegendary { get; set; }
    }

    public class FlavorTextEntries
    {
        public string Flavor_Text { get; set; }

        public Dictionary<string, string> Language { get; set; }
    }
}
