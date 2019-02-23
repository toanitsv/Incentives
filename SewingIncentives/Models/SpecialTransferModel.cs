using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    public class SpecialTransferModel
    {
        public int SpecialTransferId { get; set; }
        public string WorkerId { get; set; }
        public int TransferType { get; set; } //1:In, 2:Out
        public string DestinationLineId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
