   M S B A M L   `   `   `  ����F  BPatientCare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PatientCare.UtilityClassPatientCare.UtilityClass  5   	        PatientCare.View.LoginView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   <	commonWPF&clr-namespace:PatientCare.UtilityClass     5   	     � Title$  
User Login��5   	   $	��250��6   $	��400��6)    � WindowStartupLocation$ CenterScreen=�65    � WindowStyle$ 
ToolWindow=�6Z    � 
ResizeMode$ NoResize=�6s    � WindowState$ Normal=�6�   .��5      � $��10q�6   $
��auto��6    � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5	      $��35 �6   6*   �� 5
      $��35 �6   6*   �� 5      $��35 �6   6*   �� 5      $��30 �6   6*   .+�5   
   ]� $��30��6   $��1��6    $��2q�6-   .+�5      �� $	��130��6    �� TextAlignment$
 Left=�6$   	User Name6D   �� 5      -   5        $��txtUserName��5      $	��200��6)   $
��Left=�65   ��6P   �� *
UserName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   5      ]� 5   
   $��30��6   $��2��6    $��2q�6-   .+�5      �� $	��130��6   $
 Left=�6$   User Password6H   G� 5      -   5        $��txtPassword��5      	 G� PasswordChar$	 *��6.   $	��200��6@   $
��Left=�6L   -   'PatientCare.UtilityClass.PasswordHelper
   Attach
 .    Password 5   
   �� *
Password+$ TwoWay=�$ PropertyChanged=�6u   5      ]� 5   
   $
��auto��6   $��3��6"   $��Center=�6/   $
��left=�6J   $��	130,0,0,0q�6e   .+�5      �� -   5        $��btnLogin��5      $��OK��6%   $��75��62   $��28��6=   ��6J   �� *LoginCommand+6m   �� 5      -   5        $��btnClose��5      $��Close��6%   $��75��65   $��28��6@   ��6L   �� *CancelLoginCommand+6u   5      ]� 5   
   $��35��6   $��4��6    .+�5      �� 
���  �� �� Padding$ 5q�6*   $��Normal$�66   $��15(�6J   ��6X   �� *
LoginError+6v   5      5       5!      