Module MainModule
    Public File As String = (Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Autosoft\Auto-Helper\Decimals.txt")
    Public Directory As String = (Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Autosoft\Auto-Helper")
	Public CurrentUser As String
    Public Grade As Decimal
    Public MultiplicationDivision As Boolean = False
End Module
