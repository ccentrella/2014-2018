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
            MaxAddend2.Value = My.Settings.MaxAddend2AdditionSubtraction
            MaxFactor2.Value = My.Settings.MaxFactor2Multiplication
			MaxDividend.Value = My.Settings.MaxDividendDivision
			MinAddend2.Value = My.Settings.MinAddend2AdditionSubtraction
			MinFactor2.Value = My.Settings.MinFactor2Multiplication
			MinDividend.Value = My.Settings.MinDividendDivision
			MaxAddend1.Value = My.Settings.MaxAddend1AdditionSubtraction
			MaxFactor1.Value = My.Settings.MaxFactor1Multiplication
			MaxDivisor.Value = My.Settings.MaxDivisorDivision
			MinAddend1.Value = My.Settings.MinAddend1AdditionSubtraction
			MinFactor1.Value = My.Settings.MinFactor1Multiplication
			MinDivisor.Value = My.Settings.MinDivisorDivision
			UserName.Text = My.Settings.UserName
			Password.Text = My.Settings.Password
			ConfirmPassword.Text = Password.Text
		Catch ex As Exception
			My.Computer.FileSystem.WriteAllText(File, DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & "ACCOUNT ERROR: Loaded Settings" & " " & ex.message & vbNewLine, True)
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
					My.Settings.MaxAddend1AdditionSubtraction = MaxAddend1.Value
					My.Settings.MinAddend1AdditionSubtraction = MinAddend1.Value
					My.Settings.MaxAddend2AdditionSubtraction = MaxAddend2.Value
					My.Settings.MinAddend2AdditionSubtraction = MinAddend2.Value
					My.Settings.MaxFactor1Multiplication = MaxFactor1.Value
					My.Settings.MinFactor1Multiplication = MinFactor1.Value
					My.Settings.MaxFactor2Multiplication = MaxFactor2.Value
					My.Settings.MinFactor2Multiplication = MinFactor2.Value
					My.Settings.MaxDividendDivision = MaxDividend.Value
					My.Settings.MinDividendDivision = MinDividend.Value
					My.Settings.MaxDivisorDivision = MaxDivisor.Value
					My.Settings.MinDivisorDivision = MinDivisor.Value
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

    Private Sub MinNumeratorAdditionSubtraction_ValueChanged(sender As Object, e As EventArgs) Handles MinAddend1.ValueChanged

    End Sub

    Private Sub MaxDenominatorAdditionSubtraction_ValueChanged(sender As Object, e As EventArgs) Handles MaxAddend2.ValueChanged

    End Sub

    Private Sub MaxNumeratorAdditionSubtraction_ValueChanged(sender As Object, e As EventArgs) Handles MaxAddend1.ValueChanged

    End Sub
End Class