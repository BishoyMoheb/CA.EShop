using CA.EShop.WebApi.Presentation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Carter;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
//using CA.EShop.WebApi.Presentation.Controllers;
using CA.EShop.Application.Products_Creation;
using CA.EShop.Domain.Products;

namespace CA.EShop.WebApi.Settings
{
    ///* Old coding */
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        CreateHostBuilder(args).Build().Run();
    //    }

    //    public static IHostBuilder CreateHostBuilder(string[] args) =>
    //        Host.CreateDefaultBuilder(args)
    //            .ConfigureWebHostDefaults(webBuilder =>
    //            {
    //                webBuilder.UseStartup<Startup>();
    //            });
    //}


    /* Minimal APIs 
     * Installing NuGet Package NSwag.AspNetCore
     * Use AddOpenApiDocument and UseOpenApi */
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) 
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHBuilderI =>
                {
                    webHBuilderI.ConfigureServices((webHBContext, SerCollectionI) =>
                    {
                        MapsterConfig.RegisterMappings();

                        SerCollectionI.AddCarter();

                        SerCollectionI.AddMvcCore().AddApiExplorer();

                        SerCollectionI.AddRouting();

                        SerCollectionI.AddSwaggerGen(SGenOptions =>
                        {
                            SGenOptions.SwaggerDoc("v1", new OpenApiInfo {
                                Title = "CA EShop API",
                                Version = "v1"
                            });
                        });

                        SerCollectionI.AddOpenApiDocument(cfg =>
                        {
                            cfg.Title = "My API";
                        });

                        SerCollectionI.AddMediatR(mrsCfg =>
                               mrsCfg.RegisterServicesFromAssemblies(
                                   typeof(Mod_Product).Assembly,
                                   typeof(RProdCreateCmd).Assembly));

                        SerCollectionI.AddMarten(store_options =>
                        {
                            store_options.Connection(webHBContext.Configuration.GetConnectionString("DBConn")!);
                            store_options.Schema
                                         .For<MProduct>()
                                         .Identity(p => p.ProdID);
                            store_options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
                        });
                    });

                    webHBuilderI.Configure((webHBContext, AppBuilderI) =>
                    {
                        if (webHBContext.HostingEnvironment.IsDevelopment())
                        {
                            AppBuilderI.UseSwagger();

                            AppBuilderI.UseSwaggerUI(SOptions =>
                            {
                                SOptions.SwaggerEndpoint("/swagger/v1/swagger.json",
                                                         "CA EShop API");
                                SOptions.RoutePrefix = "swagger";
                            });

                            AppBuilderI.UseDeveloperExceptionPage();
                        }

                        AppBuilderI.UseRouting();

                        AppBuilderI.UseOpenApi();

                        AppBuilderI.UseEndpoints(endpointsRB_I =>
                        {
                            endpointsRB_I.MapControllers();
                            endpointsRB_I.MapCarter();
                        });
                    });
                });
    }
}
