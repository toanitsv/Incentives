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
using Microsoft.Reporting.WinForms;

using SewingIncentives.Models;
using SewingIncentives.Controllers;
using SewingIncentives.DataSets;
using SewingIncentives.Helpers;
using System.Data;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for AssemblyReportIncentives.xaml
    /// </summary>
    public partial class AssemblyReportIncentivesWindow : Window
    {
        List<MonthSWO> monthList;
        List<LinePerformanceDetailModel> linePerformanceDetailList;
        List<LinePerformanceModel> linePerformanceList;
        SectionModel section;
        List<IncentiveGradeModel> incentiveGradeList;
        List<String> positionList;
        List<LineModel> lineModelList;
        List<AssemblySpecialIncentiveModel> assemblySpecialIncentiveList;
        ComboBoxItem areaItem;
        string[] oldArr = { "F1", "F2", "F3", "F4A", "F4B" };
        string[] newArr = { "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12A", "F12B" };
        string[] monthArr = { "0", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        BackgroundWorker bwViewReport;
        BackgroundWorker bwSpecialIncentive;
        public AssemblyReportIncentivesWindow(SectionModel _section)
        {
            monthList = new List<MonthSWO>();
            this.section = _section;
            linePerformanceDetailList = new List<LinePerformanceDetailModel>();
            incentiveGradeList = new List<IncentiveGradeModel>();
            positionList = new List<String>();
            lineModelList = new List<LineModel>();
            linePerformanceList = new List<LinePerformanceModel>();
            assemblySpecialIncentiveList = new List<AssemblySpecialIncentiveModel>();
            areaItem = new ComboBoxItem();

            bwViewReport = new BackgroundWorker();
            bwViewReport.DoWork +=new DoWorkEventHandler(bwViewReport_DoWork);
            bwViewReport.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(bwViewReport_RunWorkerCompleted);

            bwSpecialIncentive = new BackgroundWorker();
            bwSpecialIncentive.DoWork +=new DoWorkEventHandler(bwSpecialIncentive_DoWork);
            bwSpecialIncentive.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(bwSpecialIncentive_RunWorkerCompleted);

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wfhAreaIncentive.Visibility = Visibility.Visible;
            gridSpecialIncentive.Visibility = Visibility.Collapsed;
            DateTime dtNow = DateTime.Now;
            string[] yearList = { (dtNow.Year - 1).ToString(), dtNow.Year.ToString(), (dtNow.Year + 1).ToString() };
            cbYear.ItemsSource = yearList;
            cbYear.SelectedItem = yearList[1];
            
            for (int i = 1; i <= 12; i++)
            {
                MonthSWO mSWO = new MonthSWO();
                mSWO.ID = i;
                mSWO.Name = monthArr[i];
                monthList.Add(mSWO);
            }

            cbMonth.ItemsSource = monthList.Select(s=>s.Name).ToList();
            cbMonth.SelectedItem = monthList.Where(w => w.ID == dtNow.Month).Select(s=>s.Name).FirstOrDefault();

        }
        int yearSearch = 0, monthSearch = 0;
        public string monthName = "", yearName = "";
        string selectArea = "";

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            if (bwViewReport.IsBusy == true)
            {
                return;
            }

            this.Cursor = Cursors.Wait;
            btnViewReport.IsEnabled = false;
            wfhAreaIncentive.Visibility = Visibility.Visible;
            gridSpecialIncentive.Visibility = Visibility.Collapsed;
            GetWindowInfor();
            bwViewReport.RunWorkerAsync();
        }
        
        private void bwViewReport_DoWork(object sender, DoWorkEventArgs e)
        {
            linePerformanceDetailList = LinePerformanceDetailController.SelectBySection(section.SectionId, monthSearch, yearSearch);
            lineModelList = LineController.Select(section.SectionId);
            if (selectArea == "Old")
            {
                linePerformanceDetailList = linePerformanceDetailList.Where(w => oldArr.Contains(w.LineId) == true).ToList();
                lineModelList = lineModelList.Where(w => oldArr.Contains(w.LineId) == true).OrderBy(o => o.Ordinal).ToList();
            }
            if (selectArea == "New")
            {
                linePerformanceDetailList = linePerformanceDetailList.Where(w => newArr.Contains(w.LineId) == true).ToList();
                lineModelList = lineModelList.Where(w => newArr.Contains(w.LineId) == true).OrderBy(o=>o.Ordinal).ToList();
            }
            incentiveGradeList = IncentiveGradeModel.Select().Where(w => w.SectionId == section.SectionId).ToList();
            positionList = linePerformanceDetailList.Select(s => s.IncentiveGrade).Distinct().ToList();
        }
        private void bwViewReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTable dt = new AssemblyIncentiveStatistics().Tables["AssemblyIncentive"];
            foreach (var line in lineModelList)
            {
                foreach (string position in positionList)
                {
                    IncentiveGradeModel incentiveGradeModel = incentiveGradeList.Where(w => w.Name == position).FirstOrDefault();
                    int incentivePerPosition = linePerformanceDetailList.Where(w => w.LineId == line.LineId && w.IncentiveGrade == position).Select(s => s.Incentive).Sum();
                    if (incentiveGradeModel != null)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Line"] = "Line " + line.Name;
                        dr["Position"] = incentiveGradeModel.Name;
                        dr["Incentive"] = incentivePerPosition;
                        dt.Rows.Add(dr);
                    }
                }
                
            }

            ReportParameter rp1 = new ReportParameter("Month", monthName.ToUpper());
            ReportParameter rp2 = new ReportParameter("Year", yearName.ToString());
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "AssemblyIncentivePerPosition";
            rds.Value = dt;

            //reportViewer.LocalReport.ReportPath = @"E:\SV PROJECT\SWO\Assembly\1.18.03.14\SewingIncentives\Reports\AssemblyIncentiveReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\AssemblyIncentiveReport.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();

            this.Cursor = null;
            btnViewReport.IsEnabled = true;
        }

        private void btnSpecialIncentive_Click(object sender, RoutedEventArgs e)
        {
            if (bwSpecialIncentive.IsBusy == true)
            {
                return;
            }

            this.Cursor = Cursors.Wait;
            btnSpecialIncentive.IsEnabled = false;

            assemblySpecialIncentiveList.Clear();
            dgSpecialIncentive.ItemsSource = null;
            wfhAreaIncentive.Visibility = Visibility.Collapsed;
            gridSpecialIncentive.Visibility = Visibility.Visible;
            GetWindowInfor();
            bwSpecialIncentive.RunWorkerAsync();
        }
        private void bwSpecialIncentive_DoWork(object sender, DoWorkEventArgs e)
        {
            linePerformanceList = LinePerformanceController.SelectBySection(section.SectionId, monthSearch, yearSearch);
            lineModelList = LineController.Select(section.SectionId).OrderBy(o=>o.Ordinal).ToList();
            foreach (var lineModel in lineModelList)
            {
                LinePerformanceModel linePerformanceModel = linePerformanceList.Where(w => w.LineId == lineModel.LineId).FirstOrDefault();
                if (linePerformanceModel != null)
                {
                    AssemblySpecialIncentiveModel assySpecialIncentive = new AssemblySpecialIncentiveModel();
                    assySpecialIncentive.Line = "Line " + lineModel.Name;
                    assySpecialIncentive.Output = linePerformanceList.Where(w => w.LineId == lineModel.LineId).Select(s => s.Output).Sum();
                    assySpecialIncentive.QuotaTarget = linePerformanceList.Where(w => w.LineId == lineModel.LineId).Select(s => s.QuotaTarget).Sum();
                    assySpecialIncentive.Lacking = assySpecialIncentive.QuotaTarget - assySpecialIncentive.Output;
                    assySpecialIncentive.PercentOutput = Math.Round((double)assySpecialIncentive.Output * 100 / (double)assySpecialIncentive.QuotaTarget, 2, MidpointRounding.AwayFromZero);

                    assemblySpecialIncentiveList.Add(assySpecialIncentive);
                }
            }
        }
        private void bwSpecialIncentive_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgSpecialIncentive.ItemsSource = assemblySpecialIncentiveList;
            this.Cursor = null;
            btnSpecialIncentive.IsEnabled = true;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            List<AssemblySpecialIncentiveModel> assySpecialIncentiveCalculateList = new List<AssemblySpecialIncentiveModel>();
            List<AssemblySpecialIncentiveModel> assySpecialIncentiveTempList = new List<AssemblySpecialIncentiveModel>();

            assySpecialIncentiveTempList = dgSpecialIncentive.Items.OfType<AssemblySpecialIncentiveModel>().ToList();

            foreach (var assySpecialIncentive in assySpecialIncentiveTempList)
            {
                AssemblySpecialIncentiveModel assySpecialIncentiveTemp = new AssemblySpecialIncentiveModel();
                assySpecialIncentiveTemp.Line = assySpecialIncentive.Line;
                assySpecialIncentiveTemp.Output = assySpecialIncentive.Output;
                assySpecialIncentiveTemp.QuotaTarget = assySpecialIncentive.QuotaTarget;

                assySpecialIncentiveTemp.Lacking = assySpecialIncentiveTemp.QuotaTarget - assySpecialIncentiveTemp.Output;
                assySpecialIncentiveTemp.PercentOutput = Math.Round((double)assySpecialIncentiveTemp.Output * 100 / (double)assySpecialIncentiveTemp.QuotaTarget, 2, MidpointRounding.AwayFromZero);

                assySpecialIncentiveTemp.SUPIncentive = assySpecialIncentive.SUPIncentive;
                assySpecialIncentiveTemp.LLT1MECHIncentive = assySpecialIncentive.LLT1MECHIncentive;
                assySpecialIncentiveTemp.LL23Incentive = assySpecialIncentive.LL23Incentive;
                assySpecialIncentiveTemp.WorkerIncentive = assySpecialIncentive.WorkerIncentive;

                assySpecialIncentiveCalculateList.Add(assySpecialIncentiveTemp);
            }

            dgSpecialIncentive.ItemsSource = null;
            dgSpecialIncentive.ItemsSource = assySpecialIncentiveCalculateList;
        }

        private void GetWindowInfor()
        {
            monthName = cbMonth.SelectedItem as string;
            yearName = cbYear.SelectedValue as string;
            Int32.TryParse(yearName, out yearSearch);

            areaItem = cbArea.SelectedItem as ComboBoxItem;
            selectArea = areaItem.Name;
            monthSearch = monthList.Where(w => w.Name == monthName).Select(s => s.ID).FirstOrDefault();
        }
        private class MonthSWO
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        private void btnPrintReport_Click(object sender, RoutedEventArgs e)
        {
            List<AssemblySpecialIncentiveModel> tranferList = dgSpecialIncentive.Items.OfType<AssemblySpecialIncentiveModel>().ToList();
            AssemblySpecialPrintWindow window = new AssemblySpecialPrintWindow(tranferList, monthName, yearName);
            window.Show();
        }
    }
}
