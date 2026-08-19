using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Shared
{
    ///* Normal way */
    //public static class GResultExtensions
    //{
    //    public static TResult<T> Ensure<T>(
    //        this TResult<T> tresult_T, Func<T, bool> FPredict,
    //        GenError genError)
    //    {
    //        if (tresult_T.IsFailure)
    //        {
    //            return tresult_T;
    //        }
    //        if (FPredict(tresult_T.Value))
    //        {
    //            return tresult_T;
    //        }
    //        return GenResult.Failure<T>(genError);
    //    }
    //}

    public static class GResultExtensions
    {
        /* Using ternary conditional operator way */
        public static TResult<T> Ensure<T>(this TResult<T> tResult_T, 
                                           Func<T, bool> FPredict, 
                                           GenError genError)
        {
            if (tResult_T.IsFailure)
            {
                return tResult_T;
            }
            return FPredict(tResult_T.Value) ?
                tResult_T :
                GenResult.Failure<T>(genError);
        }
        
        /* Function to map string result to Email formate result */
        public static TResult<TOut> MapToEmailFormate<TIn,TOut>
            (this TResult<TIn> tResult_TIn,
             Func<TIn,TOut> FMapping)
        {
            return tResult_TIn.IsSuccess ?
                GenResult.Success(FMapping(tResult_TIn.Value)) :
                GenResult.Failure<TOut>(tResult_TIn.genError);
        }
    }
}
