   M S B A M L   `   `   `  ����  DD_QLQuanNet&     DD_QLQuanNet.ServicesUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      $	��400��5      $	��780��6   
����� �.��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��0,0,0,10q�6;   .+�5      �� -   5        $	��Add��5      $	��100��6#   $��5q�6/   6:   �� 5      -   5        $
��Edit��5      $	��100��6$   $��5q�60   6;   �� 5      -   5        $��Delete��5      $	��100��6&   $��5q�62   6=   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��ServicesDataGrid��5      $��1��6+      AutoGenerateColumns$ False��68      
IsReadOnly$
 True��6T      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Service ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	ServiceId+  5      $ Service Name��6%    6;   �� *ServiceName+  5      $ Price��6%    64   �� *Price+5      5      5      