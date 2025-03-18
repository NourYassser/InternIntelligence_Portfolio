namespace InternIntelligence_Portfolio.Models
{
    public class Skills
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
