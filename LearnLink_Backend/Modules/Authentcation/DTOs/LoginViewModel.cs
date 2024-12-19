﻿using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Modules.Authentcation.DTOs
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        [StringLength(16, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
