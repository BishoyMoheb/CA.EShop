using System.Collections.Generic;

namespace CA.EShop.WebApi.Presentation
{
    public sealed record RCreateProdRequest(string PName,
                                                   decimal PPrice,
                                                   List<string> LTags);
}
