using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    class Campregistration:Baseclass
    {
        public int Id { get; set; }
        public int CampId { get; set; }
        public int DonorId { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
