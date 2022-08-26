// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringHelper.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Utils
{
    #region

    using System;
    using System.Linq;

    #endregion

    /// <summary>
    /// The string helper.
    /// </summary>
    public static class StringHelper
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// The get random string.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}