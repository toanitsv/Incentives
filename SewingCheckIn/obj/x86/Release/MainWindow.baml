   M S B A M L   `   `   `  ����   SewingCheckIn    SewingCheckIn.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$$  Sewing Check-In - Saoviet Corp��$	��250��$	��300�� � WindowStyle$ 
ToolWindow=� � WindowStartupLocation$ CenterScreen=�$��Tahoma)�$��13(�.���  � ColumnDefinitions �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��0��$��0,0,10,5q�$��Choose Line: ��$��Bottom=�$
��Bold"�� $��1��$��0��$��0,0,0,5q� �� $��* ��� $
��Auto �.+��� -    ��Name$ cboLines��$��0�� � ToolTip$ Click to Choose Line���� -    ��Name$ btnAdd��$��1��$��5,0,0,0q�$	�� + ���� $��0��$��1��$��0,0,10,0q�$��Choose Date:��$��Center=�$
��Bold"�� $��1��$��1�� �� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� -   $ cboDay��$��0��$	 Day���� $��1��$��/��$��Center=��� -   $ cboMonth��$��2��$ Month���� $��3��$��/��$��Center=��� -   $ cboYear��$��4��$
 Year���� -    ��Name$ txtLine��$��0��$��2��$��2��$��0,5,0,5q�
�������	 �� 
IsReadOnly	 . $
 �� HorizontalScrollBarVisibility$

 Auto=��� $��0��$��2��$��3��$
��Left=�$��Bottom=�$
��Bold"�$ Click to Clear All���� -    ��Name$ 	linkClear�� �� 	IsEnabled .  .�Clear�� $��0��$��2��$��3��$��Right=�$��Bottom=�$
��Bold"�$# Click to Proceed and Check-In���� -	   $ linkProceed�� .  .�Click to Proceed and Check-In