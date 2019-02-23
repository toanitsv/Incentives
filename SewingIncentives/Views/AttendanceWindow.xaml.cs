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

using SewingIncentives.Controllers;
using SewingIncentives.Models;
using System.Data;
using SewingIncentives.DataSets;
namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for AttendanceWindow.xaml
    /// </summary>
    public partial class AttendanceWindow : Window
    {
        SectionModel section;
        BackgroundWorker threadLoad;        
        DataTable dt;
        BackgroundWorker threadShow;        
        List<PersonalModel> personalList;
        List<WorkerLeaveModel> workerLeaveList;
        DataTable dtPrint;
        String line;
        List<LeaveTypeModel> leaveTypeList;
        List<HolidayModel> holidayList;
        int month;
        int year;
        public AttendanceWindow(SectionModel section)
        {
            this.section = section;
            threadLoad = new BackgroundWorker();
            threadLoad.WorkerSupportsCancellation = true;
            threadLoad.DoWork += new DoWorkEventHandler(threadLoad_DoWork);
            threadLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadLoad_RunWorkerCompleted);
            dt = new DataTable();
            threadShow = new BackgroundWorker();
            threadShow.DoWork += new DoWorkEventHandler(threadShow_DoWork);
            threadShow.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadShow_RunWorkerCompleted);
            dtPrint = new AttendanceDataSet().Tables["AttendanceTable"];
            leaveTypeList = LeaveTypeModel.Create();
            holidayList = new List<HolidayModel>();
            InitializeComponent();
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
            personalList = PersonalController.Select(section.Keyword);            
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
            dgMain.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Clear();

            DataGridTextColumn dgcNumberOf = new DataGridTextColumn();
            dgcNumberOf.Header = "No.";
            dgcNumberOf.Binding = new Binding("NumberOf");
            dgMain.Columns.Add(dgcNumberOf);
            dt.Columns.Add("NumberOf", typeof(Int32));

            DataGridTextColumn dgcName = new DataGridTextColumn();
            dgcName.Header = "Name";
            dgcName.Binding = new Binding("Name");
            dgMain.Columns.Add(dgcName);
            dt.Columns.Add("Name", typeof(String));

            DataGridTextColumn dgcID = new DataGridTextColumn();
            dgcID.Header = "ID #";
            dgcID.Binding = new Binding("WorkerId");
            dgMain.Columns.Add(dgcID);
            dt.Columns.Add("WorkerId", typeof(String));

            year = (cboYear.SelectedItem as int?).Value;
            month = (cboMonth.SelectedItem as int?).Value;
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddDays(DateTime.DaysInMonth(year, month) - 1);
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                DataGridTextColumn dgcDate = new DataGridTextColumn();
                dgcDate.MinWidth = 25;
                dgcDate.Header = date.Day.ToString();
                dgcDate.Binding = new Binding(String.Format("Day_{0}", date.Day));

                Style style = new Style(typeof(DataGridCell));
                style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center));
                Setter setter = new Setter();
                setter.Property = DataGridCell.BackgroundProperty;
                setter.Value = new Binding(String.Format("Day_{0}_Background", date.Day));
                style.Setters.Add(setter);
                dgcDate.CellStyle = style;

                dgMain.Columns.Add(dgcDate);

                dt.Columns.Add(String.Format("Day_{0}", date.Day), typeof(String));
                dt.Columns.Add(String.Format("Day_{0}_Background", date.Day), typeof(Brush));
            }

            line = cboLine.SelectedItem as String;
            if (String.IsNullOrEmpty(line) == false && threadShow.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                btnShow.IsEnabled = false;
                object[] arguments = { line };                
                threadShow.RunWorkerAsync(arguments);
            }
        }

        void threadShow_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arguments = e.Argument as object[];
            string line = arguments[0] as String;            
            workerLeaveList = WorkerLeaveController.Select(year, month);
            holidayList = HolidayController.Select(year, month);
            e.Result = SourceController.Select(line, year, month);
        }

        void threadShow_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<SourceModel> sourceList = e.Result as List<SourceModel>;
            
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddDays(DateTime.DaysInMonth(year, month) - 1);

            List<PersonalModel> personalList_D1 = personalList.Where(p => p.Department == line).OrderBy(p => p.WorkerId).ToList();
            List<DateTime> holidayList_D1 = holidayList.Select(h => h.Date.Date).ToList();
            dtPrint.Rows.Clear();
            int i = 1;
            foreach (PersonalModel personal in personalList_D1)
            {                
                List<SourceModel> sourceList_D1 = sourceList.Where(w => w.CardId == personal.CardId).ToList();
                List<WorkerLeaveModel> workerLeaveList_D1 = workerLeaveList.Where(w => w.WorkerId.ToLower() == personal.WorkerId.ToLower()).ToList();
                DataRow dr = dt.NewRow();
                dr["NumberOf"] = i;
                if (personal != null)
                {
                    dr["Name"] = personal.Name;
                }
                dr["WorkerId"] = personal.WorkerId;
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {                    
                    DataRow drPrint = dtPrint.NewRow();
                    drPrint["NumberOf"] = i;
                    if (personal != null)
                    {
                        drPrint["Name"] = personal.Name;
                    }
                    drPrint["WorkerId"] = personal.WorkerId;
                    drPrint["Day"] = date.Day;
                    SourceModel source = sourceList_D1.Where(w => w.Date.Date == date.Date).FirstOrDefault();
                    if (source != null)
                    {
                        dr[String.Format("Day_{0}", date.Day)] = "√";
                        dr[String.Format("Day_{0}_Background", date.Day)] = Brushes.LightGray;

                        drPrint["Working"] = "√";
                        drPrint["Background"] = "LightGrey";
                    }
                    else
                    {
                        dr[String.Format("Day_{0}_Background", date.Day)] = Brushes.Transparent;
                        drPrint["Background"] = "Transparent";
                        if (date.DayOfWeek != DayOfWeek.Sunday && holidayList_D1.Contains(date.Date) == false)
                        {
                            List<WorkerLeaveModel> workerLeaveList_D2 = workerLeaveList_D1.Where(w => w.StartDate.Date <= date.Date && date.Date <= w.EndDate.Date).ToList();
                            if (workerLeaveList_D2.Count > 0)
                            {
                                WorkerLeaveModel workerLeave = workerLeaveList_D2.OrderBy(w => w.CreatedDate).Last();
                                LeaveTypeModel leaveType = leaveTypeList.Where(l => l.LeaveTypeId == workerLeave.LeaveType).FirstOrDefault();
                                if(leaveType != null)
                                {
                                    dr[String.Format("Day_{0}_Background", date.Day)] = leaveType.BackgroundColor;
                                    drPrint["Background"] = leaveType.ReportBackgroundColor;
                                }                                
                            }
                            else
                            {
                                dr[String.Format("Day_{0}_Background", date.Day)] = Brushes.Red;
                                drPrint["Background"] = "Red";
                            }
                        }                        

                        drPrint["Working"] = "";
                    }
                    dtPrint.Rows.Add(drPrint);
                }
                dt.Rows.Add(dr);
                i = i + 1;
            }
            dgMain.ItemsSource = null;
            dgMain.ItemsSource = dt.AsDataView();
            btnPrint.IsEnabled = true;
            btnShow.IsEnabled = true;
            this.Cursor = null;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            bool includeBackground = chboIncludeBackground.IsChecked.Value;
            AttendancePrintWindow window = new AttendancePrintWindow(dtPrint, year, month, line,includeBackground);
            window.Show();
        }
    }
}
