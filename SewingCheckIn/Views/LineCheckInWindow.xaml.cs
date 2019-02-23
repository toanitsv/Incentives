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

using SewingCheckIn.Models;
using System.ComponentModel;
using SewingCheckIn.Controllers;
using SewingCheckIn.ViewModels;
using System.Text.RegularExpressions;
using System.Xml.Linq;
namespace SewingCheckIn.Views
{
    /// <summary>
    /// Interaction logic for LineCheckInWindow.xaml
    /// </summary>
    public partial class LineCheckInWindow : Window
    {
        List<PersonalModel> personalLines;
        List<String> lines;
        DateTime date;
        string xml;
        private readonly BackgroundWorker workerLoad;
        List<SourceModel> sources;
        List<LineCheckInViewModel> lineCheckIns;
        List<String> lineAfters;
        List<XmlLineCheckInModel> xmlLineCheckIns;
        string lineWorkAt;
        bool isAllowClose;
        public LineCheckInWindow()
        {
            InitializeComponent();
        }

        public LineCheckInWindow(List<PersonalModel> personalLines, List<String> lines, DateTime date, string xml)
        {
            this.personalLines = personalLines;
            this.lines = lines;
            this.date = date;
            this.xml = xml;
            workerLoad = new BackgroundWorker();
            workerLoad.DoWork += new DoWorkEventHandler(workerLoad_DoWork);
            workerLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerLoad_RunWorkerCompleted);
            sources = new List<SourceModel>();
            lineCheckIns = new List<LineCheckInViewModel>();
            lineAfters = new List<String>();
            xmlLineCheckIns = new List<XmlLineCheckInModel>();
            isAllowClose = false;
            InitializeComponent();
        }

        private void workerLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (PersonalModel personal in personalLines)
            {
                XmlLineCheckInModel xmlLineCheckIn = xmlLineCheckIns.Where(x => x.CardId == personal.CardId).LastOrDefault();
                LineCheckInViewModel lineCheckIn = new LineCheckInViewModel();
                lineCheckIn.CardId = personal.CardId;
                lineCheckIn.WorkerId = personal.WorkerId;
                lineCheckIn.Name = personal.Name;
                lineCheckIn.Department = personal.Department;
                lineCheckIn.Position = personal.Position;
                lineCheckIn.HiredDate = personal.HiredDate;
                if (xmlLineCheckIn != null)
                {
                    lineCheckIn.WorkAt = xmlLineCheckIn.WorkAt;
                    lineCheckIn.IsLineCheckIn = true;
                    lineCheckIn.LineTime = xmlLineCheckIn.LineTime;
                }
                List<SourceModel> sourceWorkers = sources.Where(s => s.CardId == personal.CardId).ToList();
                if (sourceWorkers.Count() > 0)
                {
                    lineCheckIn.IsTerminalCheckIn = true;
                    foreach (SourceModel source in sourceWorkers)
                    {
                        lineCheckIn.TerminalTime += source.Time.Substring(0, 2) + ":" + source.Time.Substring(2, 2) + " | ";
                    }
                }

                lineCheckIns.Add(lineCheckIn);
            }

            for (int i = 0; i <= lineAfters.Count - 1; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                gridShowButton.ColumnDefinitions.Add(cd);

                string lineAfter = lineAfters[i];
                Button btnLineAfter = new Button();
                Grid.SetColumn(btnLineAfter, i);
                btnLineAfter.Margin = new Thickness(1, 0, 1, 0);
                btnLineAfter.Content = lineAfter;
                btnLineAfter.ToolTip = lineAfter;
                btnLineAfter.FontSize = 12;
                btnLineAfter.Background = Brushes.LightGray;
                btnLineAfter.FontWeight = FontWeights.Bold;
                btnLineAfter.Click += new RoutedEventHandler(btnLineAfter_Click);
                gridShowButton.Children.Add(btnLineAfter);
            }

            dgCheckInList.ItemsSource = null;
            dgCheckInList.ItemsSource = lineCheckIns;
        }

        private void btnLineAfter_Click(object sender, RoutedEventArgs e)
        {
            Button btnLineAfter = (Button)sender;
            lineWorkAt = (string)btnLineAfter.Content;
            txtCardId.IsEnabled = true;
            txtCardId.Focus();
            txtCardId.SelectAll();
            btnCheckIn.IsEnabled = true;
            btnPrint.IsEnabled = true;
            btnCheckIn.Content = "Check-In for\n" + lineWorkAt;
        }

        private void workerLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            xmlLineCheckIns = new List<XmlLineCheckInModel>();
            XDocument xmlDocument = XDocument.Load(xml);
            foreach (XElement xmlElement in xmlDocument.Element("LineCheckIns").Elements("LineCheckIn"))
            {
                XmlLineCheckInModel xmlLineCheckIn = new XmlLineCheckInModel();
                xmlLineCheckIn.CardId = xmlElement.Element("CardId").Value;
                xmlLineCheckIn.WorkAt = xmlElement.Element("WorkAt").Value;
                xmlLineCheckIn.LineTime = xmlElement.Element("LineTime").Value;
                xmlLineCheckIns.Add(xmlLineCheckIn);
            }

            foreach (string line in lines)
            {
                string lineNumber = Regex.Match(line, @"\d+").Value;
                //if (String.IsNullOrEmpty(lineNumber) == false)
                //{
                //    lineAfters.Add("SEWING LINE " + lineNumber + "A");
                //    lineAfters.Add("SEWING LINE " + lineNumber + "B");
                //}
                //else
                //{
                //    lineAfters.Add(line);
                //}
                sources.AddRange(SourceController.Get(line, date));
            }
            //lineAfters = lineAfters.Distinct().ToList();
            lineAfters = lines.Distinct().ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (workerLoad.IsBusy == false)
            {
                workerLoad.RunWorkerAsync();
            }
        }

        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtCardId.Text;
            LineCheckInViewModel lineCheckIn;
            lineCheckIn = lineCheckIns.Where(s => s.CardId == keyword || s.WorkerId == keyword).FirstOrDefault();
            if (lineCheckIn != null)
            {
                lineCheckIn.WorkAt = lineWorkAt;
                lineCheckIn.IsLineCheckIn = true;
                lineCheckIn.LineTime = String.Format("{0:HH:mm}", DateTime.Now);

                XDocument xmlDocument = XDocument.Load(xml);
                XElement xmlElement = xmlDocument.Element("LineCheckIns");
                xmlElement.Add(new XElement(
                    "LineCheckIn",
                    new XElement("CardId", lineCheckIn.CardId),
                    new XElement("WorkAt", lineCheckIn.WorkAt),
                    new XElement("LineTime", lineCheckIn.LineTime)));
                xmlDocument.Save(xml);

                dgCheckInList.ItemsSource = null;
                dgCheckInList.ItemsSource = lineCheckIns;
                dgCheckInList.ScrollIntoView(lineCheckIn);
                DataGridRow row = (DataGridRow)dgCheckInList.ItemContainerGenerator.ContainerFromItem(lineCheckIn);
                row.Background = Brushes.Green;
                row.Foreground = Brushes.White;
            }
            txtCardId.SelectAll();
            txtCardId.Focus();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            LineCheckInReportWindow window = new LineCheckInReportWindow(lineCheckIns);
            window.ShowDialog();
            isAllowClose = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = !isAllowClose;
        }
    }
}
