using AutoMapper;
using MC_Projekt_wypozyczalni.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Core.Types;
using System.Data;
using static System.Formats.Asn1.AsnWriter;

namespace MC_Projekt_wypozyczalni.Areas.Admin.Controllers
{
   
    [Authorize(Roles = "Admin")]
    public class RoleManagerController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RoleManagerController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        // GET: RoleManagerController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(string name)
        {
            TempData["roleName"] = name;

            return View();
        }

        public async Task<ActionResult> AddUser(string name, string role, string loggedUser)
        {
            var user = await _userManager.FindByNameAsync(name);

            await _userManager.AddToRoleAsync(user, role);

            if (name == loggedUser)
                await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(string name, string role, string loggedUser)
        {
            var user = await _userManager.FindByNameAsync(name);

            await _userManager.RemoveFromRoleAsync(user, role);

            if (name == loggedUser)
                await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
