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

        public ProjectsService(IGenericRepo<Projects> genericRepo)
        {
            _genericRepo = genericRepo;
        }

        public List<Projects> GetAll()
        {
            return _genericRepo.GetAll().Select(p => new Projects
            {
                Title = p.Title,
                Description = p.Description,
                GitHubUrl = p.GitHubUrl,
                CreatedAt = DateTime.UtcNow

            }).ToList();
        }

        public Projects GetById(int id)
        {
            var getProjects = _genericRepo.GetById(id);
            if (getProjects == null)
            {
                return null;
            }
            return getProjects;

        }
        public void Add(ProjectDtos projectDtos)
        {
            var projects = new Projects()
            {
                Title = projectDtos.Title,
                Description = projectDtos.Description,
                GitHubUrl = projectDtos.GitHubUrl,
                CreatedAt = DateTime.UtcNow
            };
            _genericRepo.Add(projects);
        }

        public void Delete(int id)
        {
            var getId = _genericRepo.GetById(id);
            _genericRepo.Delete(getId);
        }
        public void Update(int Id, ProjectDtos projectDtos)
        {
            var projects = _genericRepo.GetById(Id);

            projects.Title = projectDtos.Title;
            projects.Description = projectDtos.Description;
            projects.GitHubUrl = projectDtos.GitHubUrl;
            projects.CreatedAt = DateTime.UtcNow;

            _genericRepo.Update(projects);
        }
    }
}
