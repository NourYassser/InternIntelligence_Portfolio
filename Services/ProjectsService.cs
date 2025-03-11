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
        void Update(ProjectDtos projectDtos);
        void Save();
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
                Descriptions = p.Descriptions,
                UserId = p.UserId
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
                Descriptions = projectDtos.Descriptions,
                UserId = projectDtos.UserId
            };
            _genericRepo.Add(projects);
            _genericRepo.Save();
        }

        public void Delete(int id)
        {
            var getId = _genericRepo.GetById(id);
            _genericRepo.Delete(getId);
            _genericRepo.Save();
        }

        public void Save()
        {
            _genericRepo.Save();
        }

        public void Update(ProjectDtos projectDtos)
        {
            var projects = _genericRepo.GetById(projectDtos.Id);

            projects.Title = projectDtos.Title;
            projects.Descriptions = projectDtos.Descriptions;
            projects.UserId = projectDtos.UserId;

            _genericRepo.Update(projects);
            _genericRepo.Save();
        }
    }
}
