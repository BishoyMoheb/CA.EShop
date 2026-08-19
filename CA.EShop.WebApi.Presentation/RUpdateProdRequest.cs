using System.Collections.Generic;

namespace CA.EShop.WebApi.Presentation
{
    public sealed record RUpdateProdRequest(string PName,
                                                   decimal PPrice,
                                                   List<string> LTags);
}
