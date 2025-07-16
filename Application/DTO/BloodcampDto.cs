using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
 public   class BloodcampDto
    {

        public int Id { get; set; }
        public int BankId { get; set; }
        public string CampName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool is_verified {  get; set; }

    }
}
