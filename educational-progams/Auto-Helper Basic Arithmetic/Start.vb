Public Class Start

	Private Sub AdditionSubtractionQuiz_Click(sender As Object, e As EventArgs) Handles AdditionSubtractionQuiz.Click
		MultiplicationDivision = False
		Dim Input As String = InputBox("Please enter your name.", "Enter Name (Auto-Helper)")
		If Input <> "" Then
			CurrentUser = Input
			Exercises.Show()
			Me.Close()
		End If

	End Sub

	Private Sub MultiplicationDivisionQuiz_Click(sender As Object, e As EventArgs) Handles MultiplicationDivisionQuiz.Click
		MultiplicationDivision = True
		Dim Input = InputBox("Please enter your name.", "Enter Name (Auto-Helper)")
		If Input <> "" Then
			CurrentUser = Input
			Exercises.Show()
			Me.Close()
		End If
	End Sub

	Private Sub Start_Click(sender As Object, e As EventArgs) Handles Me.Click
		UserName.Clear()
		Password.Clear()
		UserName.ClearUndo()
		Password.ClearUndo()
		Panel2.Hide()
		LinkLabel1.Visible = True
	End Sub

	Private Sub Start_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		If My.Settings.AdditionSubtraction = "None" Then
			AdditionSubtractionQuiz.Visible = False
			Dim NewLocation As Point
			NewLocation = MultiplicationDivisionQuiz.Location
			NewLocation.Y -= 56
			MultiplicationDivisionQuiz.Location = NewLocation
			NewLocation = AdditionSubtractionHelp.Location
			NewLocation.Y -= 56
			AdditionSubtractionHelp.Location = NewLocation
			NewLocation = MultiplicationDivisionHelp.Location
			NewLocation.Y -= 56
			MultiplicationDivisionHelp.Location = NewLocation
		End If
		If My.Settings.MultiplicationDivision = "None" Then
			MultiplicationDivisionQuiz.Visible = False
			Dim NewLocation As Point
			NewLocation = AdditionSubtractionHelp.Location
			NewLocation.Y -= 56
			AdditionSubtractionHelp.Location = NewLocation
			NewLocation = MultiplicationDivisionHelp.Location
			NewLocation.Y -= 56
			MultiplicationDivisionHelp.Location = NewLocation
		End If
		If FileIO.FileSystem.FileExists(Directory) = False Then System.IO.Directory.CreateDirectory(Directory)
		If FileIO.FileSystem.FileExists(File) = False Then System.IO.File.Create(File)

	End Sub

	Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
		Panel2.Show()
		LinkLabel1.Hide()
	End Sub

	Private Sub LinkLabel1_MouseHover(sender As Object, e As EventArgs) Handles LinkLabel1.MouseHover
		Panel2.Show()
		LinkLabel1.Hide()
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		UserName.Clear()
		Password.Clear()
		UserName.ClearUndo()
		Password.ClearUndo()
		Panel2.Hide()
		LinkLabel1.Visible = True
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Login()
	End Sub

	Private Sub Login()
		If UserName.Text = "" Then
			Password.Clear()
			Password.ClearUndo()
			UserName.Select()
			MessageBox.Show("No User-Name was entered. Please enter a valid User-Name.", "Please enter User-Name (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		ElseIf Password.Text = "" Then
			Password.Select()
			MessageBox.Show("No Password was entered. Please enter a valid Password.", "Please enter Password (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Else
			If UserName.Text = My.Settings.UserName And Password.Text = My.Settings.Password Then
				Account.Show()
				Me.Close()
				UserName.Clear()
				Password.Clear()
				UserName.ClearUndo()
				Password.ClearUndo()
				Panel2.Hide()
				LinkLabel1.Visible = True
			Else
				UserName.Clear()
				Password.Clear()
				UserName.ClearUndo()
				Password.ClearUndo()
				UserName.Select()
				MessageBox.Show("An incorrect user-name or password or password has been entered. Please enter a valid user-name and password.", "Incorrect User-Name or Password (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			End If
		End If
	End Sub

	Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles Password.KeyDown
		If e.KeyCode = Keys.Enter Then Login()
	End Sub

	Private Sub Panel2_VisibleChanged(sender As Object, e As EventArgs) Handles Panel2.VisibleChanged
		If Panel2.Visible = True Then UserName.Select()
	End Sub

	Private Sub AdditionSubtractionHelp_Click(sender As Object, e As EventArgs) Handles AdditionSubtractionHelp.Click
		Help.Show()
		MultiplicationDivision = False
		Me.Close()
	End Sub

	Private Sub MultiplicationDivisionHelp_Click(sender As Object, e As EventArgs) Handles MultiplicationDivisionHelp.Click
		Help.Show()
		MultiplicationDivision = True
		Me.Close()
	End Sub

	Private Sub Start_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
		Dim Mypen As New Pen(Brushes.DodgerBlue, 3)
		e.Graphics.DrawLine(Mypen, 0, 330, Me.Width, 330)
		Mypen.Dispose()
		e.Dispose()
	End Sub

End Class
