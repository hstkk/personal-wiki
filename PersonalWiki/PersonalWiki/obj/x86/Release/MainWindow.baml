   M S B A M L   `   `   `  ���� `  \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35tNclr-namespace:Microsoft.Windows.Controls.Ribbon;assembly=RibbonControlsLibrary!Microsoft.Windows.Controls.Ribbon  G CPersonalWiki, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PersonalWikiPersonalWiki    PersonalWiki.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     aribbonNclr-namespace:Microsoft.Windows.Controls.Ribbon;assembly=RibbonControlsLibrary     ,localclr-namespace:PersonalWiki     -   4   .Microsoft.Windows.Controls.Ribbon.RibbonWindow   Name$  RibbonWindow�� � Title$ Personal wiki��$	��640��$	��480�� � Icon$' !tango-accessories-text-editor.ico�� � 	Resources  �� Source�� $ MainWindowResources.xaml?�%     J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$
 Help��  -   $ local:Commands.ShowPage��  -   $ local:Commands.NewPage��  -   $ local:Commands.NewProject��  -   $ local:Commands.CloseTab��  -   $ Delete��  -   $" local:Commands.DeleteProject��  -	   $
 Find��  -
   $ local:Commands.ImportPage��.��]� .+�.   (Microsoft.Windows.Controls.Ribbon.Ribbon  -     Name$ Ribbon��$	��Top=�   ApplicationMenu =   7Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenu  8   2Microsoft.Windows.Controls.Ribbon.RibbonMenuButton	   SmallImageSource$.	 (Images\16x16\accessories-text-editor.png��.@�A   ;Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��Import��6   0Microsoft.Windows.Controls.Ribbon.RibbonMenuItem
   ImageSource$$
 Images\32x32\document-open.png�� m� Command$ local:Commands.ImportPage��  $��Export as...��$'
 !Images\32x32\document-save-as.png��.@�  $	��txt��$$
 Images\32x32\document-save.png��$ local:Commands.ExportTxt��  $	��txt��$$
 Images\32x32\document-save.png��$ local:Commands.ExportHtml��7   1Microsoft.Windows.Controls.Ribbon.RibbonSeparator    $��Print��$%
 Images\32x32\document-print.png��$ Print��    $��About PersonalWiki��$#
 Images\32x32\help-browser.png��$
 Help��    $
��Quit��$#
 Images\32x32\process-stop.png��$ Close��.@�1	   +Microsoft.Windows.Controls.Ribbon.RibbonTab	  -    	 Name$ HomeTab��$
��Home��.@�3
   -Microsoft.Windows.Controls.Ribbon.RibbonGroup
  -    
 Name$	 New��$	��New��.@�4   .Microsoft.Windows.Controls.Ribbon.RibbonButton     SmallImageSource$# Images\16x16\document-new.png��   Label$
 Page��$��local:Commands.NewPage��  $! Images\16x16\folder-new.png��$ Project��$��local:Commands.NewProject��
  -   $ 	Clipboard��$��	Clipboard��.@�     LargeImageSource$! Images\32x32\edit-paste.png��$ Paste��$��Paste��  $ Images\16x16\edit-cut.png��$	 Cut��$	��Cut��  $  Images\16x16\edit-copy.png��$
 Copy��$
��Copy��   ToolTipTitle$ Copy (Ctrl+C)��   ToolTipDescription$6 0Copies the selected content on to the clipboard.��
  -   $
 Text��$
��Text��.@�� .+��� -   $
��font��$��0,0,5,0q� �� SelectedValuePath$ Source������ �� -   $
��size������ 6   0Microsoft.Windows.Controls.Ribbon.RibbonCheckBox  -   $
��wrap��   Label$
 Wrap�� d� 	IsChecked ��    settings �� Source# [   �� Path$ Default.TextWrap�  -   $��
spellcheck��$ 
Spellcheck�� �� # [  $ Default.SpellCheck�
  -   $ Editing��$��Editing��.@�  $  Images\16x16\edit-find.png��$
 Find��$
��Find��  $( "Images\16x16\edit-find-replace.png��$ Replace��$��Replace��  $&  Images\16x16\edit-select-all.png��$ 
Select all��$��	SelectAll��� $��Bottom=� � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+��� $
��Auto=�$
��Auto=�.��R� -   $��ProjectsTreeView��$��0,0,9,0q�$��0������ ��Z�   �� DataType   PersonalWiki.ProjectResult �� ItemsSource � ContextMenu �� 	StaysOpen m� CommandParameter  PersonalWiki.PageResult �� Command �� CommandParameter  � ContextMenu��%o  (��       *, +)(�� �     *, +)�� "   �� *Pages+.R��� ���� $ Title� ��  . .@�m� $��Remove project��$" local:Commands.DeleteProject�� �� $ Id��� "  .R��� �� $ local:Commands.ShowPage�� �� $ Id�.�.��� ���� $ Title�  ��  . .@�m� $��Remove page��$ Delete�� �� $ Id�.��� $��1��
����ح��� -   $��
tabControl��$��5q�  CloseTabItem#���  $��2��