using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    public class AssemblySpecialIncentiveModel
    {
        public string Line { get; set; }
        public int Output { get; set; }
        public int QuotaTarget { get; set; }
        public int Lacking { get; set; }
        public double PercentOutput { get; set; }
        public int SUPIncentive { get; set; }
        public int LL23Incentive { get; set; }
        public int LLT1MECHIncentive { get; set; }
        public int WorkerIncentive { get; set; }
    }
}
