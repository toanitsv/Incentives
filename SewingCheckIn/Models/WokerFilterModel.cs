using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingCheckIn.Models
{    

    public class WokerFilterModel
    {
        public static List<WokerFilterModel> Create()
        {
            List<WokerFilterModel> workerFilters = new List<WokerFilterModel>();
            workerFilters.Add(new WokerFilterModel { Name = "New Worker", MinValue = 0, MaxValue = 1, Ratio = 0 });
            workerFilters.Add(new WokerFilterModel { Name = "> 1 Month", MinValue = 1, MaxValue = 2, Ratio = 0.5 });
            workerFilters.Add(new WokerFilterModel { Name = "> 2 Month", MinValue = 2, MaxValue = 3, Ratio = 0.8 });
            workerFilters.Add(new WokerFilterModel { Name = "Old Worker", MinValue = 3, MaxValue = 0, Ratio = 1 });

            return workerFilters;
        }

        public string Name { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public double Ratio { get; set; }
    }
}
