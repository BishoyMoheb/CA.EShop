using System.Threading;
using System.Threading.Tasks;
using CA.EShop.Application.Abstractions.IMessaging;
using CA.EShop.Domain.Products;
using CA.EShop.Domain.Shared;
using Marten;

namespace CA.EShop.Application.Products_Creation.ProductUpdating
{
    internal class UProdCmdHandler : ICommandHandler<RUpdateProdCmd>
    {
        private readonly IDocumentSession _docSession_I;

        public UProdCmdHandler(IDocumentStore DocStore_I)
        {
            _docSession_I = DocStore_I.DirtyTrackedSession();
        }

        public async Task<GenResult> Handle(RUpdateProdCmd rUPCmd_Request, CancellationToken CToken)
        {
            MProduct? mProductToUpdate = await _docSession_I
                               .LoadAsync<MProduct>(rUPCmd_Request.PId, CToken);
            if (mProductToUpdate is null)
            {
                return GenResult.Failure(new GenError(
                                         "Product.NotFound",
                                         $"The product Id {rUPCmd_Request.PId} " +
                                         $"was not found"));
            }
            mProductToUpdate.ProdName = rUPCmd_Request.PName;
            mProductToUpdate.ProdPrice = rUPCmd_Request.PPrice;
            mProductToUpdate.LStr_Tags = rUPCmd_Request.LTags;
            await _docSession_I.SaveChangesAsync(CToken);
            return GenResult.Success();
        }
    }
}
