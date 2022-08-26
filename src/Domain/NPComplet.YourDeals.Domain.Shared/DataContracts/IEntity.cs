namespace NPComplet.YourDeals.Domain.Shared.DataContracts
{
    /// <summary>
/// 
/// </summary>
/// <typeparam name="TId"></typeparam>
    public interface IEntity<TId> : IEntity
    {
        public TId Id { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IEntity
    {
    }
}