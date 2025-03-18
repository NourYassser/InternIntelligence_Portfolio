using System.ComponentModel.DataAnnotations;

namespace InternIntelligence_Portfolio.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool isLoggedIn { get; set; } = false;
    }
}
