using InternIntelligence_Portfolio.Dtos.Achievements;
using InternIntelligence_Portfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternIntelligence_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(_Achievementservice.GetById(id));
        }

        [HttpPost("AddAchievements")]
        public IActionResult Add(AchievementDtos ach)
        {
            _Achievementservice.Add(ach);
            return NoContent();
        }

        [HttpPut("EditAchievements")]
        public IActionResult UpdateAchievements(AchievementDtos dtos)
        {
            var Achievements = _Achievementservice.GetById(dtos.Id);
            if (Achievements == null)
            {
                return BadRequest();
            }
            _Achievementservice.Update(dtos);
            return NoContent();
        }

        [HttpDelete("DeleteAchievements")]
        public IActionResult DeleteAchievements(int id)
        {
            _Achievementservice.Delete(id);
            return NoContent();
        }
    }
}
