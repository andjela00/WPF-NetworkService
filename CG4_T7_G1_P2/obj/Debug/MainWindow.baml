   M S B A M L   `   `   `  ????G  CCG4_T7_G1_P2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CG4_T7_G1_P2CG4_T7_G1_P2  5   	   ?$clr-namespace:CG4_T7_G1_P2.ViewModelCG4_T7_G1_P2.ViewModel  5   	   7 clr-namespace:CG4_T7_G1_P2.ViewsCG4_T7_G1_P2.Views  5	   	       CG4_T7_G1_P2.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:CG4_T7_G1_P2     5   	   :	viewModel$clr-namespace:CG4_T7_G1_P2.ViewModel     5   	   2views clr-namespace:CG4_T7_G1_P2.Views     5	   	     ? Title$  
MainWindow??5   	   $	??600??6   $	??917??6)    ? 
ResizeMode$ NoResize=?65    ? DataContext 5      0  *CG4_T7_G1_P2.ViewModel.MainWindowViewModel  5   
   6*    ? 	Resources 5       ?? DataType1   +CG4_T7_G1_P2.ViewModel.NetworkViewViewModel(   "CG4_T7_G1_P2.Views.NetworkViewView1   +CG4_T7_G1_P2.ViewModel.NetworkDataViewModel(   "CG4_T7_G1_P2.Views.NetworkDataView/   )CG4_T7_G1_P2.ViewModel.DataChartViewModel&    CG4_T7_G1_P2.Views.DataChartView??%?   (??       5      *, +)(?? 2     5      *, +)(?? d     5      *, +)?? 5   
   "  6   .R?5        5      ?? 5   
   "  6   .R?5        5      ?? 5   
   "  6   .R?5        5      5      .??5$      ? .+?5%   
   ? $??62??6   $	??Top=?6   
???OO/?$
??10,0q?6N   .+?5&      ?? $??monito??6   $??Center=?6&   $	??Top=?6C   $??0 10 0 0q?6[   $??22(?6m   
???????$
??Bold"?6?   5'      ]? 5(   
   $??0??6   
???OO/?$??60??6@   $	??Top=?6L   $??	20,2,20,0q?6d   .+?5)      o? $	??Top=?6   $	??Top=?6(   $??Center=?6@   $??0 0 0 0q?6]   
???OO/?.@?5*      m? $??0 5 20 0q?6   
???????$?? Network Data??6?   $??50??6V   $	??260??6b    m? CommandParameter$ NetworkData??6?   $??14(?6?   
???????$
??Bold"?6?    m? Command 6n   ?? *
NavCommand+6?   m? 5+      $??0 5 0 0q?6   
???????$?? Network View??6?   $??50??6V   $	??260??6b   $ NetworkView??6?   $??14(?6?   
???????$
??Bold"?6?    6n   ?? *
NavCommand+6?   m? 5-      $??20 5 0 0q?6   
???????$?? Data Chart??6?   $??50??6T   $	??260??6`   $ 	DataChart??6?   $??14(?6?   
???????$
??Bold"?6?    6l   ?? *
NavCommand+6?   5.      5/      ? 50   
   $??0,2,0,0q?6   .+?51      ?? $??
10,60,10,0q?6B   ??6   ?? *CurrentViewModel+6B   52      53      55      