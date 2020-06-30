using DataAnnotationsExtensions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Progress.Models
{
    public class Model
    {
        [Required]
        [DisplayName("Email")]
        [JsonProperty(PropertyName = "email")]
        [Email(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
    }
}