using InternIntelligence_Portfolio.Dtos.Skills;
using InternIntelligence_Portfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternIntelligence_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        [HttpGet("GetSkills")]
        public IActionResult GetAll()
        {
            return Ok(_skillService.GetAll());
        }

        [HttpGet("GetSkillsById")]
        public IActionResult GetById(int id)
        {
            return Ok(_skillService.GetById(id));
        }

        [HttpPost("AddSkills")]
        public IActionResult Add(SkillsDto skill)
        {
            _skillService.Add(skill);
            return NoContent();
        }

        [HttpPut("EditSkills")]
        public IActionResult UpdateSkills(SkillsDto dtos)
        {
            var Skills = _skillService.GetById(dtos.Id);
            if (Skills == null)
            {
                return BadRequest();
            }
            _skillService.Update(dtos);
            return NoContent();
        }

        [HttpDelete("DeleteSkills")]
        public IActionResult DeleteSkills(int id)
        {
            _skillService.Delete(id);
            return NoContent();
        }
    }
}
