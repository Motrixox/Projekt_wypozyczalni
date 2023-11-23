using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MC_Projekt_wypozyczalni.Areas.Users.Controllers
{
	[Authorize(Roles = "User")]
	public class UserController : Controller
	{
		[Route("user/")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
