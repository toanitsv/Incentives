   M S B A M L   `   `   `  ����   SewingIncentives0    *SewingIncentives.Views.SelectSectionWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$(  "Sewing Incentives - Select Section��$	��100��$	��300�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � WindowStyle$ 
ToolWindow=�.��� 
�������.+�� $��Center=� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto �.+��� $��Section:��$��Bottom=�$
��Bold"��� -    ��Name$ 
cboSection��$��1��$��5,0,0,0q� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ 	SectionId���� -   	 ��Name$	 btnOk��$��2��$��2��$��OK��$��10,0,0,0q�$��75��
����iA�
�������$
��Bold"�
 �� 	IsDefault
 . �� $��1��$��	Password:��$��Bottom=�G� -    G�Name$ txtPassword��$��1��$��1��$��5,5,0,0q� G� PasswordChar$ *�� � ToolTip$, &Call Software Creator to Get Password!��