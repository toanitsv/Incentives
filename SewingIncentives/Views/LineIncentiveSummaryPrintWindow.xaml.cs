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
    public partial class LineIncentiveSummaryPrintWindow : Window
    {
        DataTable dt;        
        int year;
        int month;
        string line;        
        public LineIncentiveSummaryPrintWindow(DataTable dt, int year, int month, string line)
        {
            InitializeComponent();
            this.dt = dt;            
            this.year = year;
            this.month = month;
            this.line = line;            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReportParameter rp1 = new ReportParameter("Line", line);
            ReportParameter rp2 = new ReportParameter("Month", string.Format("{0}/{1}", month, year));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "LineIncentiveSummary";
            rds.Value = dt;

            //reportViewer.LocalReport.ReportPath = @"C:\Users\IT02\Documents\Visual Studio 2010\Projects\Sewing Office Solution\SewingIncentives\Reports\LineIncentiveSummaryReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\LineIncentiveSummaryReport.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2});
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();
        }
    }
}
