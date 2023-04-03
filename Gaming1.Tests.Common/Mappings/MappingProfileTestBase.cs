using AutoMapper;
using System;
using Xunit;

namespace Gaming1.Tests.Common.Mappings
{
    [Collection("Sequential")]
    public abstract class MappingProfileTestBase : IDisposable
    {
        protected MapperConfiguration MapperConfiguration;
        protected readonly IMapper Mapper;

        protected MappingProfileTestBase()
        {
            MapperConfiguration = new MapperConfiguration(GetConfigurationProfiles());
            Mapper = MapperConfiguration.CreateMapper();
        }

        [Fact]
        public void Configuration_IsValid()
        {
            MapperConfiguration.AssertConfigurationIsValid();
        }

        public void Dispose()
        {
        }

        public abstract Action<IMapperConfigurationExpression> GetConfigurationProfiles();
    }
}