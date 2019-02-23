using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.ViewModels
{
    public class WorkerLoginViewModel
    {
        public int WorkerLoginId { get; set; }
        public string CardId { get; set; }
        public string WorkerId { get; set; }
        public string Name { get; set; }
        public string Line { get; set; }
        public string LineWork { get; set; }
        public string Position { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
