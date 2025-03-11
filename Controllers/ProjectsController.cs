using InternIntelligence_Portfolio.Dtos.Projects;
using InternIntelligence_Portfolio.Services;
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
            return Ok(_ProjectsService.GetById(id));
        }

        [HttpPost("AddProjects")]
        public IActionResult Add(ProjectDtos Projects)
        {
            _ProjectsService.Add(Projects);
            return NoContent();
        }

        [HttpPut("EditProjects")]
        public IActionResult UpdateProjectss(ProjectDtos dtos)
        {
            var Projectss = _ProjectsService.GetById(dtos.Id);
            if (Projectss == null)
            {
                return BadRequest();
            }
            _ProjectsService.Update(dtos);
            return NoContent();
        }

        [HttpDelete("DeleteProjects")]
        public IActionResult DeleteProjectss(int id)
        {
            _ProjectsService.Delete(id);
            return NoContent();
        }
    }
}
