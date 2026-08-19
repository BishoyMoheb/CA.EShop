using CA.EShop.Application.Products_Creation;
using Mapster;
using static CA.EShop.WebApi.Presentation.Mod_Product;

namespace CA.EShop.WebApi.Presentation
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<RCreateProdRequest, RProdCreateCmd>
                .NewConfig()
                .MapWith(src =>
                    new RProdCreateCmd(src.PName, src.PPrice, src.LTags)
                );
        }
    }
}
