using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    public class WorkerLoginModel
    {
        public int WorkerLoginId { get; set; }
        public string CardId { get; set; }
        public string WorkerId { get; set; }        
        public string LineId { get; set; }        
        public DateTime CreatedTime { get; set; }
    }
}
