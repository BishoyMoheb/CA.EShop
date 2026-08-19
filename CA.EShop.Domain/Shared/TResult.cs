using System;

namespace CA.EShop.Domain.Shared
{
    public class TResult<TValue> : GenResult
    {
        private readonly TValue? _value;

        protected internal TResult(TValue? value, bool isSuccess, GenError gen_error)
            : base(isSuccess, gen_error) 
            => _value = value;

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        public static implicit operator TResult<TValue>(TValue? value) => Create(value);
    }
}