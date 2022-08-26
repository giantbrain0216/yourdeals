#region

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Classes
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public string Enqueue(Expression<Func<Task>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }
    }
}