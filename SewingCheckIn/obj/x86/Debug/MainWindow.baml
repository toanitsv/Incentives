   M S B A M L   `   `   `  ����  SewingCheckIn    SewingCheckIn.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$$  Sewing Check-In - Saoviet Corp��5   	   $	��250��60   $	��300��6=    � WindowStyle$ 
ToolWindow=�5   	    � WindowStartupLocation$ CenterScreen=�6"   $��Tahoma)�5   	   $��13(�5   	   .��5      �  � ColumnDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6$   $��0,0,10,5q�61   $��Choose Line: ��6C   $��Bottom=�6X   $
��Bold"�6s   � 5   
   $��1��6   $��0��6   $��0,0,0,5q�6,    5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� -   5         ��Name$ cboLines��5   '   $��0��6    � ToolTip$ Click to Choose Line��69   �� 5      -   5         ��Name$ btnAdd��5      $��1��6%   $��5,0,0,0q�65   $	�� + ��6F   6T   5      �� 5   
   $��0��6   $��1��6$   $��0,0,10,0q�61   $��Choose Date:��6C   $��Center=�6W   $
��Bold"�6r   � 5   
   $��1��6   $��1��6    5      �� 5      $��* �6#   �� 5       $
��Auto �6#   �� 5!      $��* �6#   �� 5"      $
��Auto �6#   �� 5#      $��* �6#   .+�5%      �� -   5        $ cboDay��5%      $��0��6'   $	 Day��67   �� 5&      $��1��6   $��/��6(   $��Center=�61   �� 5'      -   5        $ cboMonth��5'      $��2��6)   $ Month��69   �� 5(      $��3��6   $��/��6(   $��Center=�61   �� 5)      -   5        $ cboYear��5)      $��4��6(   $
 Year��68   5*      �� 5+   
   -   5         ��Name$ txtLine��5+      $��0��6#   $��2��63   $��2��6G   $��0,5,0,5q�6T   
�������	 �� 
IsReadOnly	 . $
 �� HorizontalScrollBarVisibility$

 Auto=�6�   �� 5,   
   $��0��6   $��2��6$   $��3��68   $
��Left=�6E   $��Bottom=�6`   $
��Bold"�6{   $ Click to Clear All��6�   �� 5-      -   5         ��Name$ 	linkClear��5-       �� 	IsEnabled .  .�6U   Clear5/      50      �� 51   
   $��0��6   $��2��6$   $��3��68   $��Right=�6E   $��Bottom=�6a   $
��Bold"�6|   $# Click to Proceed and Check-In��6�   �� 52      -	   5        $ linkProceed��52       .  .�6Y   Click to Proceed and Check-In54      55      57      58      