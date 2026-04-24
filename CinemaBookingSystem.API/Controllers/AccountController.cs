using CinemaBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CinemaBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if(!ModelState.IsValid) 
                return BadRequest("Не валідні дані");

            var user = new IdentityUser
            {
                UserName = model.Name,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return Ok("Користувач успішно зареєстрований!");
            }

            return BadRequest(result.Errors.FirstOrDefault()?.Description);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserInfo(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound("Користувача не знайдено");

            return Ok(new
            {
                Name = user.UserName,
                Email = user.Email
            });
        }
    }
}
