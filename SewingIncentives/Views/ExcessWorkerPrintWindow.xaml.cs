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
    public partial class ExcessWorkerPrintWindow : Window
    {
        SectionModel section;
        BackgroundWorker bwLoadData;
        List<PersonalModel> personalList;
        BackgroundWorker bwPrint;
        DateTime date;
        List<WorkerLoginModel> workerLoginList;
        List<SourceModel> sourceList;        
        public ExcessWorkerPrintWindow(SectionModel section)
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
            workerLoginList = new List<WorkerLoginModel>();
            sourceList = new List<SourceModel>();            
            InitializeComponent();            
        }        

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            personalList = PersonalController.Select(section.Keyword);
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.IsEnabled = true;
            this.Cursor = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = DateTime.Now;
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
                date = dpDate.SelectedDate.Value;
                btnPrint.IsEnabled = false;
                bwPrint.RunWorkerAsync();
            }
        }

        private void bwPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            workerLoginList = WorkerLoginController.Select(date);
            sourceList = SourceController.Select(section.Keyword, date);            
        }

        private void bwPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTable dt = new ExcessWorkerDataSet().Tables["ExcessWorkerTable"];
            foreach (PersonalModel personal in personalList)
            {
                SourceModel source = sourceList.Where(s => s.CardId == personal.CardId).OrderBy(s => s.Time).LastOrDefault();
                WorkerLoginModel workerLogin = workerLoginList.Where(w => w.CardId == personal.CardId && w.WorkerId == personal.WorkerId).FirstOrDefault();
                if (source != null && workerLogin == null)
                {
                    DataRow dr = dt.NewRow();
                    dr["WorkerId"] = personal.WorkerId;
                    dr["Name"] = personal.Name;
                    dr["Line"] = personal.Department;
                    dr["Position"] = personal.Position;

                    dt.Rows.Add(dr);
                }
            }

            ReportParameter rp1 = new ReportParameter("Date", String.Format("{0:dd/MM/yyyy}", date));

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "ExcessWorker";
            rds.Value = dt;

            //reportViewer.LocalReport.ReportPath = @"C:\Users\IT02\Documents\Visual Studio 2010\Projects\Sewing Office Solution\SewingIncentives\Reports\ExcessWorkerReport.rdlc";
            reportViewer.LocalReport.ReportPath = @"Reports\ExcessWorkerReport.rdlc";  
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1});
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();

            btnPrint.IsEnabled = true;
            this.Cursor = null;
        }
    }
}
