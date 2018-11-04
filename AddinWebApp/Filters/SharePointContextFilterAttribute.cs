using System;
using System.Diagnostics;
using AddinWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AddinWebApp.Filters
{
	public class SharePointContextFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException(nameof(filterContext));
			}

			switch (SharePointContextProvider.CheckRedirectionStatus(filterContext.HttpContext, out var redirectUrl))
			{
				case RedirectionStatus.Ok:
					return;
				case RedirectionStatus.ShouldRedirect:
					filterContext.Result = new RedirectResult(redirectUrl.AbsoluteUri);
					break;
				case RedirectionStatus.CanNotRedirect:
					filterContext.Result = new ViewResult
						{
							ViewName = "Error",
							ViewData = new ViewDataDictionary((filterContext.Controller as Controller)?.ViewData)
							{
								Model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? filterContext.HttpContext.TraceIdentifier }

							}
						};
					break;
			}
		}
	}
}
