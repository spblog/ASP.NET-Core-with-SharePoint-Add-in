using System.Diagnostics;
using AddinWebApp.Filters;
using AddinWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddinWebApp.Controllers
{
	[SharePointContextFilter]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

			using (var clientContext = spContext.CreateUserClientContextForSPHost())
			{
				if (clientContext != null)
				{
					var spUser = clientContext.Web.CurrentUser;

					clientContext.Load(spUser, user => user.Title);

					clientContext.ExecuteQuery();

					ViewBag.UserName = spUser.Title;
				}
			}

			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
