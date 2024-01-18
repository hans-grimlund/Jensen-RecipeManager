using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.Interfaces;
using RecipeManager.Models;
using RecipeManager.Models.DTOs;

namespace RecipeManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RecipeController(IRecipeService recipeService, IErrorhandler errorhandler) : ControllerBase
    {
        private readonly IRecipeService _recipeService = recipeService;
        private readonly IErrorhandler _errorhandler = errorhandler;

        [HttpPost]
        public IActionResult AddRecipe([FromQuery]NewRecipeModel newRecipe)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0)
                    return Unauthorized();

                var status = _recipeService.AddRecipe(newRecipe, userId);
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

        [HttpPut]
        public IActionResult EditRecipe([FromQuery]RecipeDTO newRecipe)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var admin = User.IsInRole("Admin");
                var userId = GetCurrentUserId();
                if (userId == 0)
                    return BadRequest();

                var status = _recipeService.EditRecipe(newRecipe, userId, admin);
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
        public IActionResult DeleteRecipe([FromQuery]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var admin = User.IsInRole("Admin");
                var userId = GetCurrentUserId();
                if (userId == 0)
                    return BadRequest();

                var status = _recipeService.DeleteRecipe(id, userId, admin);
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
        [HttpGet("getall")]
        public IActionResult GetAllRecipes()
        {
            try
            {
                var recipes = _recipeService.GetAllRecipes();
                if (recipes.Count > 0)
                    return Ok(recipes);
                
                return NotFound();
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        [AllowAnonymous]
        [HttpGet("find")]
        public IActionResult FindRecipe([FromQuery]string searchterm)
        {
            if (!ModelState.IsValid || searchterm == null || searchterm == string.Empty)
                return BadRequest();

            try
            {
                var recipes = _recipeService.FindRecipe(searchterm);
                if (recipes.Count > 0)
                    return Ok(recipes);
                
                return NotFound();
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        private int GetCurrentUserId()
        {
            try
            {
                var idClaim = User.FindFirst("UserId");
                if (idClaim == null)
                    return 0;

                var parsed = int.TryParse(idClaim.Value, out int id);
                if (parsed)
                    return id;

                return 0;
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return 0;
            }
        }
    }
}