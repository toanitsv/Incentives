using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingCheckIn.Models
{
    public class SourceModel
    {
        public string CardId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string TerminalNo { get; set; }
    }
}
