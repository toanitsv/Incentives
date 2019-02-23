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
using System.ComponentModel;
using SewingIncentives.Controllers;
using System.Data;
using SewingIncentives.DataSets;
using Microsoft.Reporting.WinForms;
namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for OtherPositionListPrint.xaml
    /// </summary>
    public partial class OtherPositionListPrintWindow : Window
    {
        SectionModel section;
        BackgroundWorker threadLoad;
        BackgroundWorker threadSelect;
        List<PersonalModel> personalList;
        DataTable dataTable;
        public OtherPositionListPrintWindow(SectionModel _section)
        {
            InitializeComponent();

            section = _section;
            personalList = new List<PersonalModel>();

            threadLoad = new BackgroundWorker();
            threadLoad.WorkerSupportsCancellation = true;
            threadLoad.DoWork += new DoWorkEventHandler(threadLoad_DoWork);
            threadLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadLoad_RunWorkerCompleted);

            threadSelect = new BackgroundWorker();
            threadSelect.DoWork += new DoWorkEventHandler(threadSelect_DoWork);
            threadSelect.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadSelect_RunWorkerCompleted);
        }

        private void threadLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            personalList = PersonalController.Select(section.Keyword_1);
        }

        private void threadLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = null;
            dpDate.IsEnabled = true;
            dpDate.SelectedDateChanged += dpDate_SelectedDateChanged;
        }

        private void threadSelect_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime date = (e.Argument as DateTime?).Value;
            List<LinePerformanceDetailModel> linePerformanceDetailList = LinePerformanceDetailController.SelectBySection(section.SectionId, date);
            e.Result = linePerformanceDetailList;
        }

        private void threadSelect_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(string.Format("Error\n{0}", e.Error.Message), this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<LinePerformanceDetailModel> linePerformanceDetailList = e.Result as List<LinePerformanceDetailModel>;
            List<String> otherPositionList = linePerformanceDetailList.Select(l => l.OthersPosition).Distinct().ToList();
            cboPosition.ItemsSource = null;
            cboPosition.ItemsSource = otherPositionList;

            dataTable = new OtherPositionListDataSet().Tables[0];
            foreach (PersonalModel personal in personalList)
            {
                LinePerformanceDetailModel linePerformanceDetail = linePerformanceDetailList.Where(l => l.WorkerId == personal.WorkerId).FirstOrDefault();
                if (linePerformanceDetail != null)
                {
                    DataRow dr = dataTable.NewRow();
                    dr["WorkerId"] = personal.WorkerId;
                    dr["Name"] = personal.Name;
                    dr["Line"] = personal.Department;
                    dr["Position"] = personal.Position;
                    dr["OthersPosition"] = linePerformanceDetail.OthersPosition;

                    dataTable.Rows.Add(dr);
                }
            }

            this.Cursor = null;
            cboPosition.IsEnabled = true;
            btnPrint.IsEnabled = true;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string otherPosition = cboPosition.SelectedItem as string;

            if (string.IsNullOrEmpty(otherPosition) == true)
            {
                return;
            }

            DateTime date = dpDate.SelectedDate.Value;

            string expression = string.Format("OthersPosition = '{0}'", otherPosition); 
            DataRow[] dt = dataTable.Select(expression);

            ReportParameter rp1 = new ReportParameter("Date", String.Format("{0:dd/MM/yyyy}", date));
            ReportParameter rp2 = new ReportParameter("OthersPosition", otherPosition);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "OtherPositionList";
            rds.Value = dt;

            //reportViewer.LocalReport.ReportPath = @"C:\Users\IT02\Documents\Visual Studio 2010\Projects\Sewing Office Solution\SewingIncentives\Reports\OtherPositionListReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\OtherPositionListReport.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();

            btnPrint.IsEnabled = true;
            this.Cursor = null;
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {            
            DateTime date = dpDate.SelectedDate.Value;
            if (date.Date > DateTime.Now.Date)
            {
                return;
            }
            if (threadSelect.IsBusy == true)
            {
                return;
            }
            this.Cursor = Cursors.Wait;
            cboPosition.IsEnabled = false;
            btnPrint.IsEnabled = false;
            threadSelect.RunWorkerAsync(date);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = DateTime.Now;
            if (threadLoad.IsBusy == true)
            {
                return;
            }
            this.Cursor = Cursors.Wait;
            dpDate.IsEnabled = false;
            threadLoad.RunWorkerAsync();
        }
    }
}
