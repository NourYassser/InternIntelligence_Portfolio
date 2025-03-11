namespace InternIntelligence_Portfolio.Models
{
    public class Contact_Form
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public User Users { get; set; }
    }
}
