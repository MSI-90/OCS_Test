﻿using MyTaskManager.Models;
using NotionTestWork.Models;
using System.Net;
using System.Text.Json;

namespace MyTaskManager.Middleware
{
    internal class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new ExceptionRequest();
            response.ErrorMessage.Add(exception.Message);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
