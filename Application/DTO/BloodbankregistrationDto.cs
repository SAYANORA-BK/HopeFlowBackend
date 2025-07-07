using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class BloodbankregistrationDto
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
        public int role_id { get; set; }

        [Required]
        public int google_id { get; set; }
    }
}
