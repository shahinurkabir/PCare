   M S B A M L   `   `   `  ����  PatientCare    PatientCare.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��700��6   $	��800��6)    � WindowState$ Normal=�65    � WindowStartupLocation$ CenterScreen=�6J    � WindowStyle$ SingleBorderWindow=�6o    � 
ResizeMode$ 	CanResize=�6�    � Topmost .  .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $
��300* �6   .+�5
   
   o� $��0q�6   $��0��6   .@�5      m� $
��File��6   .@�5      m� -   5        $��mnuAddNewPatient��5   (   $	��New��6    m� Command 6@   ��  �� Path$ AddNewPatientCommand�6q   m� 5      -   5        $��mnuShowPatient��5   )   $
��Open��6    6?   �� $ ShowPatientsCommand�6n   m� 5      -   5        $��mnuClosePatient��5   *   $��Close��6    6A   �� $ ClosePatientCommand�6p   m� 5      $
��Exit��6    6)   �� $ ApplicationExitCommand�6[   5      m� 5      $��Options��6   .@�5      m� .@�5      m� $��Green��6   
	 � Tag$	 Green��6.   
 m� CommandParameter$
 Green��6a    6:   �� *ThemeChangeCommand+6}   m� 5      $	��Red��6   $		 Red��6,   $	
 Red��6]    66   �� *ThemeChangeCommand+6v   m� 5      $
��Blue��6   $
	 Blue��6-   $

 Blue��6_    68   �� *ThemeChangeCommand+6y   m� 5      $��White��6   $	 White��6.   $
 White��6a    6:   �� *ThemeChangeCommand+6|   5      5      5      ]� 5   
   -   5         ]�Name$ 	mainPanel��5      $��1��6(   $	��Top=�65   $��1��6M   $��Stretch=�6^   $��5q�6|   $
��auto��6�   5      �� 5   
   $��0,0,0,0q�6   $��Bottom=�6%   $��10��6@   5      5      