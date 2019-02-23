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
using System.Data;

using Microsoft.Reporting.WinForms;
using SewingIncentives.DataSets;
using SewingIncentives.ViewModels;
namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for LinePerformancePrintWindow.xaml
    /// </summary>
    public partial class LinePerformancePrintWindow : Window
    {
        List<LinePerformanceViewModel> linePerformanceViewList;
        string lineId;
        string date;
        string output;
        string patternNo;
        string articleNo;
        string w_HRS;
        string pPH;
        string incentiveGradeA;
        string incentiveGradeB_TLL;
        string incentiveGradeC;
        string incentiveGradeD_LL2;
        string incentiveGradeLL1;
        string incentiveGradeLL3;
        string incentiveGradeSUP4;
        string incentiveGradeSUP5;
        string countSUP;
        string countMECH;
        string countMONITOR;
        string countLL;
        string countQC;
        string countNewWorker;
        string count1MonthWorker;
        string count2MonthsWorker;
        string countOldWorker;
        string countTotalMP;
        string countTotalWorker;

        public LinePerformancePrintWindow(List<LinePerformanceViewModel> linePerformanceViewList, string lineId, string date,
                                          string output, string patternNo, string articleNo, string w_HRS, string pPH,
                                          string incentiveGradeA, string incentiveGradeB_TLL, string incentiveGradeC, string incentiveGradeD_LL2, string incentiveGradeLL1, string incentiveGradeLL3, string incentiveGradeSUP4, string incentiveGradeSUP5,
                                          string countSUP, string countMECH, string countMONITOR, string countLL, string countQC, string countNewWorker, string count1MonthWorker, string count2MonthsWorker, string countOldWorker, string countTotalMP, string countTotalWorker)
        {
            this.linePerformanceViewList = linePerformanceViewList;
            this.lineId = lineId;
            this.date = date;
            this.output = output;
            this.patternNo = patternNo;
            this.articleNo = articleNo;
            this.w_HRS = w_HRS;
            this.pPH = pPH;
            this.incentiveGradeA = incentiveGradeA;
            this.incentiveGradeB_TLL = incentiveGradeB_TLL;
            this.incentiveGradeC = incentiveGradeC;
            this.incentiveGradeD_LL2 = incentiveGradeD_LL2;
            this.incentiveGradeLL1 = incentiveGradeLL1;
            this.incentiveGradeLL3 = incentiveGradeLL3;
            this.incentiveGradeSUP4 = incentiveGradeSUP4;
            this.incentiveGradeSUP5 = incentiveGradeSUP5;
            this.countSUP = countSUP;
            this.countMECH = countMECH;
            this.countMONITOR = countMONITOR;
            this.countLL = countLL;
            this.countQC = countQC;
            this.countNewWorker = countNewWorker;
            this.count1MonthWorker = count1MonthWorker;
            this.count2MonthsWorker = count2MonthsWorker;
            this.countOldWorker = countOldWorker;
            this.countTotalMP = countTotalMP;
            this.countTotalWorker = countTotalWorker;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new LinePerformanceDataSet().Tables["LinePerformanceTable"];

            foreach (LinePerformanceViewModel linePerformanceView in linePerformanceViewList)
            {
                DataRow dr = dt.NewRow();
                dr["WorkerId"] = linePerformanceView.WorkerId;
                dr["Name"] = linePerformanceView.Name;
                dr["Line"] = linePerformanceView.Line;
                dr["Position"] = linePerformanceView.Position;
                dr["OthersPosition"] = linePerformanceView.OthersPosition;
                dr["TimeOut"] = linePerformanceView.TimeOut;
                string incentiveGradeName = "";
                if (linePerformanceView.IncentiveGrade != null)
                {
                    incentiveGradeName = linePerformanceView.IncentiveGrade.Name;
                }
                dr["IncentiveGrade"] = incentiveGradeName;
                dr["Incentive"] = linePerformanceView.Incentive;
                dr["SpecsIncentive"] = linePerformanceView.SpecsIncentive;
                dr["CumulativeIncentive"] = linePerformanceView.CumulativeIncentive;
                dt.Rows.Add(dr);
            }

            ReportParameter rp1 = new ReportParameter("Output", output);
            ReportParameter rp2 = new ReportParameter("PatternNo", patternNo);
            ReportParameter rp3 = new ReportParameter("ArticleNo", articleNo);
            ReportParameter rp4 = new ReportParameter("W_HRS", w_HRS);
            ReportParameter rp5 = new ReportParameter("PPH", pPH);
            ReportParameter rp6 = new ReportParameter("IncentiveGradeA", incentiveGradeA);
            ReportParameter rp7 = new ReportParameter("IncentiveGradeB_TLL", incentiveGradeB_TLL);
            ReportParameter rp8 = new ReportParameter("IncentiveGradeC", incentiveGradeC);
            ReportParameter rp9 = new ReportParameter("IncentiveGradeD_LL2", incentiveGradeD_LL2);
            ReportParameter rp10 = new ReportParameter("IncentiveGradeLL1", incentiveGradeLL1);
            ReportParameter rp11 = new ReportParameter("IncentiveGradeLL3", incentiveGradeLL3);
            ReportParameter rp12 = new ReportParameter("IncentiveGradeSUP4", incentiveGradeSUP4);
            ReportParameter rp13 = new ReportParameter("IncentiveGradeSUP5", incentiveGradeSUP5);
            ReportParameter rp14 = new ReportParameter("CountSUP", countSUP);
            ReportParameter rp15 = new ReportParameter("CountMECH", countMECH);
            ReportParameter rp16 = new ReportParameter("CountMONITOR", countMONITOR);
            ReportParameter rp17 = new ReportParameter("CountLL", countLL);
            ReportParameter rp18 = new ReportParameter("CountQC", countQC);
            ReportParameter rp19 = new ReportParameter("CountNewWorker", countNewWorker);
            ReportParameter rp20 = new ReportParameter("Count1Month", count1MonthWorker);
            ReportParameter rp21 = new ReportParameter("Count2Months", count2MonthsWorker);
            ReportParameter rp22 = new ReportParameter("CountOldWorker", countOldWorker);
            ReportParameter rp23 = new ReportParameter("CountTotalWorker", countTotalWorker);
            ReportParameter rp24 = new ReportParameter("CountTotalMP", countTotalMP);
            ReportParameter rp25 = new ReportParameter("LineId", lineId);
            ReportParameter rp26 = new ReportParameter("Date", date);


            ReportDataSource rds = new ReportDataSource();
            rds.Name = "LinePerformance";
            rds.Value = dt;

            //reportViewer.LocalReport.ReportPath = @"C:\Users\IT02\Documents\Visual Studio 2010\Projects\Sewing Office Solution\SewingIncentives\Reports\LinePerformanceReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\LinePerformanceReport.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp3, rp4, rp5,
                                                                           rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13,
                                                                           rp14, rp15, rp16, rp17, rp18, rp19, rp20, rp21, rp22, rp23, rp24,
                                                                           rp25, rp26});
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();
        }
    }
}
