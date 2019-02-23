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
namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for SelectSectionWindow.xaml
    /// </summary>
    public partial class SelectSectionWindow : Window
    {
        BackgroundWorker bwLoadData;
        List<SectionModel> sectionList;
        public SelectSectionWindow()
        {
            bwLoadData = new BackgroundWorker();
            bwLoadData.DoWork += new DoWorkEventHandler(bwLoadData_DoWork);
            bwLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadData_RunWorkerCompleted);
            sectionList = new List<SectionModel>();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (bwLoadData.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                cboSection.Items.Clear();
                bwLoadData.RunWorkerAsync();
            }
        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            sectionList = SectionController.Select();
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cboSection.ItemsSource = sectionList;
            cboSection.SelectedItem = sectionList.FirstOrDefault();
            this.Cursor = null;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            SectionModel section = (SectionModel)cboSection.SelectedItem as SectionModel;
            string password = txtPassword.Password;
            if (section != null)
            {
                if (password != section.Password)
                {
                    MessageBox.Show("Try Other Password!", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtPassword.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtPassword.ClearValue(PasswordBox.BorderBrushProperty);
                }
                MainWindow window = new MainWindow(section);
                this.Hide();
                window.ShowDialog();
                this.Close();
            }
        }        
    }
}
