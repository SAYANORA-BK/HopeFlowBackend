using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    class Doantionlog:Baseclass
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int? CampId { get; set; } 
        public DateTime DonationDate { get; set; }
        public string BloodGroup { get; set; }
        public string Notes { get; set; }
    }
}
