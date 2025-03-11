namespace InternIntelligence_Portfolio.Models
{
    public class Skills
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public int UserId { get; set; }
        public User Users { get; set; }
    }
}
