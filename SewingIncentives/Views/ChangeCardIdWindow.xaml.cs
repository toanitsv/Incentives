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
namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for ChangeCardIdWindow.xaml
    /// </summary>
    public partial class ChangeCardIdWindow : Window
    {
        BackgroundWorker threadChange;
        public ChangeCardIdWindow()
        {
            InitializeComponent();

            threadChange = new BackgroundWorker();
            threadChange.DoWork += new DoWorkEventHandler(threadChange_DoWork);
            threadChange.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadChange_RunWorkerCompleted);
        }

        private void threadChange_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arguments = e.Argument as object[];
            string workerId = arguments[0] as string;
            string cardId = arguments[1] as string;
            bool result = OtherController.ChangeCardId(workerId, cardId);
            e.Result = result;
        }

        private void threadChange_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = null;
            btnChange.IsEnabled = true;
            if (e.Error != null)
            {
                MessageBox.Show(string.Format("Error\n{0}", e.Error.Message), this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool result = (e.Result as bool?).Value;
            if (result == true)
            {
                MessageBox.Show("Changed!", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                txtWorkerId.Clear();
                txtCardId.Clear();
                txtPassword.Clear();
            }
            else
            {
                MessageBox.Show("Failed!", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            string workerId = txtWorkerId.Text;
            if (string.IsNullOrEmpty(workerId) == true)
            {
                return;
            }
            string cardId = txtCardId.Text;
            if (string.IsNullOrEmpty(cardId) == true)
            {
                return;
            }
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(password) == true || password != "1235711131719")
            {
                MessageBox.Show("Security Code Error!", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show("Confirm?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            if (threadChange.IsBusy == true)
            {
                return;
            }
            this.Cursor = Cursors.Wait;
            btnChange.IsEnabled = false;
            object[] arguments = { workerId, cardId};
            threadChange.RunWorkerAsync(arguments);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtWorkerId.Focus();
        }
    }
}
