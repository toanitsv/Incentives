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
    public partial class LineIncentiveSummaryWindow : Window
    {
        SectionModel section;
        readonly BackgroundWorker threadLoad;
        List<LineModel> lineList;
        List<PersonalModel> personalList;
        readonly BackgroundWorker threadShow;
        List<LeaveTypeModel> leaveTypeList;
        List<MinusIncentiveModel> minusIncentiveList;        
        int month;
        int year;
        string line;
        List<LinePerformanceDetailModel> performanceDetailList;
        public LineIncentiveSummaryWindow(SectionModel section)
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
            leaveTypeList = LeaveTypeModel.Create();
            minusIncentiveList = MinusIncentiveModel.Create();
            performanceDetailList = new List<LinePerformanceDetailModel>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int currentMonth = DateTime.Now.Month;
            int[] monthArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            cboMonth.ItemsSource = monthArray;
            cboMonth.SelectedItem = currentMonth;
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
            month = (cboMonth.SelectedItem as int?).Value;
            year = (cboYear.SelectedItem as int?).Value;
            line = cboLine.SelectedItem as string;
            if (new DateTime(year, month, 1) > DateTime.Now || String.IsNullOrEmpty(line) == true)
            {
                return;
            }            
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
            List<SourceModel> sourceList = SourceController.Select(line, year, month);
            List<WorkerLeaveModel> workerLeaveList = WorkerLeaveController.Select(year, month);
            performanceDetailList = LinePerformanceDetailController.SelectByPersonal(line, year, month);
            List<HolidayModel> holidayList = HolidayController.Select(year, month);

            object[] results = { personalList_D1, sourceList, workerLeaveList, performanceDetailList, holidayList };
            e.Result = results;
        }

        void threadShow_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] results = e.Result as object[];            

            List<PersonalModel> personalList_D1 = results[0] as List<PersonalModel>;
            List<SourceModel> sourceList = results[1] as List<SourceModel>;
            List<WorkerLeaveModel> workerLeaveList = results[2] as List<WorkerLeaveModel>;
            //List<LinePerformanceDetailModel> performanceDetailList = results[6] as List<LinePerformanceDetailModel>;
            List<HolidayModel> holidayList = results[4] as List<HolidayModel>;
            List<WorkerFilterModel> workerFilterList = WorkerFilterModel.Create(section.SectionId);

            DataTable dt = new LineIncentiveSummaryDataSet().Tables["LineIncentiveSummaryTable"];
            int i = 1;
            foreach (PersonalModel personal in personalList_D1)
            {
                List<LinePerformanceDetailModel> performanceDetailList_D1 = performanceDetailList.Where(p => p.WorkerId == personal.WorkerId).ToList();
                List<String> icentiveGradeList = performanceDetailList_D1.Select(p => p.IncentiveGrade).Distinct().OrderBy(g => g).ToList();
                DataRow dr = dt.NewRow();
                dr["NumberOf"] = i;
                dr["WorkerId"] = personal.WorkerId;
                dr["Name"] = personal.Name;
                string incentiveGrade = "";
                for (int l = 0; l <= icentiveGradeList.Count - 1; l++)
                {
                    incentiveGrade += String.Format("{0}; ", icentiveGradeList[l]);                    
                }
                //dr["Position"] = personal.Position;
                dr["Position"] = incentiveGrade;                
                int numberOfDayAbsent = CalculateNumberOfDayAbsent(year, month, 
                    sourceList.Where(s => s.CardId == personal.CardId).ToList(), 
                    workerLeaveList.Where(w => w.WorkerId == personal.WorkerId).ToList(), holidayList);                
                dr["NumberOfDayAbsent"] = numberOfDayAbsent;
                double minusLevel = 0;                
                MinusIncentiveModel minusIncentiveLast = minusIncentiveList.OrderBy(m => m.NumberOfDayAbsent).Last();
                if (numberOfDayAbsent >= minusIncentiveLast.NumberOfDayAbsent)
                {
                    minusLevel = minusIncentiveLast.MinusLevel;
                }
                else
                {
                    MinusIncentiveModel minusIncentive = minusIncentiveList.Where(m => m.NumberOfDayAbsent == numberOfDayAbsent).FirstOrDefault();
                    if (minusIncentive != null)
                    {
                        minusLevel = minusIncentive.MinusLevel;
                    }
                }

                //double workerRatio = 0;
                //DateTime firstDate = new DateTime(year, month, 1);
                //WorkerFilterModel workerFilter = workerFilterList.Where(w => CalculateHelper.CalculateMonth(personal.HiredDate, firstDate) >= w.MinMonth && CalculateHelper.CalculateMonth(personal.HiredDate, firstDate) < w.MaxMonth).FirstOrDefault();
                //if (workerFilter != null)
                //{
                //    workerRatio = workerFilter.Ratio;
                //}

                double incentive = performanceDetailList_D1.Select(p => (p.Incentive + p.SpecsIncentive + p.ExcessIncentive)).Sum();
                dr["Incentive"] = incentive - (minusLevel * incentive);
                dt.Rows.Add(dr);
                i++;
            }
            dgMain.ItemsSource = null;
            dgMain.ItemsSource = dt.AsDataView();

            List<String> incentiveGradeList = performanceDetailList.Select(p => p.IncentiveGrade).Distinct().OrderBy(g => g).ToList();
            DataTable dtIncentiveGradeSummary = new IncentiveGradeSummaryDataSet().Tables["IncentiveGradeSummaryTable"];
            foreach (string incentiveGrade in incentiveGradeList)
            {
                DataRow dr = dtIncentiveGradeSummary.NewRow();
                dr["IncentiveGrade"] = incentiveGrade;
                dr["Count"] = performanceDetailList.Where(p => p.IncentiveGrade == incentiveGrade).Select(p => p.WorkerId).Distinct().Count();
                dtIncentiveGradeSummary.Rows.Add(dr);
            }
            dgIncentiveGradeSummary.ItemsSource = null;
            dgIncentiveGradeSummary.ItemsSource = dtIncentiveGradeSummary.AsDataView();

            btnShow.IsEnabled = true;
            btnPrint.IsEnabled = true;            

            this.Cursor = null;
        }

        private int CalculateNumberOfDayAbsent(int year, int month, List<SourceModel> sourceList, List<WorkerLeaveModel> workerLeaveList, List<HolidayModel> holidayList)
        {
            int numberOfDayAbsent = 0;
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddDays(DateTime.DaysInMonth(year, month) - 1);
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                SourceModel source = sourceList.Where(s => s.Date.Date == date.Date).FirstOrDefault();
                if (source == null && date.DayOfWeek != DayOfWeek.Sunday && holidayList.Select(h => h.Date.Date).Contains(date.Date) == false)
                {
                    List<WorkerLeaveModel> workerLeaveList_D1 = workerLeaveList.Where(w => w.StartDate.Date <= date.Date && date.Date <= w.EndDate.Date).ToList();
                    if (workerLeaveList_D1.Count > 0)
                    {
                        WorkerLeaveModel workerLeave = workerLeaveList_D1.OrderBy(w => w.CreatedDate).Last();
                        LeaveTypeModel leaveType = leaveTypeList.Where(l => l.LeaveTypeId == workerLeave.LeaveType).FirstOrDefault();
                        if (leaveType != null && leaveType.HaveIncentive == false)
                        {
                            numberOfDayAbsent += 1;
                        }
                    }
                    else
                    {
                        numberOfDayAbsent += 1;
                    }
                }
            }
            return numberOfDayAbsent;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtReport = (dgMain.ItemsSource as DataView).ToTable();
            LineIncentiveSummaryPrintWindow window = new LineIncentiveSummaryPrintWindow(dtReport, year, month, line);
            window.Show();
        }

        private void dgMain_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && dgMain.CurrentItem != null)
            {
                DataRow dr = (dgMain.CurrentItem as DataRowView).Row;
                if (dr != null)
                {
                    string workerId = dr["WorkerId"] as string;
                    List<LinePerformanceDetailModel> performanceDetailList_D1 = performanceDetailList.Where(p => p.WorkerId == workerId).OrderBy(p => p.Date).ToList();
                    List<LineIncentiveSummaryDetailViewModel> lineIncentiveSummaryDetailViewList = new List<LineIncentiveSummaryDetailViewModel>();
                    foreach (LinePerformanceDetailModel performanceDetail in performanceDetailList_D1)
                    {
                        LineModel workAtLine = lineList.Where(l => l.LineId == performanceDetail.LineId).FirstOrDefault();
                        LineIncentiveSummaryDetailViewModel lineIncentiveSummaryDetailView = new LineIncentiveSummaryDetailViewModel
                        {
                            Date = performanceDetail.Date,
                            WorkAtLine = performanceDetail.LineId,
                            OthersPosition = performanceDetail.OthersPosition,
                            AdjustTimeOut = performanceDetail.TimeOut,
                            IncentiveGrade = performanceDetail.IncentiveGrade,
                            Incentive = performanceDetail.Incentive + performanceDetail.ExcessIncentive,
                            SpecsIncentive = performanceDetail.SpecsIncentive,
                        };
                        if (workAtLine != null)
                        {
                            lineIncentiveSummaryDetailView.WorkAtLine = string.Format("{0} ({1})", workAtLine.Name, workAtLine.LineId);
                        }
                        lineIncentiveSummaryDetailViewList.Add(lineIncentiveSummaryDetailView);
                    }

                    LineIncentiveSummaryDetailWindow window = new LineIncentiveSummaryDetailWindow(workerId, lineIncentiveSummaryDetailViewList);
                    window.ShowDialog();
                }
            }
        }
        
    }
}

