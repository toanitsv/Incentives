   M S B A M L   `   `   `  ����   SewingIncentives=    7SewingIncentives.Views.LineIncentiveSummaryDetailWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$2  ,Sewing Incentives - Incentive Summary Detail��$	��480��$	��640�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�.��� .+�&   System.Windows.Controls.DataGrid  -     Name$ dgMain��   AutoGenerateColumns .     CanUserAddRows .     FrozenColumnCount$ 2��   HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����	   
IsReadOnly	 . 
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Date��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Date+ �� StringFormat$ dd��  $ Work at Line��$
 Auto    
FontWeight$
 Bold"� �� *
WorkAtLine+  $ Others Position��$
 Auto  �� *OthersPosition+  $ Adjust Time��$
 Auto  �� *AdjustTimeOut+  $ IC Grade��$
 Auto $
 Bold"� �� *IncentiveGrade+  $ 	Incentive��$
 Auto $
 Bold"� �� *	Incentive+  $ Specs Incentive��$
 Auto  �� *SpecsIncentive+