   M S B A M L   `   `   `  ����G  CDD_QLQuanNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DD_QLQuanNetDD_QLQuanNet  5   	       DD_QLQuanNet.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:DD_QLQuanNet     5   	     � Title$  Đăng nhập��5   	   $��Center=�6   $��Center=�68    � WindowStyle$
 None=�6S    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�6   
���   � � SizeToContent$ WidthAndHeight=�6X   .��5
      � $��Center=�6   $��Center=�6(    J� RenderTransformOrigin$ 0,0.5&�6C    � Cursor$ AppStarting��6a   $	��493��6v   $	��305��6�   ���	#FFDDE6ED.+�5   
   �� $��	Username:��6   $
��Left=�6$   $��	56,54,0,0q�6?   $	��Top=�6R   ���	#FF27374D$
��Bold"�6�   �� 5   
   $��	Password:��6   $
��Left=�6$   $��
56,127,0,0q�6?   $	��Top=�6S   ���	#FF27374D$
��Bold"�6�   �� 5   
   -   5         ��Name$ txtUsername��5      $��
56,80,57,0q�6'    �� TextWrapping$
 Wrap=�6;   $	��380��6O   $��32��6[   $	��Top=�6g   $��Center=�6   �� 5   
   -   5        	 ��Name$	 btnLogin��5      $��Login��6#   $
��Left=�63   $��
60,240,0,0q�6N   $	��Top=�6b   $	��136��6z   $��29��6�   $ 0.525,0.694&�6�   
���   ����	#FF27374D$
��Bold"�6  ��6�   Z� 6  G� 5   
   -   5        
 G�Name$
 txtPassword��5      $��Center=�6+   $��	0,152,0,0q�6H   $	��Top=�6[   $	��380��6s   $��34��6   $��Center=�6�   �� 5   
   -   5        $	 btnClose��5      $��468,0,0,285q�6#   ��68   Z� ��5      �� 5       �� ImageSource$ /assets/images/close.png��6    j� Stretch$ Uniform=�6D   5      �� 5   
   -   5         ��Name$ chkRemember��5      $��Remember Login��6(   $
��Left=�6A   $��
56,198,0,0q�6\   $	��Top=�6p   ���	#FF27374D$��Italic$�6�   �� 5   
   $��LOGIN��6   $��Center=�6    $	��Top=�6=   $
��Bold"�6U   $��
Wide Latin)�6g   $��24(�6   ���	#FF27374D6�   5      5      