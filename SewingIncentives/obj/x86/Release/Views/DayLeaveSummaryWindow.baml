   M S B A M L   `   `   `  ���� g  cMicrosoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3atSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms   SewingIncentives2   ,SewingIncentives.Views.DayLeaveSummaryWindow   [rpvSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$.  (Sewing Incentives - Worker Leave Summary��$	��480��$	��800�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�.���  � RowDefinitions �� $
��Auto ��� $��* �.+���  �� Orientation$ 
Horizontal=�.+��� $��Year:��$��Bottom=��� -    ��Name$ cboYear���� $��10,0,0,0q�$��Center=�$��Line:���� -   $ cboLine��$��5,0,0,0q�$	��150���� -    ��Name$ btnShow��$��10,0,0,0q�$��75��$
��Show��$
��Bold"�{�.  � $��1��$��0,5,0,0q�.+�&   System.Windows.Controls.DataGrid  -     Name$ dgMain��   AutoGenerateColumns .  	   CanUserAddRows	 .  
   
IsReadOnly
 .    HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 No.��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *NO+  $ ID��   
FontWeight$
 Bold"� �� *
WorkerId+  $
 Name�� �� *Name+  $ 
Date Hired�� �� *	DateHired+ �� StringFormat$ 
dd/MM/yyyy��  $ 	Total Day�� �� *
TotalDay+  $ 	Leave Day�� �� *
LeaveDay+  $ 	Remaining��$
 Bold"� �� *	Remaining+