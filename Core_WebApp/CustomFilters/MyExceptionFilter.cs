using Core_WebApp.Models;
using Microsoft.AspNetCore.Hosting;
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
		private readonly ITempDataDictionaryFactory tempDataDictionaryFactory;
		private readonly IHostingEnvironment hostingEnvironment;
		/// <summary>
		/// The IModelMetadataProvider will provide metadata of any dynamic
		/// models used in the current HttpRequest
		/// </summary>
		/// <param name="metadataProvider"></param>
		public MyExceptionFilter(IModelMetadataProvider metadataProvider, 
			ITempDataDictionaryFactory tempDataDictionaryFactory,
			IHostingEnvironment hostingEnvironment)
		{
			this.metadataProvider = metadataProvider;
			this.tempDataDictionaryFactory = tempDataDictionaryFactory;
			this.hostingEnvironment = hostingEnvironment;
		}

		/// <summary>
		/// Method for handling Exception. This method should Handle exception and complete request
		/// </summary>
		/// <param name="context"></param>
		public override void OnException(ExceptionContext context)
		{
			var tempData = tempDataDictionaryFactory.GetTempData(context.HttpContext);
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
			result.TempData = tempData;
			// ViewName
			result.ViewName = "CustomError";
			// ViewData
			result.ViewData = ViewData;
		//	result.TempData.Keep();
			// setting result in HttpResponse
			context.Result = result;
			
		}
	}
}
