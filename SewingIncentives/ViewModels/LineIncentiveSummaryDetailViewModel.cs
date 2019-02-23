using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.ViewModels
{
    public class LineIncentiveSummaryDetailViewModel
    {
        public DateTime Date { get; set; }
        public string WorkAtLine { get; set; }
        public string OthersPosition { get; set; }
        public string AdjustTimeOut { get; set; }
        public string IncentiveGrade { get; set; }
        public int Incentive { get; set; }
        public int SpecsIncentive { get; set; }
    }
}
