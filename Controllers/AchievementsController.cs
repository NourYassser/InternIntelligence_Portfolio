using InternIntelligence_Portfolio.Dtos.Achievements;
using InternIntelligence_Portfolio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternIntelligence_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementService _Achievementservice;

        public AchievementsController(IAchievementService Achievementservice)
        {
            _Achievementservice = Achievementservice;
        }
        [HttpGet("GetAchievements")]
        public IActionResult GetAll()
        {
            return Ok(_Achievementservice.GetAll());
        }

        [HttpGet("GetAchievementsById")]
        public IActionResult GetById(int id)
        {
            var ach = _Achievementservice.GetById(id);
            if (ach == null)
            {
                return NotFound();
            }
            return Ok(ach);
        }

        [HttpPost("AddAchievements")]
        [Authorize]
        public IActionResult Add(AchievementDtos ach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _Achievementservice.Add(ach);
            return NoContent();
        }

        [HttpPut("EditAchievements")]
        [Authorize]
        public IActionResult UpdateAchievements(int Id, AchievementDtos dtos)
        {
            var Achievements = _Achievementservice.GetById(Id);
            if (Achievements == null)
            {
                return BadRequest();
            }
            _Achievementservice.Update(Id, dtos);
            return NoContent();
        }

        [HttpDelete("DeleteAchievements")]
        [Authorize]
        public IActionResult DeleteAchievements(int id)
        {
            try
            {
                _Achievementservice.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
