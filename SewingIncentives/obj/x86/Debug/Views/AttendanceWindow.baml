   M S B A M L   `   `   `  ����  SewingIncentives-    'SewingIncentives.Views.AttendanceWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$$  Sewing Incentives - Attendance��5   	   $	��480��60   $	��720��6=    � WindowStartupLocation$ CenterScreen=�6`    � WindowState$ 	Maximized=�6�   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� $��5,5,5,0q�6    �� Orientation$ 
Horizontal=�6&   .+�5      �� $��Month:��6   $��Bottom=�6&   �� 5      -   5         ��Name$ cboMonth��5      $��5,0,0,0q�6)   �� 5      $��/��6   $��Center=�6!   �� 5      -   5        $ cboYear��5      �� 5      $��Line:��6   $��Bottom=�6%   $��10,0,0,0q�6@   �� 5      -   5        $ cboLine��5      $��5,0,0,0q�6(   $	��100��69   �� 5      -   5         ��Name$ btnShow��5      $��10,0,0,0q�6&   $��75��68   $
��View��6F   $
��Bold"�6U    �� 	IsDefault . {�.  6�   �� 5      -   5        $ btnPrint��5      $��10,0,0,0q�6'   $��75��69   $��Print��6G    . {�.  6z   �� 5      -   5         ��Name$ chboIncludeBackground��5      $��5,0,0,0q�66   $��Include Background Color��6G   $��Bottom=�6j   5      &   System.Windows.Controls.DataGrid  5   
   -   5        	  Name$	 dgMain��5      $��1��6#   $��5,5,5,0q�60   
   AutoGenerateColumns
 .     CanUserAddRows .     
IsReadOnly .    SelectionUnit$ CellOrRowHeader=�5         CanUserResizeRows .     HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����5      �� 5   
   $��5,5,0,5q�6   $��2��6&   $ 
Horizontal=�63   $��Center=�6L   .+�5      �� $��Legend: ��6   $��Center=�6(   �� 5      $	��√��6   
�������$��20��68    �� TextAlignment$ Center=�6C   �� 5       $�� Working | ��6   $��Center=�6+   �� 5!      
���   �$��1,1,1,1q�6)   ._�5"      �� $��20��6   
�������$ Center=�6:   5#      �� 5$      $�� Holiday(Sunday) | ��6   $��Center=�63   �� 5%      $��20��6   
��� � �$ Center=�66   �� 5&      $��	 Leave | ��6   $��Center=�6)   �� 5'      $��20��6   
��� ���$ Center=�67   �� 5(      $�� Pregnant | ��6   $��Center=�6,   �� 5)      $��20��6   
����  �$ Center=�65   �� 5*      $�� Special Leave | ��6   $��Center=�61   �� 5+      $��20��6   
���  ��$ Center=�64   �� 5,      $�� Absent��6   $��Center=�6'   5-      5.      5/      