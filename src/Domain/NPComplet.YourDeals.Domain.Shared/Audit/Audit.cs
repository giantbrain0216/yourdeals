using NPComplet.YourDeals.Domain.Shared.DataContracts;
using System;


namespace NPComplet.YourDeals.Domain.Shared.Audit
{
    /// <summary>
    /// 
    /// </summary>
    public class Audit : IEntity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OldValues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NewValues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AffectedColumns { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PrimaryKey { get; set; }
    }
}