using CA.EShop.Application.Abstractions.IMessaging;
using CA.EShop.Domain.Products;
using CA.EShop.Domain.Shared;
using Marten;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CA.EShop.Application.Products_Creation
{
    internal sealed class ProdCCmdHandler : ICommandHandler<RProdCreateCmd>
    {
        private readonly IDocumentSession _docSessionI;

        public ProdCCmdHandler(IDocumentSession DocSessionI)
        {
            _docSessionI = DocSessionI;
        }

        public async Task<GenResult> Handle(RProdCreateCmd rPCCRequest, CancellationToken CToken)
        {
            var ProductToGet = new MProduct
            {
                ProdName = rPCCRequest.PName,
                ProdPrice = rPCCRequest.PPrice,
                LStr_Tags = rPCCRequest.LTags
            };
            _docSessionI.Store(ProductToGet);
            await _docSessionI.SaveChangesAsync(CToken);
            return GenResult.Success();
        }
    }
}
