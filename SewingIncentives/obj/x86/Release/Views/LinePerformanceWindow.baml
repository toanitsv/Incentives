   M S B A M L   `   `   `  ����   SewingIncentives2    ,SewingIncentives.Views.LinePerformanceWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$*  $Sewing Incentives - Line Performance��$	��500��$
��Auto�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ 	CanResize=� � WindowState$ 	Maximized=�.��� 
�������.+�� $��5,5,5,5q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��� $��0,5,0,0q�$��LINE PERFORMANCE�� �� TextAlignment$ Center=�$
��Bold"��� $��1��$��0,0,0,0q� �� Orientation$ 
Horizontal=�$��Right=�.+��� $��Line:��$��Center=��� -    ��Name$ 	cboLineId��$��5,0,0,0q�$��50��{�.   �� DisplayMemberPath$
 Name��	 �� SelectedValuePath$	 LineId���� $��5,0,0,0q�$��Date:��$��Center=�(  "System.Windows.Controls.DatePicker  -   
  Name$
 dpDate��$��5,0,0,0q�   SelectedDate$ 2000-1-1X��� -    ��Name$ btnLoad��$��5,0,0,0q�$��=>��$
��Bold"�� -    �Name$ 	gridGrade��$��2��$��0,5,0,0q� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��3* ��� $
��Auto ��� $��* ��� $
��Auto � �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Output:��$��Bottom=�� $��2��$��2��$��10,0,0,0q� �� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��W.Hrs:��$��Center=��� -    ��Name$ lblWHRS��$��1��$��5,0,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(�$
��Left=�$��59���� $��2��$
��PPH:��$��Center=��� -   $ lblPPH��$��3��$��5,0,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� $��6��$��10,0,0,0q�$��Normal(A) Incentive(đ):��$��Bottom=��� $��6��$��1��$��10,0,0,0q�$��Small(a) Incentive(đ):��$��Bottom=��� $��6��$��2��$��10,5,0,0q�$��QC Reported(Hrs):��$��Bottom=��� $��1��$	��PM:��$��Bottom=��� $��2��$��1��$��10,5,0,0q�$��
Article #:��$��Bottom=��� -    ��Name$ 	txtOutput��$��1��$��5,0,0,0q��� -	   $ txtIncentiveGradeA��$��7��$��5,0,0,0q��� -
   $ txtIncentiveGradeALower��$��7��$��1��$��5,5,0,0q��� -   $ txtQCReportedHrs��$��7��$��2��$��5,5,0,0q��� -   $ txtPatternNo��$��1��$��1��$��5,5,0,0q� �� CharacterCasing$ Upper=��� -   $ txtArticleNo��$��3��$��1��$��5,5,0,0q�$ Upper=��� -   $ btnIncentiveGradeLegend��$��8��$��5,0,0,0q�$	�� * ���� $��5��$ 
Horizontal=�$��10,0,0,0q�$��Center=�.+��� $��Excess Incentive Total:��$��Center=��� -   $ lblExcessIncentive��$��5,0,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� $��đ��$��Center=��� $��5��$��1��$ 
Horizontal=�$��10,0,0,0q�$��Center=�.+��� -   $ btnExcessIncentiveAll��$��for All��
����iA�
�������$
��Bold"�$y�	Collapsed=��� -   $ btnExcessIncentiveManual��$	��150��$��Enable Manual��
����iA�
�������$
��Bold"�$��0,0,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ dgLinePerformance��$��3��$��0,5,0,0q�   AutoGenerateColumns .     CanUserAddRows .     FrozenColumnCount$ 2��   SelectionUnit$ CellOrRowHeader=�   HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 ����� � ContextMenu �� .@�m� -    m�Name$ miTransferTo��$��Transfer to��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    
FontWeight$
 Bold"�   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn    Binding  �� *
WorkerId+  $
 Name��$
 Auto $
 Bold"� .   �� *Name+  $
 Line��$
 Auto  .   �� *Line+  $ Position��$
 Auto  .   �� *
Position+4  .System.Windows.Controls.DataGridComboBoxColumn  -   colOthersPosition��$ Others Position��$
 Auto !   SelectedItemBinding! �� *OthersPosition+  $ Time In��$
 Auto  .   �� *TimeIn+  $ Time Out��$
 Auto  .   �� *	TimeOut+  -   colAdjustTimeOut��$ Adjust Time��$
 Auto ! �� *AdjustTimeOut+  $ 	Work Hour��$
 Auto  .   �� *
WorkHour+  -   
colICGrade��"   DisplayMemberPath$
" Name��#   SelectedValuePath$# Ratio��$ IC Grade��$
 Auto $   CanUserSort$ .  ! �� *IncentiveGrade+  $ 	Incentive��$
 Auto  .   �� *	Incentive+  -   colExcessIncentive��$ Excess Incentive��$
 Auto  .   �� *ExcessIncentive+% �� UpdateSourceTrigger$% PropertyChanged=�  $ Specs Incentive��$
 Auto   �� *SpecsIncentive+  $ Cumulative Incentive��$
 Auto  .   �� *CumulativeIncentive+� $��4��$��0,5,0,0q� �� $��* ��� $��* �.+�� $��0,0,5,0q� �� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto �.+��� $
��SUP:��$��Center=��� $��2��$��10,0,0,0q�$��MECH:��$��Center=��� $��4��$��10,0,0,0q�$��MONITOR:��$��Center=��� $��1��$��0,5,0,0q�$	��LL:��$��Center=��� $��2��$��1��$��10,5,0,0q�$	��QC:��$��Center=��� -   $ lblCountSUP��$��1��$��5,0,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� -   $ lblCountMECH��$��3��$��5,0,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� -   $ lblCountMONITOR��$��5��$��5,0,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� -   $ 
lblCountLL��$��1��$��1��$��5,5,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� -   $ 
lblCountQC��$��3��$��1��$��5,5,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(�� $��1��$��5,0,0,0q� �� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto �.+��� $��New Worker:��$��Center=��� $��2��$��10,0,0,0q�$��
>2 Months:��$��Center=��� $��4��$��10,0,0,0q�$	��MP:��$��Center=��� $��1��$��0,5,0,0q�$��	>1 Month:��$��Center=��� $��2��$��1��$��10,5,0,0q�$��Old Worker:��$��Center=��� $��4��$��1��$��10,5,0,0q�$��Total:��$��Center=��� -   $ lblCountNewWorker��$��1��$��5,0,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� -   $ lblCount2Months��$��3��$��5,0,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� -   $ lblCountTotalMP��$��5��$��5,0,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� -    $ lblCount1Month��$��1��$��1��$��5,5,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� -!   $ lblCountOldWorker��$��3��$��1��$��5,5,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� -"   $ lblCountTotalWorker��$��5��$��1��$��5,5,0,0q�$��0��
����iA�$
��Bold"�$��Center=�$��15(��� $��5��$��0,5,0,0q�$ 
Horizontal=�$��Right=�.+��� -#   $ btnCaculate��$	��200��$��Calculate Performance��
����iA�
�������$
��Bold"�{�.  �� -$   $ btnSave��$	��100��$��10,0,0,0q�$
��Save��
����iA�
�������$
��Bold"�{�.  �� -%   $ btnPrint��$	��100��$��10,0,0,0q�$��Print��
����iA�
�������$
��Bold"�� -&   & �Name$& popupIncentiveGradeLegend��' � 	StaysOpen' .  ( � PlacementTarget( �� ) �� ElementName$) txtIncentiveGradeA��.���� -'   * ��Name$* lvIncentiveGrade������ .R��� $ 
Horizontal=�.+��� $��Name:���� $
��Bold"����� *Name+�� $��, ���� $��Level:���� $
��Bold"����� *Ratio+