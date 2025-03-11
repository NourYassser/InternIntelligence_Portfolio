using InternIntelligence_Portfolio.Dtos.User;
using InternIntelligence_Portfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternIntelligence_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetUsers")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("GetUsersById")]
        public IActionResult GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpPost("AddUsers")]
        public IActionResult Add(UserDtos user)
        {
            _userService.Add(user);
            return NoContent();
        }

        [HttpPut("EditUsers")]
        public IActionResult UpdateUsers(UserDtos dtos)
        {
            var users = _userService.GetById(dtos.Id);
            if (users == null)
            {
                return BadRequest();
            }
            _userService.Update(dtos);
            return NoContent();
        }

        [HttpDelete("DeleteUsers")]
        public IActionResult DeleteUsers(int id)
        {
            _userService.Delete(id);
            return NoContent();
        }
    }
}
