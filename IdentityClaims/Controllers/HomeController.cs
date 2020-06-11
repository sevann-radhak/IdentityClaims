using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityClaims.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IdentityClaims.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var claims = User.Claims.ToList();
                //var user = await _userManager.GetUserAsync(HttpContext.User);
                //await _userManager.AddClaimAsync(user, new Claim("category", "4"));
                //var claims2 = User.Claims.ToList();

                //var claimAmin = User.Claims.Any(c => );
            }

            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";

            if(User.Identity.IsAuthenticated)
            {
                if(!await _roleManager.RoleExistsAsync("Admin"))
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));

                var user = await _userManager.GetUserAsync(HttpContext.User);



                ////Add to role admin
                //if (!await _userManager.IsInRoleAsync(user, "Admin"))
                //    await _userManager.AddToRoleAsync(user, "Admin");



                ////Remove from role admin
                //if (await _userManager.IsInRoleAsync(user, "Admin"))
                //    await _userManager.RemoveFromRoleAsync(user, "Admin");



                //// Remove claim admin
                //var roleClaim = User.Claims
                //    .FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                //var remove = await _userManager.RemoveClaimAsync(
                //    user, 
                //    roleClaim);



                // Remove claim category
                var categoryClaim = User.Claims
                    .FirstOrDefault(c => c.Type == "category");
                var remove = await _userManager.RemoveClaimAsync(
                    user,
                    categoryClaim);
            }

            return View();
        }

        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "CategoryPolicy")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
