   M S B A M L   `   `   `  ����G  CDD_QLQuanNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DD_QLQuanNetDD_QLQuanNet  5   	   !    DD_QLQuanNet.UserManagement   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:DD_QLQuanNet     5   	     � Title$  UserAccoounts��5   	   $	��450��6   $	��800��6,    � WindowStyle$
 None=�68    � WindowStartupLocation$ CenterScreen=�6K    � SizeToContent$ WidthAndHeight=�6p    � 
ResizeMode$ NoResize=�6�   .��5	      � .+�5
   
   �� $��Information��6   $��	10,47,0,0q�6(   $
��Left=�6;   $	��Top=�6V   .��5      � .+�5      �� $��
Full name:��6   $	��Top=�6-   $
��Left=�6E   $��0,10,0,0q�6`   6t   �� 5      -   5         ��Name$ txtFullname��5      $��98,10,10,246q�60   $��25��6F   6T   �� 5      $��	Birthday:��6   $	��Top=�6,   $
��Left=�6D   $��0,41,0,0q�6_   6s   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpBirthdate��5      $
��Left=�62   $��	98,42,0,0q�6M   $	��Top=�6`      SelectedDateFormat$ Short=�6x      IsTodayHighlighted$ False��6�   	   FirstDayOfWeek$	 Monday=�6�   
   Text$
 ToDay��6�   �� 5      $��User group:��6   $
��Left=�6.   $	��Top=�6I   $��
205,41,0,0q�6a   6w   �� 5      -   5         ��Name$ cbxRole��5      $
��Left=�6,   $��
285,43,0,0q�6G   $	��Top=�6[   $��83��6s    �� SelectedIndex$ 0��6~   .@�5      �� $��Member��6#   66   �� 5      $��Staff��6#   65   5      �� 5      -   5         ��Name$ 	chkActive��5      $��Active��6.   $
��Left=�6?   $��
285,89,0,0q�6Z   $	��Top=�6n    d� 	IsChecked$
 TrueY�6�   �� 5      $��Email:��6   $
��Left=�6)   $��Center=�6D   �� 5      -   5        $ txtEmail��5      $��98,128,10,128q�6,   $��25��6C   6Q   �� 5      $��Phone:��6   $
��Left=�6)   $	��Top=�6D   $��	0,164,0,0q�6\   6q   �� 5      -   5        $ txtPhone��5      $��98,164,10,92q�6,   $��25��6B   6P   �� 5      $��Address:��6   $
��Left=�6+   $	��Top=�6F   $��	0,198,0,0q�6^   6s   �� 5      -   5        $ 
txtAddress��5      $��98,198,10,58q�6.   $��25��6D   6R   �� 5      $��Gender:��6   $
��Left=�6*   $��	98,84,0,0q�6E   $	��Top=�6X   �� 5      -   5        $ 	cbxGender��5      $
��Left=�6.   $��
154,86,0,0q�6I   $	��Top=�6]   $��55��6u    �� 
IsReadOnly$
 True��6�   $ 0��6�   .@�5      �� $
��Male��6#   64   �� 5      $��Female��6#   66   �� 5       $��Other��6#   65   5!      5"      5#      �� 5$   
   $��Account��6   $��416,47,10,99q�6$   .��5%      � .+�5&      �� $��	Username:��6   $
��Left=�6,   $	��Top=�6G   $��0,10,0,0q�6_   6s   �� 5'      -	   5        $ txtUsername��5'      $��100,11,12,245q�6/   $	��Top=�6F   $��25��6^   ${�False��6j   �� 5(      $��	Password:��6   $	��Top=�6,   $
��Left=�6D   $��2,56,0,0q�6_   6s   G� 5)      -
   5         G�Name$ txtPassword��5)      $��100,56,12,0q�63   $	��250��6H   $��25��6T   $	��Top=�6`   6�   �� 5*      $��Remaining Real amount:��6   $
��Left=�69   $��	2,115,0,0q�6T   $	��Top=�6g   $	��139��6   �� 5+      -   5        $ 	txtAmount��5+      $
��Left=�6-   $��146,119,0,0q�6H    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $	��120��6�   ${�False��6�   �� 5,      -   5         ��Name$ btnEnterMount��5,      $��Enter amount��60   $
��Left=�6G   $��278,119,0,0q�6b   $	��Top=�6w   6�   5-      5.      �� 5/   
   -   5        $ btnClose��5/      $��780,0,0,430q�6#   $
��Left=�6O   $��20��6j   ��68   Z� ��50      �� 51       �� ImageSource$ ./assets/images/close.png��6    j� Stretch$ Uniform=�6E   53      �� 54   
   -   5        $ 
btnAddUser��54      $	��Add��6%   $
��Left=�63   $��34��6N   $��631,386,0,0q�6Z   $	��Top=�6o   $	��148��6�   6�   �� 55   
   -   5         ��Name$ lbUserManager��55      $��Add User Account��6'   $��32��6B   $��0,10,0,0q�6N   $	��158��6`   ���	#00000000$��16(�6�   $	��Top=�6�   $
��Bold"�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   $��Center=�6�   $��Center=�6�   �� 56   
   -   5        $ btnUpdateUser��56      $��Update��6(   $
��Left=�69   $��34��6T   $��631,386,0,0q�6`   $	��Top=�6u   $	��148��6�   $y�Hidden=�6�   57      58      