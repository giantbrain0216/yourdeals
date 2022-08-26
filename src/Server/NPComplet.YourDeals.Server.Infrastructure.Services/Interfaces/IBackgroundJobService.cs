#region

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire.Annotations;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces
{
    public interface IBackgroundJobService
    {
        public string Enqueue(
            [InstantHandle] [System.Diagnostics.CodeAnalysis.NotNull] Expression<Func<Task>> methodCall);
    }
}