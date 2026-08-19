using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Shared
{
    public class GenError : IEquatable<GenError>
    {
        public static readonly GenError None = new(string.Empty, string.Empty);
        public static readonly GenError NullValue = new("GenError.NullValue", "The specified result value is null.");
        public static readonly GenError ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.");

        public GenError(string error_code, string error_message)
        {
            Error_Code = error_code;
            Error_Message = error_message;
        }

        public string Error_Code { get; }

        public string Error_Message { get; }

        public static implicit operator string(GenError error) 
            => error.Error_Code;

        public static bool operator ==(GenError? Error_A, GenError? Error_B)
        {
            if (Error_A is null && Error_B is null)
            {
                return true;
            }
            if (Error_A is null || Error_B is null)
            {
                return false;
            }
            return Error_A.Equals(Error_B);
        }

        public static bool operator !=(GenError? Error_A, GenError? Error_B) 
            => !(Error_A == Error_B);

        /// <inheritdoc />
        public virtual bool Equals(GenError? other)
        {
            if (other is null)
            {
                return false;
            }
            return Error_Code == other.Error_Code && Error_Message == other.Error_Message;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) 
            => obj is GenError error && Equals(error);

        /// <inheritdoc />
        public override int GetHashCode() 
            => HashCode.Combine(Error_Code, Error_Message);

        /// <inheritdoc />
        public override string ToString() 
            => Error_Code;
    }
}
