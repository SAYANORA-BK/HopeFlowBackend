using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    class Verification:Baseclass
    {
        public int Id { get; set; }
        public int UserId { get; set; }                      
        public string DocumentUrl { get; set; }    
        public int CountOfHemoglobin { get; set; }
        public int weight { get; set; }
        public int BloodPressure { get; set; }
        public int Pulse {  get; set; }
        public string Status { get; set; }         
        public string Remarks { get; set; }         
        public DateTime SubmittedAt { get; set; }
        public DateTime? VerifiedAt { get; set; }

    }
}
