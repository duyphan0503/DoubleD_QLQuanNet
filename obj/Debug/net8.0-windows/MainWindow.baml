   M S B A M L   `   `   `  ����G  CDD_QLQuanNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DD_QLQuanNetDD_QLQuanNet  5   	       DD_QLQuanNet.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:DD_QLQuanNet     5   	     � Title$$  DoubleD - Quản lý quán Net��5   	   $	��450��6,   $	��800��69    � WindowState$ 	Maximized=�6E   .��5
      � .+�5   
   o� $��	0,0,0,410q�6   .@�5      m� $��	Functions��6   $��24��6*   .@�5      m� $��Computer��6    m� Icon 5      �� 5      $ ��/assets/images/monitor.png��6    5      m� 5      $��Members��6    5      �� 5      $��/assets/images/member.png��6    5      m� 5      $��
User Group��6    5      �� 5      $��/assets/images/user.png��6    5      m� 5      -   5         m�Name$ ChangeAdminPassword��5      $��Change Admin Password��68    5      �� 5      $!��/assets/images/password.png��6    5       m� 5!      -   5        $ 	btnLogout��5!      $��Logout��6.    5"      �� 5#      $ ��/assets/images/logout1.png��6    5%      5&      m� 5'      $��System��6   5(      m� 5*      $��Language��6   .@�5+      m� $��English��6   m� 5,      $��
Vietnamese��6   5-      5.      �� 5/   
   $��	0,401,0,0q�6   $��Bottom=�6'   .@�50      �� $��Total member: ��6   51      �� 52      �� 53      $��Total computer: ��6   54      �� 55      �� 56      $��Online: ��6   57      �� 58      �� 59      $��Available: ��6   5:      �� 5;      �� 5<      $��Disconnected: ��6   5=      5>      �� 5?   
   $��	0,24,0,27q�6   .@�5@      �� $��	Computers��6   .��5A      &   System.Windows.Controls.DataGrid  -   5          Name$ dgComputers��5A         AutoGenerateColumns$ False��60      
IsReadOnly$
 True��6L      HorizontalGridLinesBrush �	#FF526D82   VerticalGridLinesBrush �	#B29DB2BF	   Columns	 5B      0  *System.Windows.Controls.DataGridTextColumn  5C      ,  &System.Windows.Controls.DataGridColumn
   Header$

 Name��6N      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6\   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *Station_Name+6\     5D      $
 Status��6H   $	 100 6X    6-   �� *Status+6X     5E      $

 User��6J   $	 100 6X    6-   �� *
Username+6X     5F      $
 Start��6G   $	 100 6V    6-   �� *Start+6V     5G      $

 Used��6F   $	 100 6T    6-   �� *Used+6T     5H      $
 	Remaining��6K   $	 100 6^    6-   �� *	Remaining+6^     5I      $
 Money��6I   $	 100 6X    6-   �� *	Balance+6X     5J      $

 Date��6F   $	 100 6T    6-   �� *Date+6T     5K      $
 Group��6F   $	 100 6U    6-   �� *Type+6U     5L      $

 Note��6M   $	 100 6[    6-   �� *Description+6[   5N      5O      �� 5P      $��Accounts��6   .��5Q      � ���	#FFE5E5E5.+�5R        -   5        $ 
dgAccounts��5R   =   $��	0,21,0,19q�6Q   $ False��6d   $
 True��5S         GridLinesVisibility$ 
Horizontal=�61    �	#FF9DB2BF��5R      �� *Users+	 5U        5V      $
 Username��61   $	 150 6`      
IsReadOnly$
 True��6l    6C   �� *
Username+6l     5W      $
 Money��61   $	 100 6t    6@   �� *	Balance+ �� StringFormat$ {0:#,0}��6t     5X      $
 
User Group��61   $	 100 6^    6E   �� *Role+6^     5Y      $
 Status��61   $	 250 6\    6A   �� *Status+6\    � 	Resources 5[         RowContextMenu��%n   &	        �� 5\      .@�5]   "   m� $ btnTopUp��6+   -   5        $��TopUp��5]   >   6c   5^      5_      5a      �� 5c      -   5         ��Name$ btnAddAccount��5c      $
��Left=�64   $	��Top=�6O    J� RenderTransformOrigin$ -0.422,-0.544&�6g   $��16��6�   $��16��6�   $��2,2,0,0q�6�   ��6�   Z� ��5d      �� 5e       j� Stretch$ Uniform=�6)    �� ImageSource$ .\assets\images\add.png��6;   5g      �� 5h      -   5        $ btnEditAccount��5h      $
��Left=�66   $	��Top=�6Q   $ -0.422,-0.544&�6i   $��16��6�   $��16��6�   $��22,2,0,0q�6�   ��6�   Z� ��5i      �� 5j      $ .\assets\images\edit.png��6)   $ Uniform=�6P   5l      �� 5m      -   5        $ btnDeleteAccount��5m      $ -0.422,-0.544&�67   $��16��6]   $��16��6h   $	��Top=�6�   $��
43,2,735,0q�6�   $
��Left=�6�   ��6t   Z� ��5n      �� 5o      $  .\assets\images\delete.png��6)   $ Uniform=�6R   5q      �� 5r      -	   5        $ 	btnMember��5r      $��Member��60   $��50��6A   $��24��6L   $
��Left=�6X   $��Bottom=�6s   $��0,0,0,-5q�6�   6�   �� 5s      -
   5        $ btnStaff��5s      $��Staff��6/   $��50��6?   $��24��6J   $��55,336,689,-5q�6V   $��Bottom=�6m   $
��Left=�6�   6�   �� 5t      -   5        $ btnAdmin��5t      $��Admin��60   $��50��6@   $��24��6K   $��
110,0,0,-5q�6W   $��Bottom=�6k   $
��Left=�6�   6�   5u      5v      5w      5x      5y      