   M S B A M L   `   `   `  ���� g  cMicrosoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3atSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms   SewingIncentives7   1SewingIncentives.Views.LineIncentiveSummaryWindow   [rpvSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$+  %Sewing Incentives - Incentive Summary��$	��480��$	��800�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�.���  � RowDefinitions �� $
��Auto ��� $��* �.+���  �� Orientation$ 
Horizontal=�.+��� $��Month:��$��Bottom=��� -    ��Name$ cboMonth��$��5,0,0,0q��� $��/��$��Center=��� -   $ cboYear���� $��10,0,0,0q�$��Center=�$��Line:���� -   $ cboLine��$��5,0,0,0q�$	��150���� -    ��Name$ btnShow��$��10,0,0,0q�$��75��$
��Show��$
��Bold"�{�.  �� -   $ btnPrint��$��10,0,0,0q�$��75��$��Print�� �� 	IsDefault . {�.  � $��1��$��0,5,0,0q� � ColumnDefinitions �� $	��75* ��� $	��25* �.+�&   System.Windows.Controls.DataGrid  -   	  Name$	 dgMain��
   AutoGenerateColumns
 .     CanUserAddRows .     
IsReadOnly .    HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 No.��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
NumberOf+  $ ID��   
FontWeight$
 Bold"� �� *
WorkerId+  $
 Name�� �� *Name+  $ Incentive Grades�� �� *
Position+  $ No. Days Absent�� �� *NumberOfDayAbsent+  $ 	Incentive��$
 Bold"� �� *	Incentive+  -   $	 dgIncentiveGradeSummary��$��1��
 .   .   . 
 �����
 �����   $ Incentive Grade�� �� *IncentiveGrade+  $ Count Worker�� �� *Count+