using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingCheckIn.Models
{
    public class PersonalModel
    {
        public string CardId { get; set; }
        public string WorkerId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime HiredDate { get; set; }
    }
}
