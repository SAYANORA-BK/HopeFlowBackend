using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public  class DonationDto
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int? CampId { get; set; }              
        public DateTime DonationDate { get; set; }   
        public int units_donated {  get; set; }
        public string BloodGroup { get; set; }     

    }
}
