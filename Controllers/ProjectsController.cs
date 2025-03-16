using InternIntelligence_Portfolio.Dtos.Projects;
using InternIntelligence_Portfolio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternIntelligence_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _ProjectsService;

        public ProjectsController(IProjectsService ProjectsService)
        {
            _ProjectsService = ProjectsService;
        }
        [HttpGet("GetProjects")]
        public IActionResult GetAll()
        {
            return Ok(_ProjectsService.GetAll());
        }

        [HttpGet("GetProjectsById")]
        public IActionResult GetById(int id)
        {
            var project = _ProjectsService.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }
        [Authorize]
        [HttpPost("AddProjects")]
        public IActionResult Add(ProjectDtos Projects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _ProjectsService.Add(Projects);
            return NoContent();
        }

        [HttpPut("EditProjects")]
        [Authorize]
        public IActionResult UpdateProjectss(int Id, ProjectDtos dtos)
        {
            var Projectss = _ProjectsService.GetById(Id);
            if (Projectss == null)
            {
                return BadRequest();
            }
            _ProjectsService.Update(Id, dtos);
            return NoContent();
        }

        [HttpDelete("DeleteProjects")]
        [Authorize]
        public IActionResult DeleteProjectss(int id)
        {
            try
            {
                _ProjectsService.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
