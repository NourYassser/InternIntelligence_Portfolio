using InternIntelligence_Portfolio.Dtos.User;
using InternIntelligence_Portfolio.Models;
using InternIntelligence_Portfolio.Repos;

namespace InternIntelligence_Portfolio.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        void Add(UserDtos userDtos);
        void Delete(int id);
        void Update(UserDtos userDtos);
        void Save();
    }
    public class UserService : IUserService
    {
        private readonly IGenericRepo<User> _genericRepo;

        public UserService(IGenericRepo<User> genericRepo)
        {
            _genericRepo = genericRepo;
        }
        public List<User> GetAll()
        {
            return _genericRepo.GetAll().Select(p => new User
            {
                UserName = p.UserName,
                Email = p.Email
            }).ToList();
        }

        public User GetById(int id)
        {
            var getUser = _genericRepo.GetById(id);
            if (getUser == null)
            {
                return null;
            }
            return getUser;

        }
        public void Add(UserDtos userDtos)
        {
            var user = new User()
            {
                UserName = userDtos.UserName,
                Email = userDtos.Email,
                PasswordHash = userDtos.PasswordHash
            };
            _genericRepo.Add(user);
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

        public void Update(UserDtos userDtos)
        {
            var user = _genericRepo.GetById(userDtos.Id);

            user.UserName = userDtos.UserName;
            user.Email = userDtos.Email;
            user.PasswordHash = userDtos.PasswordHash;

            _genericRepo.Update(user);
            _genericRepo.Save();
        }
    }
}
