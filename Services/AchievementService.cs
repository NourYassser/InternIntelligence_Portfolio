using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AchievementService(IGenericRepo<Achievements> genericRepo,
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
        public List<Achievements> GetAll()
        {
            int userId = GetCurrentUserId();
            return _genericRepo.GetAll()
                                .Where(a => a.UserId == userId)
                                 .Select(p => new Achievements
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
            int userId = GetCurrentUserId();

            var getAchievements = _genericRepo.GetById(id);

            if (getAchievements == null || getAchievements.UserId != userId)
            {
                return null;
            }
            return getAchievements;

        }
        public void Add(AchievementDtos achievementDtos)
        {
            int userId = GetCurrentUserId();

            var achievements = new Achievements()
            {
                Title = achievementDtos.Title,
                Description = achievementDtos.Description,
                Date = achievementDtos.Date,
                Provider = achievementDtos.Provider,
                CertificateUrl = achievementDtos.CertificateUrl,
                UserId = userId
            };
            _genericRepo.Add(achievements);
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

        public void Update(int Id, AchievementDtos achievementDtos)
        {
            int userId = GetCurrentUserId();

            var achievement = _genericRepo.GetById(Id);

            if (achievement == null || achievement.UserId != userId)
            {
                throw new UnauthorizedAccessException("Not authorized to update this achievement");
            }

            achievement.Title = achievementDtos.Title;
            achievement.Description = achievementDtos.Description;
            achievement.Date = achievementDtos.Date;
            achievement.Provider = achievementDtos.Provider;
            achievement.CertificateUrl = achievementDtos.CertificateUrl;

            _genericRepo.Update(achievement);
        }
    }
}
