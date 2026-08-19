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
            // 2nd option
            _docSession_I.DeleteWhere<MProduct>(p => p.ProdID == rDPCmd_Request.PId);
            await _docSession_I.SaveChangesAsync(CToken);
            return GenResult.Success();
        }
    }
}
