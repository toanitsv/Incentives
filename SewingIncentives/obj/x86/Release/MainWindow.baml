   M S B A M L   `   `   `  ����   SewingIncentives!    SewingIncentives.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Sewing Incentives��$	��400��$	��800�� � WindowStartupLocation$ CenterScreen=�.��� 
������� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+�o� $��0��.@�m� $
��File��.@�m� $��Report��.@�m� -    m�Name$ miExcessWorker��$��Excess Worker��m� -   $ miLineSummary��$��Line Summary��m� -   $ miOtherPositionDaily��$��Other Position Daily��m� -   $ miOtherPositionMonth��$��Other Position Summary��m� -   $ miLineIncentiveSummary��$��Incentive Summary��m� -   $ miGroupIncentive��$��Group Incentive of Worker��m� -   $ miDayLeaveSummary��$��Worker Leave Summary��m� -	   $ btnClose��$��Close��m� $��
Management��.@�m� -
   $ miLinePerformance��$��Line Performance��m� -   $ 
miPregnant��$��Pregnant List��m� -   $ miSpecialTransfer��$��Special Transfer��m� -   $ miChangeCardId��$��Change ID Card��m� -   $ miChangeWorkerNo��
���  ��$��Change Worker No��m� $��Others��.@�m� -   $ miAttendance��$��
Attendance��� $��1�� � ColumnDefinitions �� $��2* ��� $��3* � �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��2��$��0,5,0,0q�$��WORKER LOG-IN�� �� TextAlignment$ Center=�$
��Bold"�� $��2��$��Right=� �� $��* ��� $
��Auto �.+��� -    ��Name$ chbOtherDate��$��
Other Date��$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ dpOtherDate��$��1��$��5,0,0,0q�   SelectedDate$ 1/1/2016X�$y�	Collapsed=�� -   	 �Name$	 gridLine��$��1��$��0,5,5,0q� �� $��* ��� $��* ��� $��* �&   System.Windows.Controls.DataGrid  -   
  Name$
 dgWorkerLogin��$��1��$��1��$��5,5,0,0q�   AutoGenerateColumns .     AlternatingRowBackground
 ����� �� AlternationCount$ 2��   FrozenColumnCount$ 2��   
IsReadOnly .    HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 ����� � 	Resources *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   '        �� "   P�  �@ 
IsSelected . . ���  � ��
 ��iA���  � ��
 �������  � ��$
 Bold"�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    Header$ Rfid #��   
FontWeight$
 Bold"�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *CardId+  $
 Auto $
 ID #�� �� *
WorkerId+  $
 Auto $
 Name�� �� *Name+  $
 Auto $
 Line�� �� *Line+  $
 Auto $ Position�� �� *
Position+� $��1��$��2��$��5,5,0,0q�$��40�� �� $
��Auto ��� $��* �.+��� -    ��Name$ btnLogin��$	��125��$��Log-in to LINE ..��$
��Bold"�
����iA�
������� �� 	IsDefault . {�.  �� -     ��Name$  	txtCardId��$��1��$��5,0,0,0q�$��27(�
���  ��! �� TextAlignment$! Center=�{�.  