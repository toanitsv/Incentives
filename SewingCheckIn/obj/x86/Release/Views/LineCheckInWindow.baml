   M S B A M L   `   `   `  ����   SewingCheckIn+    %SewingCheckIn.Views.LineCheckInWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$"  Line Check-In - Saoviet Corp��$	��480��$	��960�� � WindowStartupLocation$ CenterScreen=�$��Tahoma)�$��13(�.���  � RowDefinitions �� $��* ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  -     Name$ dgCheckInList��$��0��   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Card Id��
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ CardId�  $	 	Worker Id��$

 Auto  �� $ WorkerId�  $
	 Name��$

 Auto  �� $
 Name�  $	 
Department��$

 Auto  �� $ 
Department�  $	 Work at��$

 Auto  �� $ WorkAt�  $	 Position��$

 Auto  �� $ Position�4  .System.Windows.Controls.DataGridCheckBoxColumn  $	 Is Terminal Check-In��$

 Auto  �� $ IsTerminalCheckIn�  $	 Terminal Time��$

 Auto  �� $ TerminalTime�  $	 Is Line Check-In��$

 Auto  �� $ IsLineCheckIn�  $	 	Line Time��$

 Auto  �� $ LineTime�� $��1��$��5q� � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��0,0,5,0q�$��Card Id:��$��Center=��� -    ��Name$ 	txtCardId��$��1��$��0,0,10,0q�$	��150��
�������
���  ��$��15(�$
��Bold"� � ToolTip$ Enter CardId��{�.  $��Center=��� -    ��Name$ 
btnCheckIn��$��2��$��0,0,10,0q�$��Check-In��$ Click to Check-In��
��� � �
�������$
��Bold"� �� 	IsDefault . {�.  �� -   $ btnPrint��$��3��$��0,0,5,0q�$��Create Report��$&  Click to Create and Print Report��
��� � �
�������$
��Bold"�{�.  � -    �Name$ gridShowButton��$��4��