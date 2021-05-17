using System.Collections.Generic;

namespace PokemonDomain.Models
{
    public class ClientResponse<T>
    {
        public T Content { get; set; }

        public Dictionary<string, string> Header { get; set; }
    }
}
