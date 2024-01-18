using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.Interfaces;
using RecipeManager.Models;

namespace RecipeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class CategoryController(ICategoryService categoryService, IErrorhandler errorhandler) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IErrorhandler _errorhandler = errorhandler;

        [HttpPost]
        public IActionResult AddCategory([FromQuery]string name)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _categoryService.AddCategory(name);
                return Ok();
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        [HttpPut]
        public IActionResult EditCategory([FromQuery]Category newCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var status = _categoryService.EditCategory(newCategory);
                if (status == Status.Ok)
                    return Ok();
                
                return BadRequest(status.ToString());
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        [HttpDelete]
        public IActionResult DeleteCategory([FromQuery]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var status = _categoryService.DeleteCategory(id);
                if (status == Status.Ok)
                    return Ok();
                
                return BadRequest(status.ToString());
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCategory([FromQuery]int id)
        {
            try
            {
                var category = _categoryService.GetCategory(id);
                if (category.Id != 0)
                    return Ok();
                
                return NotFound();
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _categoryService.GetAllCategories();
                if (categories.Count > 0)
                    return Ok(categories);
                
                return NotFound();
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }
    }
}