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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ComponentModel;
using SewingCheckIn.Models;
using SewingCheckIn.Controllers;
using SewingCheckIn.Views;
using Microsoft.Win32;
using System.Xml.Linq;
namespace SewingCheckIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker workerLoad;
        List<PersonalModel> personals;
        List<String> lines;
        List<PersonalModel> personalLines;
        public MainWindow()
        {
            workerLoad = new BackgroundWorker();
            workerLoad.DoWork += new DoWorkEventHandler(workerLoad_DoWork);
            workerLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerLoad_RunWorkerCompleted);
            personals = new List<PersonalModel>();
            lines = new List<String>();
            personalLines = new List<PersonalModel>();
            InitializeComponent();
        }

        private void workerLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cboLines.SelectedIndex = 0;
            cboDay.SelectedItem = DateTime.Now.Day;
            cboMonth.SelectedItem = DateTime.Now.Month;
            cboYear.SelectedItem = DateTime.Now.Year;            
        }

        private void workerLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            personals = PersonalController.Get("");
            List<String> lines = personals.Select(p => p.Department).Distinct().ToList();
            cboLines.Dispatcher.Invoke((Action)(() => cboLines.ItemsSource = lines));
            int[] days = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
            cboDay.Dispatcher.Invoke((Action)(() => cboDay.ItemsSource = days));
            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            cboMonth.Dispatcher.Invoke((Action)(() => cboMonth.ItemsSource = months));
            int[] years = { DateTime.Now.Year - 1, DateTime.Now.Year };
            cboYear.Dispatcher.Invoke((Action)(() => cboYear.ItemsSource = years));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (workerLoad.IsBusy == false)
            {
                workerLoad.RunWorkerAsync();
            }
        }

        private void linkProceed_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            try
            {
                date = new DateTime((int)cboYear.SelectedItem, (int)cboMonth.SelectedItem, (int)cboDay.SelectedItem);
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                MessageBox.Show("- Error Field: Date.\n- Exeption: ArgumentOutOfRangeException.\n- Choose Date Again!", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show("Do You Want Create New Profile?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Choose Location To Save Profile";
                sfd.Filter = "XML Files | *.xml";
                sfd.DefaultExt = "xml";
                sfd.FileName = "Untitle.xml";
                if (sfd.ShowDialog() == true)
                {
                    XDocument xmlDocument = new XDocument();
                    xmlDocument.Add(new XElement("LineCheckIns"));
                    xmlDocument.Save(sfd.FileName);
                    LineCheckInWindow window = new LineCheckInWindow(personalLines, lines, date, sfd.FileName);
                    window.Show();
                }
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Choose Location To Load Profile";
                ofd.Filter = "XML Files | *.xml";
                ofd.DefaultExt = "xml";
                if (ofd.ShowDialog() == true)
                {
                    LineCheckInWindow window = new LineCheckInWindow(personalLines, lines, date, ofd.FileName);
                    window.Show();
                }
            }
        }

        private void linkClear_Click(object sender, RoutedEventArgs e)
        {
            lines = new List<String>();
            personalLines = new List<PersonalModel>();
            txtLine.Text = "";
            linkClear.IsEnabled = false;
            linkProceed.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string line = (string)cboLines.SelectedItem;
            if (lines.Contains(line) == false)
            {
                personalLines.AddRange(personals.Where(p => p.Department == line).ToList());
                lines.Add(line);
                txtLine.Text = txtLine.Text + line + "\n";
                linkClear.IsEnabled = true;
                linkProceed.IsEnabled = true;
            }            
        }        
    }
}
