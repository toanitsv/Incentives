   M S B A M L   `   `   `  ����   SewingIncentives.    (SewingIncentives.Views.WorkerLeaveWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$)  #Sewing Incentives - Worker Pregnant��$	��360��$	��720�� � WindowStartupLocation$ CenterScreen=�.���  � ColumnDefinitions �� $��* ��� $��2* �.+�� $��5,5,0,5q� �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Worker No. :��$��Bottom=��� $��1��$��Start Date:��$��Bottom=��� $��2��$��	End Date:��$��Bottom=��� $��3��$��Reason:��$��Bottom=��� -    ��Name$ txtWorkerId��$��1��$��5,0,0,0q� �� CharacterCasing$ Upper=�(  "System.Windows.Controls.DatePicker  -     Name$ dpStartDate��$��1��$��1��$��5,5,0,0q�   SelectedDate$ 	3/26/2016X�  -   $ 	dpEndDate��$��1��$��2��$��5,5,0,0q�$ 	3/26/2016X��� -    ��Name$ cboLeaveType��$��1��$��3��$��5,5,0,0q�	 �� SelectedValuePath$	 LeaveTypeId������ .R��� 
 �� Orientation$
 
Horizontal=�$��0,1,0,1q�.+��� $��20�� �� TextAlignment$ Center=����� *BackgroundColor+�� $��Center=�$��5,0,0,0q����� *Name+�� -    ��Name$ btnSave��$��2��$��4��$��0,5,0,0q�$��Right=�$��75��$
��Save��$
��Bold"�� $��1��$��5,5,5,5q� �� $
��Auto ��� $��* �.+��� $
 
Horizontal=�.+��� $��Worker No.:��$��Bottom=��� -   $ txtWorkerIdToSearch��$��5,0,0,0q�$	��200��$ Upper=��� -   $ 	btnSearch��$��5,0,0,0q�$��75��$��Search��$
��Bold"� �� 	IsDefault . &   System.Windows.Controls.DataGrid  -	     Name$ dgMain��$��1��$��0,5,0,0q�   AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Worker Leave Id��   
Visibility$ 	Collapsed=�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *WorkerLeaveId+  $ 	Worker Id�� �� *
WorkerId+  $ 
Start Date�� �� *	StartDate+ �� StringFormat$ 
dd/MM/yyyy��  $ End Date�� �� *	EndDate+$ 
dd/MM/yyyy��4  .System.Windows.Controls.DataGridComboBoxColumn  -
   colLeaveType��$ Reason��   SelectedValuePath$ LeaveTypeId��   DisplayMemberPath$
 Name��   SelectedItemBinding �� *	LeaveType+4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� $ 	btnDelete��-   $	�� X ��$
��Bold"�