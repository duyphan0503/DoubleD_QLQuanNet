   M S B A M L   `   `   `  ����G  CDD_QLQuanNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DD_QLQuanNet.commonDD_QLQuanNet.common  5   	   $    DD_QLQuanNet.StationManagement   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3color!clr-namespace:DD_QLQuanNet.common     5   	     � Title$  StationManagement��5   	   $	��800��6#    � SizeToContent$ Width=�60    � 
ResizeMode$ NoResize=�6F    � WindowStartupLocation$ CenterScreen=�6\    � 	Resources 5	      0   *DD_QLQuanNet.common.StatusToColorConverter   StatusToColorConverter��%   &	          5
   
   6'   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��Auto �6    60   �� 5      $
��Auto �6    60   �� 5      $��* �6    6-   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   $��32��6?   $��10q�6K   .+�5      �� -   5         ��Name$ txtStationName��5      $	��200��62   $��Center=�6>   6�   �� 5      -   5        $��Search��5      5      5      �� 5      $��1��6   $ 
Horizontal=�6&   $��32��6?   .+�5      �� -   5        $��	10,0,10,0q�5   4   ��6G   Z� ��6]   Z� .��5      �� $��/assets/images/add.png��6   5      �� 5       -   5        $��	10,0,10,0q�5    5   ��6H   Z� ��6^   Z� .��5!      �� $��/assets/images/edit.png��6   5"      �� 5#      -   5        $��	10,0,10,0q�5#   8   ��6K   Z� ��6a   Z� .��5$      �� $��/assets/images/delete.png��6   5%      �� 5&      -   5        ��5&   ;   Z� ��6Q   Z� .��5'      �� $$��/assets/images/maintenance.png��6   5(      5)      �� 5*      $��List Station��6   $��2��6-   $��10q�6:   
���   �.��5+      �� -   5        	 ��Name$	 	lvStation��5+      
 �� SelectedValuePath$
 Station_Name��6.   $
��Left=�6O   $	��Top=�6j   ��6�   Z� ��5,      �� 5-      .R�5.      A�  A� Columns$ 6��6*   5/      ��51      �� 52      .R�53      �� $ Vertical=�6)   $��5q�6@   ��6K   �� *Status+ �� 	Converter# [  .+�54   "   �� $	��100��6)   $	��100��65   ._�55   &   �� $!��/assets/images/computer.jpg��6,   $��UniformToFill=�6Q   56   #   �� 57   "   $
��Bold"�6J   $��Center=�6\   ��6,   �� *Station_Name+6\   �� 58   "   $
��Bold"�6D   $��Center=�6V   ��6,   �� *Status+6V   59      5:      5<      5=      5>      � 5?   
   $��1��6    5@      �� 5A      $
��Auto �6    60   �� 5B      $��* �6    6-   �� 5C      $��* �6    6-   .+�5E      �� $ 
Horizontal=�6   $��10q�62   .+�5F      �� -   5        $ txtSearchUser��5F      $��32��61   $	��200��6=   $��Center=�6I   6�   �� 5G      -	   5        $��Search��5G      6G   5H      �� 5I      $��1��6   $��	Functions��6$   .��5J      �  5K      �� 5L      $��* �6(   65   �� 5M      $��* �6(   65   �� 5N      $��* �6(   65   �� 5O      $��* �6(   65   .+�5Q      �� $��0��6!   $ 
Horizontal=�6.   .+�5R      �� $��
List User:��6$   $��Center=�66   $
��Bold"�6Q   $��10q�6c   6q   �� 5S      -
   5         ��Name$ cbxListUser��5S   #   $
 Username��68    �� DisplayMemberPath$ Username��6U   $	��300��6r   $��32��6~   $��Center=�6�   6�   5T      �� 5U      -   5        $��1��5U      $��Turn off��6*   6W   �� 5V      -   5        $��2��5V      $��Turn on��6*   6U   �� 5W      $��3��6   $
��Lock��6*   6;   5X      5Y      5Z      5[      5\      