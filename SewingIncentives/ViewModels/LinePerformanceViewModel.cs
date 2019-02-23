using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SewingIncentives.Models;
using System.ComponentModel;

namespace SewingIncentives.ViewModels
{
    public class LinePerformanceViewModel : INotifyPropertyChanged
    {
        public string CardId { get; set; }
        public string WorkerId { get; set; }
        public string Name { get; set; }
        public string Line { get; set; }
        public DateTime HiredDate { get; set; }
        public double WorkerRatio { get; set; }
        public string Position { get; set; }
        public string OthersPosition { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string AdjustTimeOut { get; set; }
        public double WorkHour { get; set; } 
        public IncentiveGradeModel IncentiveGrade { get; set; }
        public int Incentive { get; set; }
        public int SpecsIncentive { get; set; }

        private string _AdjustTimeIn;
        public string AdjustTimeIn
        {
            get { return _AdjustTimeIn; }
            set
            {
                _AdjustTimeIn = value;
                OnPropertyChanged("AdjustTimeIn");
            }
        }
        private int _ExcessIncentive;
        public int ExcessIncentive
        {
            get { return _ExcessIncentive; }
            set
            {
                _ExcessIncentive = value;
                OnPropertyChanged("ExcessIncentive");
            }
        }

        public int CumulativeIncentive { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
