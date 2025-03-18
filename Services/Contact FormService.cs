using System.Security.Claims;
using InternIntelligence_Portfolio.Dtos.Contacts;
using InternIntelligence_Portfolio.Models;
using InternIntelligence_Portfolio.Repos;

namespace InternIntelligence_Portfolio.Services
{
    public interface IContact_FormService
    {
        List<Contact_Form> GetAll();
        Contact_Form GetById(int id);
        void Add(ContactsDto contactsDtos);
        void Delete(int id);
        void Update(int Id, ContactsDto contactsDtos);
    }
    public class Contact_FormService : IContact_FormService
    {
        private readonly IGenericRepo<Contact_Form> _genericRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Contact_FormService(IGenericRepo<Contact_Form> genericRepo,
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
        public List<Contact_Form> GetAll()
        {
            int userId = GetCurrentUserId();
            return _genericRepo.GetAll()
                                .Where(a => a.UserId == userId)
                                .Select(p => new Contact_Form
                                {
                                    Name = p.Name,
                                    Email = p.Email,
                                    PhoneNumber = p.PhoneNumber
                                }).ToList();
        }

        public Contact_Form GetById(int id)
        {
            int userId = GetCurrentUserId();

            var contacts = _genericRepo.GetById(id);
            if (contacts == null || contacts.UserId != userId)
            {
                return null;
            }
            return contacts;

        }
        public void Add(ContactsDto contactsDtos)
        {
            int userId = GetCurrentUserId();

            var contacts = new Contact_Form()
            {
                Name = contactsDtos.Name,
                Email = contactsDtos.Email,
                PhoneNumber = contactsDtos.PhoneNumber,
                UserId = userId
            };
            _genericRepo.Add(contacts);
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
        public void Update(int Id, ContactsDto contactsDtos)
        {
            int userId = GetCurrentUserId();

            var projects = _genericRepo.GetById(Id);
            if (projects == null || projects.UserId != userId)
            {
                throw new UnauthorizedAccessException("Not authorized to update this achievement");
            }
            projects.Name = contactsDtos.Name;
            projects.Email = contactsDtos.Email;
            projects.PhoneNumber = contactsDtos.PhoneNumber;

            _genericRepo.Update(projects);
        }
    }
}
