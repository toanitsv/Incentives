   M S B A M L   `   `   `  ����   SewingIncentives2    ,SewingIncentives.Views.SpecialTransferWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$*  $Sewing Incentives - Special Transfer��$	��360��$	��720�� � WindowStartupLocation$ CenterScreen=�.���  � ColumnDefinitions �� $��* ��� $��2* �.+�� $��5,5,0,5q� �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��144* �.+��� $��Worker No. :��$��Center=��� $��1��$��Type:��$��Bottom=��� $��2��$��Line ID:��$��Bottom=��� $��3��$��Date:��$��Bottom=��� $��4��$��Time:��$��Bottom=��� -    ��Name$ txtWorkerId��$��1��$��6,0,-1,0q� �� CharacterCasing$ Upper=� �� AcceptsReturn . $	��100���� -    ��Name$ cboTransferType��$��1��$��1��$��5,5,0,0q� �� SelectedValuePath$ TransferType��	 �� DisplayMemberPath$
	 Name���� -   $ 	cboLineId��$��1��$��2��$��5,5,0,0q�$ LineId��$
	 Name��(  "System.Windows.Controls.DatePicker  -   
  Name$
 dpDate��$��1��$��3��$��5,5,0,0q�   SelectedDate$ 
11/28/2016X��� -   $ txtTime��$��1��$��4��$��5,5,0,0q��� -    ��Name$ btnSave��$��2��$��5��$��0,5,0,0q�$��Right=�$��75��$
��Save��$
��Bold"�� $��1��$��5,5,5,5q� �� $
��Auto ��� $��* �.+���  �� Orientation$ 
Horizontal=�.+��� $��Worker No.:��$��Bottom=��� -   $ txtWorkerIdToSearch��$��5,0,0,0q�$	��200��$ Upper=��� -	   $ 	btnSearch��$��5,0,0,0q�$��75��$��Search��$
��Bold"� �� 	IsDefault . &   System.Windows.Controls.DataGrid  -
     Name$ dgMain��$��1��$��0,5,0,0q�   AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Worker Id��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
WorkerId+  $ Transfer Type�� �� *TransferName+  $
 Line�� �� *DestinationLineName+  $
 Date�� �� *Date+ �� StringFormat$ 
dd/MM/yyyy��  $
 Time�� �� *Time+4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� $ 	btnDelete��-   $	�� X ��$
��Bold"�