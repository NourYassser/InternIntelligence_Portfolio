using InternIntelligence_Portfolio.Dtos.Skills;
using InternIntelligence_Portfolio.Models;
using InternIntelligence_Portfolio.Repos;

namespace InternIntelligence_Portfolio.Services
{
    public interface ISkillService
    {
        List<Skills> GetAll();
        Skills GetById(int id);
        void Add(SkillsDto skillDtos);
        void Delete(int id);
        void Update(int Id, SkillsDto skillDtos);
    }
    public class SkillService : ISkillService
    {
        private readonly IGenericRepo<Skills> _genericRepo;

        public SkillService(IGenericRepo<Skills> genericRepo)
        {
            _genericRepo = genericRepo;
        }
        public List<Skills> GetAll()
        {
            return _genericRepo.GetAll().Select(p => new Skills
            {
                Name = p.Name,
                Category = p.Category

            }).ToList();
        }

        public Skills GetById(int id)
        {
            var getSkill = _genericRepo.GetById(id);
            if (getSkill == null)
            {
                return null;
            }
            return getSkill;

        }
        public void Add(SkillsDto skillDtos)
        {
            var skills = new Skills()
            {
                Name = skillDtos.Name,
                Category = skillDtos.Category
            };
            _genericRepo.Add(skills);
        }

        public void Delete(int id)
        {
            var getId = _genericRepo.GetById(id);
            _genericRepo.Delete(getId);
        }

        public void Update(int Id, SkillsDto skillDtos)
        {
            var skill = _genericRepo.GetById(Id);

            skill.Name = skillDtos.Name;
            skill.Category = skillDtos.Category;

            _genericRepo.Update(skill);
        }
    }
}
