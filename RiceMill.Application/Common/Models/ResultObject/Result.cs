﻿using System.Net;

namespace RiceMill.Application.Common.Models.ResultObject
{
    public class Result<T>
    {
        public bool IsSucceeded { get; set; }

        public List<Error> Errors { get; set; }

        public T Data { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public static Result<T> Failure(Error error, HttpStatusCode httpStatusCode) => new() { Errors = new List<Error> { error }, HttpStatusCode = httpStatusCode };

        public static Result<T> Failure(List<Error> errors, HttpStatusCode httpStatusCode) => new() { Errors = errors, HttpStatusCode = httpStatusCode };

        public static Result<T> Success(T data) => new() { Data = data, IsSucceeded = true, Errors = Array.Empty<Error>().ToList(), HttpStatusCode = HttpStatusCode.OK };
    }
}