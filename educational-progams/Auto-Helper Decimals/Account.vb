Public Class Account

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Settings.ShowDialog()
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Start.Show()
		Me.Close()
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		Try
			ListBox1.Items.Clear()
			Dim Stream As New System.IO.StreamReader(File)
			Do While Stream.Peek() >= 0
				ListBox1.Items.Add(Stream.ReadLine())
			Loop
			Stream.Close()
			ListBox1.Visible = True
		Catch ex As Exception
			My.Computer.FileSystem.WriteAllText(File, DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & "ACCOUNT ERROR: Viewed Data" & " " & " " & ex.Message & vbNewLine, True)
		End Try
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		If MessageBox.Show("The log will be deleted permanently. Are you sure you would like to continue?", "Delete Log?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
			Try
				My.Computer.FileSystem.WriteAllText(File, "", False)
				ListBox1.Items.Clear()
			Catch ex As Exception
				My.Computer.FileSystem.WriteAllText(File, DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & "ACCOUNT ERROR: Cleared Data" & " " & ex.Message & vbNewLine, True)
			End Try
		End If
	End Sub

	Private Sub Account_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		End Sub
End Class