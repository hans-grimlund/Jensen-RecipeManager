using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.Interfaces;
using RecipeManager.Models;

namespace RecipeManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserRepo userRepo, IErrorhandler errorhandler, IUserService userService) : ControllerBase
    {
        private readonly IUserRepo _userRepo = userRepo;
        private readonly IErrorhandler _errorhandler = errorhandler;
        private readonly IUserService _userService = userService;

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser([FromQuery] NewUserModel newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var status = _userService.AddUser(newUser);
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

        [HttpPatch("changeemail")]
        public IActionResult ChangeEmail([FromQuery] string email)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0)
                    return BadRequest();

                var status = _userService.ChangeEmail(email, userId);
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

        [HttpPatch("changepassword")]
        public IActionResult ChangePassword([FromQuery] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0)
                    return BadRequest();

                var status = _userService.ChangePassword(model, userId);
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

        [Authorize(Roles = "Admin")]
        [HttpGet("getall")]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_userService.GetAllUsers());
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0)
                    return BadRequest();
                    
                _userRepo.DeleteUser(userId);
                
                return Ok();
            }
            catch (Exception ex)
            {
                _errorhandler.LogError(ex);
                return Problem("Something went wrong...");
            }
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login([FromQuery] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var token = _userService.Login(request);
                if (token == "not found")
                    return NotFound();
                if (token == "incorrect password")
                    return BadRequest(Status.IncorrectPassword.ToString());
                
                return Ok(token);
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