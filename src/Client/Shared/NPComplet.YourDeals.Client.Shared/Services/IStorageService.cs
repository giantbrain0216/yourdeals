using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Shared.Services
{
    public interface IStorageService
    {
        /// <summary>
        /// <para>
        /// Asynchronously stores the specified data.
        /// </para>
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use.</param>
        /// <param name="value">A JSON-serializable value to be stored.</param>
        /// <returns>A <see cref="Task"/> representing the completion of the operation.</returns>
        public Task SetAsync(string key, object value);

        /// <summary>
        /// <para>
        /// Asynchronously retrieves the specified data.
        /// </para>
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use.</param>
        /// <returns>A <see cref="Task"/> representing the completion of the operation. The result is the data.</returns>
        public Task<TValue> GetAsync<TValue>(string key);

        /// <summary>
        /// Asynchronously deletes any data stored for the specified key.
        /// </summary>
        /// <param name="key">
        /// A <see cref="string"/> value specifying the name of the storage slot whose value should be deleted.
        /// </param>
        /// <returns>A <see cref="Task"/> representing the completion of the operation.</returns>
        public Task<bool> DeleteAsync(string key);
    }
}
