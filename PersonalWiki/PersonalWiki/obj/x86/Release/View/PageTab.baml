   M S B A M L   `   `   `  ���� G  CPersonalWiki, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PersonalWikiPersonalWiki      PersonalWiki.View.PageTab   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:PersonalWiki       � 	Resources     settings S� 
ObjectType&    PersonalWiki.Properties.Settings �� 
TargetType �� Triggers �� Binding �� Source �� Path �� Value �� Property	 �� Value��%�   &	        '�� 
     7   S� "  �� " �� ��  �� 8   $ Default.TextWrap� True.a���  �@��TextWrapping$
	 Wrap=�
 J� CommandBindings
 )  #System.Windows.Input.CommandBinding  -      Command$" local:Commands.ShowRevisions��  -   $
 Find��  -   $ Replace��  -   $ Print��  -   $ local:Commands.ExportTxt��  -   $ local:Commands.ExportHtml��.��]� ��. .+��� -   $��title�� �� 	MaxLength$	 100��$
��Bold"�$	��Top=����� $ Title���Z� ]� $	��Top=�.+��� -   $
��date��
����������� $
 Date� �� StringFormat$ HH:mm dd.MM.yyyy���� -	   $��unsaved��$��*��$y�Hidden=���  �� TextAlignment$ Right=���  �� Command$" local:Commands.ShowRevisions��.�	Revisions�� -
   $
��text�� �� 
AcceptsTab . $ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=� J� 	AllowDrop .  �� AcceptsReturn .  � Language$ en-US����� $
 Text���Z� ���� # [  $ Default.Font����� # [  $ Default.FontSize� �� 	IsEnabled �� # [  $ Default.SpellCheck�