using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA.EShop.Application.Abstractions.IMessaging;

namespace CA.EShop.Application.Products_Creation.ProductDeleting
{
    public sealed record RDeletingProdCmd(long PId) : ICommand;
}
