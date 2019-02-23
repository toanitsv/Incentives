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
using SewingIncentives.Models;
using SewingIncentives.DataSets;
using System.Data;
using Microsoft.Reporting.WinForms;

namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for AssemblySpecialPrintWindow.xaml
    /// </summary>
    public partial class AssemblySpecialPrintWindow : Window
    {

        List<AssemblySpecialIncentiveModel> receiveList;
        public string monthName, yearName;
        public AssemblySpecialPrintWindow(List<AssemblySpecialIncentiveModel> tranferList, string _monthName, string _yearName)
        {
            this.monthName = _monthName;
            this.yearName = _yearName;
            this.receiveList = tranferList;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new AssemblySpecialIncentiveDataSet().Tables["AssemblySpecialIncentiveTable"];

            foreach (var assySpecialIncentive in receiveList)
            {
                DataRow dr = dt.NewRow();
                dr["Line"] = assySpecialIncentive.Line;
                dr["Quantity"] = assySpecialIncentive.Output;
                dr["QuotaTarget"] = assySpecialIncentive.QuotaTarget;
                dr["Lacking"] = assySpecialIncentive.Lacking;
                dr["PercentOutput"] = assySpecialIncentive.PercentOutput / 100;
                dr["SUPIncentive"] = assySpecialIncentive.SUPIncentive;
                dr["LL23Incentive"] = assySpecialIncentive.LL23Incentive;
                dr["LLT1MECHIncentive"] = assySpecialIncentive.LLT1MECHIncentive;
                dr["WorkerIncentive"] = assySpecialIncentive.WorkerIncentive;
                dt.Rows.Add(dr);
            }

            ReportParameter rp1 = new ReportParameter("Month", monthName.ToUpper());
            ReportParameter rp2 = new ReportParameter("Year", yearName.ToString());
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "AssemblySpecialIncentiveDetail";
            rds.Value = dt;
            //reportViewer.LocalReport.ReportPath = @"E:\SV PROJECT\SWO\Assembly\1.18.03.14\SewingIncentives\Reports\AssemblySpecialIncentiveReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\AssemblySpecialIncentiveReport.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();

        }
    }
}
