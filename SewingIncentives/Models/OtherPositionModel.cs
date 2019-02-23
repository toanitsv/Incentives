using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    class OtherPositionModel
    {
        public static List<OtherPositionModel> Init()
        {
            List<OtherPositionModel> otherPositionList = new List<OtherPositionModel>();
            otherPositionList.Add(new OtherPositionModel
            {
                Name = null,
                IsCalculate = true,
                IsShow = false,
            });
            otherPositionList.Add(new OtherPositionModel
            {
                Name = "",
                IsCalculate = true,
                IsShow = false,
            });
            otherPositionList.Add(new OtherPositionModel
            {
                Name = "NaN",
                IsCalculate = true,
                IsShow = true,
            });
            otherPositionList.Add(new OtherPositionModel
            {
                Name = "Sample",
                IsCalculate = false,
                IsShow = true,
            });
            otherPositionList.Add(new OtherPositionModel
            {
                Name = "Training",
                IsCalculate = false,
                IsShow = true,
            });
            otherPositionList.Add(new OtherPositionModel
            {
                Name = "Training New Worker",
                IsCalculate = false,
                IsShow = true,
            });
            otherPositionList.Add(new OtherPositionModel
            {
                Name = "Re-work",
                IsCalculate = true,
                IsShow = true,
            });
            otherPositionList.Add(new OtherPositionModel
            {
                Name = "Prepairing",
                IsCalculate = true,
                IsShow = true,
            });

            return otherPositionList;
        }

        public string Name { get; set; }
        public bool IsCalculate { get; set; }
        public bool IsShow { get; set; }
    }
}
