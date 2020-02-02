using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core_WebApp.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }


        public IActionResult Create()
        {
            var role = new IdentityRole();
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            var res  =await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}