   M S B A M L   `   `   `  ����F  BClient_Chat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Client_ChatClient_Chat  5   	       Client_Chat.ChatWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Client_Chat     5   	   -   5        $��14(�5   	     � 
ResizeMode$  NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � Title$ BoxChatWindow��5   	   $	��722��6   $��956.818��6,   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   6-   �� 5      6!   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $��55 �6    6.   �� 5      6"   .+�5      � $��0��6   
�������.+�5      �� $��20(�6   $
��Bold"�6&   $��Center=�68   .��6S   Danh sách thành viên6i   5      �� 5      -   5         ��Name$ stackPnlFriends��5      $��5q�62   $��1��6=   5      5      � 5    
   $��1��6   .+�5!      �� $��5,0,5.4,0.2q�6   .+�5"      � $��55��6   .+�5#      ��  �� Orientation$ 
Horizontal=�6!   .+�5$      � .+�5&      � $��50��6#   $��50��6/   .+�5'   "   �� -   5        $
��Mask��5'   )   
����������	#FF3B5998$��.5q�5*   %    �� CornerRadius$ 50��5+   %   5,   #   �� 5-   "   .+�5.   &   �� -   5         ��Name$ imageAvatar��5.   ,   $��50��6A   $��50��6M   $
��Fill=�6X   	 J� OpacityMask	 5/   &   !� 50   *   
 !� Visual
 66   ��  �� ElementName$
 Mask��53   #   54      56      �� 57      -   5         ��Name$ txblockName��57   $   $��10 0 0 0q�69   $��26(�6K   $
��Name��6Y   $��Center=�6e   58      59      5:      � 5;      $	��0 5q�6   $	��560��6$   
�������.+�5<      �� -   5         ��Name$ scrollViewerMess��5<   #   $��Hidden=�6=   $
��Auto=�6b   .��5=      �� -   5        $ stackPanelMain��5=   %   $
��0 10q�6=   5?      5@      5A      � 5B      $	��0 5q�6    5C      �� 5D      6-   �� 5E      $��0.18* �6+   6;   .+�5G      �� -   5         ��Name$ txbMess��5G      $��0 0 8 0q�6/   $��50��6@   6N   �� 5H      -	   5         ��Name$ btnSend��5H       �� 	IsDefault . $��1��6?   $��16(�6O   $��Gửi��6]   6�   5I      5J      5K      5L      5M      