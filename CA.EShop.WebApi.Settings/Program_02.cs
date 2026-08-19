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
using MediatR;
using CA.EShop.Application.Products_Creation.ProductSelection;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Mapster;

namespace CA.EShop.WebApi.Settings
{
    /* Minimal APIs with the usage of AddOpenApiDocument and UseOpenApi
     * First use case Endpoints defined in Program.cs file */
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
                            endpointsRB_I.MapGet("/cproducts",
                                async HContext =>
                                {
                                    var senderI = HContext.RequestServices.GetRequiredService<ISender>();
                                    var TRes_L_rPResponse = await senderI.Send(new RQueryGetProduct());
                                    HContext.Response.ContentType = "application/json";
                                    await JsonSerializer.SerializeAsync(HContext.Response.Body, 
                                                                        TRes_L_rPResponse);
                                });

                            endpointsRB_I.MapPost("/cproducts",
                                async (HttpContext HContext) =>
                                {
                                    var rCPRequest = await HContext.Request
                                                                .ReadFromJsonAsync<RCreateProdRequest>();
                                    var senderI = HContext.RequestServices.GetRequiredService<ISender>();
                                    var rPCCmd = rCPRequest.Adapt<RProdCreateCmd>();
                                    var GResult = await senderI.Send(rPCCmd);
                                    await HContext.Response.WriteAsJsonAsync(GResult);
                                    HContext.Response.StatusCode = StatusCodes.Status200OK;
                                });

                            endpointsRB_I.MapCarter();
                        });
                    });
                });
    }
}
