using System.ComponentModel.DataAnnotations;

namespace InternIntelligence_Portfolio.Dtos.User
{
    public class UserDtos
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
    }
}
