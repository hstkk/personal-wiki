   M S B A M L   `   `   `  ����   PersonalWiki)    #PersonalWiki.View.FindReplaceDialog   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Find and replace��$	��220��$	��300�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$ Search��  -   $ Replace��  -   $ Close��.���� $��5q�.+��� .@��� $
��Find��.����  +� FocusedElement ��  �� ElementName$ search��.+��� $��
Find what:���� -   $��search���� -   $��
searchCase��$��
Match case��� .+��� $��	Find next��$��0,5,5,5q�$��Search�� �� 	IsDefault . �� $��Close��$��5,5,0,5q�$��Close�� �� IsCancel . �� $��Replace��.����  �� $
 find��.+��� $��
Find what:���� -   $
��find���� $��Replace with:���� -   $��replace���� -   $��replaceCase��$��
Match case��� .+��� $��Replace all��$��0,5,0,5q�$��Replace�� . �� $��Close��$��5,5,0,5q�$��Close�� . 