   M S B A M L   `   `   `  ����H  DHMVScaffolder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:HMVScaffolder.MvcHMVScaffolder.Mvc  5      2    ,HMVScaffolder.Mvc.DependencyScaffolderDialog   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      .uiclr-namespace:HMVScaffolder.Mvc     5        � Title$  Add MVC Dependencies��5       � WindowStartupLocation$ CenterOwner=�5       � 
ResizeMode$ 	CanResize=�5       � SizeToContent$ Height=�5      $	��600��5	      .   (HMVScaffolder.Mvc.ValidatingDialogWindow   
DialogHost 5
      �� *
DialogHost+ �� Mode$ OneWayToSource=���5      �� *DialogWidth+$ TwoWay=� � 	Resources 5       �� Source�� 5   
   $ StyleResources.xaml?�6   %    .��5      �� .+�5   
   �    
PanelStyle#��[  6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��0��6   .+�5      	� -   5        $��minimalButton��5      $��0 17 0 3q�5      $
��Bold"�5      	 	� 	GroupName$	 Configuration��5      $?��9ScaffoldMvcDependenciesMinimal_Name=_Minimal dependencies��5      
 d� 	IsChecked
 5      �� *IsMinimalSelected+�� 5      $��20 0 0 0q�6   .��5      �� $J��DAdds the minimal packages and references to start using ASP.NET MVC.��6    $
��Wrap=�6l   5      5      �� 5       $��1��6   .+�5!      	� -   5        $��
fullButton��5!      $��0 17 0 3q�5"      $
��Bold"�5#      $	 Configuration��5$      $��_Full dependencies��5%      
 5&      �� *IsFullSelected+�� 5'      $��20 0 0 0q�6   .��5(      �� $����Adds packages and configuration, as well as a default layout, error page, script libraries, and script bundling to your application.��6    $
��Wrap=�6�   5)      5*      5+      �� 5,   
     AddCancelStackPanel#��[ 6   .+�5-      .   (HMVScaffolder.Mvc.AddCancelButtonControl  .   (HMVScaffolder.Mvc.AddCancelButtonControl   AddButtonClick$ AddButton_Click��6(   5.      5/      50      