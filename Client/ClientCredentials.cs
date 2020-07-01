using System;
using System.ComponentModel.DataAnnotations;

namespace Client
{
    public class ClientCredentials
    {
        [Required]
        public string Email { get; set; }
    }
}
