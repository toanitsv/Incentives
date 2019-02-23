using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.ViewModels
{
    class SpecialTransferViewModel
    {
        public int SpecialTransferId { get; set; }
        public string WorkerId { get; set; }
        public int TransferType { get; set; }
        public string TransferName { get; set; }
        public string DestinationLineId { get; set; }
        public string DestinationLineName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
