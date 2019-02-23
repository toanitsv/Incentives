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
namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for ExcessWorkerPrintWindow.xaml
    /// </summary>
    public partial class GroupIncentivePrintWindow : Window
    {
        SectionModel section;
        List<LineModel> lineList;
        BackgroundWorker bwLoadData;
        BackgroundWorker bwPrint;        
        List<LinePerformanceModel> linePerformanceList;
        List<IncentiveGradeModel> incentiveGradeList;
        public GroupIncentivePrintWindow(SectionModel section)
        {
            this.section = section;
            bwLoadData = new BackgroundWorker();
            bwLoadData.DoWork += new DoWorkEventHandler(bwLoadData_DoWork);
            bwLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadData_RunWorkerCompleted);

            bwPrint = new BackgroundWorker();            
            bwPrint.DoWork += new DoWorkEventHandler(bwPrint_DoWork);
            bwPrint.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwPrint_RunWorkerCompleted);
            linePerformanceList = new List<LinePerformanceModel>();

            incentiveGradeList = new List<IncentiveGradeModel>();
            InitializeComponent();
        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            lineList = LineController.Select(section.SectionId);
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.IsEnabled = true;
            this.Cursor = null;
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
            incentiveGradeList = IncentiveGradeModel.Select().Where(i => i.SectionId == section.SectionId).ToList();
            if (bwLoadData.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                bwLoadData.RunWorkerAsync();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (bwPrint.IsBusy == false)
            {
                int month = (cboMonth.SelectedItem as int?).Value;
                int year = (cboYear.SelectedItem as int?).Value;
                this.Cursor = Cursors.Wait;                
                btnPrint.IsEnabled = false;
                object[] arguments = { year, month};
                bwPrint.RunWorkerAsync(arguments);
            }
        }

        private void bwPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arguments = e.Argument as object[];
            int? year = arguments[0] as int?;
            int? month = arguments[1] as int?;
            linePerformanceList = LinePerformanceController.SelectBySection(section.SectionId, month.Value, year.Value);
            object[] results = {year, month};
            e.Result = results;
        }

        private void bwPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            DataTable dt = new GroupIncentiveDataSet().Tables["GroupIncentiveTable"];
            foreach (LineModel line in lineList)
            {
                int incentiveGradeMonth = linePerformanceList.Where(l => l.LineId == line.LineId).Sum(l => l.IncentiveGradeA);
                int incentiveGradeMonthSmall = linePerformanceList.Where(l => l.LineId == line.LineId).Sum(l => l.IncentiveGradeASmall);
                foreach (IncentiveGradeModel incentiveGrade in incentiveGradeList)
                {                    
                    DataRow dr = dt.NewRow();
                    dr["Line"] = string.Format("LINE {0}", line.Name);
                    dr["IncentiveGrade"] = incentiveGrade.Name;
                    dr["Count"] = incentiveGradeMonth * incentiveGrade.Ratio;
                    if (incentiveGrade.IsSmallGrade == true)
                    {
                        dr["Count"] = incentiveGradeMonthSmall * incentiveGrade.Ratio;
                    }
                    dt.Rows.Add(dr);
                }
            }

            object[] results = e.Result as object[];
            int? year = results[0] as int?;
            int? month = results[1] as int?;

            ReportParameter rp1 = new ReportParameter("Month", String.Format("{0}/{1}", month.Value, year.Value));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "GroupIncentive";
            rds.Value = dt;

            //reportViewer.LocalReport.ReportPath = @"C:\Users\IT02\Documents\Visual Studio 2010\Projects\Sewing Office Solution\SewingIncentives\Reports\GroupIncentiveReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\GroupIncentiveReport.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();

            btnPrint.IsEnabled = true;
            this.Cursor = null;
        }
    }
}
