   M S B A M L   `   `   `  ����   SewingIncentives-    'SewingIncentives.Views.AttendanceWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$$  Sewing Incentives - Attendance��$	��480��$	��720�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�.���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��5,5,5,0q� �� Orientation$ 
Horizontal=�.+��� $��Month:��$��Bottom=��� -    ��Name$ cboMonth��$��5,0,0,0q��� $��/��$��Center=��� -   $ cboYear���� $��Line:��$��Bottom=�$��10,0,0,0q��� -   $ cboLine��$��5,0,0,0q�$	��100���� -    ��Name$ btnShow��$��10,0,0,0q�$��75��$
��View��$
��Bold"� �� 	IsDefault . {�.  �� -   $ btnPrint��$��10,0,0,0q�$��75��$��Print�� . {�.  �� -    ��Name$ chboIncludeBackground��$��5,0,0,0q�$��Include Background Color��$��Bottom=�&   System.Windows.Controls.DataGrid  -   	  Name$	 dgMain��$��1��$��5,5,5,0q�
   AutoGenerateColumns
 .     CanUserAddRows .     
IsReadOnly .    SelectionUnit$ CellOrRowHeader=�   CanUserResizeRows .     HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 ������� $��5,5,0,5q�$��2��$ 
Horizontal=�$��Center=�.+��� $��Legend: ��$��Center=��� $	��√��
�������$��20�� �� TextAlignment$ Center=��� $�� Working | ��$��Center=��� 
���   �$��1,1,1,1q�._��� $��20��
�������$ Center=��� $�� Holiday(Sunday) | ��$��Center=��� $��20��
��� � �$ Center=��� $��	 Leave | ��$��Center=��� $��20��
��� ���$ Center=��� $�� Pregnant | ��$��Center=��� $��20��
����  �$ Center=��� $�� Special Leave | ��$��Center=��� $��20��
���  ��$ Center=��� $�� Absent��$��Center=�