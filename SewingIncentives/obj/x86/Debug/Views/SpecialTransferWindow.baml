   M S B A M L   `   `   `  ����  SewingIncentives2    ,SewingIncentives.Views.SpecialTransferWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$*  $Sewing Incentives - Special Transfer��5   	   $	��360��66   $	��720��6C    � WindowStartupLocation$ CenterScreen=�5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5	      $��2* �6   .+�5   
   � $��5,5,0,5q�6    5      �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��144* �6    .+�5      �� $��Worker No. :��6   $��Center=�6,   �� 5      $��1��6   $��Type:��6%   $��Bottom=�62   �� 5      $��2��6   $��Line ID:��6%   $��Bottom=�65   �� 5      $��3��6   $��Date:��6%   $��Bottom=�62   �� 5      $��4��6   $��Time:��6%   $��Bottom=�62   �� 5      -   5         ��Name$ txtWorkerId��5      $��1��6+   $��6,0,-1,0q�6;    �� CharacterCasing$ Upper=�6M    �� AcceptsReturn . $	��100��6z   �� 5      -   5         ��Name$ cboTransferType��5      $��1��60   $��1��6@   $��5,5,0,0q�6M    �� SelectedValuePath$ TransferType��6^   	 �� DisplayMemberPath$
	 Name��6   �� 5       -   5        $ 	cboLineId��5       $��1��6*   $��2��6:   $��5,5,0,0q�6G   $ LineId��6X   $
	 Name��6s   (  "System.Windows.Controls.DatePicker  5!      -   5        
  Name$
 dpDate��5!      $��1��6)   $��3��69   $��5,5,0,0q�6F      SelectedDate$ 
11/28/2016X�6W   �� 5"      -   5        $ txtTime��5"      $��1��6'   $��4��67   $��5,5,0,0q�6D   �� 5#      -   5         ��Name$ btnSave��5#      $��2��6&   $��5��6;   $��0,5,0,0q�6H   $��Right=�6Y   $��75��6u   $
��Save��6�   $
��Bold"�6�   6�   5$      � 5%   
   $��1��6   $��5,5,5,5q�6    5&      �� 5'      $
��Auto �6    �� 5(      $��* �6    .+�5*      ��  �� Orientation$ 
Horizontal=�6   .+�5+      �� $��Worker No.:��6   $��Bottom=�6/   �� 5,      -   5        $ txtWorkerIdToSearch��5,      $��5,0,0,0q�67   $	��200��6H   $ Upper=�6W   �� 5-      -	   5        $ 	btnSearch��5-      $��5,0,0,0q�6,   $��75��6=   $��Search��6K   $
��Bold"�6\    �� 	IsDefault . 6�   5.      &   System.Windows.Controls.DataGrid  5/      -
   5          Name$ dgMain��5/      $��1��6'   $��0,5,0,0q�64      AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly .    Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Worker Id��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *
WorkerId+  54      $ Transfer Type��6)    6@   �� *TransferName+  55      $
 Line��6)    67   �� *DestinationLineName+  56      $
 Date��6)    67   �� *Date+ �� StringFormat$ 
dd/MM/yyyy��  57      $
 Time��6)    67   �� *Time+4  .System.Windows.Controls.DataGridTemplateColumn  58         CellTemplate 59      �� 5:      .R�5;   "   �� $ 	btnDelete��6)   -   5        $	�� X ��5;   <   $
��Bold"�6J   6\   5<      5>      5@      5A      5B      5C      