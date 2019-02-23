using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SewingIncentives.Models
{
    public class WorkerLeaveModel
    {
        public int WorkerLeaveId { get; set; }
        public string WorkerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class LeaveTypeModel
    {
        public int LeaveTypeId { get; set; }
        public string Name { get; set; }
        public Brush BackgroundColor { get; set; }
        public string ReportBackgroundColor { get; set; }
        public bool HaveIncentive { get; set; }

        public static List<LeaveTypeModel> Create()
        {
            List<LeaveTypeModel> leaveTypeList = new List<LeaveTypeModel>();
            leaveTypeList.Add(new LeaveTypeModel
            {
                LeaveTypeId = 1,
                Name = "Leave",
                BackgroundColor = Brushes.Green,
                ReportBackgroundColor = "Green",
                HaveIncentive = true,
            });
            leaveTypeList.Add(new LeaveTypeModel
            {
                LeaveTypeId = 2,
                Name = "Pregnant",
                BackgroundColor = Brushes.Yellow,
                ReportBackgroundColor = "Yellow",
                HaveIncentive = false,
            });
            leaveTypeList.Add(new LeaveTypeModel
            {
                LeaveTypeId = 3,
                Name = "Special Leave",
                BackgroundColor = Brushes.Blue,
                ReportBackgroundColor = "Blue",
                HaveIncentive = true,
            });
            return leaveTypeList;
        }
    }

    public class MinusIncentiveModel
    {
        public int NumberOfDayAbsent { get; set; }
        public double MinusLevel { get; set; }

        public static List<MinusIncentiveModel> Create()
        {
            List<MinusIncentiveModel> minusIncentiveList = new List<MinusIncentiveModel>();
            minusIncentiveList.Add(new MinusIncentiveModel
            {
                NumberOfDayAbsent = 1,
                MinusLevel = 0.1,
            });
            minusIncentiveList.Add(new MinusIncentiveModel
            {
                NumberOfDayAbsent = 2,
                MinusLevel = 0.4,
            });
            minusIncentiveList.Add(new MinusIncentiveModel
            {
                NumberOfDayAbsent = 3,
                MinusLevel = 0.7,
            });
            minusIncentiveList.Add(new MinusIncentiveModel
            {
                NumberOfDayAbsent = 4,
                MinusLevel = 1,
            });

            return minusIncentiveList;
        }
    }
}
