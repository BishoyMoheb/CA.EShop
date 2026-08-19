using CA.EShop.Application.Products_Creation;
using CA.EShop.Application.Products_Creation.ProductSelection;
using Carter;
using MediatR;
using Mapster;
using Carter.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CA.EShop.Application.Products_Creation.ProductUpdating;
using CA.EShop.Domain.Shared;
using CA.EShop.Application.Products_Creation.ProductDeleting;

namespace CA.EShop.WebApi.Presentation
{
    public static class Mod_Product //: CarterModule
    {
        /* Doing all the CRUD operations for the products */
        public static void AddRoutes(IEndpointRouteBuilder endpointRBuilderI)
        {
            endpointRBuilderI.MapGet("/cproducts",
                async HContext =>
                {
                    var senderI = HContext.RequestServices.GetRequiredService<ISender>();
                    var TRes_L_rPResponse = await senderI.Send(new RQueryGetProduct());
                    HContext.Response.ContentType = "application/json";
                    await HContext.Response.WriteAsJsonAsync(TRes_L_rPResponse);
                });


            endpointRBuilderI.MapPost("/cproducts",
                async HContext =>
                {
                    // Deserialize body
                    var rCPRequest = await HContext.Request
                                                .ReadFromJsonAsync<RCreateProdRequest>();

                    // Resolve ISender
                    var senderI = HContext.RequestServices.GetRequiredService<ISender>();

                    // Map to command
                    var rPCCmd = rCPRequest.Adapt<RProdCreateCmd>();

                    var GResult = await senderI.Send(rPCCmd);
                    await HContext.Response.WriteAsJsonAsync(GResult);
                    HContext.Response.StatusCode = StatusCodes.Status200OK;
                });


            endpointRBuilderI.MapPut("/cproducts/{ProdId:long}",
                async HContext =>
                {
                    var ProdId = long.Parse(HContext.Request.RouteValues["ProdId"].ToString());
                    var rUProdRequest = await HContext.Request
                                                     .ReadFromJsonAsync<RUpdateProdRequest>();
                    if (rUProdRequest is null)
                    {
                        HContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        return;
                    }
                    RUpdateProdCmd rUPCmd = rUProdRequest.Adapt<RUpdateProdCmd>() with {
                        PId = ProdId
                    };
                    var senderI = HContext.RequestServices.GetRequiredService<ISender>();
                    GenResult GResult= await senderI.Send(rUPCmd);
                    if (GResult.IsFailure)
                    {
                        HContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        return;
                    }
                    HContext.Response.StatusCode = StatusCodes.Status204NoContent;
                });


            endpointRBuilderI.MapDelete("/cproducts/{ProdId:long}",
                async HContext =>
                {
                    var ProdId = long.Parse(HContext.Request.RouteValues["ProdId"].ToString());
                    var rDProdRequest = await HContext.Request
                                                     .ReadFromJsonAsync<RDeletingProdRequest>();
                    if (rDProdRequest is null)
                    {
                        HContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        return;
                    }
                    RDeletingProdCmd rDPCmd = rDProdRequest.Adapt<RDeletingProdCmd>() with {
                        PId = ProdId
                    };
                    var senderI = HContext.RequestServices.GetRequiredService<ISender>();
                    GenResult GResult = await senderI.Send(rDPCmd);
                    if (GResult.IsFailure)
                    {
                        HContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        return;
                    }
                    HContext.Response.StatusCode = StatusCodes.Status204NoContent;
                });
        }
    }
}
