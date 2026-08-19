using CA.EShop.Application.Abstractions.IMessaging;
using System.Collections.Generic;

namespace CA.EShop.Application.Products_Creation
{
    public sealed record RProdCreateCmd(string PName,
                                        decimal PPrice,
                                        List<string> LTags) : ICommand;
}
