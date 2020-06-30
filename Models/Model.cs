using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Model
    {
        [Required]
        public string Email { get; set; }
    }
}
