using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Abstractions
{
    public sealed class Enum_NotFound_Exception : ArgumentOutOfRangeException
    {
        public Enum_NotFound_Exception(string EName, int EValue)
            : base($"The {EName} with the value {EValue} was not found.")
        {
        }
    }
}
