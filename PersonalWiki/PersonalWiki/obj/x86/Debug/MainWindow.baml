   M S B A M L   `   `   `  ����`  \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35tNclr-namespace:Microsoft.Windows.Controls.Ribbon;assembly=RibbonControlsLibrary!Microsoft.Windows.Controls.Ribbon  5   	   G CPersonalWiki, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PersonalWikiPersonalWiki 5   	      PersonalWiki.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   aribbonNclr-namespace:Microsoft.Windows.Controls.Ribbon;assembly=RibbonControlsLibrary     5   	   ,localclr-namespace:PersonalWiki     5   	   -   5        4   .Microsoft.Windows.Controls.Ribbon.RibbonWindow   Name$  RibbonWindow��5       � Title$ Personal wiki��5   	   $	��640��5      $	��480��6    � Icon$' !tango-accessories-text-editor.ico��5	   	    � 	Resources 5       �� Source�� 5   
   $ MainWindowResources.xaml?�6   %     J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$
 Help��5      6(     5   
   -   5        $ local:Commands.ShowPage��5      6R     5   
   -   5        $ local:Commands.NewPage��5      6W     5   
   -   5        $ local:Commands.NewProject��5      6=     5   
   -   5        $ local:Commands.CloseTab��5      6;     5   
   -   5        $ Delete��5      6H     5   
   -   5        $" local:Commands.DeleteProject��5      6a     5   
   -	   5        $
 Find��5      6@     5   
   -
   5        $ local:Commands.ImportPage��5      6W   .��5      ]� .+�5   
   .   (Microsoft.Windows.Controls.Ribbon.Ribbon  -   5          Name$ Ribbon��5      $	��Top=�6(      ApplicationMenu 5      =   7Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenu  5      8   2Microsoft.Windows.Controls.Ribbon.RibbonMenuButton	   SmallImageSource$.	 (Images\16x16\accessories-text-editor.png��6/   .@�5$      A   ;Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��Import��67   6   0Microsoft.Windows.Controls.Ribbon.RibbonMenuItem
   ImageSource$$
 Images\32x32\document-open.png��5%   7    m� Command$ local:Commands.ImportPage��5&   7     5'      $��Export as...��67   $'
 !Images\32x32\document-save-as.png��5(   7   .@�5)        $	��txt��6;   $$
 Images\32x32\document-save.png��5*   7   $ local:Commands.ExportTxt��5+   7     5,      $
��html��6;   $$
 Images\32x32\document-save.png��5-   7   $ local:Commands.ExportHtml��5.   7   55      7   1Microsoft.Windows.Controls.Ribbon.RibbonSeparator  56        57      $��Print��67   $%
 Images\32x32\document-print.png��58   7   $ Print��59   7     5:        5;      $��About PersonalWiki��67   $#
 Images\32x32\help-browser.png��5<   7   $
 Help��5=   7     5>        5?      $
��Quit��67   $#
 Images\32x32\process-stop.png��5@   7   $ Close��5A   7   5B      .@�5D      1	   +Microsoft.Windows.Controls.Ribbon.RibbonTab	  -   5         	 Name$ HomeTab��5D      $
��Home��5E      .@�5F      3
   -Microsoft.Windows.Controls.Ribbon.RibbonGroup
  -   5         
 Name$	 New��5F   %   $	��New��5G   %   .@�5H      4   .Microsoft.Windows.Controls.Ribbon.RibbonButton     SmallImageSource$# Images\16x16\document-new.png��6*      Label$
 Page��5I   *   $��local:Commands.NewPage��5J   *     5K      $! Images\16x16\folder-new.png��6*   $ Project��5L   *   $��local:Commands.NewProject��5M   *   5N      
  5O      -   5        $ 	Clipboard��5O   %   $��	Clipboard��5P   %   .@�5Q           LargeImageSource$! Images\32x32\edit-paste.png��6*   $ Paste��5R   *   $��Paste��5S   *     5T      $ Images\16x16\edit-cut.png��6*   $	 Cut��5U   *   $	��Cut��5V   *     5W      $  Images\16x16\edit-copy.png��6*   $
 Copy��5X   *   $
��Copy��5Y   *      ToolTipTitle$ Copy (Ctrl+C)��5Z   *      ToolTipDescription$6 0Copies the selected content on to the clipboard.��5[   *   5\      
  5]      -   5        $
 Text��5]   %   $
��Text��5^   %   .@�5_      � .+�5`      �� -   5        $
��font��5`   #   $��0,0,5,0q�5c   #    �� SelectedValuePath$ Source��5d   #   ��5b   #   �� 5d   #   �� 5e      -   5        $
��size��5e   #   ��5g   #   �� 5h      6   0Microsoft.Windows.Controls.Ribbon.RibbonCheckBox  5i      -   5        $
��wrap��5i   ,      Label$
 Wrap��5j   ,    d� 	IsChecked 5k   ,   ��    settings �� Source# [   �� Path$ Default.TextWrap�5l   ,     5m      -   5        $��
spellcheck��5n   ,   $ 
Spellcheck��5m   ,    5o   ,   �� # [  $ Default.SpellCheck�5p   ,   5u      
  5v      -   5        $ Editing��5v   %   $��Editing��66   .@�5w        $  Images\16x16\edit-find.png��6*   $
 Find��5x   *   $
��Find��5y   *     5z      $( "Images\16x16\edit-find-replace.png��6*   $ Replace��5{   *   $��Replace��5|   *     5}      $&  Images\16x16\edit-select-all.png��6*   $ 
Select all��5~   *   $��	SelectAll��5   *   5�      5�      5�      � 5�   
   $��Bottom=�6    � ColumnDefinitions 5�      �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $��* �6#   .+�5�      �� $
��Auto=�6   $
��Auto=�5�      .��5�      R� -   5        $��ProjectsTreeView��5�      $��0,0,9,0q�5�      $��0��5�      ��5�      �� ��5�      Z�  5�       �� DataType   PersonalWiki.ProjectResult �� ItemsSource � ContextMenu �� 	StaysOpen m� CommandParameter  PersonalWiki.PageResult �� Command �� CommandParameter  � ContextMenu��%{  (��       5�   3   *, +)(��     5�   '   *, +)�� 5�      "  63    6[   �� *Pages+.R�5�      �� ��6(   �� $ Title� 5�   "   �� 5�   &    . .@�5�   *   m� $��Remove project��63   $" local:Commands.DeleteProject��6K    6r   �� $ Id�5�   '   5�      5�      �� 5�      "  6'   .R�5�      �� �� 5�   "   $ local:Commands.ShowPage��6,    6N   �� $ Id�.�6s   .�5�   &   �� ��60   �� $ Title�  5�   &   �� 5�   *    . .@�5�   .   m� $��Remove page��67   $ Delete��6L    6]   �� $ Id�5�   +   .�5�   =   5�   #   5�      5�      5�      5�      5�      �� 5�      $��1��6   
����ح�5�      �� 5�      -   5        $��
tabControl��5�      $��5q�5�        CloseTabItem#���  5�      $��2��5�      5�      5�      5�      5�      