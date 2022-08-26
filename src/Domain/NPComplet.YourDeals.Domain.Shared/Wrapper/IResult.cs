using System.Collections.Generic;

namespace NPComplet.YourDeals.Domain.Shared.Wrapper
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> Messages { get; set; }

        bool Succeeded { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// 
        /// </summary>
        T Data { get; }
    }
}