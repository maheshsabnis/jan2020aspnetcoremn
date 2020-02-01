using Core_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp.CustomFilters
{
	public class MyExceptionFilter: ExceptionFilterAttribute
	{
		private readonly IModelMetadataProvider metadataProvider;
		private readonly AppDbContext ctx;
		/// <summary>
		/// The IModelMetadataProvider will provide metadata of any dynamic
		/// models used in the current HttpRequest
		/// </summary>
		/// <param name="metadataProvider"></param>
		public MyExceptionFilter(IModelMetadataProvider metadataProvider, AppDbContext ctx)
		{
			this.metadataProvider = metadataProvider;
			this.ctx = ctx;
		}

		/// <summary>
		/// Method for handling Exception. This method should Handle exception and complete request
		/// </summary>
		/// <param name="context"></param>
		public override void OnException(ExceptionContext context)
		{
			// read exception message
			string message = context.Exception.Message;
			// handle Exception
			context.ExceptionHandled = true;
			// go to view to display error messages
			var result = new ViewResult();
			// defining VeiwDataDictionary for Controller/action/errormessage
			var ViewData = new ViewDataDictionary(metadataProvider, context.ModelState);
			ViewData["controller"] = context.RouteData.Values["controller"].ToString();
			ViewData["action"] = context.RouteData.Values["action"].ToString();
			ViewData["errormessage"] = message;
			// ViewName
			result.ViewName = "CustomError";
			// ViewData
			result.ViewData = ViewData;
			// setting result in HttpResponse
			context.Result = result;
			
		}
	}
}
