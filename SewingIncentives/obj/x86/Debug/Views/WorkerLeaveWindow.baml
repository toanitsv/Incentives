   M S B A M L   `   `   `  ����  SewingIncentives.    (SewingIncentives.Views.WorkerLeaveWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$)  #Sewing Incentives - Worker Pregnant��5   	   $	��360��65   $	��720��6B    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5	      $��2* �6   .+�5   
   � $��5,5,0,5q�6    5      �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��Worker No. :��6   $��Bottom=�6,   �� 5      $��1��6   $��Start Date:��6%   $��Bottom=�68   �� 5      $��2��6   $��	End Date:��6%   $��Bottom=�66   �� 5      $��3��6   $��Reason:��6%   $��Bottom=�64   �� 5      -   5         ��Name$ txtWorkerId��5      $��1��6+   $��5,0,0,0q�6;    �� CharacterCasing$ Upper=�6L   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpStartDate��5      $��1��6.   $��1��6>   $��5,5,0,0q�6K      SelectedDate$ 	3/26/2016X�6]     5      -   5        $ 	dpEndDate��5      $��1��6,   $��2��6<   $��5,5,0,0q�6I   $ 	3/26/2016X�6Z   �� 5      -   5         ��Name$ cboLeaveType��5      $��1��6-   $��3��6=   $��5,5,0,0q�6J   	 �� SelectedValuePath$	 LeaveTypeId��6[   ��5      �� 5       .R�5!      �� 
 �� Orientation$
 
Horizontal=�6%   $��0,1,0,1q�6>   .+�5"      �� $��20��6(    �� TextAlignment$ Center=�6Z   ��63   �� *BackgroundColor+6Z   �� 5#      $��Center=�6>   $��5,0,0,0q�6Y   ��6(   �� *Name+6Y   5$      5%      5'      �� 5(      -   5         ��Name$ btnSave��5(      $��2��6&   $��4��6;   $��0,5,0,0q�6H   $��Right=�6Y   $��75��6u   $
��Save��6�   $
��Bold"�6�   6�   5)      � 5*   
   $��1��6   $��5,5,5,5q�6    5+      �� 5,      $
��Auto �6    �� 5-      $��* �6    .+�5/      �� $
 
Horizontal=�6   .+�50      �� $��Worker No.:��6   $��Bottom=�6/   �� 51      -   5        $ txtWorkerIdToSearch��51      $��5,0,0,0q�67   $	��200��6H   $ Upper=�6W   �� 52      -   5        $ 	btnSearch��52      $��5,0,0,0q�6,   $��75��6=   $��Search��6K   $
��Bold"�6\    �� 	IsDefault . 6�   53      &   System.Windows.Controls.DataGrid  54      -	   5          Name$ dgMain��54      $��1��6'   $��0,5,0,0q�64      AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly .    Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn   Header$ Worker Leave Id��6)      
Visibility$ 	Collapsed=�6d   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *WorkerLeaveId+6d     5:      $ 	Worker Id��6)    6<   �� *
WorkerId+  5;      $ 
Start Date��6)    6=   �� *	StartDate+ �� StringFormat$ 
dd/MM/yyyy��  5<      $ End Date��6)    6;   �� *	EndDate+$ 
dd/MM/yyyy��4  .System.Windows.Controls.DataGridComboBoxColumn  5=      -
   5        colLeaveType��$ Reason��5=   C      SelectedValuePath$ LeaveTypeId��6}      DisplayMemberPath$
 Name��6�      SelectedItemBinding 6S   �� *	LeaveType+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5>         CellTemplate 5?      �� 5@      .R�5A   "   �� $ 	btnDelete��6)   -   5        $	�� X ��5A   <   $
��Bold"�6J   6\   5B      5D      5F      5G      5H      5I      