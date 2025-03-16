namespace InternIntelligence_Portfolio.Dtos.Achievements
{
    public class AchievementDtos
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string CertificateUrl { get; set; } = string.Empty;
    }
}
