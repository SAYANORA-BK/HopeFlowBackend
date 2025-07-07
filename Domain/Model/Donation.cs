using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    class Donation:Baseclass
    {
        public int Id { get; set; }
        public int RequestedBy { get; set; }
        public string BloodGroup { get; set; }
        public int UnitsRequired { get; set; }
        public string Reason { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
