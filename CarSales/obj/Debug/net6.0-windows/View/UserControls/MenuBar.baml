   M S B A M L   `   `   `  ����C  ?CarSales, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:CarSales.View.UserControlsCarSales.View.UserControls  5      (    "CarSales.View.UserControls.MenuBar   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:CarSales.View.UserControls     5        � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $4 .../../Resources/MenuBarResourceDictionary.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��5* �6   �� 5      $��3* �6   .+�5   
   � $��0��6   .+�5      �� $��Center=�6   $��Center=�62   
�������$��Verdana)�6`   $��24(�6u   .��6�   
Avto.net5      5      o� 5   
   $��1��6   .@�5      m� $
��File��6   $��16(�6%   .@�5      m� $��
Import XML��6    m� Command 6/   �� *OpenXMLFileDialogCommand+m� 5      $��
Export XML��6    6/   �� *SaveXMLFileDialogCommand+m� 5       $
��Exit��6   5!      m� 5"      $	��Ads��6   $��16(�6$   .@�5#      m� $��Settings��6    6-   �� *OpenSettingsWindowCommand+5$      m� 5%      $��Tools��6   $��16(�6&   .@�5&      m� $	��Add��6    6(   �� *OpenAddWindowCommand+m� 5'      $��Delete��6    6+   �� *DeleteAdCommand+m� 5(      $
��Edit��6    6)   �� *OpenEditWindowCommand+5)      5*      5+      5,      