using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Shared
{
    public interface IValidationResult
    {
        public static readonly GenError ValidationError = new(
                "ValidationError",
                "A validation problem occurred.");

        GenError[] Arr_GErrors { get; }
    }
}
