   M S B A M L   `   `   `  ����g  cMicrosoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3atSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms  5   	    SewingIncentives2   ,SewingIncentives.Views.DayLeaveSummaryWindow   [rpvSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$.  (Sewing Incentives - Worker Leave Summary��5   	   $	��480��6:   $	��800��6G    � WindowStartupLocation$ CenterScreen=�6j    � WindowState$ 	Maximized=�6�   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Year:��6   $��Bottom=�6%   �� 5      -   5         ��Name$ cboYear��5      �� 5      $��10,0,0,0q�6   $��Center=�6*   $��Line:��6E   �� 5      -   5        $ cboLine��5      $��5,0,0,0q�6(   $	��150��69   �� 5      -   5         ��Name$ btnShow��5      $��10,0,0,0q�6&   $��75��68   $
��Show��6F   $
��Bold"�6U   {�.  6y   5      � 5   
   $��1��6   $��0,5,0,0q�6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgMain��5         AutoGenerateColumns .  	   CanUserAddRows	 .  
   
IsReadOnly
 .    HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 No.��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *NO+  5      $ ID��6)      
FontWeight$
 Bold"�6R    65   �� *
WorkerId+6R     5      $
 Name��6)    67   �� *Name+  5      $ 
Date Hired��6)    6=   �� *	DateHired+ �� StringFormat$ 
dd/MM/yyyy��  5      $ 	Total Day��6)    6<   �� *
TotalDay+  5      $ 	Leave Day��6)    6<   �� *
LeaveDay+  5       $ 	Remaining��6)   $
 Bold"�6Z    6<   �� *	Remaining+6Z   5"      5#      5$      5%      