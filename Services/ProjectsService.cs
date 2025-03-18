using System.Security.Claims;
using InternIntelligence_Portfolio.Dtos.Projects;
using InternIntelligence_Portfolio.Models;
using InternIntelligence_Portfolio.Repos;

namespace InternIntelligence_Portfolio.Services
{
    public interface IProjectsService
    {
        List<Projects> GetAll();
        Projects GetById(int id);
        void Add(ProjectDtos projectDtos);
        void Delete(int id);
        void Update(int Id, ProjectDtos projectDtos);
    }
    public class ProjectsService : IProjectsService
    {
        private readonly IGenericRepo<Projects> _genericRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProjectsService(IGenericRepo<Projects> genericRepo,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _genericRepo = genericRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetCurrentUserId()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;
            var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            throw new UnauthorizedAccessException("User Must Be LoggedIn In Order To Use This Feature!");
        }
        public List<Projects> GetAll()
        {
            int userId = GetCurrentUserId();
            return _genericRepo.GetAll()
                                .Where(a => a.UserId == userId)
                                 .Select(p => new Projects
                                 {
                                     Title = p.Title,
                                     Description = p.Description,
                                     GitHubUrl = p.GitHubUrl,
                                     CreatedAt = DateTime.UtcNow

                                 }).ToList();
        }

        public Projects GetById(int id)
        {
            int userId = GetCurrentUserId();

            var getProjects = _genericRepo.GetById(id);
            if (getProjects == null || getProjects.UserId != userId)
            {
                return null;
            }
            return getProjects;

        }
        public void Add(ProjectDtos projectDtos)
        {
            int userId = GetCurrentUserId();

            var projects = new Projects()
            {
                Title = projectDtos.Title,
                Description = projectDtos.Description,
                GitHubUrl = projectDtos.GitHubUrl,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            _genericRepo.Add(projects);
        }

        public void Delete(int id)
        {
            int userId = GetCurrentUserId();

            var getId = _genericRepo.GetById(id);

            if (getId != null && getId.UserId == userId)
            {
                _genericRepo.Delete(getId);
            }
            else
            {
                throw new UnauthorizedAccessException("Not authorized to delete this achievement");
            }
        }
        public void Update(int Id, ProjectDtos projectDtos)
        {
            int userId = GetCurrentUserId();

            var projects = _genericRepo.GetById(Id);

            if (projects == null || projects.UserId != userId)
            {
                throw new UnauthorizedAccessException("Not authorized to update this achievement");
            }
            projects.Title = projectDtos.Title;
            projects.Description = projectDtos.Description;
            projects.GitHubUrl = projectDtos.GitHubUrl;
            projects.CreatedAt = DateTime.UtcNow;

            _genericRepo.Update(projects);
        }
    }
}
