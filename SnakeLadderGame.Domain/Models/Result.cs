using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.Domain.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }
        public string Message { get; set; } = null!;
        private EnumResult Type { get; set; }
        public T? Data { get; set; }

        public bool IsNotFoundError { get { return Type == EnumResult.NotFound; } }
        public bool IsNormalError { get { return Type == EnumResult.Error; } }
        public bool IsValidationError { get { return Type == EnumResult.ValidationError; } }
        public bool IsSErverError { get { return Type == EnumResult.ServerError; } }

        public static Result<T> Success( string message, T? data)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Type = EnumResult.Success,
                Data = data,
                Message = message
            };
        }

        public static Result<T> Error(string message, T? data = default)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumResult.Error,
                Message = message,
                Data = data
            };
        }

        public static Result<T> NotFound(string message, T? data = default)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumResult.NotFound,
                Message = message,
                Data = data
            };
        }

        public static Result<T> ValidationErr(string message, T? data = default)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumResult.ValidationError,
                Message = message,
                Data = data
            };
        }

        public static Result<T> ServerError(string message, T? data = default)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumResult.ServerError,
                Message = message,
                Data = data
            };
        }
    }
}


public enum EnumResult
{
    None,
    Success,
    Error,
    NotFound,
    ValidationError,
    ServerError
}