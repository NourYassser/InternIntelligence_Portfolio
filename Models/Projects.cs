namespace InternIntelligence_Portfolio.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string GitHubUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
