   M S B A M L   `   `   `  ����  SewingIncentives:    4SewingIncentives.Views.ImportAssemblyIncentiveWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Import Assembly Incentive��5   	   $	��600��6+   $
��1000��6;    � WindowStartupLocation$ CenterScreen=�6K   .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5
   
   � $��0��6   .+�5      o� 
����ح�$��14(�6*   .@�5      m� -   5         m�Name$ miImport��5      $��Import��6-   6=   m� 5      -   5        $ miRevise��5      $��Revise��6-   6=   5      5      �� 5   
   -   5         ��Name$ groupImport��5      $��Import Data��6(   
���   �$��1��6Q   $��0,10,0,0q�6^   $��14(�6p   $y�	Collapsed=�6~   .��5      �  5      �� 5      $
��auto �6$   �� 5      $��* �6$   �� 5      $
��auto �6$   .+�5      � $��0��6   $��0,0,0,10q�6$    � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� -   5         ��Name$ btnChooseExcelFile��5      $��Choose Excel File��69   $	��150��6U   $��15(�6a   
�������$��Bottom=�6�   $
��Left=�6�   6�   5      � 5      $��1��6    5      �� 5       $��* �6(   �� 5!      $
��Auto �6(   .+�5#      �� -   5        $ 	btnImport��5#      $��2��60   $��0,5,0,0q�6=   $��30��6N   $	��150��6Z   $��Right=�6i   $��Import��6�   $
��Bold"�6�   {�.  6�   &   System.Windows.Controls.DataGrid  5$      -   5          Name$ dgvAssemblyIncentive��5$   ,   $��0��6      AutoGenerateColumns .  $��14(�6f   	   AlternatingRowBackground
	 �����
 �� AlternationCount$
 2��5%   8      RowHeaderWidth$ 10Q�6M      
IsReadOnly .    HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 ����� � 	Resources 5'      *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   '        �� 5(      "  6$    5)   "   P� 5*   &    �@ 
IsSelected . . �5+   *   ��  � ��
 ��iA�6G   �� 5,   *    � ��
 �����6G   5-   '   5/      50         Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Pattern No��61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6E   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6R   �� *	PatternNo+  53      $ Worker��61   $
 Auto 6A    6N   �� *Worker+  54      $ 	Shoe Name��61   $
 Auto 6D    6Q   �� *
ShoeName+  55      $ 
Efficiency��61   $
 Auto 6E    6R   �� *
Efficiency+  56      $ Output��61   $
 Auto 6A    6N   �� *AssemblyOutput+  57      $ 	Incentive��61   $
 Auto 6D    6Q   �� *	Incentive+59      5:      5;      5<      �� 5=   
   -   5        $ groupRevise��5=      $��Revise Data��6(   $��1��6=   $��0,10,0,0q�6J   
���   �$��14(�6p   $y�	Collapsed=�6~   .��5>      �  5?      �� 5@      $
��auto �6$   �� 5A      �� 5B      $
��auto �6$   .+�5D      ��  �� Orientation$ 
Horizontal=�6   $��0��66   $��0,5,0,10q�6C   .+�5E      �� $
��PM: ��6    $��Bottom=�6,   �� 5F      -	   5         ��Name$ txtPatternNo��5F      
���   �$	��120��6H   $��	10,0,10,0q�6T    �� CharacterCasing$ Upper=�6�   �� 5G      -
   5        $ 	btnSearch��5G      $��Search By PM��60   6G   5H        5I      -   5        $  dgvAssemblyIncentiveRevise��5I   (   $��1��6    .  $��14(�6h   
	 �����$
 2��5J   8   $ 10Q�6M    .  
 �����
 �����   CanUserAddRows .   5L      ��%�   '        �� 5M      "  6     5N      P� 5O   "    �@ 
IsSelected . . �5P   &   ��  � ��
 ��iA�6C   �� 5Q   &    � ��
 �����6C   5R   #   5T      5U       5V        5W      $ Id��6-   $
 Auto 69      
IsReadOnly .  6F   �� *Id+6]     5X      $ 
Pattern No��6-   $
 Auto 6A    .  6`   �� *	PatternNo+  5Y      $ Worker��6-   $
 Auto 6=    6J   �� *Worker+  5Z      $ 	Shoe Name��6-   $
 Auto 6@    6M   �� *
ShoeName+  5[      $ 
Efficiency��6-   $
 Auto 6A    6N   �� *
Efficiency+  5\      $ Output��6-   $
 Auto 6=    6J   �� *AssemblyOutput+  5]      $ 	Incentive��6-   $
 Auto 6@    6M   �� *	Incentive+5_      �� 5`      $��2��6   $ 
Horizontal=�6*   $��Right=�6C   .+�5a      �� -   5        $ 	btnDelete��5a      $��Delete��60   $	��100��6A   {�.  $��0,5,20,0q�6b   $
��Bold"�6�   �� 5b      -   5        $ btnSave��5b      $��0,5,0,0q�6.   $��30��6?   $	��150��6K   $
��Save��6Z   $
��Bold"�6i   {�.  6�   5c      5e      5f      5g      5h      