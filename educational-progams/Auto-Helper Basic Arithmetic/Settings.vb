Public Class Settings

	Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		FormLoad()
	End Sub

	Private Sub FormLoad()
		Try
			If (My.Settings.AdditionSubtraction = "Both") Then
				AdditionSubtractionBoth.Checked = True
			ElseIf (My.Settings.AdditionSubtraction = "Addition") Then
				AdditionOnly.Checked = True
			ElseIf (My.Settings.AdditionSubtraction = "Subtraction") Then
				SubtractionOnly.Checked = True
			Else
				AdditionSubtractionNone.Checked = True
			End If
			If (My.Settings.MultiplicationDivision = "Both") Then
				MultiplicationDivisionBoth.Checked = True
			ElseIf (My.Settings.MultiplicationDivision = "Multiplication") Then
				MultiplicationOnly.Checked = True
			ElseIf (My.Settings.MultiplicationDivision = "Division") Then
				DivisionOnly.Checked = True
			Else
				MultiplicationDivisionNone.Checked = True
			End If
			If My.Settings.AutomaticComplete Then
				CheckBox1.Checked = True
			Else
				CheckBox1.Checked = False
			End If
			ComboBox1.SelectedItem = My.Settings.Theme
			MaxAddend2.Value = My.Settings.MaxAddendValueB
			MaxFactor2.Value = My.Settings.MaxFactorValueB
			MaxDividend.Value = My.Settings.MaxDividendValue
			MinAddend2.Value = My.Settings.MinAddendValueB
			MinFactor2.Value = My.Settings.MinFactorValueB
			MinDividend.Value = My.Settings.MinDividendValue
			MaxAddend1.Value = My.Settings.MaxAddendValueA
			MaxFactor1.Value = My.Settings.MaxFactorValueA
			MaxDivisor.Value = My.Settings.MaxDivisorValue
			MinAddend1.Value = My.Settings.MinAddendValueA
			MinFactor1.Value = My.Settings.MinFactorValueA
			MinDivisor.Value = My.Settings.MinDivisorValue
			MaxSubtrahend.Value = My.Settings.MaxSubtrahendValue
			MinSubtrahend.Value = My.Settings.MinSubtrahendValue
			MaxMinuend.Value = My.Settings.MaxMinuedValue
			MinMinuend.Value = My.Settings.MinMinuedValue
			UserName.Text = My.Settings.Username
			Password.Text = My.Settings.Password
			ConfirmPassword.Text = Password.Text
		Catch ex As Exception
			My.Computer.FileSystem.WriteAllText(File, DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & "ACCOUNT ERROR: Loaded Settings" & " " & ex.Message & vbNewLine, True)
		End Try
	End Sub

	Private Sub OK()
		If UserName.Text = "" Then
			Password.Clear()
			Password.ClearUndo()
			UserName.Select()
			MessageBox.Show("No User-Name was entered. Please enter a valid User-Name.", "Please enter User-Name (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		ElseIf Password.Text = "" Then
			Password.Select()
			MessageBox.Show("No Password was entered. Please enter a valid Password.", "Please enter Password (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Information)
		ElseIf Password.Text <> ConfirmPassword.Text Then
			ConfirmPassword.Clear()
			Password.Clear()
			ConfirmPassword.ClearUndo()
			Password.ClearUndo()
			Password.Select()
			MessageBox.Show("Passwords do not match. Please insert matching passwords.", "Password Matching Error (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		Else
			If (MaxAddend2.Value > MinAddend2.Value And MaxFactor2.Value > MinFactor2.Value _
				And MaxDividend.Value > MinDividend.Value And MaxAddend1.Value > MinAddend1.Value _
				And MaxFactor1.Value > MinFactor1.Value And MaxDivisor.Value > MinDividend.Value) Then
				Try
					If (AdditionSubtractionBoth.Checked) Then
						My.Settings.AdditionSubtraction = "Both"
					ElseIf (AdditionOnly.Checked) Then
						My.Settings.AdditionSubtraction = "Addition"
					ElseIf (SubtractionOnly.Checked) Then
						My.Settings.AdditionSubtraction = "Subtraction"
					Else
						My.Settings.AdditionSubtraction = "None"
					End If
					If (MultiplicationDivisionBoth.Checked) Then
						My.Settings.MultiplicationDivision = "Both"
					ElseIf (MultiplicationOnly.Checked) Then
						My.Settings.MultiplicationDivision = "Multiplication"
					ElseIf (DivisionOnly.Checked) Then
						My.Settings.MultiplicationDivision = "Division"
					Else
						My.Settings.MultiplicationDivision = "None"
					End If
					If CheckBox1.Checked Then
						My.Settings.AutomaticComplete = True
					Else
						My.Settings.AutomaticComplete = False
					End If
					My.Settings.Theme = ComboBox1.SelectedItem
					My.Settings.MaxAddendValueA = MaxAddend1.Value
					My.Settings.MinAddendValueA = MinAddend1.Value
					My.Settings.MaxAddendValueB = MaxAddend2.Value
					My.Settings.MinAddendValueB = MinAddend2.Value
					My.Settings.MaxFactorValueA = MaxFactor1.Value
					My.Settings.MinFactorValueA = MinFactor1.Value
					My.Settings.MaxFactorValueB = MaxFactor2.Value
					My.Settings.MinFactorValueB = MinFactor2.Value
					My.Settings.MaxDivisorValue = MaxDivisor.Value
					My.Settings.MinDivisorValue = MinDivisor.Value
					My.Settings.MaxDividendValue = MaxDividend.Value
					My.Settings.MinDividendValue = MinDividend.Value
					My.Settings.MaxSubtrahendValue = MaxSubtrahend.Value
					My.Settings.MinSubtrahendValue = MinSubtrahend.Value
					My.Settings.MaxMinuedValue = MaxMinuend.Value
					My.Settings.MinMinuedValue = MinMinuend.Value
					My.Settings.UserName = UserName.Text
					My.Settings.Password = Password.Text
					Me.Close()
				Catch ex As Exception
					MessageBox.Show("One or more or the values is not a valid number. Please ensure that all values are valid whole numbers.", "Invalid Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			Else
				MessageBox.Show("All minimum values must be equal or less than the maximum values.", "Incorrect Values", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			End If
		End If
	End Sub

	Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
		Me.Close()
	End Sub

	Private Sub OK_Click(sender As Object, e As EventArgs) Handles OKButton.Click
		OK()
	End Sub

End Class