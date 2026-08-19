using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;

namespace CA.EShop.WebApi.Settings
{
    public class ServiceMapper : IMapper
    {
        private readonly TypeAdapterConfig _tAConfig;

        public ServiceMapper(TypeAdapterConfig TAConfig)
        {
            _tAConfig = TAConfig;
        }

        public TypeAdapterConfig Config { get; }

        public TypeAdapterBuilder<TSource> From<TSource>(TSource source) => throw new NotImplementedException();

        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>(_tAConfig);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TSource, TDestination>(_tAConfig);
        }

        //public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        //{
        //    return source.Adapt(source, destination, _tAConfig);
        //}

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return source.Adapt(sourceType, destinationType, _tAConfig);
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType) => throw new NotImplementedException();
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) => throw new NotImplementedException();
    }
}
