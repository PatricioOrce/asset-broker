using System;
using System.Collections.Generic;
using System.Net;

namespace Domain.Utils
{
    public class Response
    {
        public int StatusCode { get; }
        public string Message { get; }
        public IReadOnlyList<string> Errors { get; }

        public Response(int statusCode = (int)HttpStatusCode.OK, string message = null, IEnumerable<string> errors = null)
        {
            StatusCode = statusCode;
            Message = message;
            Errors = errors != null ? new List<string>(errors) : Array.Empty<string>();
        }

        public static Response Success(string message = null)
        {
            return new Response((int)HttpStatusCode.OK, message);
        }

        public static Response Error(int statusCode, IEnumerable<string> errors)
        {
            return new Response(statusCode, errors: errors);
        }

        public static Response Create(int statusCode = (int)HttpStatusCode.OK, string message = null, IEnumerable<string> errors = null)
        {
            return new Response(statusCode, message, errors);
        }
    }

    public class Response<T> : Response
    {
        public T Body { get; }

        public Response(int statusCode = (int)HttpStatusCode.OK, T body = default, string message = null, IEnumerable<string> errors = null)
            : base(statusCode, message, errors)
        {
            Body = body;
        }

        public static Response<T> Success(T body, string message = null)
        {
            return new Response<T>((int)HttpStatusCode.OK, body, message);
        }

        public static Response<T> Error(int statusCode, T body, IEnumerable<string> errors)
        {
            return new Response<T>(statusCode, body, errors: errors);
        }

        public static Response<T> Create(int statusCode = (int)HttpStatusCode.OK, T body = default, string message = null, IEnumerable<string> errors = null)
        {
            return new Response<T>(statusCode, body, message, errors);
        }
    }
}
