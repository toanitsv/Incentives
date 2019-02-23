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
using SewingIncentives.Controllers;
using SewingIncentives.Models;
using SewingIncentives.ViewModels;
using System.Collections.ObjectModel;
namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for WorkerLeaveWindow.xaml
    /// </summary>
    public partial class WorkerLeaveWindow : Window
    {
        BackgroundWorker threadSearch;
        BackgroundWorker threadInsert;
        BackgroundWorker threadDelete;
        ObservableCollection<WorkerLeaveViewModel> workerLeaveViewList;
        Int32 workerLeaveId;
        WorkerLeaveViewModel workerLeaveView;
        List<LeaveTypeModel> leaveTypeList;
        public WorkerLeaveWindow()
        {
            workerLeaveViewList = new ObservableCollection<WorkerLeaveViewModel>();

            threadSearch = new BackgroundWorker();
            threadSearch.DoWork += new DoWorkEventHandler(threadSearch_DoWork);
            threadSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadSearch_RunWorkerCompleted);

            threadInsert = new BackgroundWorker();
            threadInsert.DoWork += new DoWorkEventHandler(threadInsert_DoWork);
            threadInsert.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadInsert_RunWorkerCompleted);

            threadDelete = new BackgroundWorker();
            threadDelete.DoWork += new DoWorkEventHandler(threadDelete_DoWork);
            threadDelete.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadDelete_RunWorkerCompleted);

            workerLeaveId = 0;
            workerLeaveView = new WorkerLeaveViewModel();

            leaveTypeList = LeaveTypeModel.Create();
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string workerId = txtWorkerIdToSearch.Text;
            if (String.IsNullOrEmpty(workerId) == false && threadSearch.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                btnSearch.IsEnabled = false;
                workerLeaveViewList.Clear();
                threadSearch.RunWorkerAsync(workerId);
            }
        }

        void threadSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            string workerId = e.Argument as String;
            e.Result = WorkerLeaveController.Select(workerId);
        }

        void threadSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<WorkerLeaveModel> workerLeaveList = e.Result as List<WorkerLeaveModel>;            
            foreach (WorkerLeaveModel workerLeave in workerLeaveList)
            {
                LeaveTypeModel leaveType = leaveTypeList.Where(l => l.LeaveTypeId == workerLeave.LeaveType).FirstOrDefault();
                WorkerLeaveViewModel workerLeaveView = new WorkerLeaveViewModel
                {
                    WorkerLeaveId = workerLeave.WorkerLeaveId,
                    WorkerId = workerLeave.WorkerId,
                    StartDate = workerLeave.StartDate,
                    EndDate = workerLeave.EndDate,
                    LeaveType = leaveType,
                };
                workerLeaveViewList.Add(workerLeaveView);
            }
            dgMain.ItemsSource = workerLeaveViewList;

            btnSearch.IsEnabled = true;
            this.Cursor = null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string workerId = txtWorkerId.Text;
            DateTime startDate = dpStartDate.SelectedDate.Value;
            DateTime endDate = dpEndDate.SelectedDate.Value;

            LeaveTypeModel leaveType = cboLeaveType.SelectedItem as LeaveTypeModel;
            if (leaveType == null)
            {
                return;
            }
            WorkerLeaveModel model = new WorkerLeaveModel
            {
                WorkerLeaveId = workerLeaveId,
                WorkerId = workerId,
                StartDate = startDate,
                EndDate = endDate,
                LeaveType = leaveType.LeaveTypeId,
            };
            if (String.IsNullOrEmpty(model.WorkerId) == true || model.StartDate.Date > model.EndDate.Date)
            {
                return;
            }                

            if (threadInsert.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                btnSave.IsEnabled = false;
                threadInsert.RunWorkerAsync(model);
            }
        }

        void threadInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerLeaveModel model = e.Argument as WorkerLeaveModel;
            if (model.WorkerLeaveId == 0)
            {
                e.Result = WorkerLeaveController.Insert(model);
            }
            else
            {
                e.Result = WorkerLeaveController.Update(model);
            }
        }

        void threadInsert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool? result = e.Result as Boolean?;
            if (result.Value == true)
            {
                MessageBox.Show("Saved!", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                if (workerLeaveId != 0)
                {
                    workerLeaveView.WorkerId = txtWorkerId.Text;
                    workerLeaveView.StartDate = dpStartDate.SelectedDate.Value;
                    workerLeaveView.EndDate = dpEndDate.SelectedDate.Value;
                    workerLeaveView.LeaveType = cboLeaveType.SelectedItem as LeaveTypeModel;
                }
            }
            else
            {
                MessageBox.Show("Save Failed!", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            workerLeaveId = 0;
            btnSave.IsEnabled = true;
            this.Cursor = null;
        }

        private void dgMain_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                workerLeaveView = dgMain.CurrentItem as WorkerLeaveViewModel;
                if (workerLeaveView != null)
                {
                    workerLeaveId = workerLeaveView.WorkerLeaveId;
                    txtWorkerId.Text = workerLeaveView.WorkerId;
                    dpStartDate.SelectedDate = workerLeaveView.StartDate;
                    dpEndDate.SelectedDate = workerLeaveView.EndDate;
                    cboLeaveType.SelectedItem = workerLeaveView.LeaveType;
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirm?", this.Title, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                workerLeaveView = dgMain.CurrentItem as WorkerLeaveViewModel;
                if (workerLeaveView != null && threadDelete.IsBusy == false)
                {
                    this.Cursor = Cursors.Wait;
                    threadDelete.RunWorkerAsync(workerLeaveView.WorkerLeaveId);
                }
            }
        }

        void threadDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            int? workerLeaveIdToDelete = e.Argument as Int32?;
            e.Result = WorkerLeaveController.Delete(workerLeaveIdToDelete.Value);
        }

        void threadDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool? result = e.Result as Boolean?;
            if (result.Value == true)
            {
                workerLeaveViewList.Remove(workerLeaveView);
            }
            this.Cursor = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpStartDate.SelectedDate = DateTime.Now;
            dpEndDate.SelectedDate = DateTime.Now;
            cboLeaveType.ItemsSource = leaveTypeList;
            colLeaveType.ItemsSource = leaveTypeList;
        }
    }
}
