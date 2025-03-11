using InternIntelligence_Portfolio.Dtos.Achievements;
using InternIntelligence_Portfolio.Models;
using InternIntelligence_Portfolio.Repos;

namespace InternIntelligence_Portfolio.Services
{
    public interface IAchievementService
    {
        List<Achievements> GetAll();
        Achievements GetById(int id);
        void Add(AchievementDtos achievementDtos);
        void Delete(int id);
        void Update(AchievementDtos achievementDtos);
        void Save();
    }
    public class AchievementService : IAchievementService
    {
        private readonly IGenericRepo<Achievements> _genericRepo;

        public AchievementService(IGenericRepo<Achievements> genericRepo)
        {
            _genericRepo = genericRepo;
        }
        public List<Achievements> GetAll()
        {
            return _genericRepo.GetAll().Select(p => new Achievements
            {
                Title = p.Title,
                Descriptions = p.Descriptions,
                UserId = p.UserId
            }).ToList();
        }

        public Achievements GetById(int id)
        {
            var getAchievements = _genericRepo.GetById(id);
            if (getAchievements == null)
            {
                return null;
            }
            return getAchievements;

        }
        public void Add(AchievementDtos achievementDtos)
        {
            var achievements = new Achievements()
            {
                Title = achievementDtos.Title,
                Descriptions = achievementDtos.Descriptions,
                UserId = achievementDtos.UserId
            };
            _genericRepo.Add(achievements);
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

        public void Update(AchievementDtos achievementDtos)
        {
            var achievement = _genericRepo.GetById(achievementDtos.Id);

            achievement.Title = achievementDtos.Title;
            achievement.Descriptions = achievementDtos.Descriptions;
            achievement.UserId = achievementDtos.UserId;

            _genericRepo.Update(achievement);
            _genericRepo.Save();
        }
    }
}
