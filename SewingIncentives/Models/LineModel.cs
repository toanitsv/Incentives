using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    public class LineModel
    {
        public string LineId { get; set; }
        public string SectionId { get; set; }
        public int Ordinal { get; set; }
        public string Name { get; set; }
        public bool IsEnable { get; set; }
    }
}
