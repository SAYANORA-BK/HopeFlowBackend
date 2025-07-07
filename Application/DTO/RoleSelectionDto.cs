using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class RoleSelectionDto
    {
        [Required]
        public int Role_id { get; set; }

        [Required]
        public string role_name { get; set; }
    }
}
