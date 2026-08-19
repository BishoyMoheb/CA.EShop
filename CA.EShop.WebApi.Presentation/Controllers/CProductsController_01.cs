using System.Threading.Tasks;
using CA.EShop.Application.Products_Creation;
using CA.EShop.Application.Products_Creation.ProductSelection;
using Mapster;
using MediatR;
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
