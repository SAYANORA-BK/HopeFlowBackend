using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
   public  class AnalyticDto
    {
        public int TotalUsers { get; set; }
        public int TotalDonors { get; set; }
        public int TotalRecipients { get; set; }
        public int TotalHospital {  get; set; }
        public int TotalBloodbanks {  get; set; }
        public int TotalRequests { get; set; }
        public int FulfilledRequests { get; set; }
    }
}
