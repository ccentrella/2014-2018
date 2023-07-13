Module MainModule
	Public File As String = (Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Autosoft\Auto-Helper\Basic Arithmetic.txt")
    Public Directory As String = (Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Autosoft\Auto-Helper")
	Public CurrentUser As String
	Public Grade As Decimal
	Public REDO As Boolean = False
    Public MultiplicationDivision As Boolean = False
End Module
