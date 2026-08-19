using System.Threading;
using System.Threading.Tasks;
using CA.EShop.Application.Abstractions.IMessaging;
using CA.EShop.Domain.Products;
using CA.EShop.Domain.Shared;
using Marten;

namespace CA.EShop.Application.Products_Creation.ProductDeleting
{
    internal sealed class DProdCmdHandler : ICommandHandler<RDeletingProdCmd>
    {
        private readonly IDocumentSession _docSession_I;

        public DProdCmdHandler(IDocumentSession DocSession_I)
        {
            _docSession_I = DocSession_I;
        }

        public async Task<GenResult> Handle(RDeletingProdCmd rDPCmd_Request, CancellationToken CToken)
        {
            var prod_ToDelete = await _docSession_I
                               .LoadAsync<MProduct>(rDPCmd_Request.PId, CToken);
            if (prod_ToDelete is null)
            {
                return GenResult.Failure(new GenError(
                                         "Product.NotFound",
                                         $"The product Id {rDPCmd_Request.PId} " +
                                         $"was not found"));
            }
            // 4th option
            _docSession_I.HardDelete<MProduct>(rDPCmd_Request.PId);
            await _docSession_I.SaveChangesAsync(CToken);
            return GenResult.Success();
        }
    }
}
