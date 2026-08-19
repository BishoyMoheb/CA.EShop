using System.Threading.Tasks;
using CA.EShop.Application.Products_Creation;
using CA.EShop.Application.Products_Creation.ProductDeleting;
using CA.EShop.Application.Products_Creation.ProductSelection;
using CA.EShop.Application.Products_Creation.ProductUpdating;
using CA.EShop.Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CA.EShop.WebApi.Presentation.Mod_Product;

namespace CA.EShop.WebApi.Presentation.Controllers
{
    /* Before Minimal API */
    [ApiController]
    [Route("[controller]")]
    public class CProductsController : ControllerBase
    {
        private readonly ISender _senderI;


        public CProductsController(ISender SenderI)
            => _senderI = SenderI;


        [HttpPost]
        public async Task<IActionResult> Create(RCreateProdRequest rCPRequest,
                                               [FromServices] ISender senderI)
        {
            var rPCCmd = rCPRequest.Adapt<RProdCreateCmd>();
            await senderI.Send(rPCCmd);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _senderI.Send(new RQueryGetProduct());
            return Ok(result);
        }


        [HttpPut("{ProdId:long}")]
        public async Task<IActionResult> UpdateProduct(long ProdId,
                                 [FromBody] RUpdateProdRequest rUProdRequest)
        {
            var rUPCmd = rUProdRequest.Adapt<RUpdateProdCmd>() with {
                PId = ProdId
            };
            GenResult GResult = await _senderI.Send(rUPCmd);
            if (GResult.IsFailure)
            {
                return NotFound(GResult.genError);
            }
            return NoContent();
        }


        [HttpDelete("{ProdId:long}")]
        public async Task<IActionResult> DeleteProduct(long ProdId,
                                    [FromBody] RDeletingProdRequest rDProdRequest)
        {
            var rDPCmd = rDProdRequest.Adapt<RDeletingProdCmd>() with { 
                PId = ProdId 
            };
            GenResult GResult = await _senderI.Send(rDPCmd);
            if (GResult.IsFailure)
                return NotFound(GResult.genError);
            return NoContent();
        }
    }

    ///* Remove controller for the usage of Minimal API */
    //[NonController]
    //[ApiExplorerSettings(IgnoreApi = true)]
    //public class CProductsController : ControllerBase
    //{
    //    [HttpPost]
    //    public async Task<IActionResult> Create(RCreateProdRequest rCPRequest,
    //                                           [FromServices] ISender senderI)
    //    {
    //        var rPCCmd = rCPRequest.Adapt<RProdCreateCmd>();
    //        await senderI.Send(rPCCmd);
    //        return Ok();
    //    }
    //}
}
