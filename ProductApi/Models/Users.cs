﻿using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class Users
    {
        [Key]
        public string EmailID { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
