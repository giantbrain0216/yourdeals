// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Helper.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects
{
    #region

    using System;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///     The shared helper.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Convert string to enumeration.
        /// </summary>
        /// <param name="str">
        /// the string to convert
        /// </param>
        /// <returns>
        /// The converted enum value.
        /// </returns>
        public static T ToEnum<T>(string str)
        {
            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute =
                    ((EnumMemberAttribute[])enumType.GetField(name)
                            .GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (enumMemberAttribute.Value == str) return (T)Enum.Parse(enumType, name);
            }

            // throw exception or whatever handling you want or
            return default;
        }

        /// <summary>
        /// Convert enumeration to string
        /// </summary>
        /// <typeparam name="T">
        /// The enum type.
        /// </typeparam>
        /// <param name="type">
        /// The enum value.
        /// </param>
        /// <returns>
        /// Converted enum to string.
        /// </returns>
        public static string ToEnumString<T>(T type)
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute =
                ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true))
                .Single();
            return enumMemberAttribute.Value;
        }

        /// <summary>
        /// Convert Enum to (key, EnumMemberAttributeValue)
        /// </summary>
        /// <typeparam name="T">The enum</typeparam>
        /// <returns>Dictionary of (key, EnumMemberAttributeValue).</returns>
        public static Dictionary<T, string> EnumToDict<T>() where T : struct
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(k => k, ToEnumString);
        }
    }
}