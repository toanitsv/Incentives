   M S B A M L   `   `   `  ����  SewingIncentives=    7SewingIncentives.Views.LineIncentiveSummaryDetailWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$2  ,Sewing Incentives - Incentive Summary Detail��5   	   $	��480��6>   $	��640��6K    � WindowStartupLocation$ CenterScreen=�6W    � WindowState$ 	Maximized=�6|   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgMain��5         AutoGenerateColumns .     CanUserAddRows .     FrozenColumnCount$ 2��6V      HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����	   
IsReadOnly	 . 
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5	      ,  &System.Windows.Controls.DataGridColumn   Header$
 Date��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *Date+ �� StringFormat$ dd��  5
      $ Work at Line��6%   $
 Auto 6;      
FontWeight$
 Bold"�6g    6H   �� *
WorkAtLine+6g     5      $ Others Position��6%   $
 Auto 6>    6K   �� *OthersPosition+  5      $ Adjust Time��6%   $
 Auto 6:    6G   �� *AdjustTimeOut+  5      $ IC Grade��6%   $
 Auto 67   $
 Bold"�6g    6D   �� *IncentiveGrade+6g     5      $ 	Incentive��6%   $
 Auto 68   $
 Bold"�6c    6E   �� *	Incentive+6c     5      $ Specs Incentive��6%   $
 Auto 6>    6K   �� *SpecsIncentive+5      5      5      