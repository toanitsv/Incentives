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
using SewingIncentives.Helpers;
using SewingIncentives.ViewModels;
using SewingIncentives.Views;
using System.Collections.ObjectModel;
namespace SewingIncentives.Views
{
    /// <summary>
    /// Interaction logic for LinePerformanceWindow.xaml
    /// </summary>
    public partial class LinePerformanceWindow : Window
    {
        SectionModel section;
        BackgroundWorker bwLoadData;
        List<LineModel> lineList;
        List<PersonalModel> personalList;
        BackgroundWorker bwSearch;
        LinePerformanceModel linePerformance;
        List<LinePerformanceModel> linePerformanceMonthList;
        List<LinePerformanceDetailModel> linePerformanceDetailList;
        List<LinePerformanceDetailModel> linePerformanceDetailMonthList;
        List<WorkerLoginModel> workerLoginList;
        List<SourceModel> sourceList;
        List<LineModel> lineAllList;
        LineModel line;
        DateTime date;
        List<WorkerFilterModel> workerFilterList;
        List<IncentiveGradeModel> incentiveGradeList;
        ObservableCollection<LinePerformanceViewModel> linePerformanceViewList;
        LinePerformanceModel linePerformanceToInsert;
        List<LinePerformanceDetailModel> linePerformanceDetailToInsertList;
        BackgroundWorker bwInsert;
        bool isEdited;
        BackgroundWorker bwTransfer;
        bool isTransfered;
        LinePerformanceViewModel linePerformanceViewToTransfer;
        List<SectionModel> sectionList;
        string[] timeOutList;
        string[] adjustTimeInList;
        List<PositionModel> positionList;
        List<OtherPositionModel> otherPositionList;
        List<SpecialTransferModel> transferList;
        List<AssemblyIncentiveModel> assemblyIncentiveList;
        CieControlIncentiveModel cieControlIncentive;
        public LinePerformanceWindow(SectionModel section)
        {
            this.section = section;
            bwLoadData = new BackgroundWorker();
            bwLoadData.DoWork += new DoWorkEventHandler(bwLoadData_DoWork);
            bwLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadData_RunWorkerCompleted);
            lineList = new List<LineModel>();
            personalList = new List<PersonalModel>();
            bwSearch = new BackgroundWorker();
            bwSearch.DoWork += new DoWorkEventHandler(bwSearch_DoWork);
            bwSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwSearch_RunWorkerCompleted);
            linePerformance = new LinePerformanceModel();
            linePerformanceMonthList = new List<LinePerformanceModel>();
            linePerformanceDetailList = new List<LinePerformanceDetailModel>();
            linePerformanceDetailMonthList = new List<LinePerformanceDetailModel>();
            workerLoginList = new List<WorkerLoginModel>();
            sourceList = new List<SourceModel>();
            lineAllList = new List<LineModel>();
            line = new LineModel();
            date = new DateTime(2000, 1, 1);
            workerFilterList = new List<WorkerFilterModel>();
            incentiveGradeList = new List<IncentiveGradeModel>();
            linePerformanceViewList = new ObservableCollection<LinePerformanceViewModel>();
            linePerformanceDetailToInsertList = new List<LinePerformanceDetailModel>();
            bwInsert = new BackgroundWorker();
            bwInsert.DoWork += new DoWorkEventHandler(bwInsert_DoWork);
            bwInsert.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwInsert_RunWorkerCompleted);
            isEdited = false;
            bwTransfer = new BackgroundWorker();
            bwTransfer.DoWork += new DoWorkEventHandler(bwTransfer_DoWork);
            bwTransfer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwTransfer_RunWorkerCompleted);
            isTransfered = false;
            linePerformanceViewToTransfer = new LinePerformanceViewModel();
            sectionList = new List<SectionModel>();
            positionList = new List<PositionModel>();
            otherPositionList = new List<OtherPositionModel>();
            transferList = new List<SpecialTransferModel>();
            assemblyIncentiveList = new List<AssemblyIncentiveModel>();
            cieControlIncentive = new CieControlIncentiveModel();
            InitializeComponent();
        }
        double workhrsBefore = 0, workhrsAfter = 0;
        int incentiveBefore = 0, incentiveAfter = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timeOutList = TimeoutModel.Init();
            adjustTimeInList = AdjustTimeInModel.Init();
            positionList = PositionModel.Init();
            otherPositionList = OtherPositionModel.Init();

            dpDate.SelectedDate = DateTime.Now;
            workerFilterList = WorkerFilterModel.Create(section.SectionId);
            string[] othersPositionList = otherPositionList.Where(o => o.IsShow == true).Select(o => o.Name).ToArray();
            colOthersPosition.ItemsSource = othersPositionList;
            incentiveGradeList = IncentiveGradeModel.Select().Where(i => i.SectionId == section.SectionId).ToList();
            colICGrade.ItemsSource = incentiveGradeList;
            lvIncentiveGrade.ItemsSource = incentiveGradeList;
            colAdjustTimeOut.ItemsSource = timeOutList;
            colAdjustTimeIn.ItemsSource = adjustTimeInList;

            if (bwLoadData.IsBusy == false)
            {
                this.Cursor = Cursors.Wait;
                bwLoadData.RunWorkerAsync();
            }
        }

        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            lineAllList = LineController.Select();
            lineList = LineController.Select(section.SectionId);
            personalList = PersonalController.Select();
            sectionList = SectionController.Select();
        }

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string version = AssemblyHelper.Version();
            if (section.SectionId == "F")
            {
                this.Title = string.Format("Assembly Incentives - {0}", version);
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
            LineModel lineNaN = new LineModel
            {
                LineId = "NaN",
                Name = "NaN",
            };
            lineList.Insert(0, lineNaN);
            cboLineId.ItemsSource = lineList;
            cboLineId.SelectedItem = lineList.FirstOrDefault();
            cboLineId.IsEnabled = true;
            this.Cursor = null;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            line = (LineModel)cboLineId.SelectedItem as LineModel;
            if (String.IsNullOrEmpty(line.LineId) == false && line.LineId != "NaN")
            {
                if (bwSearch.IsBusy == false)
                {
                    this.Cursor = Cursors.Wait;
                    cboLineId.IsEnabled = false;
                    date = dpDate.SelectedDate.Value;

                    txtOutput.Clear();
                    txtArticleNo.Clear();
                    txtPatternNo.Clear();
                    txtIncentiveGradeA.Clear();
                    txtQCReportedHrs.Clear();
                    txtIncentiveGradeALower.Clear();
                    txtQuotaTarget.Clear();

                    txtIncentivePM1.Clear();
                    txtIncentivePM2.Clear();
                    txtIncentivePM3.Clear();
                    txtIncentivePM4.Clear();
                    txtIncentivePM5.Clear();

                    txtOutputPM1.Clear();
                    txtOutputPM2.Clear();
                    txtOutputPM3.Clear();
                    txtOutputPM4.Clear();
                    txtOutputPM5.Clear();

                    txtPatternNoPM1.Clear();
                    txtPatternNoPM2.Clear();
                    txtPatternNoPM3.Clear();
                    txtPatternNoPM4.Clear();
                    txtPatternNoPM5.Clear();

                    lblWHRS.Text = "0";
                    lblPPH.Text = "0";
                    lblCountSUP.Text = "0";
                    lblCountMECH.Text = "0";
                    lblCountMONITOR.Text = "0";
                    lblCountLL.Text = "0";
                    lblCountQC.Text = "0";
                    lblCountNewWorker.Text = "0";
                    lblCount1Month.Text = "0";
                    lblCount2Months.Text = "0";
                    lblCountOldWorker.Text = "0";
                    lblCountTotalMP.Text = "0";
                    lblCountTotalWorker.Text = "0";

                    lblWHRSAfter.Clear();
                    lblWHRSAfter.IsEnabled = false;
                    btnRepairWHrs.IsEnabled = false;

                    lblExcessIncentive.Tag = new int[] { 0, 0 };
                    lblExcessIncentive.Text = "0";
                    linePerformanceViewList.Clear();

                    miTransferTo.Items.Clear();
                    isEdited = false;
                    btnSave.IsEnabled = false;
                    bwSearch.RunWorkerAsync();
                }
            }
        }

        private void bwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            linePerformance = LinePerformanceController.Select(line.LineId, date);
            linePerformanceMonthList = LinePerformanceController.Select(line.LineId, date.Month, date.Year);
            linePerformanceDetailList = LinePerformanceDetailController.Select(line.LineId, date);
            linePerformanceDetailMonthList = LinePerformanceDetailController.SelectByWorkerLogin(line.LineId, date);
            workerLoginList = WorkerLoginController.Select(line.LineId, date);
            sourceList = SourceController.Select(section.Keyword, date);
            transferList = SpecialTransferController.Select(line.LineId, date);
            cieControlIncentive = CieControlIncentiveController.Select(line.LineId, date);
        }

        private void bwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (linePerformance != null)
            {
                if (linePerformance.Output > 0)
                {
                    //isEdited = true;
                }
                txtOutput.Text = linePerformance.Output.ToString();
                txtPatternNo.Text = linePerformance.PatternNo;
                txtArticleNo.Text = linePerformance.ArticleNo;
                txtIncentiveGradeA.Text = linePerformance.IncentiveGradeA.ToString();
                txtIncentiveGradeALower.Text = linePerformance.IncentiveGradeASmall.ToString();
                txtQCReportedHrs.Text = linePerformance.ReportedWorkHour.ToString();
                txtQuotaTarget.Text = linePerformance.QuotaTarget.ToString();
            }

            if (cieControlIncentive != null)
            {
                txtIncentiveGradeA.Text = cieControlIncentive.IncentiveAfter.ToString();
            }

            foreach (WorkerLoginModel workerLogin in workerLoginList)
            {
                PersonalModel personal = personalList.Where(p => p.CardId == workerLogin.CardId && p.WorkerId == workerLogin.WorkerId).FirstOrDefault();
                SourceModel sourceTimeIn = sourceList.Where(s => s.CardId == workerLogin.CardId).OrderBy(s => s.Time).FirstOrDefault();
                SourceModel source = sourceList.Where(s => s.CardId == workerLogin.CardId).OrderBy(s => s.Time).LastOrDefault();
                if (personal != null)
                {
                    LinePerformanceViewModel linePerformanceView = new LinePerformanceViewModel
                    {
                        CardId = personal.CardId,
                        WorkerId = personal.WorkerId,
                        Name = personal.Name,
                        Line = personal.Department,
                        HiredDate = personal.HiredDate,
                        Position = personal.Position,
                        AdjustTimeIn = "07:30",
                        TimeIn = "07:30",
                        TimeOut = "07:30",
                        WorkHour = 0,
                    };

                    if (source != null)
                    {
                        linePerformanceView.TimeOut = String.Format("{0}:{1}", source.Time.Substring(0, 2), source.Time.Substring(2, 2));
                    }
                    if (sourceTimeIn != null)
                    {
                        linePerformanceView.TimeIn = String.Format("{0}:{1}", sourceTimeIn.Time.Substring(0, 2), sourceTimeIn.Time.Substring(2, 2));
                    }

                    SpecialTransferModel transfer = transferList.Where(t => t.WorkerId == personal.WorkerId).FirstOrDefault();
                    if (transfer != null)
                    {
                        linePerformanceView.AdjustTimeIn = "NaN";
                        if (transfer.TransferType == 1)
                        {
                            linePerformanceView.TimeIn = string.Format("{0:HH:mm}", new DateTime(transfer.Time.Ticks));
                            
                        }
                        else if (transfer.TransferType == 2)
                        {
                            linePerformanceView.TimeOut = string.Format("{0:HH:mm}", new DateTime(transfer.Time.Ticks));
                        }
                    }

                    // Add Shift
                    WorkerShiftModel workerShift = WorkerShiftController.Select(workerLogin.WorkerId, date.Year, date.Month);
                    if (workerShift != null)
                    {
                        string[] workTimeCodeArrayy = {"", workerShift.Date1, workerShift.Date2, workerShift.Date3, workerShift.Date4, workerShift.Date5, workerShift.Date6, workerShift.Date7,
                                                        workerShift.Date8, workerShift.Date9, workerShift.Date10, workerShift.Date11, workerShift.Date12, workerShift.Date13, workerShift.Date14,
                                                        workerShift.Date15, workerShift.Date16, workerShift.Date17, workerShift.Date18, workerShift.Date19, workerShift.Date20, workerShift.Date21,
                                                        workerShift.Date22, workerShift.Date23, workerShift.Date24, workerShift.Date25, workerShift.Date26, workerShift.Date27, workerShift.Date28,
                                                        workerShift.Date29, workerShift.Date30,  workerShift.Date31 };
                        WorkTimeInfoModel workTimeInModel = WorkTimeInfoController.Select(workTimeCodeArrayy[date.Day]);
                        if (workTimeInModel != null && (workTimeInModel.TimeIn != " " || workTimeInModel.TimeIn != ""))
                        {
                            string adjustTimeIn = workTimeInModel.TimeIn.Substring(0, 2) + ":" + workTimeInModel.TimeIn.Substring(2, 2);
                            linePerformanceView.AdjustTimeIn = adjustTimeIn;
                        }
                    }

                    WorkerFilterModel workerFilter = workerFilterList.Where(w => CalculateHelper.CalculateMonth(personal.HiredDate, date) >= w.MinMonth && CalculateHelper.CalculateMonth(personal.HiredDate, date) < w.MaxMonth).FirstOrDefault();
                    if (workerFilter != null)
                    {
                        linePerformanceView.WorkerRatio = workerFilter.Ratio;
                    }
                    LinePerformanceDetailModel linePerformanceDetail = linePerformanceDetailList.Where(l => l.CardId == workerLogin.CardId && l.WorkerId == workerLogin.WorkerId).FirstOrDefault();
                    if (linePerformanceDetail != null)
                    {
                        linePerformanceView.OthersPosition = linePerformanceDetail.OthersPosition;
                        linePerformanceView.AdjustTimeOut = linePerformanceDetail.TimeOut;
                        IncentiveGradeModel incentiveGrade = incentiveGradeList.Where(i => i.Name == linePerformanceDetail.IncentiveGrade).FirstOrDefault();
                        if (incentiveGrade != null)
                        {
                            linePerformanceView.IncentiveGrade = incentiveGrade;
                        }
                        linePerformanceView.Incentive = linePerformanceDetail.Incentive;
                        linePerformanceView.ExcessIncentive = linePerformanceDetail.ExcessIncentive;
                        linePerformanceView.SpecsIncentive = linePerformanceDetail.SpecsIncentive;
                    }
                    else
                    {
                        LinePerformanceDetailModel linePerformanceDetailLast = linePerformanceDetailMonthList.Where(l => l.CardId == workerLogin.CardId && l.WorkerId == workerLogin.WorkerId && l.IncentiveGrade != "NaN").OrderBy(l => l.Date).LastOrDefault();
                        if (linePerformanceDetailLast != null)
                        {
                            IncentiveGradeModel incentiveGrade = incentiveGradeList.Where(i => i.Name == linePerformanceDetailLast.IncentiveGrade).FirstOrDefault();
                            if (incentiveGrade != null)
                            {
                                linePerformanceView.IncentiveGrade = incentiveGrade;
                            }
                        }
                    }
                    linePerformanceView.CumulativeIncentive = linePerformanceDetailMonthList.Where(l => l.CardId == personal.CardId && l.WorkerId == personal.WorkerId && l.Date.Date < date.Date).Sum(l => (l.Incentive + l.SpecsIncentive)) + linePerformanceView.Incentive + linePerformanceView.ExcessIncentive + linePerformanceView.SpecsIncentive;
                    linePerformanceViewList.Add(linePerformanceView);
                }
            }

            cboLineId.IsEnabled = true;
            dgLinePerformance.ItemsSource = linePerformanceViewList;
            if (linePerformanceViewList.Count > 0)
            {
                foreach (SectionModel section1 in sectionList)
                {
                    MenuItem miSection = new MenuItem
                    {
                        Header = section1.Name,
                        ToolTip = section1.SectionId,
                    };
                    List<LineModel> lineList1 = lineAllList.Where(l => l.SectionId == section1.SectionId).ToList();
                    foreach (LineModel line1 in lineList1)
                    {
                        if (line1.LineId != line.LineId)
                        {
                            MenuItem miLine = new MenuItem
                            {
                                Header = String.Format("LINE {0}", line1.Name),
                                Tag = line1.LineId,
                                ToolTip = String.Format("Transfer to LINE {0}", line1.LineId),
                            };
                            if (section1.SectionId == "F")
                            {
                                MenuItem miLineClean = new MenuItem
                                {
                                    Header = "Clean/Repair",
                                    Tag = line1.LineId,
                                    ToolTip = String.Format("Transfer to LINE {0}(Clean/Repair)", line1.LineId),
                                };
                                miLineClean.Click += new RoutedEventHandler(miLineClean_Click);
                                MenuItem miLineBorrow = new MenuItem
                                {
                                    Header = "Borrow",
                                    Tag = line1.LineId,
                                    ToolTip = String.Format("Transfer to LINE {0}(Borrow)", line1.LineId),
                                };
                                miLineBorrow.Click += new RoutedEventHandler(miLine_Click);
                                miLine.Items.Add(miLineClean);
                                miLine.Items.Add(miLineBorrow);
                            }
                            else
                            {
                                miLine.Click += new RoutedEventHandler(miLine_Click);
                            }

                            miSection.Items.Add(miLine);
                        }
                    }
                    miTransferTo.Items.Add(miSection);
                }
            }
            btnCalculateIncentive.IsEnabled = true;
            btnRepairWHrs.IsEnabled = true;
            lblWHRSAfter.Visibility = Visibility.Collapsed;
            btnRefresh.Visibility = Visibility.Collapsed;
            this.Cursor = null;
        }

        private void miLineClean_Click(object sender, RoutedEventArgs e)
        { }

        private void miLine_Click(object sender, RoutedEventArgs e)
        {
            MenuItem miLine = (MenuItem)sender as MenuItem;
            linePerformanceViewToTransfer = (LinePerformanceViewModel)dgLinePerformance.CurrentItem as LinePerformanceViewModel;
            if (linePerformanceViewToTransfer != null)
            {
                if (bwTransfer.IsBusy == false)
                {
                    this.Cursor = Cursors.Wait;
                    isTransfered = false;
                    miTransferTo.Tag = miLine.Tag;
                    bwTransfer.RunWorkerAsync();
                }
            }
        }

        private void bwTransfer_DoWork(object sender, DoWorkEventArgs e)
        {
            string lineIdNew = "";
            miTransferTo.Dispatcher.Invoke((Action)(() => lineIdNew = miTransferTo.Tag.ToString()));
            if (String.IsNullOrEmpty(lineIdNew) == false)
            {
                isTransfered = LinePerformanceDetailController.Transfer(line.LineId, date, linePerformanceViewToTransfer.CardId, linePerformanceViewToTransfer.WorkerId, lineIdNew);
            }
        }

        private void bwTransfer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isTransfered == true)
            {
                workerLoginList.RemoveAll(w => w.CardId == linePerformanceViewToTransfer.CardId && w.WorkerId == linePerformanceViewToTransfer.WorkerId);
                linePerformanceViewList.Remove(linePerformanceViewToTransfer);
            }
            this.Cursor = null;
        }

        private void btnCaculate_Click(object sender, RoutedEventArgs e)
        {
            int normalIncentive = 0;
            int.TryParse(txtIncentiveGradeA.Text, out normalIncentive);

            int smallIncentive = 0;
            int.TryParse(txtIncentiveGradeALower.Text, out smallIncentive);

            double reportWHrs = 0;
            double.TryParse(txtQCReportedHrs.Text, out reportWHrs);

            //double wHrsTotal = 0;
            //int wkrTotal = 0;
            //double mpTotal = 0;
            //foreach (LinePerformanceViewModel l in linePerformanceViewList)
            //{
            //    if (l.Line != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(l.Position) == true && otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(l.OthersPosition) == true)
            //    {
            //        double wHrs = 0;
            //        if (String.IsNullOrEmpty(l.AdjustTimeOut) == true || l.AdjustTimeOut == "NaN")
            //        {
            //            if (String.IsNullOrEmpty(l.AdjustTimeIn) == true || l.AdjustTimeIn == "NaN")
            //            {
            //                wHrs = CalculateHelper.CalculateWHRS(l.TimeIn, l.TimeOut);
            //            }
            //            else
            //            {
            //                wHrs = CalculateHelper.CalculateWHRS(l.AdjustTimeIn, l.TimeOut);
            //            }
            //        }
            //        else
            //        {
            //            if (String.IsNullOrEmpty(l.AdjustTimeIn) == true || l.AdjustTimeIn == "NaN")
            //            {
            //                wHrs = CalculateHelper.CalculateWHRS(l.TimeIn, l.AdjustTimeOut);
            //            }
            //            else
            //            {
            //                wHrs = CalculateHelper.CalculateWHRS(l.AdjustTimeIn, l.AdjustTimeOut);
            //            }
            //        }

            //        l.WorkHour = wHrs;
            //        wHrsTotal += wHrs;
            //        wkrTotal += 1;
            //        mpTotal += l.WorkerRatio;
            //    }
            //}

            //lblWHRS.Text = Math.Round(wHrsTotal / wkrTotal, 1).ToString();
            //int output = 0;
            //if (int.TryParse(txtOutput.Text, out output) == true)
            //{
            //    lblPPH.Text = Math.Round(output / wHrsTotal, 1).ToString();
            //}

            foreach (LinePerformanceViewModel linePerformanceView in linePerformanceViewList)
            {
                int incentive = 0;
                if (linePerformanceView.IncentiveGrade != null)
                {
                    incentive = (int)(normalIncentive * linePerformanceView.IncentiveGrade.Ratio);
                }

                double percentWorkHour = 1;
                if (linePerformanceView.WorkHour > 0 && reportWHrs > 0)
                {
                    percentWorkHour = linePerformanceView.WorkHour / reportWHrs;
                }

                if (percentWorkHour > 1)
                {
                    percentWorkHour = 1;
                }

                linePerformanceView.Incentive = (int)((double)incentive * linePerformanceView.WorkerRatio * percentWorkHour);
                if (linePerformanceView.IncentiveGrade != null && linePerformanceView.IncentiveGrade.IsSmallGrade == true)
                {
                    linePerformanceView.Incentive = smallIncentive;
                }
                linePerformanceView.CumulativeIncentive = linePerformanceDetailMonthList.Where(l => l.CardId == linePerformanceView.CardId && l.WorkerId == linePerformanceView.WorkerId && l.Date.Date < date.Date).Sum(l => (l.Incentive + l.SpecsIncentive)) + linePerformanceView.Incentive + linePerformanceView.SpecsIncentive + linePerformanceView.ExcessIncentive;
            }
            dgLinePerformance.ItemsSource = null;
            dgLinePerformance.ItemsSource = linePerformanceViewList;

            lblCountSUP.Text = linePerformanceViewList.Where(l => l.Position.Contains("SUPERVISOR") == true).Count().ToString();
            lblCountMECH.Text = linePerformanceViewList.Where(l => l.Position.Contains("MECHANIC") == true).Count().ToString();
            lblCountMONITOR.Text = linePerformanceViewList.Where(l => l.Position.Contains("MONITOR") == true || l.Position.Contains("MAKE BALANCE") == true).Count().ToString();
            lblCountLL.Text = linePerformanceViewList.Where(l => l.Position.Contains("LINE LEADER") == true).Count().ToString();
            lblCountQC.Text = linePerformanceViewList.Where(l => l.Line == "ASSEMBLY QUALITY" || l.Position.Contains("QC") == true).Count().ToString();

            lblCountNewWorker.Text = linePerformanceViewList.Where(l => l.Line != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(l.Position) == true && otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(l.OthersPosition) == true && CalculateHelper.CalculateMonth(l.HiredDate, date) < 1).Count().ToString();
            lblCount1Month.Text = linePerformanceViewList.Where(l => l.Line != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(l.Position) == true && otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(l.OthersPosition) == true && CalculateHelper.CalculateMonth(l.HiredDate, date) >= 1 && CalculateHelper.CalculateMonth(l.HiredDate, date) < 2).Count().ToString();
            lblCount2Months.Text = linePerformanceViewList.Where(l => l.Line != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(l.Position) == true && otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(l.OthersPosition) == true && CalculateHelper.CalculateMonth(l.HiredDate, date) >= 2 && CalculateHelper.CalculateMonth(l.HiredDate, date) < 3).Count().ToString();
            lblCountOldWorker.Text = linePerformanceViewList.Where(l => l.Line != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(l.Position) == true && otherPositionList.Where(o => o.IsCalculate == true).Select(o => o.Name).Contains(l.OthersPosition) == true && CalculateHelper.CalculateMonth(l.HiredDate, date) >= 3).Count().ToString();
            //lblCountTotalWorker.Text = linePerformanceViewList.Where(l => l.Line != "ASSEMBLY QUALITY" && positionList.Where(p => p.IsCalculate == true).Select(p => p.Name).Contains(l.Position) == true).Count().ToString();
            //lblCountTotalMP.Text = mpTotal.ToString();
            lblCountTotalWorker.Text = linePerformanceViewList.Count.ToString();

            //Calculate Excess Incentive Total
            int excessIncentiveTotalBeforeDate = linePerformanceMonthList.Where(l => l.Date.Date < date.Date).Sum(l => l.ExcessIncentive);
            int excessIncentiveWorkerBeforeDate = linePerformanceDetailMonthList.Where(l => l.Date.Date < date.Date).Sum(l => l.ExcessIncentive);
            int excessIncentiveTotalDate = 0;
            foreach (LinePerformanceViewModel linePerformanceView in linePerformanceViewList)
            {
                if (linePerformanceView.WorkHour > 0 && linePerformanceView.IncentiveGrade != null && linePerformanceView.IncentiveGrade.IsSmallGrade == false && linePerformanceView.WorkerRatio >= 1)
                {
                    //int a = ((int)Math.Round(linePerformanceView.IncentiveGrade.Ratio * normalIncentive, 0) - linePerformanceView.Incentive);
                    //if (a > 0)
                    //{
                    //    MessageBox.Show(linePerformanceView.WorkerId + " | " + a.ToString());
                    //}
                    
                    //chi tinh excessInc khi cong nhan cua chuyen do, chuyen khac chuyen qua khong tin update 27/03/2017

                    SpecialTransferModel transfer = transferList.Where(t => t.WorkerId == linePerformanceView.WorkerId && t.TransferType != 2 && t.Date == date).FirstOrDefault();
                    if (transfer == null)
                    {
                        //MessageBox.Show(linePerformanceView.WorkerId + " | " + transfer);
                        excessIncentiveTotalDate += ((int)Math.Round(linePerformanceView.IncentiveGrade.Ratio * normalIncentive, 0) - linePerformanceView.Incentive);
                    }
                }
            }
            int excessIncentiveWorkerDate = linePerformanceViewList.Where(l => l.WorkHour >= reportWHrs && l.IncentiveGrade != null && l.IncentiveGrade.IsSmallGrade == false).Sum(l => l.ExcessIncentive);
            int excessIncentiveTotal = (excessIncentiveTotalBeforeDate - excessIncentiveWorkerBeforeDate) + (excessIncentiveTotalDate); // - excessIncentiveWorkerDate);


            lblExcessIncentive.Text = (excessIncentiveTotal - excessIncentiveWorkerDate).ToString();
            lblExcessIncentive.Tag = new int[] {excessIncentiveTotalDate, excessIncentiveTotal};

            if (isEdited == false)
            {
                btnSave.IsEnabled = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int output = 0;
            int.TryParse(txtOutput.Text, out output);
            if (output < 0)
            {
                txtOutput.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtOutput.ClearValue(TextBox.BorderBrushProperty);
            }

            string patternNo = txtPatternNo.Text;
            if (output > 0 && String.IsNullOrEmpty(patternNo) == true)
            {
                txtPatternNo.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtPatternNo.ClearValue(TextBox.BorderBrushProperty);
            }

            string articleNo = txtArticleNo.Text;
            //if (output > 0 && String.IsNullOrEmpty(articleNo) == true)
            //{
            //    txtArticleNo.BorderBrush = Brushes.Red;
            //    return;
            //}
            //else
            //{
            //    txtArticleNo.ClearValue(TextBox.BorderBrushProperty);
            //}

            int incentiveGradeA = 0;
            int.TryParse(txtIncentiveGradeA.Text, out incentiveGradeA);
            if (output > 0 && incentiveGradeA <= 0)
            {
                txtIncentiveGradeA.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtIncentiveGradeA.ClearValue(TextBox.BorderBrushProperty);
            }

            int quotaTargetInsert = 0;
            int.TryParse(txtQuotaTarget.Text, out quotaTargetInsert);
            if (quotaTargetInsert < 0)
            {
                txtQuotaTarget.BorderBrush = Brushes.Red;
                return;
            }

            int incentiveGradeASmall = 0;
            int.TryParse(txtIncentiveGradeALower.Text, out incentiveGradeASmall);
            if (output > 0 && incentiveGradeASmall < 0)
            {
                txtIncentiveGradeALower.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtIncentiveGradeALower.ClearValue(TextBox.BorderBrushProperty);
            }


            double reportedWhrs = 0;
            double.TryParse(txtQCReportedHrs.Text, out reportedWhrs);
            if (output > 0 && reportedWhrs <= 0)
            {
                txtQCReportedHrs.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtQCReportedHrs.ClearValue(TextBox.BorderBrushProperty);
            }

            int[] excessIncentiveArray = lblExcessIncentive.Tag as int[];
            int excessIncentive = excessIncentiveArray[0];
            if (excessIncentive < 0)
            {
                excessIncentive = 0;
            }

            linePerformanceToInsert = new LinePerformanceModel
            {
                LineId = line.LineId,
                Date = date,
                Output = output,
                PatternNo = patternNo,
                ArticleNo = articleNo,
                IncentiveGradeA = incentiveGradeA,
                IncentiveGradeASmall = incentiveGradeASmall,
                ReportedWorkHour = reportedWhrs,
                ExcessIncentive = excessIncentive,
                QuotaTarget = quotaTargetInsert
            };

            linePerformanceDetailToInsertList.Clear();
            foreach (LinePerformanceViewModel linePerformanceView in dgLinePerformance.Items)
            {
                LinePerformanceDetailModel linePerformanceDetail = new LinePerformanceDetailModel
                {
                    LineId = line.LineId,
                    Date = date,
                    CardId = linePerformanceView.CardId,
                    WorkerId = linePerformanceView.WorkerId,
                    TimeOut = linePerformanceView.AdjustTimeOut,
                    OthersPosition = linePerformanceView.OthersPosition,
                    IncentiveGrade = "NaN",
                    Incentive = linePerformanceView.Incentive,
                    ExcessIncentive = linePerformanceView.ExcessIncentive,
                    SpecsIncentive = linePerformanceView.SpecsIncentive,
                };
                if (linePerformanceView.IncentiveGrade != null)
                {
                    linePerformanceDetail.IncentiveGrade = linePerformanceView.IncentiveGrade.Name;
                }

                if (String.IsNullOrEmpty(linePerformanceDetail.OthersPosition) == true)
                {
                    linePerformanceDetail.OthersPosition = "NaN";
                }
                if (String.IsNullOrEmpty(linePerformanceDetail.TimeOut) == true)
                {
                    linePerformanceDetail.TimeOut = "NaN";
                }
                if (String.IsNullOrEmpty(linePerformanceDetail.IncentiveGrade) == true)
                {
                    linePerformanceDetail.IncentiveGrade = "NaN";
                }
                linePerformanceDetailToInsertList.Add(linePerformanceDetail);
            }

            if (MessageBox.Show("Confirm Save?" +
                "\n(BẠN PHẢI CHẮC CHẮN ĐÃ ĐIỀN ĐẦY ĐỦ THÔNG TIN NHƯ LÀ OTHERS POSITION, TIMEOUT, INCENTIVE GRADE?)", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (bwInsert.IsBusy == false)
                {
                    this.Cursor = Cursors.Wait;
                    btnSave.IsEnabled = false;
                    bwInsert.RunWorkerAsync();
                }
            }
        }

        private void bwInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            LinePerformanceController.Insert(linePerformanceToInsert);
            foreach (LinePerformanceDetailModel linePerformanceDetail in linePerformanceDetailToInsertList)
            {
                LinePerformanceDetailController.Insert(linePerformanceDetail);
            }
        }

        private void bwInsert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = null;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Saved!", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            //linePerformanceViewList.Clear();
            btnCaculate.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            List<LinePerformanceViewModel> linePerformanceViewToPrintList = dgLinePerformance.Items.OfType<LinePerformanceViewModel>().ToList();
            LinePerformancePrintWindow window = new LinePerformancePrintWindow(linePerformanceViewToPrintList, line.Name, String.Format("{0:dd/MM/yyyy}", date),
                                                                               txtOutputPM1.Text, txtPatternNoPM1.Text, txtArticleNo.Text, lblWHRS.Text, lblPPH.Text,
                                                                               txtIncentiveGradeA.Text, "", "", "", "", "", "", "",
                                                                               lblCountSUP.Text, lblCountMECH.Text, lblCountMONITOR.Text, lblCountLL.Text, lblCountQC.Text, lblCountNewWorker.Text, lblCount1Month.Text, lblCount2Months.Text, lblCountOldWorker.Text, lblCountTotalMP.Text, lblCountTotalWorker.Text);
            window.Show();
        }

        private void btnIncentiveGradeLegend_Click(object sender, RoutedEventArgs e)
        {
            if (popupIncentiveGradeLegend.IsOpen == true)
            {
                popupIncentiveGradeLegend.IsOpen = false;
                return;
            }
            else
            {
                popupIncentiveGradeLegend.IsOpen = true;
                return;
            }
        }

        private void btnExcessIncentiveAll_Click(object sender, RoutedEventArgs e)
        {
            double reportWHrs = 0;
            double.TryParse(txtQCReportedHrs.Text, out reportWHrs);
            int excessIncentiveTotal = 0;
            int.TryParse(lblExcessIncentive.Text, out excessIncentiveTotal);

            if (excessIncentiveTotal < 100)
                return;

            int countWorkerHaveExcessIncentive = linePerformanceViewList.Where(l => l.WorkHour >= reportWHrs && l.IncentiveGrade != null && l.IncentiveGrade.IsSmallGrade == false).Count();
            int workerExcessIncentive = excessIncentiveTotal / countWorkerHaveExcessIncentive;
            foreach (LinePerformanceViewModel linePerformanceView in linePerformanceViewList)
            {
                if (linePerformanceView.WorkHour >= reportWHrs && linePerformanceView.IncentiveGrade != null && linePerformanceView.IncentiveGrade.IsSmallGrade == false)
                {
                    linePerformanceView.ExcessIncentive = workerExcessIncentive;
                    excessIncentiveTotal -= workerExcessIncentive;
                }
            }
        }

        private void btnExcessIncentiveManual_Click(object sender, RoutedEventArgs e)
        {
            //double reportWHrs = 0;
            //double.TryParse(txtQCReportedHrs.Text, out reportWHrs);
            //int excessIncentiveTotal = 0;
            //int.TryParse(lblExcessIncentive.Text, out excessIncentiveTotal);
            //int workerExcessIncentiveTotal = linePerformanceViewList.Where(l => l.WorkHour >= reportWHrs && l.IncentiveGrade != null && l.IncentiveGrade.IsSmallGrade == false).Sum(l => l.ExcessIncentive);
            //foreach (LinePerformanceViewModel linePerformanceView in linePerformanceViewList)
            //{
            //    if (linePerformanceView.WorkHour >= reportWHrs && linePerformanceView.IncentiveGrade != null && linePerformanceView.IncentiveGrade.IsSmallGrade == false)
            //    {
            //        linePerformanceView.ExcessIncentive = 0;
            //    }
            //}
            //excessIncentiveTotal += workerExcessIncentiveTotal;
            //lblExcessIncentive.Text = excessIncentiveTotal.ToString();
            //lblExcessIncentive.Tag = excessIncentiveTotal;
            colExcessIncentive.IsReadOnly = false;
        }

        private void dgLinePerformance_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column == colExcessIncentive)
            {
                double reportWHrs = 0;
                double.TryParse(txtQCReportedHrs.Text, out reportWHrs);

                int[] excessIncentiveArray = lblExcessIncentive.Tag as int[];

                int excessIncentiveTotal = excessIncentiveArray[1];
                int workerExcessIncentiveTotal = linePerformanceViewList.Where(l => l.WorkHour >= reportWHrs && l.IncentiveGrade != null && l.IncentiveGrade.IsSmallGrade == false).Sum(l => l.ExcessIncentive);
                if (workerExcessIncentiveTotal > excessIncentiveTotal)
                {
                    TextBox textBox = e.EditingElement as TextBox;
                    textBox.Text = "0";
                }
                else
                {
                    lblExcessIncentive.Text = (excessIncentiveTotal - workerExcessIncentiveTotal).ToString();
                }

            }
        }

        double mpTotalStandard = 0;
        int incentiveA;
        int quotaTarget;
        bool calculateBefore = false;
        private void btnCalculateIncentive_Click(object sender, RoutedEventArgs e)
        {
            lblWHRSAfter.Visibility = Visibility.Collapsed;
            btnRefresh.Visibility = Visibility.Collapsed;
            double wHrsTotal = 0;
            int wkrTotal = 0;
            double mpTotal = 0;
            incentiveA = 0;
            quotaTarget = 0;
            foreach (LinePerformanceViewModel l in linePerformanceViewList)
            {
                double wHrs = 0;
                if (String.IsNullOrEmpty(l.AdjustTimeOut) == true || l.AdjustTimeOut == "NaN")
                {
                    if (String.IsNullOrEmpty(l.AdjustTimeIn) == true || l.AdjustTimeIn == "NaN")
                    {
                        wHrs = CalculateHelper.CalculateWHRS(l.TimeIn, l.TimeOut);
                    }
                    else
                    {
                        wHrs = CalculateHelper.CalculateWHRS(l.AdjustTimeIn, l.TimeOut);
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(l.AdjustTimeIn) == true || l.AdjustTimeIn == "NaN")
                    {
                        wHrs = CalculateHelper.CalculateWHRS(l.TimeIn, l.AdjustTimeOut);
                    }
                    else
                    {
                        wHrs = CalculateHelper.CalculateWHRS(l.AdjustTimeIn, l.AdjustTimeOut);
                    }
                }
                l.WorkHour = wHrs;
                wHrsTotal += wHrs;
                mpTotal += l.WorkerRatio;
                wkrTotal += 1;
            }
            lblCountTotalMP.Text = mpTotal.ToString();

            lblWHRS.Text = Math.Round(wHrsTotal / wkrTotal, 1).ToString();
            int output = 0;
            if (int.TryParse(txtOutputPM1.Text, out output) == true)
            {
                lblPPH.Text = Math.Round(output / wHrsTotal, 1).ToString();
            }

            double whrs = Math.Round(wHrsTotal / wkrTotal, 1); //MidpointRounding.AwayFromZero

            workhrsBefore = whrs;
            calculateBefore = true;
            mpTotalStandard = mpTotal;
            CalculateIncentiveStandard(whrs, mpTotal);
            txtIncentiveGradeA.Text = incentiveA.ToString();
            txtQuotaTarget.Text = quotaTarget.ToString();
            dgLinePerformance.ItemsSource = null;
            dgLinePerformance.ItemsSource = linePerformanceViewList;

            btnCaculate.IsEnabled = true;
            btnRepairWHrs.IsEnabled = true;
        }

        public void CalculateIncentiveStandard(double whrs, double mpTotal)
        {
            txtIncentivePM1.Text = "";
            txtIncentivePM2.Text = "";
            txtIncentivePM3.Text = "";
            txtIncentivePM4.Text = "";
            txtIncentivePM5.Text = "";

            int output = 0, outputPM1 = 0, outputPM2 = 0, outputPM3 = 0, outputPM4 = 0, outputPM5 = 0;
            Int32.TryParse(txtOutputPM1.Text.ToString(), out outputPM1);
            Int32.TryParse(txtOutputPM2.Text.ToString(), out outputPM2);
            Int32.TryParse(txtOutputPM3.Text.ToString(), out outputPM3);
            Int32.TryParse(txtOutputPM4.Text.ToString(), out outputPM4);
            Int32.TryParse(txtOutputPM5.Text.ToString(), out outputPM5);

            string PM = "", PM1 = "", PM2 = "", PM3 = "", PM4 = "", PM5 = "";
            PM1 = txtPatternNoPM1.Text.ToString();
            PM2 = txtPatternNoPM2.Text.ToString();
            PM3 = txtPatternNoPM3.Text.ToString();
            PM4 = txtPatternNoPM4.Text.ToString();
            PM5 = txtPatternNoPM5.Text.ToString();

            List<AssemblyIncentiveModel> IncentivePM1List = new List<AssemblyIncentiveModel>();
            IncentivePM1List = AssemblyIncentiveController.Select(PM1);
            List<AssemblyIncentiveModel> IncentivePM2List = new List<AssemblyIncentiveModel>();
            IncentivePM2List = AssemblyIncentiveController.Select(PM2);
            List<AssemblyIncentiveModel> IncentivePM3List = new List<AssemblyIncentiveModel>();
            IncentivePM3List = AssemblyIncentiveController.Select(PM3);
            List<AssemblyIncentiveModel> IncentivePM4List = new List<AssemblyIncentiveModel>();
            IncentivePM4List = AssemblyIncentiveController.Select(PM4);
            List<AssemblyIncentiveModel> IncentivePM5List = new List<AssemblyIncentiveModel>();
            IncentivePM5List = AssemblyIncentiveController.Select(PM5);

            int qtyWorkerPM1 = 0, qtyWorkerPM2 = 0, qtyWorkerPM3 = 0, qtyWorkerPM4 = 0, qtyWorkerPM5 = 0;
            double speedPM1 = 0, speedPM2 = 0, speedPM3 = 0, speedPM4 = 0, speedPM5 = 0;
            double actTime = 0, actTimePM1 = 0, actTimePM2 = 0, actTimePM3 = 0, actTimePM4 = 0, actTimePM5 = 0;

            speedPM1 = CalSpeed(IncentivePM1List, whrs, mpTotal);
            //speedPM1 = CalSpeed(IncentivePM1List, 600, 10, 54);
            if (speedPM1 == -1)
            {
                MessageBox.Show("PM1 Không Tồn Tại", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (speedPM1 == 0)
            {
                MessageBox.Show("speed PM1 = 0", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            actTimePM1 = (double)outputPM1 / speedPM1;
            qtyWorkerPM1 = IncentivePM1List.Select(s => s.Worker).FirstOrDefault();

            PM = PM1;
            output = outputPM1;
            if (calculatePM2 == true)
            {
                speedPM2 = CalSpeed(IncentivePM2List, whrs, mpTotal);
                //speedPM2 = CalSpeed(IncentivePM2List, 1051, 10, 54);
                if (speedPM2 == -1)
                {
                    MessageBox.Show("PM2 Không Tồn Tại", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (speedPM2 == 0)
                {
                    MessageBox.Show("speed PM2 = 0", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                actTimePM2 = (double)outputPM2 / speedPM2;
                qtyWorkerPM2 = IncentivePM2List.Select(s => s.Worker).FirstOrDefault();
                PM = PM + "," + PM2;
                output = output + outputPM2;
            }
            if (calculatePM3 == true)
            {
                speedPM3 = CalSpeed(IncentivePM3List, whrs, mpTotal);
                if (speedPM3 == -1)
                {
                    MessageBox.Show("PM3 Không Tồn Tại", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (speedPM3 == 0)
                {
                    MessageBox.Show("speed PM3 = 0", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                actTimePM3 = (double)outputPM3 / speedPM3;
                qtyWorkerPM3 = IncentivePM3List.Select(s => s.Worker).FirstOrDefault();
                PM = PM + "," + PM3;
                output = output + outputPM3;
            }
            if (calculatePM4 == true)
            {
                speedPM4 = CalSpeed(IncentivePM4List, whrs, mpTotal);
                if (speedPM4 == -1)
                {
                    MessageBox.Show("PM4 Không Tồn Tại", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (speedPM4 == 0)
                {
                    MessageBox.Show("speed PM4 = 0", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                actTimePM4 = (double)outputPM4 / speedPM4;
                qtyWorkerPM4 = IncentivePM4List.Select(s => s.Worker).FirstOrDefault();
                PM = PM + "," + PM4;
                output = output + outputPM4;
            }
            if (calculatePM5 == true)
            {
                speedPM5 = CalSpeed(IncentivePM5List, whrs, mpTotal);
                if (speedPM5 == -1)
                {
                    MessageBox.Show("PM5 Không Tồn Tại", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (speedPM5 == 0)
                {
                    MessageBox.Show("speed PM5 = 0", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                actTimePM5 = (double)outputPM5 / speedPM5;
                qtyWorkerPM5 = IncentivePM5List.Select(s => s.Worker).FirstOrDefault();
                PM = PM + "," + PM5;
                output = output + outputPM5;
            }
            actTime = actTimePM1 + actTimePM2 + actTimePM3 + actTimePM4 + actTimePM5;

            double cf = 0;
            double hrsPM1 = 0, hrsPM2 = 0, hrsPM3 = 0, hrsPM4 = 0, hrsPM5 = 0;

            cf = whrs / actTime;

            hrsPM1 = actTimePM1 * cf;
            hrsPM2 = actTimePM2 * cf;
            hrsPM3 = actTimePM3 * cf;
            hrsPM4 = actTimePM4 * cf;
            hrsPM5 = actTimePM5 * cf;

            double quotaPM1 = 0, quotaPM2 = 0, quotaPM3 = 0, quotaPM4 = 0, quotaPM5 = 0;
            quotaPM1 = hrsPM1 * speedPM1;
            quotaPM2 = hrsPM2 * speedPM2;
            quotaPM3 = hrsPM3 * speedPM3;
            quotaPM4 = hrsPM4 * speedPM4;
            quotaPM5 = hrsPM5 * speedPM5;

            quotaTarget = (int)(quotaPM1 + quotaPM2 + quotaPM3 + quotaPM4 + quotaPM5);

            double pairPerHrsPM1 = 0, pairPerHrsPM2 = 0, pairPerHrsPM3 = 0, pairPerHrsPM4 = 0, pairPerHrsPM5 = 0;
            pairPerHrsPM1 = ((double)outputPM1 / hrsPM1 / mpTotal) * (double)qtyWorkerPM1;
            pairPerHrsPM2 = ((double)outputPM2 / hrsPM2 / mpTotal) * (double)qtyWorkerPM2;
            pairPerHrsPM3 = ((double)outputPM3 / hrsPM3 / mpTotal) * (double)qtyWorkerPM3;
            pairPerHrsPM4 = ((double)outputPM4 / hrsPM4 / mpTotal) * (double)qtyWorkerPM4;
            pairPerHrsPM5 = ((double)outputPM5 / hrsPM5 / mpTotal) * (double)qtyWorkerPM5;

            int pairPerHrsComparePM1 = 0, pairPerHrsComparePM2 = 0, pairPerHrsComparePM3 = 0, pairPerHrsComparePM4 = 0, pairPerHrsComparePM5 = 0;

            pairPerHrsComparePM1 = (int)(Math.Round((double)outputPM1 / mpTotal * (double)qtyWorkerPM1 / whrs * 10, MidpointRounding.AwayFromZero));

            if (calculatePM2 == true || calculatePM2 == true || calculatePM2 == true || calculatePM2 == true)
            {
                pairPerHrsComparePM1 = (int)(Math.Round(pairPerHrsPM1 * whrs, MidpointRounding.AwayFromZero));
                pairPerHrsComparePM2 = (int)(Math.Round(pairPerHrsPM2 * whrs, MidpointRounding.AwayFromZero));
                pairPerHrsComparePM3 = (int)(Math.Round(pairPerHrsPM3 * whrs, MidpointRounding.AwayFromZero));
                pairPerHrsComparePM4 = (int)(Math.Round(pairPerHrsPM4 * whrs, MidpointRounding.AwayFromZero));
                pairPerHrsComparePM5 = (int)(Math.Round(pairPerHrsPM5 * whrs, MidpointRounding.AwayFromZero));
            }

            int incentivePM1 = 0, incentivePM2 = 0, incentivePM3 = 0, incentivePM4 = 0, incentivePM5 = 0;
            incentivePM1 = CalIncentive(IncentivePM1List, pairPerHrsComparePM1);
            incentivePM2 = CalIncentive(IncentivePM2List, pairPerHrsComparePM2);
            incentivePM3 = CalIncentive(IncentivePM3List, pairPerHrsComparePM3);
            incentivePM4 = CalIncentive(IncentivePM4List, pairPerHrsComparePM4);
            incentivePM5 = CalIncentive(IncentivePM5List, pairPerHrsComparePM5);

            int incentiveToSumPM1 = incentivePM1, incentiveToSumPM2 = 0, incentiveToSumPM3 = 0, incentiveToSumPM4 = 0, incentiveToSumPM5 = 0;
            if (calculatePM2 == true || calculatePM2 == true || calculatePM2 == true || calculatePM2 == true)
            {
                incentiveToSumPM1 = (int)(Math.Round(incentivePM1 * hrsPM1 / whrs, MidpointRounding.AwayFromZero));
                incentiveToSumPM2 = (int)(Math.Round(incentivePM2 * hrsPM2 / whrs, MidpointRounding.AwayFromZero));
                incentiveToSumPM3 = (int)(Math.Round(incentivePM3 * hrsPM3 / whrs, MidpointRounding.AwayFromZero));
                incentiveToSumPM4 = (int)(Math.Round(incentivePM4 * hrsPM4 / whrs, MidpointRounding.AwayFromZero));
                incentiveToSumPM5 = (int)(Math.Round(incentivePM5 * hrsPM5 / whrs, MidpointRounding.AwayFromZero));
            }

            incentiveA = incentiveToSumPM1 + incentiveToSumPM2 + incentiveToSumPM3 + incentiveToSumPM4 + incentiveToSumPM5;
            if (calculateBefore == true)
            {
                incentiveBefore = incentiveA;
                calculateBefore = false;
            }
            if (calculateAfter == true)
            {
                incentiveAfter = incentiveA;
                calculateAfter = false;
            }

            txtIncentivePM1.Text = incentivePM1.ToString();
            txtIncentivePM2.Text = incentivePM2.ToString();
            txtIncentivePM3.Text = incentivePM3.ToString();
            txtIncentivePM4.Text = incentivePM4.ToString();
            txtIncentivePM5.Text = incentivePM5.ToString();

            txtOutput.Text = output.ToString();
            txtPatternNo.Text = PM.ToString();
        }

        public double CalSpeed(List<AssemblyIncentiveModel> incentiveList , double whrs, double mp)
        {
            double speed = 0;
            if (incentiveList.Count == 0)
            {
                return -1;
            }
            int qtyPair100 = incentiveList.Where(w => w.Efficiency == 1.01).Select(s => s.AssemblyOutput).FirstOrDefault();
            int qtyWorker100 = incentiveList.Select(s => s.Worker).FirstOrDefault();
            speed = (double)qtyPair100 / (double)qtyWorker100 * mp / whrs;
            return speed;
        }

        public int CalIncentive(List<AssemblyIncentiveModel> incentiveList, int pairPerHrs)
        {
            if (incentiveList.Count == 0)
            {
                return 0;
            }
            int incentive = 0;
            if (pairPerHrs < incentiveList.Select(s => s.AssemblyOutput).Min() - 30)
            {
                return 0;
            }
            if (incentiveList.Select(s => s.AssemblyOutput).Min() - 30 < pairPerHrs && pairPerHrs < incentiveList.Select(s => s.AssemblyOutput).Min() + 30)
            {
                return incentiveList.Select(s => s.Incentive).Min();
            }
            if (pairPerHrs > incentiveList.Select(s => s.AssemblyOutput).Max())
            {
                return incentiveList.Select(s => s.Incentive).Max();
            }

            for (int i = 0; i < incentiveList.Count - 1; i++)
            {
                if (incentiveList[i].AssemblyOutput + 30 < pairPerHrs && pairPerHrs < incentiveList[i + 1].AssemblyOutput + 30)
                {
                    incentive = incentiveList[i].Incentive;
                }
            }
            return incentive;
        }

        string pm2Click = "+";
        bool calculatePM2;
        private void btnPM2_Click(object sender, RoutedEventArgs e)
        {
            if (pm2Click == "+")
            {
                pm2Click = "-";
                btnPM2.Content = pm2Click;
                pm3Click = "+";
                btnPM3.Content = pm3Click;
                pm4Click = "+";
                btnPM4.Content = pm4Click;
                pm5Click = "+";
                btnPM5.Content = pm4Click;

                btnPM3.Visibility = Visibility.Visible;
                groupPM2.Visibility = Visibility.Visible;
                calculatePM2 = true;
                return;
            }
            if (pm2Click == "-")
            {
                pm2Click = "+";
                btnPM2.Content = pm2Click;
                pm3Click = "+";
                btnPM3.Content = pm3Click;
                pm4Click = "+";
                btnPM4.Content = pm4Click;
                pm5Click = "+";
                btnPM5.Content = pm5Click;
                btnPM3.Visibility = Visibility.Collapsed;
                groupPM2.Visibility = Visibility.Collapsed;
                btnPM4.Visibility = Visibility.Collapsed;
                groupPM3.Visibility = Visibility.Collapsed;
                btnPM5.Visibility = Visibility.Collapsed;
                groupPM4.Visibility = Visibility.Collapsed;
                groupPM5.Visibility = Visibility.Collapsed;
                calculatePM2 = false;
                calculatePM3 = false;
                calculatePM4 = false;
                calculatePM5 = false;
                return;
            }
        }

        string pm3Click = "+";
        bool calculatePM3;
        private void PM3_Click(object sender, RoutedEventArgs e)
        {
            if (pm3Click == "+")
            {
                pm3Click = "-";
                btnPM3.Content = pm3Click;
                pm4Click = "+";
                btnPM4.Content = pm4Click;
                pm5Click = "+";
                btnPM5.Content = pm5Click;

                btnPM4.Visibility = Visibility.Visible;
                groupPM3.Visibility = Visibility.Visible;
                calculatePM3 = true;
                return;
            }
            if (pm3Click == "-")
            {
                pm3Click = "+";
                btnPM3.Content = pm3Click;
                pm4Click = "+";
                btnPM4.Content = pm4Click;
                pm5Click = "+";
                btnPM5.Content = pm5Click;

                btnPM4.Visibility = Visibility.Collapsed;
                groupPM3.Visibility = Visibility.Collapsed;
                btnPM5.Visibility = Visibility.Collapsed;
                groupPM4.Visibility = Visibility.Collapsed;

                groupPM5.Visibility = Visibility.Collapsed;
                calculatePM3 = false;
                calculatePM4 = false;
                calculatePM5 = false;
                return;
            }
        }

        string pm4Click = "+";
        bool calculatePM4;
        private void btnPM4_Click(object sender, RoutedEventArgs e)
        {
            if (pm4Click == "+")
            {
                pm4Click = "-";
                btnPM4.Content = pm4Click;
                btnPM5.Content = pm5Click;
                btnPM5.Visibility = Visibility.Visible;
                groupPM4.Visibility = Visibility.Visible;
                calculatePM4 = true;
                return;
            }
            if (pm4Click == "-")
            {
                pm4Click = "+";
                btnPM4.Content = pm4Click;
                pm5Click = "+";
                btnPM5.Content = pm5Click;

                btnPM5.Visibility = Visibility.Collapsed;
                groupPM4.Visibility = Visibility.Collapsed;
                groupPM5.Visibility = Visibility.Collapsed;

                calculatePM4 = false;
                calculatePM5 = false;
                return;
            }
        }

        string pm5Click = "+";
        bool calculatePM5;
        private void btnPM5_Click(object sender, RoutedEventArgs e)
        {
            if (pm5Click == "+")
            {
                pm5Click = "-";
                btnPM5.Content = pm5Click;
                groupPM5.Visibility = Visibility.Visible;
                calculatePM5 = true;
                return;
            }
            if (pm5Click == "-")
            {
                pm5Click = "+";
                btnPM5.Content = pm5Click;
                groupPM5.Visibility = Visibility.Collapsed;
                calculatePM5 = false;
                return;
            }
        }

        private void btnRepairWHrs_Click(object sender, RoutedEventArgs e)
        {
            lblWHRSAfter.Visibility = Visibility.Visible;
            lblWHRSAfter.IsEnabled = true;
            lblWHRSAfter.Focus();

            btnRefresh.IsEnabled = true;
            btnRefresh.Visibility = Visibility.Visible;
        }

        bool calculateAfter = false;
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            calculateAfter = true;
            Double.TryParse(lblWHRSAfter.Text.ToString(), out workhrsAfter);
            CalculateIncentiveStandard(workhrsAfter, mpTotalStandard);
            txtIncentiveGradeA.Text = incentiveA.ToString();
            if (incentiveBefore != incentiveAfter)
            {
                AssemblyRequestIncentiveWindow window = new AssemblyRequestIncentiveWindow(line, date, workhrsBefore, workhrsAfter, incentiveBefore, incentiveAfter);
                window.ShowDialog();
            }
        }

        private void cboLineId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
