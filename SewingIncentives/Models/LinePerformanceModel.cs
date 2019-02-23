using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    class LinePerformanceModel
    {
        public string LineId { get; set; }
        public DateTime Date { get; set; }
        public int Output { get; set; }
        public string PatternNo { get; set; }
        public string ArticleNo { get; set; }
        public int IncentiveGradeA { get; set; }
        public int IncentiveGradeASmall { get; set; }
        public double ReportedWorkHour { get; set; }
        public int ExcessIncentive { get; set; }
        public int QuotaTarget { get; set; }
    }
}
