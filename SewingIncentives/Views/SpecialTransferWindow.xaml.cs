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
using System.Collections.ObjectModel;
using SewingIncentives.ViewModels;

namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for SpecialTransferWindow.xaml
    /// </summary>
    public partial class SpecialTransferWindow : Window
    {
        BackgroundWorker threadLoad;
        List<LineModel> lineList;
        List<SpecialTransferTypeModel> typeList;
        BackgroundWorker threadInsert;
        BackgroundWorker threadSelect;
        BackgroundWorker threadDelete;
        ObservableCollection<SpecialTransferViewModel> specialTransferViewList;
        public SpecialTransferWindow()
        {
            InitializeComponent();

            lineList = new List<LineModel>();

            threadLoad = new BackgroundWorker();
            threadLoad.DoWork += new DoWorkEventHandler(threadLoad_DoWork);
            threadLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadLoad_RunWorkerCompleted);

            threadInsert = new BackgroundWorker();
            threadInsert.DoWork += new DoWorkEventHandler(threadInsert_DoWork);
            threadInsert.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadInsert_RunWorkerCompleted);

            threadSelect = new BackgroundWorker();
            threadSelect.DoWork += new DoWorkEventHandler(threadSelect_DoWork);
            threadSelect.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadSelect_RunWorkerCompleted);

            threadDelete = new BackgroundWorker();
            threadDelete.DoWork += new DoWorkEventHandler(threadDelete_DoWork);
            threadDelete.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadDelete_RunWorkerCompleted);

            specialTransferViewList = new ObservableCollection<SpecialTransferViewModel>();
        }            

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Now;
            dpDate.SelectedDate = today;
            txtTime.Text = string.Format("{0:HH:mm}", today);

            typeList = SpecialTransferTypeModel.Create();
            cboTransferType.ItemsSource = typeList.Where(s => s.TransferType == 1).ToList();
            cboTransferType.SelectedItem = typeList.FirstOrDefault();

            threadLoad.RunWorkerAsync();
        }

        void threadLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cboLineId.ItemsSource = lineList;
            cboLineId.SelectedItem = lineList.FirstOrDefault();
        }

        void threadLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            lineList = LineController.SelectAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string workerIdText = txtWorkerId.Text;
            if (string.IsNullOrWhiteSpace(workerIdText) == true)
                return;
            workerIdText = workerIdText.Trim().Replace("\r\n", "|");
            string[] workerIdArray = workerIdText.Split('|');           
            int transferType = (int)cboTransferType.SelectedValue;
            string lineId = (string)cboLineId.SelectedValue;
            DateTime date = dpDate.SelectedDate.Value;
            TimeSpan time;
            if (TimeSpan.TryParse(txtTime.Text, out time) == false)
                return;
            List<SpecialTransferModel> modelList = new List<SpecialTransferModel>();
            foreach (string workerId in workerIdArray)
            {
                if (string.IsNullOrEmpty(workerId.Trim()) == false && string.IsNullOrWhiteSpace(workerId.Trim()) == false)
                {
                    SpecialTransferModel model = new SpecialTransferModel
                    {
                        WorkerId = workerId.Trim(),
                        TransferType = transferType,
                        DestinationLineId = lineId,
                        Date = date,
                        Time = time,
                    };
                    modelList.Add(model);
                }
            }
            
            if (threadInsert.IsBusy == false && modelList.Count > 0)
            {
                this.Cursor = Cursors.Wait;
                btnSave.IsEnabled = false;
                threadInsert.RunWorkerAsync(modelList);
            }
        }

        void threadInsert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool result = (e.Result as bool?).Value;
            if (result == true)
            {
                MessageBox.Show("Saved!", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Something Failed!", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            btnSave.IsEnabled = true;
            this.Cursor = null;
        }

        void threadInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            List<SpecialTransferModel> modelList = e.Argument as List<SpecialTransferModel>;
            e.Result = SpecialTransferController.Insert(modelList);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string workerId = txtWorkerIdToSearch.Text;
            if (String.IsNullOrEmpty(workerId) == false && threadSelect.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                btnSearch.IsEnabled = false;
                specialTransferViewList.Clear();
                threadSelect.RunWorkerAsync(workerId);
            }
        }

        void threadSelect_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<SpecialTransferModel> list = e.Result as List<SpecialTransferModel>;
            foreach (SpecialTransferModel item in list)
            {
                SpecialTransferViewModel viewItem = new SpecialTransferViewModel
                {
                    SpecialTransferId = item.SpecialTransferId,
                    WorkerId = item.WorkerId,
                    TransferType = item.TransferType,
                    DestinationLineId = item.DestinationLineId,
                    Date = item.Date,
                    Time = item.Time,
                };

                SpecialTransferTypeModel type = typeList.Where(t => t.TransferType == item.TransferType).FirstOrDefault();
                if (type != null)
                {
                    viewItem.TransferName = type.Name;
                }

                LineModel line = lineList.Where(l => l.LineId == item.DestinationLineId).FirstOrDefault();
                if (line != null)
                {
                    viewItem.DestinationLineName = line.Name;
                }

                specialTransferViewList.Add(viewItem);
            }
            dgMain.ItemsSource = specialTransferViewList;

            btnSearch.IsEnabled = true;
            this.Cursor = null;
        }

        void threadSelect_DoWork(object sender, DoWorkEventArgs e)
        {
            string workerId = e.Argument as String;
            e.Result = SpecialTransferController.Select(workerId);
        }

        private void dgMain_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirm?", this.Title, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                SpecialTransferViewModel item = dgMain.CurrentItem as SpecialTransferViewModel;
                if (item != null && threadDelete.IsBusy == false)
                {
                    this.Cursor = Cursors.Wait;
                    threadDelete.RunWorkerAsync(item.SpecialTransferId);
                }
            }
        }

        void threadDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool result = (e.Result as bool?).Value;
            if (result == true)
            {
                SpecialTransferViewModel item = dgMain.CurrentItem as SpecialTransferViewModel;
                specialTransferViewList.Remove(item);
            }
            this.Cursor = null;
        }

        void threadDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            int id = (e.Argument as int?).Value;
            e.Result = SpecialTransferController.Delete(id);
        }    
    }
}
