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
using Microsoft.Reporting.WinForms;
using SewingIncentives.Helpers;
namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for LineSummaryPrintWindow.xaml
    /// </summary>
    public partial class LineSummaryPrintWindow : Window
    {
        SectionModel section;
        BackgroundWorker bwLoadData;
        List<LineModel> lineList;
        List<PersonalModel> personalList;
        BackgroundWorker bwPrint;
        DateTime date;
        List<WorkerLoginModel> workerLoginList;
        List<WorkerFilterModel> workerFilterList;
        List<LinePerformanceDetailModel> linePerformanceDetailList;
        List<PositionModel> positionList;
        List<OtherPositionModel> otherPositionList;
        public LineSummaryPrintWindow(SectionModel section)
        {
            this.section = section;
            bwLoadData = new BackgroundWorker();
            bwLoadData.DoWork += new DoWorkEventHandler(bwLoadData_DoWork);
            bwLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadData_RunWorkerCompleted);
            lineList = new List<LineModel>();
            personalList = new List<PersonalModel>();
            bwPrint = new BackgroundWorker();
            date = new DateTime(2000, 1, 1);
            bwPrint.DoWork += new DoWorkEventHandler(bwPrint_DoWork);
            bwPrint.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwPrint_RunWorkerCompleted);
            workerLoginList = new List<WorkerLoginModel>();
            workerFilterList = new List<WorkerFilterModel>();
            linePerformanceDetailList = new List<LinePerformanceDetailModel>();
            positionList = new List<PositionModel>();
            otherPositionList = new List<OtherPositionModel>();
            InitializeComponent();
        }        

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            lineList = LineController.Select(section.SectionId);
            personalList = PersonalController.Select();
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.IsEnabled = true;
            this.Cursor = null;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (bwPrint.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                date = dpDate.SelectedDate.Value;
                btnPrint.IsEnabled = false;
                bwPrint.RunWorkerAsync();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            positionList = PositionModel.Init();
            otherPositionList = OtherPositionModel.Init();

            dpDate.SelectedDate = DateTime.Now;
            workerFilterList = WorkerFilterModel.Create(section.SectionId);
            if (bwLoadData.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                bwLoadData.RunWorkerAsync();
            }
        }

        private void bwPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            workerLoginList = WorkerLoginController.SelectByLine(section.SectionId, date);
            linePerformanceDetailList = LinePerformanceDetailController.SelectBySection(section.SectionId, date);
        }

        private void bwPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTable dt = new LineSummaryDataSet().Tables["LineSummaryTable"];            
            foreach (LineModel line in lineList)
            {
                List<WorkerLoginModel> workerLoginLineList = workerLoginList.Where(w => w.LineId == line.LineId).ToList();
                List<String> workerIdList = workerLoginLineList.Select(w => w.WorkerId).Distinct().ToList();
                List<PersonalModel> personalLineList = personalList.Where(p => workerIdList.Contains(p.WorkerId) == true).ToList();

                int countTotalWorker = personalLineList.Where(p => p.Department != "ASSEMBLY QUALITY" && positionList.Where(p1 => p1.IsCalculate == true).Select(p1 => p1.Name).Contains(p.Position) == true).Count();
                double countTotalMP = 0;
                int countNewWorker = 0;
                int count1Month = 0;
                int count2Months = 0;
                int countOldWorker = 0;
                foreach (PersonalModel personal in personalLineList)
                {
                    WorkerFilterModel workerFilter = workerFilterList.Where(w => CalculateHelper.CalculateMonth(personal.HiredDate, date) >= w.MinMonth && CalculateHelper.CalculateMonth(personal.HiredDate, date) < w.MaxMonth).FirstOrDefault();
                    LinePerformanceDetailModel linePerformanceDetail = linePerformanceDetailList.Where(l => l.CardId == personal.CardId && l.WorkerId == personal.WorkerId).FirstOrDefault();
                    if (personal.Department != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(personal.Position) == true && workerFilter != null && (linePerformanceDetail == null || otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(linePerformanceDetail.OthersPosition) == true))
                    {
                        countTotalMP += workerFilter.Ratio;
                    }
                    if (personal.Department != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(personal.Position) == true && CalculateHelper.CalculateMonth(personal.HiredDate, date) < 1 && (linePerformanceDetail == null || otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(linePerformanceDetail.OthersPosition) == true))
                    {
                        countNewWorker += 1;
                    }
                    else if (personal.Department != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(personal.Position) == true && CalculateHelper.CalculateMonth(personal.HiredDate, date) >= 1 && CalculateHelper.CalculateMonth(personal.HiredDate, date) < 2 && (linePerformanceDetail == null || otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(linePerformanceDetail.OthersPosition) == true))
                    {
                        count1Month += 1;
                    }
                    else if (personal.Department != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(personal.Position) == true && CalculateHelper.CalculateMonth(personal.HiredDate, date) >= 2 && CalculateHelper.CalculateMonth(personal.HiredDate, date) < 3 && (linePerformanceDetail == null || String.IsNullOrEmpty(linePerformanceDetail.OthersPosition) == true || otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(linePerformanceDetail.OthersPosition) == true))
                    {
                        count2Months += 1;
                    }
                    else if (personal.Department != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(personal.Position) == true && CalculateHelper.CalculateMonth(personal.HiredDate, date) >= 3 && (linePerformanceDetail == null || String.IsNullOrEmpty(linePerformanceDetail.OthersPosition) == true || otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(linePerformanceDetail.OthersPosition) == true))
                    {
                        countOldWorker += 1;
                    }
                }               

                int countSUP = personalLineList.Where(p => p.Position.Contains("SUPERVISOR") == true).Count();
                int countMECH = personalLineList.Where(p => p.Position.Contains("MECHANIC") == true).Count();
                int countMONITOR = personalLineList.Where(p => p.Position.Contains("MONITOR") == true || p.Position.Contains("MAKE BALANCE") == true).Count();
                int countLL = personalLineList.Where(p => p.Position.Contains("LINE LEADER") == true).Count();
                int countQC = personalLineList.Where(p => p.Department == "ASSEMBLY QUALITY" ||  p.Position.Contains("QC") == true).Count();

                DataRow dr = dt.NewRow();
                dr["Line"] = line.Name;
                dr["CountTotalWorker"] = countTotalWorker.ToString();
                dr["CountTotalMP"] = countTotalMP.ToString();
                dr["CountNewWorker"] = countNewWorker.ToString();
                dr["Count1Month"] = count1Month.ToString();
                dr["Count2Months"] = count2Months.ToString();
                dr["CountOldWorker"] = countOldWorker.ToString();

                dr["CountSUP"] = countSUP.ToString();
                dr["CountMECH"] = countMECH.ToString();
                dr["CountMONITOR"] = countMONITOR.ToString();
                dr["CountLL"] = countLL.ToString();
                dr["CountQC"] = countQC.ToString();

                dt.Rows.Add(dr);
            }            

            ReportParameter rp1 = new ReportParameter("Date", String.Format("{0:dd/MM/yyyy}", date));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "LineSummary";
            rds.Value = dt;

            //reportViewer.LocalReport.ReportPath = @"C:\Users\IT02\Documents\Visual Studio 2010\Projects\Sewing Office Solution\SewingIncentives\Reports\LineSumaryReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\LineSumaryReport.rdlc";  
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();

            btnPrint.IsEnabled = true;
            this.Cursor = null;
        }

    }
}
