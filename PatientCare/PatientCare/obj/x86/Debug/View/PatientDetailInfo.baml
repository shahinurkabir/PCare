   M S B A M L   `   `   `  ����F  BPatientCare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PatientCare.UtilityClassPatientCare.UtilityClass  5      (    "PatientCare.View.PatientDetailInfo   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <	commonWPF&clr-namespace:PatientCare.UtilityClass     5      $
��auto��5      $
��auto��6     � 	Resources  5	         ErrorTemplate �� Padding �� CornerRadius��%~   &	        �� 5
   
   .R�5      �� 
���  ��$��1q�6'   $ 2q�6;   $ 2��6G   ._�5      �� 5      5      5      .��5      � $
��auto��6   $
��auto��6    � RowDefinitions 5   
   �� 5      $��28 �6   �� 5      $��28 �6   �� 5      $��28 �6   �� 5      $��28 �6   �� 5      $��28 �6   �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   6-   �� 5      $	��180 �6   6-   .+�5   
   �� $��First Name ��6   $
��Bold"�6'   $
��Left=�6:   $��0��6U   $��0��6c   �� 5    
   -   5        $��txtFirstName��5       $	��180��6&   $
��Left=�62   $��1��6M    =� ErrorTemplate# [  6  ��6^   ��  �� Path$ PatientInfo.FirstName� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�	 �� ValidatesOnExceptions	 . 
 �� NotifyOnValidationError
 .  �� ValidatesOnDataErrors . 6  �� 5!   
   $��
Last Name ��6   $
��Bold"�6'   $
��Left=�69   $��1��6T   $��0��6b   �� 5"   
   -   5        $��txtLastName��5"      $	��180��6%   $
��Left=�61   $��1��6L   $��1��6Z   # [  6  ��6j   �� $ PatientInfo.LastName�$ TwoWay=�$ PropertyChanged=�	 . 
 .  . 6  �� 5#   
   $
��SSN ��6   $
��Bold"�6    $
��Left=�62   $��2��6M   $��0��6[   �� 5$   
   -   5        $��txtSSN��5$       �� 	MaxLength$ 9��6    2   ,PatientCare.UtilityClass.TextBoxMaskBehavior   Mask$ Integer=�6/   $	��180��6\   $
��Left=�6h   $��2��6�   $��1��6�   # [  6M  ��6�   �� $ PatientInfo.SSN�$ TwoWay=�$ PropertyChanged=�	 . 
 .  . 6M  �� 5%   
   $��Date Of Birth ��6   $
��Bold"�6*   $
��Left=�6<   $��3��6W   $��0��6e   (  "System.Windows.Controls.DatePicker  5&   
   -   5        $��dpkDateOfBirth��5&      $	��180��6+   $
��Left=�68   $��3��6S   $��1��6a   # [  6     Text 6q   �� $ PatientInfo.DOB�$ TwoWay=�$ PropertyChanged=�	 . 
 .  . 6  �� 5(   
   $��Doctor ��6   $
��Bold"�6#   $
��Left=�65   $��4��6P   $��0��6^   �� 5)   
   $��4��6   $��1��6"    �� Orientation$ 
Horizontal=�63   .+�5*      �� -   5        $��txtDoctorRef��5*      $	��130��6*   $
��Left=�66   # [  6�    �� 
IsReadOnly . ��6Q   �� $ PatientInfo.DoctorName�	 . 
 .  . 6  �� 5+      -   5        $��btnRefDoctor��5+   #   $	��...��6   $��50��67   ��6B   �� $# PatientInfo.ShowDoctorCommand�5,      ]� 5.   
   $��5��6   $��30��6!   $	��Top=�6-   $��2��6F   $
��Left=�6Z   $��
120,10,0,0q�6v   .+�5/      �� -   5        $��btnSavePatient��5/      $
��Save��6+   $��75��6:   $��0��6�   ��6E   �� $* $PatientInfo.CommandSavePatientDetail�6�   �� 50      -   5        $��btnSaveAndNext��50      $
��Next��6+   $��75��6:   $��1��6�   ��6E   �� $* $PatientInfo.CommandSavePatientDetail�6�   51      52      53      