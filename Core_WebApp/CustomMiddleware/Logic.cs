using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp.CustomMiddleware
{
	// a class that will define schema for Error Response
	public class ErrorInformation
	{
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}

	// a class that will contain logic for middleware
	// since this class will auto invoked in Http Context
	// inject the RequetDelegate as ctor injection
	// the class must have a method named Invoke() / InvokeAsync()
	// this will accpet HttpContext as inout parameter. This method
	// will be auto-0invoked by RequestDelegate. This method will contain logic for
	// Middleware
	public class ErrorMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				// if no exception occure in Http Request then
				// proceed to next middleware
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleException(context, ex);
			}
		}

		// helper method for exception logic and response
		private async Task HandleException(HttpContext ctx, Exception ex)
		{
			// set the error response
			ctx.Response.StatusCode = 500;

			// set the error Information
			var errorInfo = new ErrorInformation()
			{
				ErrorCode = ctx.Response.StatusCode,
				ErrorMessage = ex.Message
			};

			// serialize the object in  JSON

			string errorResponse = JsonConvert.SerializeObject(errorInfo);

			// write the response
			await ctx.Response.WriteAsync(errorResponse);
		}
	}

	// write an exetnsion class that will be used to register the ErrorMiddlweare
	// as ASP.NET Core Middlerware in Http Pipeline
	public static class MyMiddleware
	{
		public static void UseCustomErrorMiddleware(this IApplicationBuilder app)
		{
			// using 'UseMiddleware<T>()' method class of IApplicationBuilder 
			// register the custom muiddleware
			// T is a class that is ctor injected by RequestDelegate and have Invoke() / InvokeAsync()
			// method
			app.UseMiddleware<ErrorMiddleware>();

		}
	}
}
