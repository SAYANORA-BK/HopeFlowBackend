using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    class Bloodbank:Baseclass
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string Location { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string RegisteredBy { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
