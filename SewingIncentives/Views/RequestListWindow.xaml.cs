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
using SewingIncentives.Controllers;
using System.ComponentModel;

namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for RequestListWindow.xaml
    /// </summary>
    public partial class RequestListWindow : Window
    {
        List<CieControlIncentiveModel> requestList;
        BackgroundWorker threadAccept;
        List<LineModel> lineList;
        public RequestListWindow()
        {
            threadAccept = new BackgroundWorker();
            threadAccept.DoWork += new DoWorkEventHandler(threadAccept_DoWork);
            threadAccept.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadAccept_RunWorkerCompleted);
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int currentMonth = DateTime.Now.Month;
            int[] monthArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            cbMonth.ItemsSource = monthArray;
            cbMonth.SelectedItem = currentMonth;
            int currentYear = DateTime.Now.Year;
            int[] yearArray = { currentYear - 1, currentYear, currentYear + 1 };
            cbYear.ItemsSource = yearArray;
            cbYear.SelectedItem = currentYear;

            lineList = LineController.Select();
            cbLine.ItemsSource = lineList.Where(w => w.SectionId == "F").Select(s=>s.LineId).ToList();
            cbLine.SelectedItem = lineList.Where(w => w.SectionId == "F").Select(s => s.LineId).FirstOrDefault();

            string lineId = "";
            lineId = cbLine.SelectedValue as string;

            int _month = 0, _year = 0;
            Int32.TryParse(cbMonth.SelectedValue.ToString(), out _month);
            Int32.TryParse(cbYear.SelectedValue.ToString(), out _year);
            requestList = CieControlIncentiveController.Select(lineId, _month, _year);
            dgvRequest.ItemsSource = requestList;
        }

        CieControlIncentiveModel acceptModel = new CieControlIncentiveModel();
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirm?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                acceptModel = dgvRequest.CurrentItem as CieControlIncentiveModel;
                if (acceptModel != null && threadAccept.IsBusy == false)
                {
                    this.Cursor = Cursors.Wait;
                    object[] arguments = { acceptModel};
                    threadAccept.RunWorkerAsync(arguments);
                }
            }
        }

        private void threadAccept_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arguments = e.Argument as object[];
            CieControlIncentiveModel acceptModel = arguments[0] as CieControlIncentiveModel;
            DateTime acceptedTime = DateTime.Now;
            e.Result = CieControlIncentiveController.Update(acceptModel.LineId, acceptModel.RequestTime, acceptedTime);
        }

        private void threadAccept_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool? result = e.Result as Boolean?;
            if (result.Value == true)
            {
                requestList.Remove(acceptModel);
                dgvRequest.ItemsSource = requestList;
                if (requestList.Count == 0)
                {
                    dgvRequest.ItemsSource = null;
                }
            }
            this.Cursor = null;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgvRequest.ItemsSource = null;
            string lineId = "";
            lineId = cbLine.SelectedValue as string;
            int _month = 0, _year = 0;
            Int32.TryParse(cbMonth.SelectedValue.ToString(), out _month);
            Int32.TryParse(cbYear.SelectedValue.ToString(), out _year);
            requestList = CieControlIncentiveController.Select(lineId, _month, _year);
            dgvRequest.ItemsSource = requestList;
        }
    }
}
