using AutoMapper;
using Microsoft.Extensions.Logging;
using Movie.Data.DataConfigurations;
//using Movie.Data.DataConfigurations;

namespace Controller.Tests.Helpers;

public class MapperFactory
{
    public static IMapper Create()
    {
        var configExpression = new MapperConfigurationExpression();
        configExpression.AddProfile<MapperProfile>();

        var config = new MapperConfiguration(configExpression, new LoggerFactory());
        //config.AssertConfigurationIsValid();
        return config.CreateMapper();
    }
}