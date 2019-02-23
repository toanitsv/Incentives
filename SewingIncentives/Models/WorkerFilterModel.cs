using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    class WorkerFilterModel
    {
        public static List<WorkerFilterModel> Create(string sectionId)
        {
            //Sewing
            List<WorkerFilterModel> workerFilterList_E = new List<WorkerFilterModel>();
            workerFilterList_E.Add(new WorkerFilterModel
            {
                MinMonth = -999,
                MaxMonth = 1,
                Ratio = 0
            });
            workerFilterList_E.Add(new WorkerFilterModel
            {
                MinMonth = 1,
                MaxMonth = 2,
                Ratio = 0.5
            });
            workerFilterList_E.Add(new WorkerFilterModel
            {
                MinMonth = 2,
                MaxMonth = 3,
                Ratio = 0.8
            });
            workerFilterList_E.Add(new WorkerFilterModel
            {
                MinMonth = 3,
                MaxMonth = 999,
                Ratio = 1
            });

            //Assembly
            List<WorkerFilterModel> workerFilterList_F = new List<WorkerFilterModel>();
            workerFilterList_F.Add(new WorkerFilterModel
            {
                MinMonth = -999,
                MaxMonth = 1,
                Ratio = 0.5
            });
            workerFilterList_F.Add(new WorkerFilterModel
            {
                MinMonth = 1,
                MaxMonth = 999,
                Ratio = 1
            });            

            //Others
            List<WorkerFilterModel> workerFilterList = new List<WorkerFilterModel>();
            workerFilterList.Add(new WorkerFilterModel
            {
                MinMonth = -999,
                MaxMonth = 1,
                Ratio = 1,
            });
            workerFilterList.Add(new WorkerFilterModel
            {
                MinMonth = 1,
                MaxMonth = 2,
                Ratio = 1
            });
            workerFilterList.Add(new WorkerFilterModel
            {
                MinMonth = 2,
                MaxMonth = 3,
                Ratio = 1
            });
            workerFilterList.Add(new WorkerFilterModel
            {
                MinMonth = 3,
                MaxMonth = 999,
                Ratio = 1
            });

            if (sectionId == "E")
            {
                return workerFilterList_E;
            }
            if (sectionId == "F")
            {
                return workerFilterList_F;
            }
            return workerFilterList;
        }

        public int MinMonth { get; set; }
        public int MaxMonth { get; set; }
        public double Ratio { get; set; }
    }
}
