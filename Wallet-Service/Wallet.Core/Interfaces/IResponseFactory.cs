using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Wallet.Dtos;

namespace Wallet.Core.Repository
{
     public interface IResponseFactory
    {
        ExecutionResponse<T> ExecutionResponse<T>(string message, T data = null, bool status = false, int statusCode = StatusCodes.Status200OK) where T : class;
        ExecutionResponse<IEnumerable<T>> ExecutionResponseList<T>(string message, IEnumerable<T> data = null, bool status = false, int statusCode = StatusCodes.Status200OK) where T : class;
        object IExecutionResponseObject(string message, object data = null, bool status = false, int statusCode = StatusCodes.Status200OK);
        //IPagedExecutionResponse<IEnumerable<T>> PagedExecutionResponse<T>(string message, IEnumerable<T> data = null, long totalRecord = 0, bool status = false) where T : class;
        PagedExecutionResponse<T> PagedExecutionResponse<T>(string message, T data = null, long totalRecord = 0, bool status = false, int statusCode = StatusCodes.Status200OK) where T : class;
    }
}
