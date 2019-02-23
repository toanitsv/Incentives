using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    public class AssemblyIncentiveModel
    {
        public int Id { get; set; }
        public string PatternNo { get; set; }
        public string ShoeName { get; set; }
        public double Efficiency { get; set; }
        public int AssemblyOutput { get; set; }
        public int Incentive { get; set; }
        public int Worker { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
