﻿using System.ComponentModel.DataAnnotations;

namespace FrontEnd.APIModels
{
    public class RegisterAPI
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
