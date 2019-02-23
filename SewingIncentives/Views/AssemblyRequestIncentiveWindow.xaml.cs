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

namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for AssemblyRequestIncentive.xaml
    /// </summary>
    public partial class AssemblyRequestIncentiveWindow : Window
    {
        int incentiveBefore, incentiveAfter;
        double workHrsBefore, workHrsAfter;
        LineModel line;
        DateTime date;
        public AssemblyRequestIncentiveWindow(LineModel _line, DateTime _date, double _workHrsBefore, double _workHrsAfter, int _incentiveBefore, int _incentiveAfter)
        {
            this.line= _line;
            this.date = _date;
            this.workHrsBefore = _workHrsBefore;
            this.workHrsAfter = _workHrsAfter;
            this.incentiveBefore = _incentiveBefore;
            this.incentiveAfter = _incentiveAfter;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtLine.Text = line.LineId;
            txtDate.Text = date.ToShortDateString();
            txtWorkHrsBefore.Text = workHrsBefore.ToString();
            txtWorkHrsAfter.Text = workHrsAfter.ToString();
            txtIncentiveBefore.Text = incentiveBefore.ToString();
            txtIncentiveAfter.Text = incentiveAfter.ToString();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            int intcentiveBf = 0, incentiveAf = 0;
            double workHrsBf = 0, workHrsAf = 0;

            Int32.TryParse(txtIncentiveBefore.Text.ToString(), out intcentiveBf);
            Int32.TryParse(txtIncentiveAfter.Text.ToString(), out incentiveAf);
            Double.TryParse(txtWorkHrsBefore.Text.ToString(), out workHrsBf);
            Double.TryParse(txtWorkHrsAfter.Text.ToString(), out workHrsAf);

            string reason = "";
            reason = txtReason.Text.Trim();
            CieControlIncentiveModel insertModel = new CieControlIncentiveModel() {
                WorkHrsBefore = workHrsBf,
                WorkHrsAfter = workHrsAf,
                LineId = line.LineId,
                Reason = reason,
                RequestTime = date,
                IncentiveBefore = intcentiveBf,
                IncentiveAfter = incentiveAf
            };
            if (CieControlIncentiveController.Insert(insertModel) == false)
            {
                MessageBox.Show("Error !!", "Infor", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Sent !", "Infor", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
