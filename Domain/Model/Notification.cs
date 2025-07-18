﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
 public   class Notification :Baseclass
    {
        public int Id { get; set; }
        public int SentBy { get; set; }  
        public string Message { get; set; }
        public string TargetRole { get; set; }
        public DateTime SentAt { get; set; }
    }
}
