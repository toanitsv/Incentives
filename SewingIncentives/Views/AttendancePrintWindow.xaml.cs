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
    public partial class AttendancePrintWindow : Window
    {
        DataTable dt;
        int year;
        int month;
        string line;
        bool includeBackground;
        public AttendancePrintWindow(DataTable dt, int year, int month, string line, bool includeBackground)
        {
            this.dt = dt;
            this.year = year;
            this.month = month;
            this.line = line;
            this.includeBackground = includeBackground;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReportParameter rp1 = new ReportParameter("Line", line);
            ReportParameter rp2 = new ReportParameter("Month", string.Format("{0}/{1}", month, year));         
            ReportParameter rp3 = new ReportParameter("IncludeBackground", includeBackground.ToString());
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "Attendance";
            rds.Value = dt;

            //reportViewer.LocalReport.ReportPath = @"C:\Users\IT02\Documents\Visual Studio 2010\Projects\Sewing Office Solution\SewingIncentives\Reports\AttendanceReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\AttendanceReport.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3});
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();
        }
    }
}
