using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CA.EShop.Application.Abstractions.IMessaging;
using CA.EShop.Domain.Products;
using CA.EShop.Domain.Shared;
using Marten;

namespace CA.EShop.Application.Products_Creation.ProductSelection
{
    internal sealed class QGetProdHandler
        : IQueryHandler<RQueryGetProduct, List<RProdResponse>>
    {
        private readonly IQuerySession _querySessionI;

        public QGetProdHandler(IQuerySession QuerySessionI)
        {
            _querySessionI = QuerySessionI;
        }

        public async Task<TResult<List<RProdResponse>>> Handle(RQueryGetProduct rQGProd_Request, CancellationToken CToken)
        {
            IReadOnlyList<RProdResponse> ROList_I_rPResponses
                = await _querySessionI.Query<MProduct>()
                                      .Select(p => new RProdResponse(p.ProdID,
                                                                     p.ProdName,
                                                                     p.ProdPrice,
                                                                     p.LStr_Tags))
                                      .ToListAsync(CToken);
            return ROList_I_rPResponses.ToList();
        }
    }
}
