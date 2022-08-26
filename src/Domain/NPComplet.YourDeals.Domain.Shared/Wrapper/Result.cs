using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Domain.Shared.Wrapper
{
    /// <summary>
    /// 
    /// </summary>
    public class Result : IResult
    {
        /// <summary>
        /// 
        /// </summary>
        public Result()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        public bool Succeeded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IResult Fail()
        {
            return new Result { Succeeded = false };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IResult Fail(string message)
        {
            return new Result { Succeeded = false, Messages = new List<string> { message } };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static IResult Fail(List<string> messages)
        {
            return new Result { Succeeded = false, Messages = messages };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static Task<IResult> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IResult Success()
        {
            return new Result { Succeeded = true };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IResult Success(string message)
        {
            return new Result { Succeeded = true, Messages = new List<string> { message } };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Task<IResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result, IResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public Result()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static Result<T> Fail()
        {
            return new Result<T> { Succeeded = false };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public new static Result<T> Fail(string message)
        {
            return new Result<T> { Succeeded = false, Messages = new List<string> { message } };
        }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="message"></param>
      /// <param name="data"></param>
      /// <returns></returns>
        public new static Result<T> Fail(T data,string message)
        {
            return new Result<T> { Succeeded = false, Messages = new List<string> { message },Data=data };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public new static Result<T> Fail(List<string> messages)
        {
            return new Result<T> { Succeeded = false, Messages = messages };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static Task<Result<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public new static Task<Result<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public new static Task<Result<T>> FailAsync(T data,string message)
        {
            return Task.FromResult(Fail(data,message));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public new static Task<Result<T>> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static Result<T> Success()
        {
            return new Result<T> { Succeeded = true };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public new static Result<T> Success(string message)
        {
            return new Result<T> { Succeeded = true, Messages = new List<string> { message } };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<T> Success(T data, string message)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = new List<string> { message } };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static Result<T> Success(T data, List<string> messages)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = messages };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static Task<Result<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public new static Task<Result<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task<Result<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Task<Result<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }
    }
}