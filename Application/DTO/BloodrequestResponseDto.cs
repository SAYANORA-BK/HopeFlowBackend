using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
   public   class BloodrequestResponseDto
    {
        public int Id { get; set; }
        public string RequesterName { get; set; }
        public string blood_group { get; set; }
        public int UnitsRequired { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
