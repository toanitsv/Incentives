   M S B A M L   `   `   `  ����  SewingIncentives0    *SewingIncentives.Views.SelectSectionWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$(  "Sewing Incentives - Select Section��5   	   $	��100��64   $	��300��6A    � WindowStartupLocation$ CenterScreen=�6M    � 
ResizeMode$ NoResize=�6r    � WindowStyle$ 
ToolWindow=�6�   .��5      � 
�������.+�5   
   � $��Center=�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5	      $��* �6#   �� 5
      $
��Auto �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��Section:��6   $��Bottom=�6(   $
��Bold"�6C   �� 5      -   5         ��Name$ 
cboSection��5      $��1��6+   $��5,0,0,0q�6;    �� DisplayMemberPath$
 Name��6M    �� SelectedValuePath$ 	SectionId��6f   �� 5      -   5        	 ��Name$	 btnOk��5      $��2��6$   $��2��64   $��OK��6E   $��10,0,0,0q�6R   $��75��6d   
����iA�
�������$
��Bold"�6�   
 �� 	IsDefault
 . 6�   �� 5      $��1��6   $��	Password:��6%   $��Bottom=�66   G� 5      -   5         G�Name$ txtPassword��5      $��1��6/   $��1��6?   $��5,5,0,0q�6L    G� PasswordChar$ *��6]    � ToolTip$, &Call Software Creator to Get Password!��6n   5      5      5      