using System.Collections.Generic;
using CA.EShop.Application.Abstractions.IMessaging;

namespace CA.EShop.Application.Products_Creation.ProductUpdating
{
    public sealed record RUpdateProdCmd(long PId,
                                        string PName,
                                        decimal PPrice,
                                        List<string> LTags) : ICommand;
}
