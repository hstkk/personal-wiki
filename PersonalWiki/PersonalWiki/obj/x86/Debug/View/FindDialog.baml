   M S B A M L   `   `   `  ����  PersonalWiki    PersonalWiki.FindDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	Revisions��5   	   $	��300��6   $	��300��6(    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ Close��5      6)     5   
   -   5        $
 Find��5      6@   .��5	      �� $
��Auto=�6   $
��Auto=�5
      .��5   
   �� $��5q�6    +� FocusedElement 5      ��  �� ElementName$ search��.+�5      �� $
��Wrap=�6   $
��Bold"�5      $��Global find��5      �� 5      $
��Wrap=�6   $��10(�5      $_��YPlease note that the selected page will be opened to a new tab if it is not already open.��5      �� 5      $��
Find what:��6   �� 5      -   5        $��search��5      5      �� 5      $��Find all��6   $��0,5,0,5q�5      $
��Find��5       �� 	IsDefault . 5      &   System.Windows.Controls.DataGrid  5      -   5        $��dataGrid��5         CanUserReorderColumns .    CanUserResizeColumns .    CanUserResizeRows .  	   CanUserSortColumns	 . 
   AlternatingRowBackground

 ����� �� AlternationCount$ 2��5!         
IsReadOnly .    SelectionMode$ Single=�5#      ��5      �� 5$      �� 5%      $��Close��6   $��5,5,0,5q�5&      $��Close��5'       �� IsCancel . 5(      5)      5*      5+      