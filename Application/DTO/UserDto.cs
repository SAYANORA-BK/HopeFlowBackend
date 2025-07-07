using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
 public   class UserDto
    {
        public int Id { get; set; }
        public string full_name { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string hashpassword { get; set; }
        [Required]
        public string role { get; set; }

        [Required]
        public int google_id { get; set; }
    }
}
