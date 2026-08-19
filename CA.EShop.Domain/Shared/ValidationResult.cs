using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Shared
{
    ///* Old way of writting */
    //public sealed class ValidationResult : GenResult, IValidationResult
    //{
    //    private ValidationResult(GenError[] arr_GErrors)
    //        : base(false, IValidationResult.ValidationError)
    //    {
    //        Arr_GErrors = arr_GErrors;
    //    }

    //    public GenError[] Arr_GErrors { get; }

    //    public static ValidationResult WithErrors(GenError[] Arr_GErrors)
    //    {
    //        return new(Arr_GErrors);
    //    }
    //}

    /* Milan way of writting */
    public sealed class ValidationResult : GenResult, IValidationResult
    {
        private ValidationResult(GenError[] arr_GErrors)
            : base(false, IValidationResult.ValidationError) 
            => Arr_GErrors = arr_GErrors;

        public GenError[] Arr_GErrors { get; }

        public static ValidationResult WithErrors(GenError[] Arr_GErrors)
            => new(Arr_GErrors);
    }
}
