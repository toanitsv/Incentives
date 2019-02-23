using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    class SpecialTransferTypeModel
    {
        public int TransferType { get; set; }
        public string Name { get; set; }

        public static List<SpecialTransferTypeModel> Create()
        {
            List<SpecialTransferTypeModel> results = new List<SpecialTransferTypeModel>();
            results.Add(new SpecialTransferTypeModel { TransferType = 1, Name = "In"});
            results.Add(new SpecialTransferTypeModel { TransferType = 2, Name = "Out" });
            return results;
        }
    }
}
