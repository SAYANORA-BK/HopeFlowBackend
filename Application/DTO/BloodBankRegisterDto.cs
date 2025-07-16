using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.DTO
{
    public class BloodBankRegisterDto
    {

        [Required]
        public string full_name { get; set; }

        [Required, EmailAddress]
        public string email { get; set; }

        [Required]
        public string hashpassword { get; set; }

        [Required]
        public string phone_number { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public int role_id { get; set; } 

        [Required]
        public IFormFile certificate { get; set; } 

    }
}
