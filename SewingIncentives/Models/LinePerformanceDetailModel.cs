using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    class LinePerformanceDetailModel
    {
        public string LineId { get; set; }
        public DateTime Date { get; set; }
        public string CardId { get; set; }
        public string WorkerId { get; set; }
        public string OthersPosition { get; set; }
        public string TimeOut { get; set; }
        public string IncentiveGrade { get; set; }
        public int Incentive { get; set; }
        public int ExcessIncentive { get; set; }
        public int SpecsIncentive { get; set; }        
    }
}
