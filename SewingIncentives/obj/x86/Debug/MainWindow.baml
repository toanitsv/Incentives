   M S B A M L   `   `   `  ����  SewingIncentives!    SewingIncentives.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Sewing Incentives��5   	   $	��400��5   	   $	��800��6    � WindowStartupLocation$ CenterScreen=�5   9   .��5      � 
������� � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   o� $��0��6   .@�5      m� -   5         m�Name$ miRequestList��5      $��Request List��6.   $y�	Collapsed=�6D   6[   m� 5      $
��File��6   .@�5      m� $��Report��6   .@�5      m� -   5        $ miExcessWorker��5      $��Excess Worker��67   6N   m� 5      -   5        $ miLineSummary��5      $��Line Summary��66   6L   m� 5      -   5        $ miOtherPositionDaily��5      $��Other Position Daily��6=   6[   m� 5      -   5        $ miOtherPositionMonth��5      $��Other Position Summary��6=   6]   m� 5      -   5        $ miLineIncentiveSummary��5      $��Incentive Summary��6?   6Z   m� 5      -   5        $ miGroupIncentive��5      $��Group Incentive of Worker��69   6\   m� 5      -	   5        $ miDayLeaveSummary��5      $��Worker Leave Summary��6:   6X   m� 5      -
   5        $  miAssemblyReportIncentives��5      $��Assembly Incentive��6C   {�.  6q   5      m� 5      -   5        $ btnClose��5      $��Close��6-   6<   5      m� 5      $��
Management��6   .@�5      m� -   5        $ miLinePerformance��5      $��Line Performance��66   6P   m� 5      -   5        $ 
miPregnant��5      $��Pregnant List��6/   6F   m� 5      -   5        $ miSpecialTransfer��5      $��Special Transfer��66   6P   m� 5       -   5        $ miChangeCardId��5       $��Change ID Card��63   6K   m� 5!      -   5        $ miChangeWorkerNo��5!      
���  ��$��Change Worker No��6F   6`   5"      m� 5#      $��Others��6   .@�5$      m� -   5        $ miAttendance��5$      $��
Attendance��61   6E   5%      m� 5&      $��Import��6   .@�5'      m� -   5        $ miStandardIncentive��5'      $��Standard Incentive��68   6T   5(      m� 5)      -   5        $ 	miVersion��5)   (   $��Version��6   5*      5+      � 5,   
   $��1��6    � ColumnDefinitions 5-      �� 5.      $��2* �6#   �� 5/      $��3* �6#    51      �� 52      $
��Auto �6    �� 53      $��* �6    �� 54      $
��Auto �6    .+�56      �� $��2��6   $��0,5,0,0q�6,   $��WORKER LOG-IN��6=    �� TextAlignment$ Center=�6R   $
��Bold"�6i   � 57      $��2��6   $��Right=�6'    58      �� 59      $��* �6'   �� 5:      $
��Auto �6'   .+�5<      �� -   5         ��Name$ chbOtherDate��5<      $��
Other Date��61   $��Center=�6F   6�   (  "System.Windows.Controls.DatePicker  5=      -   5          Name$ dpOtherDate��5=      $��1��62   $��5,0,0,0q�6B      SelectedDate$ 1/1/2016X�6S   $y�	Collapsed=�6k   5>      � 5?      -   5        	 �Name$	 gridLine��5?      $��1��6%   $��0,5,5,0q�62    5@      �� 5A      $��* �6'   �� 5B      $��* �6'   �� 5C      $��* �6'   5E      &   System.Windows.Controls.DataGrid  5F      -   5        
  Name$
 dgWorkerLogin��5F      $��1��6.   $��1��6>   $��5,5,0,0q�6K      AutoGenerateColumns .     AlternatingRowBackground
 ����� �� AlternationCount$ 2��6�      FrozenColumnCount$ 2��6�      
IsReadOnly .    HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 ����� � 	Resources 5I      *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   '        �� 5J      "  6    5K      P� 5L       �@ 
IsSelected . . �5M   "   ��  � ��
 ��iA�6?   �� 5N   "    � ��
 �����6?   �� 5O   "    � ��$
 Bold"�6?   5P      5R      5S         Columns 5T      0  *System.Windows.Controls.DataGridTextColumn  5U      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6)      Header$ Rfid #��66      
FontWeight$
 Bold"�6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *CardId+6a     5V      $
 Auto 6)   $
 ID #��66    6D   �� *
WorkerId+  5W      $
 Auto 6)   $
 Name��66    6D   �� *Name+  5X      $
 Auto 6)   $
 Line��66    6D   �� *Line+  5Y      $
 Auto 6)   $ Position��66    6H   �� *
Position+5[      � 5\      $��1��6   $��2��6#   $��5,5,0,0q�60   $��40��6A    5]      �� 5^      $
��Auto �6'   �� 5_      $��* �6'   .+�5a      �� -   5         ��Name$ btnLogin��5a      $	��125��6+   $��Log-in to LINE ..��67   $
��Bold"�6S   
����iA�
������� �� 	IsDefault . {�.  6�   �� 5b      -   5          ��Name$  	txtCardId��5b      $��1��6-   $��5,0,0,0q�6=   $��27(�6N   
���  ��! �� TextAlignment$! Center=�6m   {�.  6�   5c      5d      5e      5f      