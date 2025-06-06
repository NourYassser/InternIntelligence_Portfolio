﻿namespace InternIntelligence_Portfolio.Models
{
    public class Contact_Form
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
