   M S B A M L   `   `   `  ����  SewingCheckIn+    %SewingCheckIn.Views.LineCheckInWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$"  Line Check-In - Saoviet Corp��5   	   $	��480��6.   $	��960��6;    � WindowStartupLocation$ CenterScreen=�6G   $��Tahoma)�5   	   $��13(�5   	   .��5      �  � RowDefinitions 5   
   �� 5	      $��* �6   �� 5
      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgCheckInList��5      $��0��6*      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Card Id��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto 66   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   ��  �� Path$ CardId�  5      $	 	Worker Id��6%   $

 Auto 68    6E   �� $ WorkerId�  5      $
	 Name��6%   $

 Auto 63    6@   �� $
 Name�  5      $	 
Department��6%   $

 Auto 69    6F   �� $ 
Department�  5      $	 Work at��6%   $

 Auto 66    6C   �� $ WorkAt�  5      $	 Position��6%   $

 Auto 67    6D   �� $ Position�4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $	 Is Terminal Check-In��6)   $

 Auto 6G    6T   �� $ IsTerminalCheckIn�  5      $	 Terminal Time��6%   $

 Auto 6<    6I   �� $ TerminalTime�  5      $	 Is Line Check-In��6)   $

 Auto 6C    6P   �� $ IsLineCheckIn�  5      $	 	Line Time��6%   $

 Auto 68    6E   �� $ LineTime�5      � 5   
   $��1��6   $��5q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5       $��* �6#   .+�5"      �� $��0��6   $��0,0,5,0q�6(   $��Card Id:��69   $��Center=�6I   �� 5#      -   5         ��Name$ 	txtCardId��5#      $��1��6)   $��0,0,10,0q�69   $	��150��6K   
�������
���  ��$��15(�6{   $
��Bold"�6�    � ToolTip$ Enter CardId��6�   {�.  $��Center=�6�   �� 5$      -   5         ��Name$ 
btnCheckIn��5$      $��2��6)   $��0,0,10,0q�69   $��Check-In��6K   $ Click to Check-In��6^   
��� � �
�������$
��Bold"�6�    �� 	IsDefault . {�.  6�   �� 5%      -   5        $ btnPrint��5%      $��3��6'   $��0,0,5,0q�67   $��Create Report��6H   $&  Click to Create and Print Report��6`   
��� � �
�������$
��Bold"�6�   {�.  6�   � 5&      -   5         �Name$ gridShowButton��5&      $��4��6+   6=   5'      5(      5)      