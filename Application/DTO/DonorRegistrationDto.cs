using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.DTO
{
    public class DonorRegistrationDto
    {
        [Required]
        public string full_name { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string hashpassword { get; set; }

        [Required]
        [Phone]
        public string phone_number { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string blood_group { get; set; }

        [Required]
        public int role_id { get; set; } = 2;

        
        [Required]
        public IFormFile certificate { get; set; }
    }
}
