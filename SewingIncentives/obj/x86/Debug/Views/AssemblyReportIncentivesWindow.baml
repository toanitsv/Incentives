   M S B A M L   `   `   `  ����g  cMicrosoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3atSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms  5   	    SewingIncentives;   5SewingIncentives.Views.AssemblyReportIncentivesWindow   [rpvSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Assembly Incentives��5   	   $	��600��6%   $
��1000��65    � WindowStartupLocation$ CenterScreen=�6E   .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5	      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� $��Area: ��6   $��Center=�6&   �� 5      -   5         ��Name$ cbArea��5      $��10,0,0,0q�6'   $��13(�69   $
��Bold"�6G   $��60��6Y    �� SelectedIndex$ 0��6g   @�5      �� 5      -   5        $	��Old��5   1   $	��Old��6#   61   �� 5      -   5        $	��New��5   1   $	��New��6#   61   5      �� 5      $��Time: ��6   $��Center=�6&   $��20,0,0,0q�6A   �� 5      -   5        $ cbYear��5      $��70��6'   $��5,0,0,0q�65   �� 5      -   5        $ cbMonth��5      $	��100��6(   $��5,0,0,0q�67   5      �� 5      -   5         ��Name$ btnViewReport��5      $��View Report��6,   $��13(�6B   $��20,0,0,0q�6P   $
��Bold"�6b   $	��100��6t   6�   �� 5      -   5        $ btnSpecialIncentive��5      $��Special Incentive��62   $��13(�6N   $��50,0,0,0q�6\   $
��Bold"�6n   $	��150��6�   6�   5      7  1System.Windows.Forms.Integration.WindowsFormsHost  5   
   -	   5          Name$ wfhAreaIncentive��5      $��1��65   $��0,5,0,0q�6B   $y�	Collapsed=�6S      Child. 5      /   )Microsoft.Reporting.WinForms.ReportViewer  -
   5        reportViewer��	   ShowFindControls	 .  5   5   5      � 5   
   -   5        
 �Name$
 gridSpecialIncentive��5      $y�	Collapsed=�6-   $��1��6D   $��0,5,0,0q�6Q    5      �� 5      $��* �6    �� 5       $
��auto �6    .+�5"      &   System.Windows.Controls.DataGrid  -   5          Name$ dgSpecialIncentive��5"         AutoGenerateColumns .     CanUserAddRows .     CanUserResizeRows .  $��13(�5#   H      HorizontalGridLinesBrush
 ��ح�   VerticalGridLinesBrush
 ��ح�   Columns 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Header$ PRODUCTION
OUTPUT(PAIRS)��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6O      
FontWeight$
 Bold"�6u      
IsReadOnly .    CanUserSort .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 6\   �� *Line+6�     5'      $ OUTPUT��6)   $
 auto 69    .  6F   �� *Output+6a     5(      $ TARGET
QUOTA��6)   $
 auto 6C    .   6P   �� *QuotaTarget+6p     5)      $ LACKING��6)   $
 auto 6:    .  6G   �� *	Lacking+6c     5*      $ % OUTPUT��6)   $
 auto 6;    .  6H   �� *PercentOutput+6j     5+      $ 	SUP
(VND)��6)      MinWidth$ 90Q�6@    .   6N   �� *SUPIncentive+6o     5,      $ LL 2-3
(VND)��6)   $ 90Q�6C    .   6Q   �� *LL23Incentive+6s     5-      $ LLT-1/MECH
(VND)��6)   $ 90Q�6G    .   6U   �� *LLT1MECHIncentive+6{     5.      $ WORKER
(VND)��6)   $ 90Q�6C    .   6Q   �� *WorkerIncentive+6u   50      �� 51      $��1��6   $ 
Horizontal=�6&   $��Right=�6?   $��0,5,0,0q�6[   .+�52      �� -   5        $ btnCalculate��52      $��	Calculate��6/   $
��Bold"�6C   $��13(�6U   $	��100��6c   6r   �� 53      -   5        $ btnPrintReport��53      $��Print Report��61   $
��Bold"�6H   $��13(�6Z   $	��100��6h   $��20,0,0,0q�6w   6�   54      55      56      57      