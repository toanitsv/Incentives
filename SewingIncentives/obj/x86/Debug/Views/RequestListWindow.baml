   M S B A M L   `   `   `  ����  SewingIncentives.    (SewingIncentives.Views.RequestListWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Request List��5   	   $	��500��6   $	��800��6.    � WindowStartupLocation$ CenterScreen=�6T   $��14(�6y   .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5
   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� $��Line: ��6   $��Bottom=�6&   �� 5      -   5         ��Name$ cbLine��5      $��5,0,0,0q�6'   $��60��68   �� 5      $��
Month/Year��6   $��Bottom=�6*   $��5,0,0,0q�6E   �� 5      -   5        $ cbMonth��5      $��5,0,0,0q�6(   $��40��69   �� 5      $��/��6   $��Bottom=�6!   $��5,0,0,0q�6<   �� 5      -   5        $ cbYear��5      $��5,0,0,0q�6'   $��50��68   �� 5      -   5         ��Name$ 	btnSearch��5      $��Search��6(   $��20,0,0,0q�69   $��80��6K   6Y   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
dgvRequest��5      $��1��6'   $��0,10,0,0q�64   $��12(�6F      AutoGenerateColumns .     CanUserAddRows .  	   
IsReadOnly	 . 
   SelectionUnit$
 CellOrRowHeader=�5         CanUserResizeRows .     HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Line Id��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *LineId+  5      $ WorkHours
Before��6%    6C   �� *WorkHrsBefore+  5      $ WorkHours
After��6%    6B   �� *WorkHrsAfter+  5      $ Incentive
Before��6%    6C   �� *IncentiveBefore+  5      $ Incentive
After��6%    6B   �� *IncentiveAfter+  5      $ Request
Time��6%    6?   �� *RequestTime+ �� StringFormat$ 
MM/dd/yyyy��  5      $ Reason��6%    65   �� *Reason+4  .System.Windows.Controls.DataGridTemplateColumn  5       $ Action��6)      CellTemplate 5!      �� 5"      .R�5#      �� $ 	btnAccept��6%   -   5        $��Accept��5#   8   $
��Bold"�6I   6[   5$      5&      5(      5)      5*      