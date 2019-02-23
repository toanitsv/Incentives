using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.ViewModels
{
    class DayLeaveSummaryViewModel
    {
        public int NO { get; set; }
        public string WorkerId { get; set; }
        public string Name { get; set; }
        public DateTime DateHired { get; set; }
        public int TotalDay { get; set; }
        public int LeaveDay { get; set; }
        public int Remaining { get; set; }
    }
}
