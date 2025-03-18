using InternIntelligence_Portfolio.Dtos.Skills;
using InternIntelligence_Portfolio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternIntelligence_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var skill = _skillService.GetById(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }

        [HttpPost("AddSkills")]
        public IActionResult Add(SkillsDto skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _skillService.Add(skill);
            return NoContent();
        }

        [HttpPut("EditSkills")]
        public IActionResult UpdateSkills(int Id, SkillsDto dtos)
        {
            var Skills = _skillService.GetById(Id);
            if (Skills == null)
            {
                return BadRequest();
            }
            _skillService.Update(Id, dtos);
            return NoContent();
        }

        [HttpDelete("DeleteSkills")]
        public IActionResult DeleteSkills(int id)
        {
            try
            {
                _skillService.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
