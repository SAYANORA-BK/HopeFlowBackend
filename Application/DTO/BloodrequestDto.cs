using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
 public   class BloodrequestDto
    {
        public string BloodGroup { get; set; }
        public int UnitsRequired { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
    }
}
