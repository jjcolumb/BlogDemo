;#DropDB BlogDemoEasyTest

#Application BlogDemoWin
#Application BlogDemoWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef BlogDemoWin

*Action About...
 
*Action OK

#EndIf

#IfDef BlogDemoWeb
*Action Navigation(My Details)
#EndIf
