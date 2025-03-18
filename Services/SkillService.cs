using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SkillService(IGenericRepo<Skills> genericRepo,
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
        public List<Skills> GetAll()
        {
            int userId = GetCurrentUserId();
            return _genericRepo.GetAll()
                                .Where(a => a.UserId == userId)
                                 .Select(p => new Skills
                                 {
                                     Name = p.Name,
                                     Category = p.Category

                                 }).ToList();
        }

        public Skills GetById(int id)
        {
            int userId = GetCurrentUserId();

            var getSkill = _genericRepo.GetById(id);
            if (getSkill == null || getSkill.UserId != userId)
            {
                return null;
            }
            return getSkill;

        }
        public void Add(SkillsDto skillDtos)
        {
            int userId = GetCurrentUserId();

            var skills = new Skills()
            {
                Name = skillDtos.Name,
                Category = skillDtos.Category,
                UserId = userId
            };
            _genericRepo.Add(skills);
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

        public void Update(int Id, SkillsDto skillDtos)
        {
            int userId = GetCurrentUserId();

            var skill = _genericRepo.GetById(Id);
            if (skill == null || skill.UserId != userId)
            {
                throw new UnauthorizedAccessException("Not authorized to update this achievement");
            }
            skill.Name = skillDtos.Name;
            skill.Category = skillDtos.Category;

            _genericRepo.Update(skill);
        }
    }
}
