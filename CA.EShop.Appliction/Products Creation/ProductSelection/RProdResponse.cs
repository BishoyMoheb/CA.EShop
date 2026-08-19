using System.Collections.Generic;

namespace CA.EShop.Application.Products_Creation.ProductSelection
{
    public sealed record RProdResponse(long PId,
                                        string PName,
                                        decimal PPrice,
                                        List<string> LTags);
}
