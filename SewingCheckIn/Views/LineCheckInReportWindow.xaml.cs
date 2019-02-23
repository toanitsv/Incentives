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

using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Drawing.Printing;
using System.Drawing;
using SewingCheckIn.ViewModels;
using SewingCheckIn.Models;
using SewingCheckIn.Helpers;
namespace SewingCheckIn.Views
{
    /// <summary>
    /// Interaction logic for LineCheckInReportWindow.xaml
    /// </summary>
    public partial class LineCheckInReportWindow : Window
    {
        PrintDocument document;
        List<LineCheckInViewModel> lineCheckIns;
        List<String> lineWorkAts;
        int lineCheckInIndexPrinted;
        int lineWorkAtIndexPrinted;
        int workerFilterIndexPrinted;
        int pageIndexPrinting;
        public LineCheckInReportWindow()
        {
            InitializeComponent();
        }

        public LineCheckInReportWindow(List<LineCheckInViewModel> lineCheckIns)
        {
            document = new PrintDocument() { DocumentName = "Line Check-In Report" };
            document.PrintPage += new PrintPageEventHandler(document_PrintPage);
            this.lineCheckIns = lineCheckIns;
            lineWorkAts = new List<String>();
            lineCheckInIndexPrinted = 0;
            lineWorkAtIndexPrinted = 0;
            workerFilterIndexPrinted = 0;
            pageIndexPrinting = 1;
            InitializeComponent();
            System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
            contextMenu.MenuItems.Add("&Print Report", new EventHandler(PrintReport));
            printPreview.ContextMenu = contextMenu;
        }

        private void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            Margins margin = document.DefaultPageSettings.Margins;
            int paperWidth = (int)document.DefaultPageSettings.PrintableArea.Width - margin.Left - margin.Right;
            int paperHeight = (int)document.DefaultPageSettings.PrintableArea.Height - margin.Top - margin.Bottom;
            StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            System.Drawing.Brush foreColor = System.Drawing.Brushes.Black;
            System.Drawing.Brush backColor = System.Drawing.Brushes.LightGray;
            System.Drawing.Pen borderColor = System.Drawing.Pens.Black;

            System.Drawing.Rectangle rectangleTitle = new System.Drawing.Rectangle(0 + margin.Left, 0 + margin.Top, paperWidth, 50);
            Font fontTitle = new Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            e.Graphics.DrawString("Line Check-In Report", fontTitle, foreColor, rectangleTitle, sf);

            Font fontPrintAt = new Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            StringFormat sfPrintAt = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };
            e.Graphics.DrawString(string.Format("Printed at: {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now), fontPrintAt, foreColor, rectangleTitle, sfPrintAt);
            sfPrintAt.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(string.Format("Page " + pageIndexPrinting), fontPrintAt, foreColor, rectangleTitle, sfPrintAt);

            Font fontColumnName = new Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            int tableWorkerWidth = 300;

            #region Main Table
            System.Drawing.Rectangle rectangleNo = new System.Drawing.Rectangle(0 + margin.Left, rectangleTitle.Y + rectangleTitle.Height, 40, 35);
            System.Drawing.Rectangle rectangleWorkerId = new System.Drawing.Rectangle(rectangleNo.X + rectangleNo.Width, rectangleNo.Y, 80, rectangleNo.Height);
            System.Drawing.Rectangle rectangleName = new System.Drawing.Rectangle(rectangleWorkerId.X + rectangleWorkerId.Width, rectangleNo.Y, 250, rectangleNo.Height);
            System.Drawing.Rectangle rectanglePosition = new System.Drawing.Rectangle(rectangleName.X + rectangleName.Width, rectangleNo.Y, 200, rectangleNo.Height);
            System.Drawing.Rectangle rectangleTerminalTime = new System.Drawing.Rectangle(rectanglePosition.X + rectanglePosition.Width, rectangleNo.Y, paperWidth - (rectangleNo.Width + rectangleWorkerId.Width + rectangleName.Width + rectanglePosition.Width), rectangleNo.Height);
            if (lineWorkAtIndexPrinted == 0 && workerFilterIndexPrinted == 0)
            {

                e.Graphics.FillRectangle(backColor, rectangleNo);
                e.Graphics.DrawRectangle(borderColor, rectangleNo);
                e.Graphics.DrawString("No.", fontColumnName, foreColor, rectangleNo, sf);

                e.Graphics.FillRectangle(backColor, rectangleWorkerId);
                e.Graphics.DrawRectangle(borderColor, rectangleWorkerId);
                e.Graphics.DrawString("WorkerId", fontColumnName, foreColor, rectangleWorkerId, sf);

                e.Graphics.FillRectangle(backColor, rectangleName);
                e.Graphics.DrawRectangle(borderColor, rectangleName);
                e.Graphics.DrawString("Name", fontColumnName, foreColor, rectangleName, sf);

                e.Graphics.FillRectangle(backColor, rectanglePosition);
                e.Graphics.DrawRectangle(borderColor, rectanglePosition);
                e.Graphics.DrawString("Position", fontColumnName, foreColor, rectanglePosition, sf);

                e.Graphics.FillRectangle(backColor, rectangleTerminalTime);
                e.Graphics.DrawRectangle(borderColor, rectangleTerminalTime);
                e.Graphics.DrawString("Department", fontColumnName, foreColor, rectangleTerminalTime, sf);

                rectangleNo = new System.Drawing.Rectangle(rectangleNo.X, rectangleNo.Y + rectangleNo.Height, rectangleNo.Width, 20);
                rectangleWorkerId = new System.Drawing.Rectangle(rectangleWorkerId.X, rectangleNo.Y, rectangleWorkerId.Width, rectangleNo.Height);
                rectangleName = new System.Drawing.Rectangle(rectangleName.X, rectangleNo.Y, rectangleName.Width, rectangleNo.Height);
                rectanglePosition = new System.Drawing.Rectangle(rectanglePosition.X, rectangleNo.Y, rectanglePosition.Width, rectangleNo.Height);
                rectangleTerminalTime = new System.Drawing.Rectangle(rectangleTerminalTime.X, rectangleNo.Y, rectangleTerminalTime.Width, rectangleNo.Height);

            }
            Font fontRow = new Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            List<LineCheckInViewModel> lineCheckInPrints = lineCheckIns.Where(l => l.IsTerminalCheckIn == true && l.IsLineCheckIn == false).OrderBy(l => l.Department).ThenBy(l => l.Position).ThenBy(l => l.WorkerId).ToList();
            for (int i = lineCheckInIndexPrinted; i <= lineCheckInPrints.Count() - 1; i++)
            {
                LineCheckInViewModel lineCheckIn = lineCheckInPrints[i];
                e.Graphics.DrawRectangle(borderColor, rectangleNo);
                e.Graphics.DrawString((i + 1).ToString(), fontRow, foreColor, rectangleNo, sf);
                rectangleNo.Y += rectangleNo.Height;

                e.Graphics.DrawRectangle(borderColor, rectangleWorkerId);
                e.Graphics.DrawString(lineCheckIn.WorkerId, fontRow, foreColor, rectangleWorkerId, sf);
                rectangleWorkerId.Y += rectangleNo.Height;

                e.Graphics.DrawRectangle(borderColor, rectangleName);
                e.Graphics.DrawString(lineCheckIn.Name, fontRow, foreColor, rectangleName, sf);
                rectangleName.Y += rectangleNo.Height;

                e.Graphics.DrawRectangle(borderColor, rectanglePosition);
                e.Graphics.DrawString(lineCheckIn.Position, fontRow, foreColor, rectanglePosition, sf);
                rectanglePosition.Y += rectangleNo.Height;

                e.Graphics.DrawRectangle(borderColor, rectangleTerminalTime);
                e.Graphics.DrawString(lineCheckIn.Department, fontRow, foreColor, rectangleTerminalTime, sf);
                rectangleTerminalTime.Y += rectangleNo.Height;

                lineCheckInIndexPrinted = i + 1;

                if (rectangleNo.Y > paperHeight - margin.Top - margin.Bottom)
                {
                    rectangleNo.Y = 0;
                    pageIndexPrinting += 1;
                    e.HasMorePages = true;
                    return;
                }
            }
            #endregion

            #region Worker Table
            int yWorkerTableCurrent = rectangleNo.Y;
            DateTime dtCurrent = DateTime.Now;
            List<WokerFilterModel> workerFilters = WokerFilterModel.Create();
            for (int j = lineWorkAtIndexPrinted; j <= lineWorkAts.Count - 1; j++)
            {
                string lineWorkAt = lineWorkAts[j];
                System.Drawing.Rectangle rectangleWorkerTitle = new System.Drawing.Rectangle(0 + margin.Left, yWorkerTableCurrent, tableWorkerWidth, 35);
                List<LineCheckInViewModel> allCheckInPrints = lineCheckIns.Where(l => l.IsTerminalCheckIn == true && l.IsLineCheckIn == true && l.WorkAt == lineWorkAt).ToList();
                if (workerFilterIndexPrinted == 0)
                {
                    e.Graphics.DrawString("Calculate for " + lineWorkAt, fontColumnName, foreColor, rectangleWorkerTitle, sf);
                    yWorkerTableCurrent += rectangleWorkerTitle.Height;
                }
                System.Drawing.Rectangle rectangleWorkerFilterName = new System.Drawing.Rectangle(rectangleWorkerTitle.X, yWorkerTableCurrent, tableWorkerWidth / 3, 25);
                System.Drawing.Rectangle rectangleWorkerCount = new System.Drawing.Rectangle(rectangleWorkerFilterName.X + rectangleWorkerFilterName.Width, rectangleWorkerFilterName.Y, tableWorkerWidth / 3, rectangleWorkerFilterName.Height);
                System.Drawing.Rectangle rectangleWorkerRatio = new System.Drawing.Rectangle(rectangleWorkerCount.X + rectangleWorkerCount.Width, rectangleWorkerFilterName.Y, tableWorkerWidth / 3, rectangleWorkerFilterName.Height);
                int workerCountTotal = 0;
                double workerRatioTotal = 0;
                for (int i = workerFilterIndexPrinted; i <= workerFilters.Count - 1; i++)
                {
                    WokerFilterModel workerFilter = workerFilters[i];
                    e.Graphics.FillRectangle(backColor, rectangleWorkerFilterName);
                    e.Graphics.DrawRectangle(borderColor, rectangleWorkerFilterName);
                    e.Graphics.DrawString(workerFilter.Name, fontColumnName, foreColor, rectangleWorkerFilterName, sf);
                    rectangleWorkerFilterName.Y += rectangleWorkerFilterName.Height;

                    int workerCount = 0;
                    if (workerFilter.MinValue == 0 && workerFilter.MaxValue > 0)
                    {
                        workerCount = allCheckInPrints.Where(a => DateTimeHelper.CalculateMonth(a.HiredDate, dtCurrent) <= workerFilter.MaxValue).Count();
                    }
                    if (workerFilter.MinValue > 0 && workerFilter.MaxValue == 0)
                    {
                        workerCount = allCheckInPrints.Where(a => DateTimeHelper.CalculateMonth(a.HiredDate, dtCurrent) > workerFilter.MinValue).Count();
                    }
                    if (workerFilter.MinValue > 0 && workerFilter.MaxValue > 0)
                    {
                        workerCount = allCheckInPrints.Where(a => DateTimeHelper.CalculateMonth(a.HiredDate, dtCurrent) > workerFilter.MinValue && DateTimeHelper.CalculateMonth(a.HiredDate, dtCurrent) <= workerFilter.MaxValue).Count();
                    }
                    workerCountTotal += workerCount;
                    e.Graphics.DrawRectangle(borderColor, rectangleWorkerCount);
                    e.Graphics.DrawString(workerCount.ToString(), fontColumnName, foreColor, rectangleWorkerCount, sf);
                    rectangleWorkerCount.Y += rectangleWorkerCount.Height;

                    double workerRatio = (double)workerCount * workerFilter.Ratio;
                    workerRatioTotal += workerRatio;
                    e.Graphics.DrawRectangle(borderColor, rectangleWorkerRatio);
                    e.Graphics.DrawString(workerRatio.ToString(), fontColumnName, foreColor, rectangleWorkerRatio, sf);
                    rectangleWorkerRatio.Y += rectangleWorkerRatio.Height;

                    yWorkerTableCurrent += rectangleWorkerFilterName.Height;
                    workerFilterIndexPrinted = i + 1;

                    if (yWorkerTableCurrent + rectangleWorkerFilterName.Height > paperHeight - margin.Top - margin.Bottom)
                    {
                        yWorkerTableCurrent = 0;
                        pageIndexPrinting += 1;
                        e.HasMorePages = true;
                        return;
                    }
                }

                workerFilterIndexPrinted = 0;

                e.Graphics.FillRectangle(backColor, rectangleWorkerFilterName);
                e.Graphics.DrawRectangle(borderColor, rectangleWorkerFilterName);
                e.Graphics.DrawString("TOTAL", fontColumnName, foreColor, rectangleWorkerFilterName, sf);
                e.Graphics.DrawRectangle(borderColor, rectangleWorkerCount);
                e.Graphics.DrawString(workerCountTotal.ToString(), fontColumnName, foreColor, rectangleWorkerCount, sf);
                e.Graphics.DrawRectangle(borderColor, rectangleWorkerRatio);
                e.Graphics.DrawString(workerRatioTotal.ToString(), fontColumnName, foreColor, rectangleWorkerRatio, sf);

                yWorkerTableCurrent += rectangleWorkerFilterName.Height;
                lineWorkAtIndexPrinted = j + 1;
            }
            lineWorkAtIndexPrinted = 0;
            lineCheckInIndexPrinted = 0;
            pageIndexPrinting = 1;
            e.HasMorePages = false;
            #endregion
        }

        public void PrintReport(object sender, EventArgs e)
        {
            document.Print();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lineWorkAts = lineCheckIns.Where(l => String.IsNullOrEmpty(l.WorkAt) == false).OrderBy(l => l.WorkAt).Select(l => l.WorkAt).Distinct().ToList();
            document.DefaultPageSettings.Landscape = false;
            document.DefaultPageSettings.Margins = new Margins(20, 20, 10, 10);
            printPreview.Document = document;
        }


    }
}
