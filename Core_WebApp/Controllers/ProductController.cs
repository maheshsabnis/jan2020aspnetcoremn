using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using Core_WebApp.Services;

namespace Core_WebApp.Controllers
{
    /// <summary>
    /// DI the Repository Classes
    /// Controller: Base class for MVC Controllers that has following
    /// IActionFilter, IFilterMetadata, IAsyncActionFilter,  the Action FIlter Interfaces
    /// IDisposable, tyhe Memeory management Interface 
    /// All Action method of Controller class are HttpGet By Default 
    /// 
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IRepository<Product, int> repository;
        public ProductController(IRepository<Product, int> repository)
        {
            this.repository = repository;
        }
        public async  Task<IActionResult> Index()
        {
            var res = await repository.GetAsync();
            return View(res); // return the Index View
        }

        public IActionResult Create()
        {
            return View(new Product()); // return the create view
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product Product)
        {
            // check for the validation
            if (ModelState.IsValid)
            {
                var res = await repository.CreateAsync(Product);
                return RedirectToAction("Index"); // return to the Index View
            }
            return View(Product); // stey on create view with validation error messages
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await repository.GetAsync(id);
            return View(res); // return view with data to be edited
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Product Product)
        {
            // check for the validation
            if (ModelState.IsValid)
            {
                var res = await repository.UpdateAsync(id,Product);
                return RedirectToAction("Index"); // return to the Index View
            }
            return View(Product); // stey on edit view with validation error messages
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.DeleteAsync(id);
            if (res) // delete successful
            {
                return RedirectToAction("Index"); // return to the Index View
            }
            return RedirectToAction("Index"); // return to the Index View
        }
    }
}