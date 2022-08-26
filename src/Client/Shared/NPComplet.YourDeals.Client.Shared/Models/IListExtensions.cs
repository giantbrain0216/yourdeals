// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IListExtensions.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Client.Shared.Models
{
    #region

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The i list extensions.
    /// </summary>
    public static class IListExtensions
    {
        // Basically a Polyfill since we now expose IList instead of List
        // which is better but IList doesn't have AddRange
        /// <summary>
        /// The add range.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (list is List<T> asList) asList.AddRange(items);
            else
                foreach (var item in items)
                    list.Add(item);
        }
    }
}