using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Net;

namespace Domain.Utils
{
    public class Response
    {
        public int StatusCode { get; }
        public string Message { get; }

        public Response(int statusCode = (int)HttpStatusCode.OK, string message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public static Response Create(int statusCode = (int)HttpStatusCode.OK, string message = null)
        {
            return new Response(statusCode, message);
        }
    }

    public class Response<T> : Response
    {
        public T Body { get; }

        public Response(int statusCode = (int)HttpStatusCode.OK, T body = default, string message = null)
            : base(statusCode, message)
        {
            Body = body;
        }

        public static Response<T> Create(int statusCode = (int)HttpStatusCode.OK, T body = default, string message = null)
        {
            return new Response<T>(statusCode, body, message);
        }
    }
}
