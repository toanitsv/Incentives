using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    public class CieControlIncentiveModel
    {
        public double WorkHrsBefore { get; set; }
        public double WorkHrsAfter { get; set; }
        public string LineId { get; set; }
        public string Reason { get; set; }
        public int IncentiveBefore { get; set; }
        public int IncentiveAfter { get; set; }
        public bool Accepted { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
