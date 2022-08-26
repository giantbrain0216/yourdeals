using System;
using System.Collections.Generic;

namespace NPComplet.YourDeals.Domain.Shared.Wrapper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedResult<T> : Result
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<T> Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="succeeded"></param>
        /// <param name="data"></param>
        /// <param name="messages"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        internal PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static PaginatedResult<T> Failure(List<string> messages)
        {
            return new PaginatedResult<T>(false, default, messages);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new PaginatedResult<T>(true, data, null, count, page, pageSize);
        }
        /// <summary>
        /// 
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public bool HasPreviousPage => CurrentPage > 1;
        /// <summary>
        /// 
        /// </summary>

        public bool HasNextPage => CurrentPage < TotalPages;
    }
}