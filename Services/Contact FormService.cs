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
        void Update(ContactsDto contactsDtos);
        void Save();
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
                Email = contactsDtos.Email,
                PhoneNumber = contactsDtos.PhoneNumber
            };
            _genericRepo.Add(contacts);
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

        public void Update(ContactsDto contactsDtos)
        {
            var projects = _genericRepo.GetById(contactsDtos.Id);

            projects.Email = contactsDtos.Email;
            projects.PhoneNumber = contactsDtos.PhoneNumber;

            _genericRepo.Update(projects);
            _genericRepo.Save();
        }
    }
}
