using CA.EShop.Application;
using CA.EShop.WebApi.Presentation;
using CA.EShop.WebApi.Presentation.Controllers;
using Carter;
using Mapster;
using MapsterMapper;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CA.EShop.WebApi.Settings
{
    /* Using 
     * SerCollectionI.AddControllers()
                     .AddApplicationPart(typeof(CProductsController).Assembly)
                     .AddControllersAsServices()
     * SerCollectionI.AddMvcCore().AddApiExplorer();  */
    public class Startup
    {
        public Startup(IConfiguration config_i)
        {
            ConfigI = config_i;
        }

        public IConfiguration ConfigI { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection SerCollectionI)
        {
            ///* AddEndpointsApiExplorer() does not exist in .NET 5.0. 
            ///  but SerCollectionI.AddMvcCore().AddApiExplorer(); is alternative */
            //SerCollectionI.AddEndpointsApiExplorer();

            SerCollectionI.AddControllers()
                          .AddApplicationPart(typeof(CProductsController).Assembly)
                          .AddControllersAsServices();

            SerCollectionI.AddMvcCore().AddApiExplorer();

            SerCollectionI.AddCarter();

            SerCollectionI.AddSwaggerGen(SGenOptions =>
            {
                SGenOptions.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "CA EShop API",
                    Version = "v1"
                });
            });

            SerCollectionI.AddMediatR(mrsCfg =>
                mrsCfg.RegisterServicesFromAssembly(typeof(Ass_Presentation).Assembly));

            SerCollectionI.AddMarten(store_options =>
            {
                store_options.Connection(ConfigI.GetConnectionString("DBConn")!);
                store_options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder AppBuilderI, IWebHostEnvironment WHEnvI)
        {
            if (WHEnvI.IsDevelopment())
            {
                AppBuilderI.UseSwagger();
                AppBuilderI.UseSwaggerUI(SOptions =>
                {
                    SOptions.SwaggerEndpoint("/swagger/v1/swagger.json",
                                             "CA EShop API");
                    SOptions.RoutePrefix = "swagger";
                });
            }

            AppBuilderI.UseHttpsRedirection();

            AppBuilderI.UseRouting();

            AppBuilderI.UseAuthorization();

            AppBuilderI.UseEndpoints(endpointsRB_I =>
            {
                endpointsRB_I.MapControllers();
                endpointsRB_I.MapCarter();
            });
        }
    }


    ///* Minimal APIs, Mapster, MediatR, and Carter in .NET5
    // * Installing NuGet Package NSwag.AspNetCore
    // * Use AddOpenApiDocument and UseOpenApi */
    //public class Startup
    //{
    //    public Startup(IConfiguration config_i)
    //    {
    //        ConfigI = config_i;
    //    }

    //    public IConfiguration ConfigI { get; }

    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    public void ConfigureServices(IServiceCollection SerCollectionI)
    //    {
    //        ///* AddEndpointsApiExplorer() does not exist in .NET 5.0. 
    //        ///  but SerCollectionI.AddMvcCore().AddApiExplorer(); is alternative */
    //        //SerCollectionI.AddEndpointsApiExplorer();

    //        MapsterConfig.RegisterMappings();

    //        SerCollectionI.AddCarter();

    //        SerCollectionI.AddMvcCore().AddApiExplorer();

    //        SerCollectionI.AddSwaggerGen(SGenOptions =>
    //        {
    //            SGenOptions.SwaggerDoc("v1", new OpenApiInfo {
    //                Title = "CA EShop API",
    //                Version = "v1"
    //            });
    //        });

    //        SerCollectionI.AddOpenApiDocument(cfg =>
    //        {
    //            cfg.Title = "My API";
    //        });

    //        SerCollectionI.AddMediatR(mrsCfg =>
    //            mrsCfg.RegisterServicesFromAssembly(typeof(Ass_Presentation).Assembly));

    //        SerCollectionI.AddMarten(store_options =>
    //        {
    //            store_options.Connection(ConfigI.GetConnectionString("DBConn")!);
    //            store_options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
    //        });
    //    }

    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public void Configure(IApplicationBuilder AppBuilderI, IWebHostEnvironment WHEnvI)
    //    {
    //        if (WHEnvI.IsDevelopment())
    //        {
    //            AppBuilderI.UseSwagger();
    //            AppBuilderI.UseSwaggerUI(SOptions =>
    //            {
    //                SOptions.SwaggerEndpoint("/swagger/v1/swagger.json", 
    //                                         "CA EShop API");
    //                SOptions.RoutePrefix = "swagger";
    //            });
    //        }

    //        AppBuilderI.UseHttpsRedirection();

    //        AppBuilderI.UseRouting();

    //        AppBuilderI.UseOpenApi();

    //        AppBuilderI.UseAuthorization();

    //        AppBuilderI.UseEndpoints(endpointsRB_I =>
    //        {
    //            endpointsRB_I.MapControllers();
    //            endpointsRB_I.MapCarter();
    //        });
    //    }
    //}
}
