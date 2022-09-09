﻿using System.Text.Json.Serialization;

namespace SampleCQRSApplication.Authentication
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
