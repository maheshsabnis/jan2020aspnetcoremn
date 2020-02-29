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
    [Route("api/[controller]")]
    [ApiController] // <-- attribute that is used to map the POSTED form data from Body to CLR object 
    public class CategoryAPIController : ControllerBase
    {
        // ControllerBase: Base class that manages WEB API Request processing for
        // 1. mapping Http methods to Http request type using HTTP Method attributes
        // HttpGetAttribute --> Map with Http Get Request from client
        // HttpPostAttribute --> Map with Http Post Request from client
        // HttpPutAttribute --> Map with Http Put Request from client
        // HttpDeleteAttribute --> Map with Delete Get Request from client
        // 2. Manages Http Responses using ObjectResult -> OkResult --> Ok()
        // NotFound(), BadRequest(), etc.
        // Mapping Request parameters to methods using Parameter Binding to CLR Objects
        // FromBody --> Request Post data through Body
        // FromQuery --> Request Post data through QueryString
        // FromForm --> Request Post data through Form Model aka FormData
        // FormRoute --> Request Post data through Route Parameters

        private readonly IRepository<Category, int> _catRepository;
        public CategoryAPIController(IRepository<Category,int> catRepository)
        {
            _catRepository = catRepository;
        }

        [HttpGet] // --> map with Http Get Request from client application
        public async Task<IActionResult> GetAsync()
        {
            var cats = await _catRepository.GetAsync();
            return Ok(cats); // status code as 200 with data in Http Response
        }

        [HttpPost] // --> map with Http Post request from client application
        public async Task<IActionResult> PostAsync(Category cat)
        {
            if (ModelState.IsValid)
            {
                if (cat.BasePrice < 0) throw new Exception("Base Price Cannot be -ve");
                cat = await _catRepository.CreateAsync(cat);
                return Ok(cat);
            }
            return BadRequest(ModelState); // invalid data response
        }
        [HttpPut("{id}")] // --> map with Http Put request from client application with id parameter in url
        public async Task<IActionResult> PutAsync(int id, Category cat)
        {
            if (ModelState.IsValid)
            {
                cat = await _catRepository.UpdateAsync(id, cat);
                return Ok(cat);
            }
            return BadRequest(ModelState); // invalid data response
        }
        [HttpDelete("{id}")] // --> map with Http Delete request from client application with id parameter in url
        public async Task<IActionResult> DeleteAsync(int id)
        {
                var res = await _catRepository.DeleteAsync(id);
                return Ok(res);
        }
    }
}