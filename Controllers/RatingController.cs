using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.Interfaces;
using RecipeManager.Models;

namespace RecipeManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RatingController(IRatingService ratingService, IErrorhandler errorhandler) : ControllerBase
    {
        private readonly IRatingService _ratingService = ratingService;
        private readonly IErrorhandler _errorhandler = errorhandler;

        [HttpPost]
        public IActionResult AddRating([FromQuery]int Rating, int RecipeID)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0)
                    return BadRequest();

                NewRatingModel ratingModel = new(Rating, RecipeID);

                var status = _ratingService.AddRating(ratingModel, userId);
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

        [HttpPatch]
        public IActionResult EditRating([FromQuery]EditRatingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var admin = User.IsInRole("Admin");
                var userId = GetCurrentUserId();
                if (userId == 0)
                    return BadRequest();
                
                var status = _ratingService.EditRating(model, userId, admin);
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
        public IActionResult DeleteRating([FromQuery]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var admin = User.IsInRole("Admin");
                var userId = GetCurrentUserId();
                if (userId == 0)
                    return BadRequest();
                
                var status = _ratingService.DeleteRating(id, userId, admin);
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
        public IActionResult GetAllRatings()
        {
            try
            {
                var ratings = _ratingService.GetAllRatings();
                if (ratings.Count < 1)
                    return NotFound();

                return Ok(ratings);
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        [AllowAnonymous]
        [HttpGet("fromrecipe")]
        public IActionResult GetRatingsFromRecipe([FromQuery]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var ratings = _ratingService.GetRatingsFromRecipe(id);
                if (ratings.Count < 1)
                    return NotFound();

                return Ok(ratings);
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        [AllowAnonymous]
        [HttpGet("fromuser")]
        public IActionResult GetRatingsFromUser([FromQuery]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var ratings = _ratingService.GetRatingsFromUser(id);
                if (ratings.Count < 1)
                    return NotFound();

                return Ok(ratings);
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