// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapLogic.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Client.Shared.Logic
{
    #region

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using LiteDB;

    using Microsoft.MobileBlazorBindings.ProtectedStorage;

    using NPComplet.YourDeals.Client.Shared.Helpers;
    using NPComplet.YourDeals.Client.Shared.Logic.Interfaces;
    using NPComplet.YourDeals.Domain.Shared.Deal;

    #endregion

    /// <summary>
    /// The map logic.
    /// </summary>
    public class MapLogic : IMapLogic, IDisposable
    {
        private readonly HttpClient httpClient;

        private readonly IProtectedStorage protectedStorage;

        private ILiteCollection<Deal> deals;

        private Task initTask;

        private LiteDatabase liteDatabase;

        private ILiteCollection<Deal> localEdits;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapLogic"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="protectedStorage">
        /// The protected storage.
        /// </param>
        public MapLogic(HttpClient httpClient, IProtectedStorage protectedStorage)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.protectedStorage = protectedStorage ?? throw new ArgumentNullException(nameof(protectedStorage));
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            if (this.liteDatabase != null)
            {
                this.liteDatabase.Dispose();
                this.liteDatabase = null;
            }
        }

        /// <summary>
        /// The fetch changes async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task FetchChangesAsync()
        {
            await this.EnsureLiteDb();
            var syncDate = DateTime.Now;
            var mostRecentlyUpdated =
                this.deals.Query().OrderByDescending(x => x.ModificationDateTimeUtc).FirstOrDefault();
            var since = mostRecentlyUpdated?.ModificationDateTimeUtc ?? DateTime.MinValue;

            // trick to leave timezone info behind.
            since = new DateTime(since.Ticks, DateTimeKind.Unspecified);
            var deals = await this.httpClient.GetFromJsonAsync<Deal[]>(Config.GetAllOffers);
            foreach (var deal in deals) this.deals.Upsert(deal.Id, deal);
            await this.protectedStorage.SetAsync("last_update_date", syncDate);
        }

        /// <summary>
        /// The get last update date async.
        /// </summary>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public async ValueTask<DateTime?> GetLastUpdateDateAsync()
        {
            return await this.protectedStorage.GetAsync<DateTime?>("last_update_date");
        }

        /// <summary>
        /// The get nearst deal.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IList<Deal>> GetNearstDeal()
        {
            await this.EnsureLiteDb();

            return await Task.Run(
                       () =>
                           {
                               var result = this.localEdits.Query().ToList();

                               if (result != null) return result;

                               return this.deals.Query().Where(p => p.Id != null).ToList();
                           });
        }

        /// <summary>
        /// The get outstanding local edits async.
        /// </summary>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public async ValueTask<Deal[]> GetOutstandingLocalEditsAsync()
        {
            await this.EnsureLiteDb();
            return await Task.Run(() => this.localEdits.Query().ToArray());
        }

        private async Task EnsureLiteDb()
        {
            if (this.liteDatabase != null)
            {
                await this.FetchChangesAsync();
                return;
            }

            void InitTask()
            {
                var dbFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    Assembly.GetExecutingAssembly().GetName().Name);

                if (!Directory.Exists(dbFolder)) Directory.CreateDirectory(dbFolder);

                var dbLocation = Path.Combine(dbFolder, "lite.db");
                this.liteDatabase = new LiteDatabase(dbLocation);

                this.deals = this.liteDatabase.GetCollection<Deal>("deals");

                this.deals.EnsureIndex(x => x.Id);

                this.localEdits = this.liteDatabase.GetCollection<Deal>("localEdits");

                this.localEdits.EnsureIndex(x => x.Id);
                this.localEdits.EnsureIndex(x => x.ModificationDateTimeUtc);
            }

            await this.FetchChangesAsync();
            Task task = null;

            if ((task = Interlocked.CompareExchange(ref this.initTask, new Task(InitTask), null)) == null)
            {
                task = this.initTask;
                task.Start(TaskScheduler.Default);
            }

            await task;
        }
    }
}