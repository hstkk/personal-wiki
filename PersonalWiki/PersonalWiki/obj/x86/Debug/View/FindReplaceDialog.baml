   M S B A M L   `   `   `  ����  PersonalWiki)    #PersonalWiki.View.FindReplaceDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Find and replace��5   	   $	��220��6"   $	��300��6/    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ Search��5      5        5	   
   -   5        $ Replace��5	      5        5   
   -   5        $ Close��5      5      .��5      �� $��5q�6   .+�5   
   �� .@�5      �� $
��Find��6   .��5      ��  +� FocusedElement 6   ��  �� ElementName$ search��.+�5      �� $��
Find what:��6   �� 5      -   5        $��search��5      5      �� 5      -   5        $��
searchCase��5      $��
Match case��5      5      � 5      .+�5      �� $��	Find next��6!   $��0,5,5,5q�5   !   $��Search��5   !    �� 	IsDefault . 5   !   �� 5      $��Close��6!   $��5,5,0,5q�5   !   $��Close��5   !    �� IsCancel . 5    !   5!      5"      5#      �� 5$      $��Replace��6   .��5%      ��  6   �� $
 find��.+�5&      �� $��
Find what:��6   �� 5'      -   5        $
��find��5'      �� 5(      $��Replace with:��6   �� 5)      -   5        $��replace��5)      �� 5*      -   5        $��replaceCase��5+      $��
Match case��5*      5+      � 5,      .+�5-      �� $��Replace all��6!   $��0,5,0,5q�5.   !   $��Replace��5/   !    . 50   !   �� 51      $��Close��6!   $��5,5,0,5q�52   !   $��Close��53   !    . 54   !   55      56      57      58      59      5:      