using System.Security.Claims;
using InternIntelligence_Portfolio.Dtos.UserDto;
using InternIntelligence_Portfolio.Models;
using InternIntelligence_Portfolio.Models.DataBase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace InternIntelligence_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(UserDtos dto)
        {
            var existingUser = _context.Users.FirstOrDefault(p => p.UserName == dto.UserName
                                                                    || p.Email == dto.Email);
            if (existingUser is not null)
            {
                return BadRequest("Error, Already Registered");
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = hashedPassword,
                isLoggedIn = false
            };

            _context.Add(user);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserDtos dto)
        {
            try
            {
                var existingUser = _context.Users.FirstOrDefault(p => p.UserName == dto.UserName
                                                                        && p.Email == dto.Email);
                if (existingUser == null)
                {
                    return BadRequest("User does not exist!");
                }
                bool passwordIsValid = BCrypt.Net.BCrypt.Verify(dto.Password, existingUser.Password);

                if (!passwordIsValid)
                {
                    return BadRequest("Email or password is incorrect!");
                }
                existingUser.isLoggedIn = true;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingUser.UserName),
                    new Claim(ClaimTypes.Email, existingUser.Email),
                    new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Configure cookie persistence
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                _context.SaveChanges();
                return Ok("Login Succeeded!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during login: {ex.Message}");
            }

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var user = _context.Users.FirstOrDefault(p => p.isLoggedIn == true);
                if (user is null)
                {
                    return BadRequest("You Must Be LoggedIn In Order To LogOut");
                }
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                user.isLoggedIn = false;
                _context.SaveChanges();
                return Ok("Logged out successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during logout: {ex.Message}");
            }
        }

    }
}
