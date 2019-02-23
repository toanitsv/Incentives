using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    class PositionModel
    {
        public static List<PositionModel> Init()
        {
            List<PositionModel> positionList = new List<PositionModel>();

            positionList.Add(new PositionModel
            {
                Name = "CEMENTER",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "CEMENTER DOUBLE LASTING",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "DRAW LINE",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "HEEL LASTER",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "OUTSOLE STITCHER",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "OVERSEAMING",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "PRODUCTION STAFF",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "SIDE LASTER",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "SLIP LASTER",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "SOLE ATTACHER",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "SPRAY HOT MELT",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "TOE LASTER",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "UNIVERSAL PRESS",
                IsCalculate = true,
            });
            positionList.Add(new PositionModel
            {
                Name = "WORKER",
                IsCalculate = true,
            });

            return positionList;
        }

        public string Name { get; set; }
        public bool IsCalculate { get; set; }
    }
}
