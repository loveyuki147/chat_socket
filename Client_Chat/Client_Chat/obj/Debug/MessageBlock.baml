   M S B A M L   `   `   `  ����F  BClient_Chat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Client_ChatClient_Chat  5          Client_Chat.MessegaBlock   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Client_Chat     5      $
��auto��5      $	��600��6-   ��6<   ��   �� ElementName$  
messageBox�� �� Path$ Height�.��5      � .+�5	   
   �� -   5         ��Name$ 	stackMain��5	       �� Orientation$ 
Horizontal=�6(   $��0,0,0.4,0.4q�6A   .+�5      � -   5         �Name$ 
gridAvatar��5      $��40��6'   $��40��63   $	��Top=�6>   $��0 7 5 0q�6V   .+�5      �� -   5        $
��Mask��5      
�������
����  �$��2q�5   %    �� CornerRadius$ 50��5   %   5      �� 5      .+�5      �� -   5         ��Name$ imageAvatar��5      $��40��61   $��40��6=   $
��Fill=�6H   ��6W   �� $ Source� J� OpacityMask 5      !� 5       !� Visual 6&   �� $
  Mask��5      5      � 5      -   5        $ GridMessage��5      $	��Top=�6(   $��0 0 5 0q�6@   .+�5      �� -   5        $��Mask2��5      
����������	#FF3B5998$��.5q�5   %   $ 10��5    %   5!      �� 5"      -   5        $ stackPanelMain��5"      .+�5#      �� -   5        	 ��Name$	 txbMess��5#       $	��520��5$       $	��100��5%       
 �� Padding$
 5q�5'   V   ��5&       �� $  
messageBox��$ Height���5'       �� $  
messageBox��$ Width��� 5(      -	   5        $ 
messageBox��5(   (   $��0q�6<   .+�5)   !   �� -
   5         ��Name$ lbnameDisplay��5)   '   ���	#FF3B5998$��13(�5*   '   ��65   �� $ NameDisplay�6\   �� 5+   "   -   5        $	 txbMessMain��5+   ,   $��14(�6A   $��5 2 10 2q�6O   $
��Wrap=�5,   ,   $�� ��5-   ,   5.   .   �� 5/   "   -   5        $ lbTime��5/   (   $��13(�68   ���	#FF3B5998��51   (   �� $
 Time�6H   53      55       56      !� 57       6&   �� $  Mask2��5:      5;      5>      5?      5@      