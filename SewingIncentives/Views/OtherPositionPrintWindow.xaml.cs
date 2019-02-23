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
    public partial class OtherPositionPrintWindow : Window
    {
        SectionModel section;
        List<LineModel> lineList;
        BackgroundWorker bwLoadData;
        List<PersonalModel> personalList;
        BackgroundWorker bwPrint;
        DateTime date;        
        List<LinePerformanceDetailModel> linePerformanceDetailList;
        public OtherPositionPrintWindow(SectionModel section)
        {
            this.section = section;
            bwLoadData = new BackgroundWorker();
            bwLoadData.DoWork += new DoWorkEventHandler(bwLoadData_DoWork);
            bwLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadData_RunWorkerCompleted);
            personalList = new List<PersonalModel>();
            bwPrint = new BackgroundWorker();
            date = new DateTime(2000, 1, 1);
            bwPrint.DoWork += new DoWorkEventHandler(bwPrint_DoWork);
            bwPrint.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwPrint_RunWorkerCompleted);
            linePerformanceDetailList = new List<LinePerformanceDetailModel>();            
            InitializeComponent();            
        }        

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            personalList = PersonalController.Select(section.Keyword);
            lineList = LineController.Select();
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
                this.Cursor = Cursors.Wait;                
                date = new DateTime((cboYear.SelectedItem as int?).Value, (cboMonth.SelectedItem as int?).Value, 1);
                btnPrint.IsEnabled = false;
                bwPrint.RunWorkerAsync();
            }
        }

        private void bwPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            linePerformanceDetailList = LinePerformanceDetailController.SelectBySection(section.SectionId, date.Month, date.Year);                       
        }

        private void bwPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTable dt = new OtherPositionDataSet().Tables["OtherPositionTable"];

            var otherPositionList = linePerformanceDetailList.GroupBy(l => new { l.WorkerId, l.OthersPosition }).Select(l => new { WorkerId = l.Key.WorkerId, OthersPosition = l.Key.OthersPosition }).OrderBy(o => o.WorkerId).ToList();

            foreach (var otherPosition in otherPositionList)
            {
                if (String.IsNullOrEmpty(otherPosition.OthersPosition) == true || otherPosition.OthersPosition != "NaN")
                {
                    DataRow dr = dt.NewRow();
                    dr["WorkerId"] = otherPosition.WorkerId;
                    PersonalModel personal = personalList.Where(p => p.WorkerId == otherPosition.WorkerId).FirstOrDefault();
                    if (personal != null)
                    {
                        dr["Name"] = personal.Name;
                    }
                    dr["OtherPosition"] = otherPosition.OthersPosition;
                    string dateList = "";                   
                    List<LinePerformanceDetailModel> linePerformanceDetailWorkerList =
                            linePerformanceDetailList.Where(l => l.WorkerId == otherPosition.WorkerId && l.OthersPosition == otherPosition.OthersPosition).OrderBy(l => l.Date).ToList();
                    foreach (LinePerformanceDetailModel linePerformanceDetailWorker in linePerformanceDetailWorkerList)
                    {
                        dateList += linePerformanceDetailWorker.Date.Day + "; ";
                    }
                    string lineIdList = "";
                    foreach (string lineId in linePerformanceDetailWorkerList.Select(l => l.LineId).Distinct())
                    {
                        LineModel line = lineList.Where(l => l.LineId == lineId).FirstOrDefault();
                        if (line != null)
                        {
                            lineIdList += line.Name + "; ";
                        }
                        else
                        {
                            lineIdList += lineId + "; ";
                        }
                    }
                    dr["Date"] = dateList + "\n(Line: " + lineIdList + ")";

                    dt.Rows.Add(dr);
                }
            }            

            ReportParameter rp1 = new ReportParameter("Month", String.Format("{0:MM/yyyy}", date));

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "OtherPosition";
            rds.Value = dt;

            //reportViewer.LocalReport.ReportPath = @"C:\Users\IT02\Documents\Visual Studio 2010\Projects\Sewing Office Solution\SewingIncentives\Reports\OtherPositionReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\OtherPositionReport.rdlc";  
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1});
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();

            btnPrint.IsEnabled = true;
            this.Cursor = null;
        }
    }
}
