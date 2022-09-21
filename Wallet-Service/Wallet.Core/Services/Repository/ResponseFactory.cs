using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Wallet.Dtos;

namespace Wallet.Core.Repository
{
     public class ResponseFactory : IResponseFactory
    {
        public ExecutionResponse<T> ExecutionResponse<T>(string message, T data = null, bool status = false, int statusCode = StatusCodes.Status200OK) where T : class
        {

            return new ExecutionResponse<T>
            {
                Status = status,
                Message = message,
                Data = data,
                StatusCode = statusCode
            };
        }

        public PagedExecutionResponse<T> PagedExecutionResponse<T>(string message, T data = null, long totalRecord = 0, bool status = false, int statusCode = StatusCodes.Status200OK) where T : class
        {
            return new PagedExecutionResponse<T>
            {
                Status = status,
                Message = message,
                Data = data,
                TotalRecords = totalRecord,
                StatusCode = statusCode
            };
        }

        public ExecutionResponse<IEnumerable<T>> ExecutionResponseList<T>(string message, IEnumerable<T> data = null, bool status = false, int statusCode = StatusCodes.Status200OK) where T : class
        {
            return new ExecutionResponse<IEnumerable<T>>
            {
                Status = status,
                Message = message,
                Data = data,
                StatusCode = statusCode
            };
        }

        public object IExecutionResponseObject(string message, object data = null, bool status = false, int statusCode = StatusCodes.Status200OK)
        {
            return new
            {
                Status = status,
                Message = message,
                Data = data,
                StatusCode = statusCode
            };
        }


    }
}
