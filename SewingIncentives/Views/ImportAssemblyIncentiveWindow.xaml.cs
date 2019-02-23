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
using Excel = Microsoft.Office.Interop.Excel;
using SewingIncentives.Models;
using SewingIncentives.Controllers;
using SewingIncentives.Helpers;
using Microsoft.Win32;
using System.Reflection;

namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for ImportAssemblyIncentive.xaml
    /// </summary>
    public partial class ImportAssemblyIncentiveWindow : Window
    {
        BackgroundWorker bwLoadExcelData;
        BackgroundWorker bwImport;
        BackgroundWorker bwSearch;
        BackgroundWorker bwUpdate;
        string filePath;
        List<AssemblyIncentiveModel> assemblyIncentiveViewList;
        List<AssemblyIncentiveModel> assemblyIncentiveImportList;
        List<AssemblyIncentiveModel> assemblyIncentiveList;
        List<AssemblyIncentiveModel> assemblyIncentiveSearchList;
        List<AssemblyIncentiveModel> assemblyIncentiveUpdateList;
        public ImportAssemblyIncentiveWindow()
        {
            filePath = "";
            bwLoadExcelData = new BackgroundWorker();
            bwLoadExcelData.DoWork += new DoWorkEventHandler(bwLoadExcelData_DoWork);
            bwLoadExcelData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadExcelData_RunWorkerCompleted);

            bwImport = new BackgroundWorker();
            bwImport.DoWork += new DoWorkEventHandler(bwImport_DoWork);
            bwImport.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwImport_RunWorkerCompleted);

            bwSearch = new BackgroundWorker();
            bwSearch.DoWork += new DoWorkEventHandler(bwSearch_DoWork);
            bwSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwSearch_RunWorkerCompleted);

            bwUpdate = new BackgroundWorker();
            bwUpdate.DoWork += new DoWorkEventHandler(bwUpdate_DoWork);
            bwUpdate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwUpdate_RunWorkerCompleted);

            assemblyIncentiveViewList = new List<AssemblyIncentiveModel>();
            assemblyIncentiveImportList = new List<AssemblyIncentiveModel>();
            assemblyIncentiveList = new List<AssemblyIncentiveModel>();
            assemblyIncentiveSearchList = new List<AssemblyIncentiveModel>();
            assemblyIncentiveUpdateList = new List<AssemblyIncentiveModel>();

            InitializeComponent();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (bwImport.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                btnImport.IsEnabled = false;
                assemblyIncentiveImportList = dgvAssemblyIncentive.Items.OfType<AssemblyIncentiveModel>().ToList();
                bwImport.RunWorkerAsync();
            }
        }
        private void bwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (AssemblyIncentiveModel model in assemblyIncentiveImportList)
            {
                AssemblyIncentiveController.Insert(model);
                Dispatcher.Invoke(new Action(() =>
                {
                    dgvAssemblyIncentive.SelectedItem = model;
                    dgvAssemblyIncentive.ScrollIntoView(model);
                }));
            }
        }
        private void bwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnImport.IsEnabled = true;
            this.Cursor = null;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Insert Completed!", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        string patternNo = "";
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgvAssemblyIncentiveRevise.ItemsSource = null;
            if (bwSearch.IsBusy == true)
            {
                
                return;
            } 
            patternNo = txtPatternNo.Text.ToUpper();
            btnSearch.IsEnabled = false;
            btnDelete.IsEnabled = false;
            this.Cursor = Cursors.Wait;
            assemblyIncentiveSearchList.Clear();
            dgvAssemblyIncentiveRevise.ItemsSource = null;
            bwSearch.RunWorkerAsync();
        }
        private void bwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            assemblyIncentiveList = AssemblyIncentiveController.Select(patternNo);
            assemblyIncentiveSearchList = assemblyIncentiveList.Where(w => w.PatternNo == patternNo).ToList();
        }
        private void bwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSearch.IsEnabled = true;
            this.Cursor = null;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (assemblyIncentiveSearchList.Count == 0)
            {
                MessageBox.Show("Not Found!", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            dgvAssemblyIncentiveRevise.ItemsSource = assemblyIncentiveSearchList;
            btnDelete.IsEnabled = true;
            btnSave.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (bwUpdate.IsBusy == false)
            {
                btnSave.IsEnabled = false;
                this.Cursor = Cursors.Wait;
                assemblyIncentiveUpdateList = dgvAssemblyIncentiveRevise.Items.OfType<AssemblyIncentiveModel>().ToList();
                bwUpdate.RunWorkerAsync();
            }
            else
            {
                return;
            }
        }
        private void bwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (AssemblyIncentiveModel model in assemblyIncentiveUpdateList)
            {
                AssemblyIncentiveController.Update(model);
                Dispatcher.Invoke(new Action(() =>
                {
                    dgvAssemblyIncentiveRevise.SelectedItem = model;
                    dgvAssemblyIncentiveRevise.ScrollIntoView(model);

                }));
            }
        }
        private void bwUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSave.IsEnabled = true;
            this.Cursor = null;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Saved!!!", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnChooseExcelFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Incentive Data";
            openFileDialog.Filter = "EXCEL Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            dgvAssemblyIncentive.ItemsSource = null;
            assemblyIncentiveViewList.Clear();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                if (bwLoadExcelData.IsBusy == false)
                {
                    btnChooseExcelFile.IsEnabled = false;
                    this.Cursor = Cursors.Wait;
                    //groupIncentiveList.Clear();
                    bwLoadExcelData.RunWorkerAsync();
                }
            }
            else
            {
                return;
            }
        }
        private void bwLoadExcelData_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application excelApplication = new Excel.Application();
            Excel.Workbook excelWorkbook = excelApplication.Workbooks.Open(filePath);
            //excelApplication.Visible = true;
            Excel.Worksheet excelWorksheet;
            Excel.Range excelRange;
            try
            {
                excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets[1];
                excelRange = excelWorksheet.UsedRange;

                string patternNo = "";
                patternNo = GetString.GetPatternNoFromFilePath(filePath);

                if (patternNo == "")
                {
                    MessageBox.Show("Có Lỗi Xảy Ra Khi Lấy PM. Kiểm Tra Tên ExcelFile !!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string shoeName = "";
                shoeName = GetString.GetShoeNameFromFilePath(filePath);
                if (shoeName == "")
                {
                    MessageBox.Show("Có Lỗi Xảy Ra Khi Lấy ShoeName. Kiểm Tra Tên ExcelFile !!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                for (int i = 2; i <= excelRange.Rows.Count; i++)
                {
                    AssemblyIncentiveModel assemblyIncentive = new AssemblyIncentiveModel();
                    assemblyIncentive.ShoeName = shoeName;
                    assemblyIncentive.PatternNo = patternNo;

                    var efficiencyValue = (excelRange.Cells[i, 1] as Excel.Range).Value2;
                    string efficiencyString = "";
                    double efficiency = 0;
                    if (efficiencyValue != null)
                    {
                        efficiencyString = efficiencyValue.ToString();
                        double.TryParse(efficiencyString, out efficiency);
                    }
                    assemblyIncentive.Efficiency = efficiency;

                    var outputValue = (excelRange.Cells[i, 2] as Excel.Range).Value2;
                    string outputString = "";
                    double output = 0;
                    if (outputValue != null)
                    {
                        outputString = outputValue.ToString();
                        Double.TryParse(outputString, out output);
                    }
                    assemblyIncentive.AssemblyOutput = (int)Math.Round(output, MidpointRounding.AwayFromZero);

                    var incentiveValue = (excelRange.Cells[i, 3] as Excel.Range).Value2;
                    string incentiveString = "";
                    int incentive = 0;
                    if (incentiveValue != null)
                    {
                        incentiveString = incentiveValue.ToString();
                        Int32.TryParse(incentiveString, out incentive);
                    }
                    assemblyIncentive.Incentive = incentive;

                    var workerValue = (excelRange.Cells[i, 4] as Excel.Range).Value2;
                    string workerString = "";
                    int worker = 0;
                    if (workerValue != null)
                    {
                        workerString = workerValue.ToString();
                        Int32.TryParse(workerString, out worker);
                    }
                    assemblyIncentive.Worker = worker;

                    assemblyIncentiveViewList.Add(assemblyIncentive);
                }
            }
            catch
            {
                assemblyIncentiveViewList.Clear();
            }
            finally
            {
                excelWorkbook.Close(false, Missing.Value, Missing.Value);
                excelApplication.Quit();
            }
        }
        private void bwLoadExcelData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = null;
            btnChooseExcelFile.IsEnabled = true;
            if (assemblyIncentiveViewList.Count() > 0)
            {
                dgvAssemblyIncentive.ItemsSource = assemblyIncentiveViewList;
                btnImport.IsEnabled = true;
                MessageBox.Show(string.Format("Read Completed !"), this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Excel file error. Try Again!", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void miRevise_Click(object sender, RoutedEventArgs e)
        {
            groupRevise.Visibility = Visibility.Visible;
            groupImport.Visibility = Visibility.Collapsed;
            txtPatternNo.Focus();
        }
        private void miImport_Click(object sender, RoutedEventArgs e)
        {
            groupImport.Visibility = Visibility.Visible;
            groupRevise.Visibility = Visibility.Collapsed;
        }
        private void txtPatternNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSearch.IsDefault = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            groupImport.Visibility = Visibility.Visible;
            groupRevise.Visibility = Visibility.Collapsed;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string patternNo = "";
            if (assemblyIncentiveSearchList.Count > 0)
            {
                patternNo = assemblyIncentiveSearchList.Select(s => s.PatternNo).FirstOrDefault();
            }
            if (MessageBox.Show("Confirm Delete?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AssemblyIncentiveController.Delete(patternNo);
                MessageBox.Show("Deleted!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                dgvAssemblyIncentiveRevise.ItemsSource = null;
            }
        }
    }
}
