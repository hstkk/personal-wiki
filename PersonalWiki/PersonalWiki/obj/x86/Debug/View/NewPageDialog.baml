   M S B A M L   `   `   `  ����  PersonalWiki%    PersonalWiki.View.NewPageDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  New page��5   	   $	��173��6   $	��300��6'    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$	 New��5      6@     5   
   -   5        $ Close��5      6)   .��5	      �� $��5q�6    +� FocusedElement 5
      ��  �� ElementName$ title��.+�5   
   �� $
��Wrap=�6   �� 6)   .�6.   
New page68   6?   �� 5   
   �� 5   
   Title:6   �� 5   
   -   5        $��title��5       �� 	MaxLength$	 100��6   �� 5   
   
Project:6   �� 5   
   -   5        $��combobox��5       �� DisplayMemberPath$ Title��5       �� SelectedValuePath$ Id��5      ��5      ��  �� SelectedValue 5      �� *Id+� 5   
   $��0,5,0,0q�6   .+�5      �� $��Create��6   $��0,0,5,0q�6&   $	��New��67   �� 5      $��Cancel��6   $��5,0,0,0q�6&   $��Close��67   	 �� IsCancel	 . 6G   5      5      5      