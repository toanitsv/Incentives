   M S B A M L   `   `   `  ����g  cMicrosoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3atSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms  5   	    SewingIncentives7   1SewingIncentives.Views.LineIncentiveSummaryWindow   [rpvSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$+  %Sewing Incentives - Incentive Summary��5   	   $	��480��67   $	��800��6D    � WindowStartupLocation$ CenterScreen=�6g    � WindowState$ 	Maximized=�6�   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Month:��6   $��Bottom=�6&   �� 5      -   5         ��Name$ cboMonth��5      $��5,0,0,0q�6)   �� 5      $��/��6   $��Center=�6!   �� 5      -   5        $ cboYear��5      �� 5      $��10,0,0,0q�6   $��Center=�6*   $��Line:��6E   �� 5      -   5        $ cboLine��5      $��5,0,0,0q�6(   $	��150��69   �� 5      -   5         ��Name$ btnShow��5      $��10,0,0,0q�6&   $��75��68   $
��Show��6F   $
��Bold"�6U   {�.  6y   �� 5      -   5        $ btnPrint��5      $��10,0,0,0q�6'   $��75��69   $��Print��6G    �� 	IsDefault . {�.  6z   5      � 5   
   $��1��6   $��0,5,0,0q�6    � ColumnDefinitions 5      �� 5      $	��75* �6#   �� 5      $	��25* �6#   .+�5      &   System.Windows.Controls.DataGrid  -   5        	  Name$	 dgMain��5      
   AutoGenerateColumns
 .     CanUserAddRows .     
IsReadOnly .    HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����   Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$	 No.��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *
NumberOf+  5"      $ ID��6)      
FontWeight$
 Bold"�6R    65   �� *
WorkerId+6R     5#      $
 Name��6)    67   �� *Name+  5$      $ Incentive Grades��6)    6C   �� *
Position+  5%      $ No. Days Absent��6)    6B   �� *NumberOfDayAbsent+  5&      $ 	Incentive��6)   $
 Bold"�6Z    6<   �� *	Incentive+6Z   5(        5)      -   5        $	 dgIncentiveGradeSummary��5)   '   $��1��6   
 .   .   . 
 �����
 ����� 5,        5-      $ Incentive Grade��6)    6B   �� *IncentiveGrade+  5.      $ Count Worker��6)    6?   �� *Count+50      51      52      53      