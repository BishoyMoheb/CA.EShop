using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CA.EShop.Domain.Abstractions
{
    public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>, IComparable<Enumeration<TEnum>>
        where TEnum : Enumeration<TEnum>
    {
        private static readonly string EnumerationName = typeof(TEnum).Name;
        private static readonly Lazy<Dictionary<int, TEnum>> LDic_Enumerations
            = new(() => Get_AllEnumeration_Options().ToDictionary(dItem => dItem.E_Value));

        protected Enumeration(int EValue, string EName)
        {
            E_Value = EValue;
            E_Name = EName;
        }

        protected Enumeration()
        {
            E_Value = default;
            E_Name = string.Empty;
        }

        public static IReadOnlyCollection<TEnum> ROCol_I_TEnum 
            => LDic_Enumerations.Value.Values.ToList().AsReadOnly();

        public int E_Value { get; private set; }

        public string E_Name { get; private set; }

        public static bool operator ==(Enumeration<TEnum> Enum_A, Enumeration<TEnum> Enum_B)
        {
            if (Enum_A is null && Enum_B is null)
            {
                return true;
            }
            if (Enum_A is null || Enum_B is null)
            {
                return false;
            }
            return Enum_A.Equals(Enum_B);
        }

        public static bool operator !=(Enumeration<TEnum> Enum_A, Enumeration<TEnum> Enum_B) 
            => !(Enum_A == Enum_B);

        public static TEnum? FromEValue(int e_value) 
            => LDic_Enumerations.Value.TryGetValue(e_value, out TEnum? enumeration) 
            ? enumeration 
            : throw new Enum_NotFound_Exception(EnumerationName, e_value);

        public static bool ContainsEValue(int evalue) 
            => LDic_Enumerations.Value.ContainsKey(evalue);

        public bool Equals(Enumeration<TEnum>? E_TEnum_Other)
        {
            if (E_TEnum_Other is null)
            {
                return false;
            }
            return GetType() == E_TEnum_Other.GetType() && E_TEnum_Other.E_Value.Equals(E_Value);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration<TEnum> otherValue)
            {
                return false;
            }
            return GetType()==obj.GetType() && otherValue.E_Value.Equals(E_Value);
        }

        public int CompareTo(Enumeration<TEnum>? otherValue)
            => otherValue is null ? 1 : E_Value.CompareTo(otherValue.E_Value);

        /// <inheritdoc />
        public override int GetHashCode() => E_Value.GetHashCode() * 37;

        private static IEnumerable<TEnum> Get_AllEnumeration_Options()
        {
            Type enumType = typeof(TEnum);
            IEnumerable<Type> enumerationTypes = Assembly
                .GetAssembly(enumType)!
                .GetTypes()
                .Where(type => enumType.IsAssignableFrom(type));
            var LEnumerations = new List<TEnum>();
            foreach (Type eType in enumerationTypes)
            {
                List<TEnum> L_Enumeration_TOptions = Get_FieldsOfType<TEnum>(eType);
                LEnumerations.AddRange(L_Enumeration_TOptions);
            }
            return LEnumerations;
        }

        private static List<TFieldType> Get_FieldsOfType<TFieldType>(Type enumType) 
            => enumType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fieldInfo => enumType.IsAssignableFrom(fieldInfo.FieldType))
                .Select(fieldInfo => (TFieldType)fieldInfo.GetValue(null)!)
                .ToList();
    }
}
