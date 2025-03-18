﻿using System.ComponentModel.DataAnnotations;

namespace InternIntelligence_Portfolio.Dtos.UserDto
{
    public class UserDtos
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
