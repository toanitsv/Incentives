using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using SewingIncentives.Models;
namespace SewingIncentives.ViewModels
{
    public class WorkerLeaveViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int _WorkerLeaveId;
        public int WorkerLeaveId
        {
            get { return _WorkerLeaveId; }
            set
            {
                _WorkerLeaveId = value;
                OnPropertyChanged("WorkerLeaveId");
            }
        }

        private string _WorkerId;
        public string WorkerId
        {
            get { return _WorkerId; }
            set
            {
                _WorkerId = value;
                OnPropertyChanged("WorkerId");
            }
        }

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                _StartDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        private LeaveTypeModel _LeaveType;
        public LeaveTypeModel LeaveType
        {
            get { return _LeaveType; }
            set
            {
                _LeaveType = value;
                OnPropertyChanged("LeaveType");
            }
        }
    }
}
