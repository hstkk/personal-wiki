   M S B A M L   `   `   `  ����G  CPersonalWiki, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PersonalWikiPersonalWiki  5          PersonalWiki.View.PageTab   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:PersonalWiki     5        � 	Resources  5	         settings S� 
ObjectType&    PersonalWiki.Properties.Settings �� 
TargetType �� Triggers �� Binding �� Source �� Path �� Value �� Property	 �� Value��%�   &	        '��      7   S� 5
   
   "  6.   �� 5   
   " ��6    5      �� 5       6   �� 8   $ Default.TextWrap� True6j   .a�5      ��  �@��TextWrapping$
	 Wrap=�65   5      5      5      
 J� CommandBindings
 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$" local:Commands.ShowRevisions��5      6_     5   
   -   5        $
 Find��5      6I     5   
   -   5        $ Replace��5      6L     5   
   -   5        $ Print��5      6B     5   
   -   5        $ local:Commands.ExportTxt��5      6Y     5   
   -   5        $ local:Commands.ExportHtml��5      6[   .��5      ]� ��. .+�5      �� -   5        $��title��5       �� 	MaxLength$	 100��5      $
��Bold"�5      $	��Top=�5!      ��5      �� $ Title���5       Z� 5#      ]� 5$      $	��Top=�6   .+�5%      �� -   5        $
��date��5%      
���������5'      �� $
 Date� �� StringFormat$ HH:mm dd.MM.yyyy���� 5(      -	   5        $��unsaved��5(      $��*��5)      $y�Hidden=�5*      �� 5+       �� TextAlignment$ Right=�6   �� 5,       �� Command$" local:Commands.ShowRevisions��6   .�6C   	Revisions5.      5/      50      �� 52      -
   5        $
��text��52       �� 
AcceptsTab . $ �� HorizontalScrollBarVisibility$
 Auto=�56      " �� VerticalScrollBarVisibility$
 Auto=�57       J� 	AllowDrop .  �� AcceptsReturn .  � Language$ en-US�5?      ��54      �� $
 Text���55      Z� ��5<      �� # [  $ Default.Font���5=      �� # [  $ Default.FontSize� �� 	IsEnabled 5>      �� # [  $ Default.SpellCheck�5?      5A      5B      