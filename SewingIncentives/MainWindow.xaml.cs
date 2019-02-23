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
using SewingIncentives.Models;
using SewingIncentives.Controllers;
using System.Collections.ObjectModel;
using SewingIncentives.ViewModels;
using SewingIncentives.Views;
using SewingIncentives.Helpers;
namespace SewingIncentives
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SectionModel section;
        BackgroundWorker bwLoadData;
        List<LineModel> lineList;
        List<PersonalModel> personalList;
        List<SourceModel> sourceList;
        List<WorkerLoginModel> workerLoginList;
        ObservableCollection<WorkerLoginViewModel> workerLoginViewList;
        BackgroundWorker bwInsertWorkerLogin;
        WorkerLoginModel workerLoginToImport;
        WorkerLoginViewModel workerLoginViewToImport;
        bool resultInsert;
        List<LineModel> lineAllList;
        BackgroundWorker threadOtherDate;
        public MainWindow(SectionModel section)
        {
            InitializeComponent();
            this.section = section;
            bwLoadData = new BackgroundWorker();
            bwLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadData_RunWorkerCompleted);
            bwLoadData.DoWork += new DoWorkEventHandler(bwLoadData_DoWork);
            lineList = new List<LineModel>();
            personalList = new List<PersonalModel>();
            sourceList = new List<SourceModel>();
            workerLoginList = new List<WorkerLoginModel>();
            workerLoginViewList = new ObservableCollection<WorkerLoginViewModel>();
            bwInsertWorkerLogin = new BackgroundWorker();
            bwInsertWorkerLogin.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwInsertWorkerLogin_RunWorkerCompleted);
            bwInsertWorkerLogin.DoWork += new DoWorkEventHandler(bwInsertWorkerLogin_DoWork);
            workerLoginToImport = new WorkerLoginModel();
            workerLoginViewToImport = new WorkerLoginViewModel();
            resultInsert = false;

            threadOtherDate = new BackgroundWorker();
            threadOtherDate.DoWork += new DoWorkEventHandler(threadOtherDate_DoWork);
            threadOtherDate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadOtherDate_RunWorkerCompleted);
            lineAllList = new List<LineModel>();
        }        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            dpOtherDate.SelectedDate = DateTime.Now.Date;
            if (bwLoadData.IsBusy == false)
            {
                workerLoginViewList.Clear();
                this.Cursor = Cursors.Wait;
                bwLoadData.RunWorkerAsync();
            }
        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime today = DateTime.Now;
            lineList = LineController.Select(section.SectionId);
            personalList = PersonalController.Select(section.Keyword);
            workerLoginList = WorkerLoginController.SelectByLine(section.SectionId, today);
            lineAllList = LineController.Select();
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string version = AssemblyHelper.Version();
            miRequestList.Visibility = Visibility.Collapsed;
            if (section.SectionId == "F")
            {
               this.Title =  string.Format("Assembly Incentives - {0}", version);
               miAssemblyReportIncentives.IsEnabled = true;
            }
            if (section.SectionId == "E")
            {
                this.Title = string.Format("Sewing Incentives - {0}", version);
            }
            if (section.SectionId == "B" || section.SectionId == "C")
            {
                this.Title = string.Format("Cut-Prep Incentives - {0}", version);
            }
            if (section.SectionId == "G")
            {
                this.Title = string.Format("Outsole Incentives - {0}", version);
            }
            if (section.SectionId == "IE")
            {
                miRequestList.Visibility = Visibility.Visible;
            }
            int countColumn = gridLine.ColumnDefinitions.Count();
            int countRow = countRow = lineList.Count / countColumn;
            if (lineList.Count % countColumn != 0)
            {
                countRow = lineList.Count / countColumn + 1;
            }
            gridLine.RowDefinitions.Clear();
            for (int i = 1; i <= countRow; i++)
            {
                RowDefinition rd = new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star),
                };
                gridLine.RowDefinitions.Add(rd);
            }

            for (int i = 0; i <= lineList.Count() - 1; i++)
            {
                LineModel line = lineList[i];
                Button btnLine = new Button();
                Grid.SetColumn(btnLine, i % countColumn);
                Grid.SetRow(btnLine, i / countColumn);
                btnLine.Margin = new Thickness(3, 1, 3, 1);
                btnLine.Content = String.Format("LINE {0}", line.Name);
                btnLine.Tag = line;
                btnLine.ToolTip = String.Format("Log-in to LINE {0}", line.Name);
                btnLine.Background = Brushes.RoyalBlue;
                btnLine.Foreground = Brushes.White;
                btnLine.FontWeight = FontWeights.Bold;
                btnLine.Click += new RoutedEventHandler(btnLine_Click);
                gridLine.Children.Add(btnLine);
            }

            foreach (WorkerLoginModel workerLogin in workerLoginList)
            {
                PersonalModel personal = personalList.Where(p => p.CardId == workerLogin.CardId || p.WorkerId == workerLogin.WorkerId).FirstOrDefault();
                if (personal != null)
                {
                    WorkerLoginViewModel workerLoginView = new WorkerLoginViewModel
                    {
                        CardId = personal.CardId,
                        WorkerId = personal.WorkerId,
                        Name = personal.Name,
                        Line = personal.Department,
                        LineWork = workerLogin.LineId,
                        Position = personal.Position,
                    };
                    workerLoginViewList.Add(workerLoginView);
                }
            }
            dgWorkerLogin.ItemsSource = workerLoginViewList;
            txtCardId.IsEnabled = true;
            this.Cursor = null;
        }        

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            Button btnLine = (Button)sender as Button;
            btnLogin.IsEnabled = true;
            LineModel tag = btnLine.Tag as LineModel;
            btnLogin.Content = String.Format("Log-in to\nLINE {0}", tag.Name);
            btnLogin.Tag = tag.LineId;
            txtCardId.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string cardId = txtCardId.Text;
            if (chbOtherDate.IsChecked == true)
            {
                if (threadOtherDate.IsBusy == false)
                {
                    this.Cursor = Cursors.Wait;
                    txtCardId.IsEnabled = false;

                    DateTime date = dpOtherDate.SelectedDate.Value.Date;
                    List<Object> arguments = new List<Object>();
                    arguments.Add(cardId);
                    arguments.Add(date);
                    threadOtherDate.RunWorkerAsync(arguments);
                }
            }
            else
            {
                PersonalModel personal = personalList.Where(p => p.CardId == cardId || p.WorkerId == cardId).FirstOrDefault();
                if (personal == null)
                {
                    MessageBox.Show("This Worker Not Found!", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCardId.Focus();
                    txtCardId.SelectAll();
                    return;
                }
                workerLoginViewToImport = workerLoginViewList.Where(w => w.CardId == personal.CardId && w.WorkerId == personal.WorkerId).FirstOrDefault();
                if (workerLoginViewToImport != null)
                {
                    dgWorkerLogin.SelectedItem = workerLoginViewToImport;
                    string lineWork = workerLoginViewToImport.LineWork;
                    LineModel line = lineList.Where(l => l.LineId == lineWork).FirstOrDefault();
                    if (line != null)
                    {
                        lineWork = line.Name;
                    }
                    MessageBox.Show(String.Format("Id: {0}\nName: {1}\nPosition: {2}\nWork at Line: LINE {3}", workerLoginViewToImport.WorkerId, workerLoginViewToImport.Name, workerLoginViewToImport.Position, lineWork), this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                    txtCardId.Focus();
                    txtCardId.SelectAll();
                    return;
                }
                workerLoginViewToImport = new WorkerLoginViewModel
                {
                    CardId = personal.CardId,
                    WorkerId = personal.WorkerId,
                    Name = personal.Name,
                    Line = personal.Department,
                    LineWork = btnLogin.Tag.ToString(),
                    Position = personal.Position,
                };

                workerLoginToImport = new WorkerLoginModel
                {
                    CardId = personal.CardId,
                    WorkerId = personal.WorkerId,
                    LineId = btnLogin.Tag.ToString(),
                };

                if (bwInsertWorkerLogin.IsBusy == false)
                {
                    this.Cursor = Cursors.Wait;
                    resultInsert = false;
                    txtCardId.IsEnabled = false;
                    bwInsertWorkerLogin.RunWorkerAsync();
                }
            }           
        }

        private void threadOtherDate_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Object> arguments = e.Argument as List<Object>;
            string cardId = arguments[0] as String;
            DateTime? date = arguments[1] as DateTime?;

            PersonalModel personal = personalList.Where(p => p.CardId == cardId || p.WorkerId == cardId).FirstOrDefault();
            //WorkerLoginModel workerLogin = WorkerLoginController.SelectTop1(cardId, date.Value);
            LinePerformanceDetailModel linePerformanceDetail = LinePerformanceDetailController.SelectTop1(cardId, date.Value);

            List<Object> results = new List<Object>();
            results.Add(personal);
            //results.Add(workerLogin);
            results.Add(linePerformanceDetail);
            e.Result = results;            
        }

        private void threadOtherDate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<Object> results = e.Result as List<Object>;
            PersonalModel personal = results[0] as PersonalModel;
            //WorkerLoginModel workerLogin = results[1] as WorkerLoginModel;
            LinePerformanceDetailModel linePerformanceDetail = results[1] as LinePerformanceDetailModel;
            string name = "";
            string position = "";
            if (personal != null)
            {
                name = personal.Name;
                position = personal.Position;
            }
            if (linePerformanceDetail != null)
            {
                string lineWork = linePerformanceDetail.LineId;
                LineModel line = lineAllList.Where(l => l.LineId == lineWork).FirstOrDefault();
                if (line != null)
                {
                    lineWork = line.Name;
                }

                MessageBox.Show(String.Format("Id: {0}\nName: {1}\nPosition: {2}\nDate: {6:dd-MM-yyyy}\nWork at Line: LINE {3}\nOther Position: {4}\nIncentive Grade: {5}"
                    , linePerformanceDetail.WorkerId, name, position, lineWork, linePerformanceDetail.OthersPosition, linePerformanceDetail.IncentiveGrade, linePerformanceDetail.Date), this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Null!", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            txtCardId.IsEnabled = true;
            txtCardId.Focus();
            txtCardId.SelectAll();
            this.Cursor = null;            
        }

        private void bwInsertWorkerLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            resultInsert = WorkerLoginController.Insert(workerLoginToImport);
        }

        private void bwInsertWorkerLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (resultInsert == true)
            {
                workerLoginViewList.Insert(0, workerLoginViewToImport);
                dgWorkerLogin.SelectedItem = workerLoginViewToImport;
            }
            txtCardId.IsEnabled = true;
            txtCardId.Focus();
            txtCardId.SelectAll();
            this.Cursor = null;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Confirm Close?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void miLinePerformance_Click(object sender, RoutedEventArgs e)
        {
            LinePerformanceWindow window = new LinePerformanceWindow(section);
            window.Show();
        }

        private void miExcessWorker_Click(object sender, RoutedEventArgs e)
        {
            ExcessWorkerPrintWindow window = new ExcessWorkerPrintWindow(section);
            window.Show();
        }

        private void miLineSummary_Click(object sender, RoutedEventArgs e)
        {
            LineSummaryPrintWindow window = new LineSummaryPrintWindow(section);
            window.Show();
        }

        private void miOtherPosition_Click(object sender, RoutedEventArgs e)
        {
            OtherPositionPrintWindow window = new OtherPositionPrintWindow(section);
            window.Show();
        }

        private void chbOtherDate_Checked(object sender, RoutedEventArgs e)
        {
            dpOtherDate.Visibility = Visibility.Visible;
        }

        private void chbOtherDate_Unchecked(object sender, RoutedEventArgs e)
        {
            dpOtherDate.Visibility = Visibility.Collapsed;
        }

        private void miPregnant_Click(object sender, RoutedEventArgs e)
        {
            WorkerLeaveWindow window = new WorkerLeaveWindow();
            window.Show();
        }

        private void miAttendance_Click(object sender, RoutedEventArgs e)
        {
            AttendanceWindow window = new AttendanceWindow(section);
            window.Show();
        }

        private void miLineIncentiveSummary_Click(object sender, RoutedEventArgs e)
        {
            LineIncentiveSummaryWindow window = new LineIncentiveSummaryWindow(section);
            window.Show();
        }

        private void miGroupIncentive_Click(object sender, RoutedEventArgs e)
        {
            GroupIncentivePrintWindow window = new GroupIncentivePrintWindow(section);
            window.Show();
        }

        private void miOtherPositionDaily_Click(object sender, RoutedEventArgs e)
        {
            OtherPositionListPrintWindow window = new OtherPositionListPrintWindow(section);
            window.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void miChangeCardId_Click(object sender, RoutedEventArgs e)
        {
            ChangeCardIdWindow window = new ChangeCardIdWindow();
            window.ShowDialog();
        }

        private void miSpecialTransfer_Click(object sender, RoutedEventArgs e)
        {
            SpecialTransferWindow window = new SpecialTransferWindow();
            window.ShowDialog();
        }

        private void miChangeWorkerNo_Click(object sender, RoutedEventArgs e)
        {
            ChangeWorkerIdWindow window = new ChangeWorkerIdWindow();
            window.Show();
        }

        private void miDayLeaveSummary_Click(object sender, RoutedEventArgs e)
        {
            DayLeaveSummaryWindow window = new DayLeaveSummaryWindow(section);
            window.Show();
        }

        private void miStandardIncentive_Click(object sender, RoutedEventArgs e)
        {
            ImportAssemblyIncentiveWindow window = new ImportAssemblyIncentiveWindow();
            window.Show();
        }

        private void miVersion_Click(object sender, RoutedEventArgs e)
        {
            string version = "Update History\n" +
                "1.18.03.23" + ": " + "Update: Calculate, Report incentives for assembly, put TimeShift";
            MessageBox.Show(version, string.Format("Current Version: {0}", AssemblyHelper.Version()), MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void miRequestList_Click(object sender, RoutedEventArgs e)
        {
            RequestListWindow window = new RequestListWindow();
            window.Show();
        }

        private void miAssemblyReportIncentives_Click(object sender, RoutedEventArgs e)
        {
            AssemblyReportIncentivesWindow window = new AssemblyReportIncentivesWindow(section);
            window.Show();
        }
    }
}
