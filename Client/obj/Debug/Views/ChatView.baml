   M S B A M L   `   `   `  ����G  CKChat Client, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:KChatClient.ViewsKChatClient.Views  5      3clr-namespace:KChatClient.DataKChatClient.Data  5      5clr-namespace:KChatClient.UtilsKChatClient.Utils  5	      U QMaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null{Sclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf#MaterialDesignThemes.Wpf.Converters 5           KChatClient.Views.ChatView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      +localclr-namespace:KChatClient.Views  5      )dataclr-namespace:KChatClient.Data  5      +utilsclr-namespace:KChatClient.Utils  5	      X TMaterialDesignThemes.MahApps, Version=0.0.10.0, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5
      f
ConvertersSclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf   5         MaterialDesignPaper#���   5        � 	Resources  5        SendButtonStyle �� 
TargetType �� Property �� Value  PrimaryHueDarkBrush �� 
TargetType'  !MaterialDesignThemes.Wpf.PackIcon�  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null]]   Kind J� SnapsToDevicePixels �� Triggers P� Property	 P� Value
 �� 
TargetName  PrimaryHueMidBrush  SendFileButtonStyle   ParticipantsListBoxItemStyle �� RelativeSource �� AncestorType ��Name �� Padding ]� 
Conditions �� Property �� Value  ParticipantsDataTemplate  MaterialDesignDivider �� Path � ColumnDefinitions J� OpacityMask !� Visual �� ElementName A�Name  ByteBmpSrcConverter �� 	Converter 	 
BlankImage �� TargetNullValue J� Opacity �� Triggers ^� 
Conditions �� Binding �� Triggers �� Value  �� Binding 
 MessagesDataTemplate! �� CornerRadius" � RowDefinitions# �� StringFormat��%�  &	       &	 �    &	 �    &	 �    7  7 	 &	
 w    �� 5   
   " ��6(   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 ���� 6,   �� 5       � ��# �  6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ Medium"�6+   �� 5       � ��$ 14(�6)   �� 5       � ��$ 1q�60   �� 5       � ��$ Stretch=�6;   �� 5       � ��$ Stretch=�69   �� 5       � ��$ 0q�6(   �� 5       � �� 5      �� 5      " ��6&   .R�5      � ���	#00000000.+�5         $��SendFileIcon��66   $
 Send=�6J    . 
������#��z��5"   6   #��z��5#   6   #��z��5$   6   $
��Auto��6Y   $
��Auto��6f   5%       5&      P� 5'       �@J�IsMouseOver	 . . �5(   "   �� $
 SendFileIcon��6?   # �  6Y    � ��5)      P� 5*       �@J�IsKeyboardFocused	 . . �5+   "   �� $
 SendFileIcon��6?   # �  6Y    � ��5,      P� 5-       � {�	 .  . �5.   "   ��  �@J�Opacity$
 0.23Q�6<   5/      51      53      54      �� 56   
   " ��6,   .�57      ��  � ��
 ���� 6+   �� 58       � ��
 ���� 6,   �� 59       � ��# �  6+   �� 5:       �@�Cursor$
 Hand��6'   �� 5;       � ��$ Medium"�6+   �� 5<       � ��$ 14(�6)   �� 5=       � ��$ 1q�60   �� 5>       � ��$ Stretch=�6;   �� 5?       � ��$ Stretch=�69   �� 5@       � ��$ 0q�6(   �� 5A       � �� 5B      �� 5C      " ��6&   .R�5D      � ���	#00000000.+�5E        $��SendIcon��66   $ Clippy=�6F    . 
������#��z��5G   6   #��z��5H   6   #��z��5I   6   $
��Auto��6Y   $
��Auto��6f   5J       5K      P� 5L       �@J�IsMouseOver	 . . �5M   "   �� $
 SendIcon��6?   # �  6U    � ��5N      P� 5O       �@J�IsKeyboardFocused	 . . �5P   "   �� $
 SendIcon��6?   # �  6U    � ��5Q      P� 5R       � {�	 .  . �5S   "   ��  �@J�Opacity$
 0.23Q�6<   5T      5V      5X      5Y      �� 5\   
   " ��65   .�5]      ��  � ��
 �����6+   �� 5^       � �� 5_      �� *HorizontalContentAlignment+ �� " ���� 5`       � �� 5a      �� *VerticalContentAlignment+ �� " ���� 5b       � ��$ 2,0,0,0q�6(   �� 5c       � ��$ 0q�6'   �� 5d       � �� 5e      �� 5f      " ��6&   .R�5g      �� $ Bd��6!   #��z��6-   #��z��6Y   #��z��5h   !   # z��6K    . ._�5i      �� #��z��6/   # z 5j   /   #��z��5k   /   5l       5m      P� 5n       �@��
IsSelected	 . . �5o   "   �� $
 Bd��6?    � ��
 �����6O   �� 5p   "   # � .�5q   )    � ��5r      ]� 5s       5t   "   �� 5u   &    �@��
IsSelected . 6F   �� 5v   &    �@��IsSelectionActive .  6V   ./�5x   "   �� $
 Bd��6?    � �� �#E3F2FD6O   �� 5y   "   # � .�5z   )    � ��5{      P� 5|       � {�	 .  . �5}   "   �� # � ��6?    � ��5~      5�      5�      5�      �� 5�   
   .R�5�      �� $��0,0,0,1q�6   #���  6/   $��50��5�      ��5�      �� $ ActualWidth� �� *FindAncestor+$ ListBoxItemL�._�5�      � $	��5,0q�6    5�      �� 5�      $��50 �6+   �� 5�      �� 5�      $��15 �6+   �� 5�      $��25 �6+   .+�5�      � $��6q�6    .  5�      !� 5�       6*   �� $ ClipEllipse��.+�5�      A� $ ClipEllipse��6"   
�������67     5�      $ AccountCircle=�62    . $
��Auto��6b   $
��Auto��6o   $��-4q�5�   2   $��Stretch=�6>   $��Stretch=�6\   �� 5�      $��UniformToFill=�5�   K   ��5�       �� *Photo+8   8  5�   K   5�      �� 5�      $��1��6    $��Center=�60   $
��Left=�6K   $	��5,0q�5�       $��SemiBold"�6-   $��CharacterEllipsis=�6C    . ��5�       �� *Name+66     5�      $��NewMessageIcon��6.   $��2��6D    . $��Center=�5�   .   $��Center=�6I   $ MessageReplyText=�5�   .   $	 0.5Q�6F   $y�Hidden=�6T   A� 5�      $��3��6   $��Center=�6.   $��Center=�6I   $��8��5�      $��8��6(   ��5�      �� 5�      " A�6$   .�5�   "   ��  � �� �#F4433669    5�   "   ^� 5�   &    5�   *   �� 5�   .    68   �� *DataContext.IsConnected+ �� *FindAncestor+$ UserControlL� True5�   z   �� 5�   .    68   �� *
IsLoggedIn+ True6W   0�5�   *   �� 5�   .    � �� �#64DD176E   5�   '   5�      5�      5�      5�       5�      �� 5�      $
 True��6D     6   �� *HasSentNewMessage+.a�5�      �� $
 NewMessageIcon��6    � y�$ Visible=�6O   5�      5�      �� 5�   
   .R�5�      �� $��MessageBorder��6   $��40��6*   $��1q�69   
�������$��
10,0,60,10q�5�      ���#BCAAA4$! 2��6?    . ._�5�      � " 5�      �� 5�      �� 5�      $��15 �6(   .+�5�      �� $��Stretch=�6    $��Stretch=�6<   $��7,5,7,0q�6Z   $
��Wrap=�5�       ��64   �� *	Message+�� 5�      $��1��6    $��Right=�6-   $��Stretch=�6I   $��0,0,5,0q�5�       $��10(�61   $	 0.8Q�6?   ��5�       �� *Time+$# {0:t}��5�      5�       5�      �� 5�      $
 True��6A     6   �� *IsOriginNative+.a�5�      �� $
 MessageBorder��6    � ��$ 
60,0,10,10q�6J   �� 5�      $
 MessageBorder��6    � �� �#7e80f76N   �� 5�      $
 MessageBorder��6    � ��
 �����6O   5�      5�      5�      .��5�      � 
�������" 5�   
   �� 5�      �� 5�      $
��Auto �6    5�   
   �� 5�      $	��220 �6   �� 5�      .+�5�   
   �� $��2��6   $��0,0,1,0q�6"    . 
�������._�5�      �� #���  5�      #���  5�      $��Disabled=�5�      $
��Auto=�5�      
���������5�      �� *Participants+$ �� SelectedItem$ 5�      �� *SelectedParticipant+% � ContextMenu% 5�      �� 5�      .@�5�      m� -   5        & m�Name$& contxMenuSetTask��5�   5   $��Add Task��6#   6O   m� 5�      $��
View Tasks��6#   ' m� Command$'  ��67   6D   m� 5�      $��	Send File��6#   ' 66   �� *SendFileCommand+6\   5�      5�      5�      �� 5�   
   -   5        ( ��Name$( MessagesItemsCtrl��5�      $��1��62   $��0,5,0,0q�6B   #��� 
 5�      $
��Auto=�5�      ��5�      �� *SelectedParticipant.Chatter+.  (System.Windows.Interactivity.Interaction)   	Behaviors) 5�      4   .KChatClient.Utils.BringNewItemIntoViewBehavior  5�      ��5�      �� 5      " ��6"   .R�5     �� .��5     �� 5     5     5     �� 5  
   $��1��6   $��1��6    . 
�������._�5
     � 
������� 5     �� 5     �� 5     $��50 �6'   .+�5     �� -   5        * ��Name$* MessageTxtBox��5     $��10,10,25,10q�61   $��80��6F   $��1q�5     
���   �
�������
���   �+ �� TextWrapping$
+ Wrap=�5     , �� AcceptsReturn, . - �� 
CaretBrush
- �   �.  (MaterialDesignThemes.Wpf.TextFieldAssist.   DecorationVisibility$. Hidden=�5     "/ �� VerticalScrollBarVisibility$
/ Auto=�5     ��5     �� *	Message+0 �� UpdateSourceTrigger$0 PropertyChanged=��� 5     -   5        1 ��Name$1 
SendButton��5     $��360,10,40,10q�6-   #���  5     $��2��5  @   ��6   �� *SendMessageCommand+6@   �� 5     -   5        $1 FileSendButton��5     $��
15,10,5,10q�61   #���  5     $��1��5  =   ��6   �� *SendFileCommand+6=   5     5     � 5   
   -   5        2 �Name$2 TaskInputBox��5      $y�	Collapsed=�6%   .+�5!     � 
���   �$	 0.5Q�6&   �� 5"     $	��250��5#     
�������
���   �$��1q�5&     $! 0,0,0,0��5'     $��Center=�5(     $��Center=�5)     $��291,122,-321,136q�6(   ._�5*     �� .+�5+     �� $��5q�6    $��Task Description:��6+   $
��Bold"�6D   $��Cambria)�6V   
�������6k   �� 5,     -   5        $* InputTextBox��5,  �   $	��150��6   $��Center=�6-   $��Center=�6J   3 � Cursor$3 Arrow��6e   
- �����
���������6�   �� *TaskDescription+$0 PropertyChanged=��� 5-     4 �� Orientation$4 
Horizontal=�6!   $��Center=�6:   .+�5.     �� -   5        $1 	YesButton��5.  !   $��5q�64   $	��Yes��6?   
��������6|   �� *SetNewTaskCommand+�� 5/     -	   5        $1 NoButton��5/  !   $��5q�63   $��No��6>   
������6c   50     51     52     53     55     56     