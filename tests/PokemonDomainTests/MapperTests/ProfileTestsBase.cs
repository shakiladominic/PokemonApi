using AutoMapper;
using NUnit.Framework;

namespace PokemonDomainTests.MapperTests
{
    [TestFixture]
    public abstract class ProfileTestsBase<T>
        where T : Profile, new()
    {
        /// <summary>
        /// The mapper configured with the profile <see cref="T"/>.
        /// </summary>
        protected IMapper Mapper { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ProfileTestsBase()
        {
            Mapper = new Mapper(new MapperConfiguration(conf => conf.AddProfile(new T())));
        }
    }
}