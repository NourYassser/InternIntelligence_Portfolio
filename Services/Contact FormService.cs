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

        public Contact_FormService(IGenericRepo<Contact_Form> genericRepo)
        {
            _genericRepo = genericRepo;
        }
        public List<Contact_Form> GetAll()
        {
            return _genericRepo.GetAll().Select(p => new Contact_Form
            {
                Name = p.Name,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber
            }).ToList();
        }

        public Contact_Form GetById(int id)
        {
            var contacts = _genericRepo.GetById(id);
            if (contacts == null)
            {
                return null;
            }
            return contacts;

        }
        public void Add(ContactsDto contactsDtos)
        {
            var contacts = new Contact_Form()
            {
                Name = contactsDtos.Name,
                Email = contactsDtos.Email,
                PhoneNumber = contactsDtos.PhoneNumber
            };
            _genericRepo.Add(contacts);
        }

        public void Delete(int id)
        {
            var getId = _genericRepo.GetById(id);
            _genericRepo.Delete(getId);
        }
        public void Update(int Id, ContactsDto contactsDtos)
        {
            var projects = _genericRepo.GetById(Id);

            projects.Name = contactsDtos.Name;
            projects.Email = contactsDtos.Email;
            projects.PhoneNumber = contactsDtos.PhoneNumber;

            _genericRepo.Update(projects);
        }
    }
}
