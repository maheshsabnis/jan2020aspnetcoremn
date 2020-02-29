using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using Core_WebApp.Services;
namespace Core_WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CombineController : ControllerBase
    {
        private readonly IRepository<Category, int> cRepo;
        private readonly IRepository<Product, int> pRepo;

        public CombineController(IRepository<Category, int> cRepo, IRepository<Product, int> pRepo)
        {
            this.cRepo = cRepo;
            this.pRepo = pRepo;
        }

        [HttpPost]
        [ActionName("CombineData")]
        public async Task<IActionResult> PostAsync(CatPrd data)
        {
            data.Category = await cRepo.CreateAsync(data.Category);
            foreach (var prd in data.Products)
            {
                prd.CategoryRowId = data.Category.CategoryRowId;
                await pRepo.CreateAsync(prd);
            }
            return Ok("Successful");
        }

        [HttpPost]
        [ActionName("PostQuery")]
       // public async Task<IActionResult> PostQueryAsync(string CategoryId, string CategoryName, int BasePrice)
        public async Task<IActionResult> PostQueryAsync([FromQuery]Category cat)
        {

            cat = await cRepo.CreateAsync(cat);
            
            return Ok("Successful");
        }

        [HttpPost("{CategoryId}/{CategoryName}/{BasePrice}")]
        [ActionName("PostRoute")]
        public async Task<IActionResult> PostRouteAsync([FromRoute]Category cat)
        {

            cat = await cRepo.CreateAsync(cat);

            return Ok("Successful");
        }

        [HttpPost]
        [ActionName("PostForm")]
        public async Task<IActionResult> PostFormAsync([FromForm]Category cat)
        {

            cat = await cRepo.CreateAsync(cat);

            return Ok("Successful");
        }

    }
}