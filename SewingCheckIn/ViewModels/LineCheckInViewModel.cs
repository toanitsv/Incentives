using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingCheckIn.ViewModels
{
    public class LineCheckInViewModel
    {
        public string CardId { get; set; }
        public string WorkerId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string WorkAt { get; set; }
        public string Position { get; set; }
        public DateTime HiredDate { get; set; }
        public bool IsTerminalCheckIn { get; set; }
        public string TerminalTime { get; set; }
        public bool IsLineCheckIn { get; set; }
        public string LineTime { get; set; }
    }
}
