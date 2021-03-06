﻿using DataAnnotationsExtensions;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Progress.Models
{
    public class ModelVM
    {
        [DisplayName("Email")]
        [JsonProperty(PropertyName = "email")]
        [Email(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
    }
}