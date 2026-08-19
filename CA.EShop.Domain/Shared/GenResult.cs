using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Shared
{
    public class GenResult
    {
        protected internal GenResult(bool isSuccess, GenError gen_error)
        {
            if (isSuccess && gen_error != GenError.None)
            {
                throw new InvalidOperationException();
            }
            if (!isSuccess && gen_error == GenError.None)
            {
                throw new InvalidOperationException();
            }
            IsSuccess = isSuccess;
            genError = gen_error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public GenError genError { get; }

        public static GenResult Success() => new(true, GenError.None);

        public static TResult<TValue> Success<TValue>(TValue value) 
            => new(value, true, GenError.None);

        public static GenResult Failure(GenError error) 
            => new(false, error);

        public static TResult<TValue> Failure<TValue>(GenError error) 
            => new(default, false, error);

        public static GenResult Create(bool condition) 
            => condition ? Success() : Failure(GenError.ConditionNotMet);

        public static TResult<TValue> Create<TValue>(TValue? value) 
            => value is not null ? Success(value) : Failure<TValue>(GenError.NullValue);
    }
}
