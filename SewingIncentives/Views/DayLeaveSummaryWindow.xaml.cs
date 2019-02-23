using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using SewingIncentives.Models;
using SewingIncentives.Controllers;
using System.Data;
using SewingIncentives.DataSets;
using SewingIncentives.ViewModels;
using SewingIncentives.Helpers;

namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for LineIncentiveSummaryWindow.xaml
    /// </summary>
    public partial class DayLeaveSummaryWindow : Window
    {
        SectionModel section;
        readonly BackgroundWorker threadLoad;
        List<LineModel> lineList;
        List<PersonalModel> personalList;
        readonly BackgroundWorker threadShow;
        int year;
        string line;
        public DayLeaveSummaryWindow(SectionModel section)
        {
            InitializeComponent();
            this.section = section;
            threadLoad = new BackgroundWorker();
            threadLoad.WorkerSupportsCancellation = true;
            threadLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadLoad_RunWorkerCompleted);
            threadLoad.DoWork += new DoWorkEventHandler(threadLoad_DoWork);
            lineList = new List<LineModel>();
            personalList = new List<PersonalModel>();
            threadShow = new BackgroundWorker();
            threadShow.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadShow_RunWorkerCompleted);
            threadShow.DoWork += new DoWorkEventHandler(threadShow_DoWork);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            int[] yearArray = { currentYear - 1, currentYear };
            cboYear.ItemsSource = yearArray;
            cboYear.SelectedItem = currentYear;
            if (threadLoad.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                threadLoad.RunWorkerAsync();
            }
        }

        void threadLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            lineList = LineController.Select();
            personalList = PersonalController.Select(section.Keyword_1);
        }

        void threadLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<String> lineList = personalList.Select(l => l.Department).Distinct().OrderBy(l => l).ToList();
            cboLine.ItemsSource = lineList;
            btnShow.IsEnabled = true;
            this.Cursor = null;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            year = (cboYear.SelectedItem as int?).Value;
            line = cboLine.SelectedItem as string;
            if (threadShow.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                btnShow.IsEnabled = false;
                threadShow.RunWorkerAsync();
            }
        }

        void threadShow_DoWork(object sender, DoWorkEventArgs e)
        {
            List<PersonalModel> personalList_D1 = personalList.Where(p => p.Department == line).OrderBy(p => p.WorkerId).ToList();

            List<WorkerLeaveModel> workerLeaveList = WorkerLeaveController.Select(year, 1, line);
            object[] results = { personalList_D1, workerLeaveList };
            e.Result = results;
        }

        void threadShow_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnShow.IsEnabled = true;
            this.Cursor = null;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            object[] results = e.Result as object[];

            List<PersonalModel> personalList_D1 = results[0] as List<PersonalModel>;
            List<WorkerLeaveModel> workerLeaveList = results[1] as List<WorkerLeaveModel>;

            List<DayLeaveSummaryViewModel> dayLeaveSummaryViewList = new List<DayLeaveSummaryViewModel>();
            int i = 1;
            foreach (PersonalModel personal in personalList_D1)
            {
                DayLeaveSummaryViewModel dayLeaveSummaryView = new DayLeaveSummaryViewModel
                {
                    NO = i,
                    WorkerId = personal.WorkerId,
                    Name = personal.Name,
                    DateHired = personal.HiredDate.Date,
                    TotalDay = 0,
                    LeaveDay = 0,
                    Remaining = 0,
                };

                //Cal Total Day
                int totalDayLeave = 0;
                DateTime mockEndDate = new DateTime(year, 12, 31);
                DateTime hiredDate = personal.HiredDate.Date;
                if (year < hiredDate.Year)
                {
                    totalDayLeave = 0;
                }
                else if (year == hiredDate.Year)
                {
                    int totalMonth = 0;
                    totalMonth = mockEndDate.Month - hiredDate.Month;
                    if (hiredDate.Day == 1)
                    {
                        totalMonth += 1;
                    }                    
                    totalDayLeave = (int)Math.Round((double)((double)totalMonth * 14 / 12), MidpointRounding.ToEven);
                }
                else
                {
                    totalDayLeave = 14 + (year - hiredDate.Year) / 5;
                }
                
                //Cal Day Leave
                int leaveDay = 0;
                List<WorkerLeaveModel> workerLeavebyWorkerList = workerLeaveList.Where(w => w.WorkerId == personal.WorkerId).ToList();
                foreach (WorkerLeaveModel workerLeave in workerLeavebyWorkerList)
                {
                    for (DateTime d = workerLeave.StartDate.Date; d <= workerLeave.EndDate.Date; d = d.AddDays(1))
                    {
                        if (d.Year == year)
                        {
                            leaveDay += 1;
                        }
                    }
                }


                dayLeaveSummaryView.TotalDay = totalDayLeave;
                dayLeaveSummaryView.LeaveDay = leaveDay;
                dayLeaveSummaryView.Remaining = totalDayLeave - leaveDay;
                dayLeaveSummaryViewList.Add(dayLeaveSummaryView);
                i++;
            }

            dgMain.ItemsSource = null;
            dgMain.ItemsSource = dayLeaveSummaryViewList;
        }
    }
}

