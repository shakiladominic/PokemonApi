using PokemonDomain.Models;
using AutoMapper;
using System.Linq;

namespace PokemonDomain.Mappers
{
    /// <summary>
    /// Mapper class to map the response from Pokemon-Species endpoint to actual response object.
    /// </summary>
    public class GetPokemonDtoResponseProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPokemonDtoResponseProfile"/> class.
        /// </summary>
        public GetPokemonDtoResponseProfile()
        {
            CreateMap<GetPokemonDtoResponse, GetPokemonResponse>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                    src.Flavor_Text_Entries.FirstOrDefault(x => x.Language["name"] == "en").Flavor_Text))
                .ForMember(dest => dest.Habitat, opt => opt.MapFrom(src =>
                src.Habitat["name"]));
        }
    }
}
