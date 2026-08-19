using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Abstractions
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        /// <summary>
        /// Gets the atomic values of the value object.
        /// </summary>
        /// <returns>The collection of objects representing the value object values.</returns>
        public abstract IEnumerable<object> Get_AtomicValues();

        public static bool operator ==(ValueObject? VObj_First, ValueObject? VObj_Second)
        {
            if (VObj_First is null && VObj_Second is null)
            {
                return true;
            }

            if (VObj_First is null || VObj_Second is null)
            {
                return false;
            }

            return VObj_First.Equals(VObj_Second);
        }

        public static bool operator !=(ValueObject? VObj_First, ValueObject? VObj_Second) 
            => !(VObj_First == VObj_Second);

        /// <inheritdoc />
        public bool Equals(ValueObject? VObj_Other)
        {
            return VObj_Other is not null && Check_ValuesEquality(VObj_Other);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            if (obj is not ValueObject VOther)
            {
                return false;
            }
            return Check_ValuesEquality(VOther);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Get_AtomicValues()
                   .Aggregate(
                        default(int),
                        (HCode, value) => HashCode.Combine(HCode, value.GetHashCode()));
        }

        /// <summary>
        /// Checks if the values of the specified value object are equal to the values of the current instance.
        /// </summary>
        /// <param name="other">The other value object.</param>
        /// <returns>True if the values of the specified value object are equal to the values of the current instance, 
        /// otherwise false.</returns>
        private bool Check_ValuesEquality(ValueObject VOthers)
        {
            return Get_AtomicValues().SequenceEqual(VOthers.Get_AtomicValues());
        }
    }
}
