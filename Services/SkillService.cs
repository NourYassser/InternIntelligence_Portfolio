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
        void Update(SkillsDto skillDtos);
        void Save();
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
                Title = p.Title,
                Descriptions = p.Descriptions,
                UserId = p.UserId
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
                Title = skillDtos.Title,
                Descriptions = skillDtos.Descriptions,
                UserId = skillDtos.UserId
            };
            _genericRepo.Add(skills);
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

        public void Update(SkillsDto skillDtos)
        {
            var skill = _genericRepo.GetById(skillDtos.Id);

            skill.Title = skillDtos.Title;
            skill.Descriptions = skillDtos.Descriptions;
            skill.UserId = skillDtos.UserId;

            _genericRepo.Update(skill);
            _genericRepo.Save();
        }
    }
}
