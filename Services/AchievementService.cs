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
        void Update(int Id, AchievementDtos achievementDtos);
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
                Description = p.Description,
                Date = p.Date,
                Provider = p.Provider,
                CertificateUrl = p.CertificateUrl

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
                Description = achievementDtos.Description,
                Date = achievementDtos.Date,
                Provider = achievementDtos.Provider,
                CertificateUrl = achievementDtos.CertificateUrl
            };
            _genericRepo.Add(achievements);
        }

        public void Delete(int id)
        {
            var getId = _genericRepo.GetById(id);
            _genericRepo.Delete(getId);
        }

        public void Update(int Id, AchievementDtos achievementDtos)
        {
            var achievement = _genericRepo.GetById(Id);

            achievement.Title = achievementDtos.Title;
            achievement.Description = achievementDtos.Description;
            achievement.Date = achievementDtos.Date;
            achievement.Provider = achievementDtos.Provider;
            achievement.CertificateUrl = achievementDtos.CertificateUrl;

            _genericRepo.Update(achievement);
        }
    }
}
