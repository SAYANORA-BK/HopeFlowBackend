using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
   public   class BloodInventoryDto
    {
        public int BloodBankId { get; set; }
        public string BloodGroup { get; set; }
        public int UnitsAvailable { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
