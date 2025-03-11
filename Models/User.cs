namespace InternIntelligence_Portfolio.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Skills> Skill { get; set; } = new HashSet<Skills>();
        public ICollection<Achievements> Achievement { get; set; } = new HashSet<Achievements>();
        public ICollection<Projects> Project { get; set; } = new HashSet<Projects>();
        public Contact_Form Contacts { get; set; }
    }
}
