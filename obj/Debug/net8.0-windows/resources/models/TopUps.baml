   M S B A M L   `   `   `  ����  DD_QLQuanNet*    $DD_QLQuanNet.resources.models.TopUps   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ] YXceed.Wpf.Toolkit.NET5, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  TopUps��5   	   $
��Left=�6   $	��Top=�63    � SizeToContent$ WidthAndHeight=�6K    � WindowStartupLocation$ CenterScreen=�6j   $
��Auto��6�    � WindowStyle$ 
ToolWindow=�6�    � 
ResizeMode$ NoResize=�6�   .��5	      � $��Center=�6   $��Center=�6(   $
��Auto��6C   $
��Auto��6Q   $��10q�6^    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��	Username:��61   $��5q�6B   $
��Bold"�6M   $��Center=�6_   �� 5   
   -   5         ��Name$ txtUsername��5   /   $��0��6   $��1��6   $��5q�6D   $��32��6O   ${�False��6[   $��Stretch=�6m   $��Center=�6�   �� 5   
   $��1��6   $��0��6!   $��	Add Time:��61   $��5q�6B   $
��Bold"�6M   $��Center=�6_   �� 5   
   $��1��6   $��1��6"    �� Orientation$ 
Horizontal=�62   .+�5      %  Xceed.Wpf.Toolkit.IntegerUpDown  -   5        	  Name$	 iudHour��5   !   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]
   Minimum
 062      Maximum 	9999999996>      Value 06R   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]   	Increment 16\      FormatString$
 {0}h��6j   $��32��6�   $	��100��6�   $
��Left=�6�   $	��Top=�6�   $��Right=�6�   $��5q�6�   6�     5      -   5        $	 	iudMinute��5   !   
 064    606@    06M    16W   $
 {0}m��6e   $	��100��6{   $��32��6�   $
��Left=�6�   $	��Top=�6�   $��Right=�6�   $��5q�6�   6�   5      �� 5   
   $��2��6   $��0��6!   $��
Add Money:��61   $
��Bold"�6C   $��Center=�6U   $��5q�6p   �� 5   
   -   5        $ txtMoney��5   /   $��2��6   $��1��6   $��32��6A   $	��250��6M   $��Center=�6\   $��5q�6~   $��Stretch=�6�    � 
InputScope$ Digits��6�   6$  �� 5    
   $��3��6   $��0��6!   $��	New Time:��61   $��5q�6B   $
��Bold"�6M   $��Center=�6_   �� 5!   
   -   5        $ 
txtNewTime��5!   /   $��3��6   $��1��6   $��5q�6C   $��32��6N   $��Center=�6Z   ${�False��6|   $��Stretch=�6�   �� 5"   
   $��4��6   $��0��6!   $��
New Money:��61   $��5q�6C   $
��Bold"�6N   $��Center=�6`   �� 5#   
   -   5        $ txtNewMoney��5#   /   $��4��6   $��1��6   $��5q�6D   $��32��6O   $��Center=�6[   ${�False��6}   $��Stretch=�6�   �� 5$   
   $��5��6   $��0��6!   $��Notes:��61   $��5q�6?   $
��Bold"�6J   $��Center=�6\   �� 5%   
   -   5        $ txtNotes��5%   /   $��5��6   $��1��6   $��5q�6A   $��32��6L   $��Center=�6X   $��Stretch=�6z   �� 5&   
   $��6��6   $��1��6"   $��2��62   $ 
Horizontal=�6F   $��Center=�6_   .+�5'      �� -   5         ��Name$ btnTopUp_OK��5'      $��OK��6*   $	��100��67   $��30��6C   $��5q�6O   6v   �� 5(      -	   5        $ btnTopUp_Cancel��5(      $��Cancel��6.   $	��100��6?   $��30��6K   $��5q�6W   6�   5)      5*      5+      