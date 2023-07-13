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
				CheckBox2.Checked = True
			Else
				CheckBox2.Checked = False
			End If
			If My.Settings.ReduceRequired Then
				CheckBox3.Checked = True
			Else
				CheckBox3.Checked = False
			End If
			If (My.Settings.CommonDenominator) <> False Then CheckBox1.Checked = False
			ComboBox1.SelectedItem = My.Settings.Theme
            MaxDenominatorAdditionSubtraction.Value = My.Settings.MaxDenominatorAdditionSubtraction
            MaxDenominatorMultiplication.Value = My.Settings.MaxDenominatorMultiplication
            MaxDenominatorDivision.Value = My.Settings.MaxDenominatorDivision
            MinDenominatorAdditionSubtraction.Value = My.Settings.MinDenominatorAdditionSubtraction
            MinDenominatorMultiplication.Value = My.Settings.MinDenominatorMultiplication
            MinDenominatorDivision.Value = My.Settings.MinDenominatorDivision
            MaxNumeratorAdditionSubtraction.Value = My.Settings.MaxNumeratorAdditionSubtraction
            MaxNumeratorMultiplication.Value = My.Settings.MaxNumeratorMultiplication
            MaxNumeratorDivision.Value = My.Settings.MaxNumeratorDivision
            MinNumeratorAdditionSubtraction.Value = My.Settings.MinNumeratorAdditionSubtraction
            MinNumeratorMultiplication.Value = My.Settings.MinNumeratorMultiplication
            MinNumeratorDivision.Value = My.Settings.MinNumeratorDivision
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
            If (MaxDenominatorAdditionSubtraction.Value > MinDenominatorAdditionSubtraction.Value And MaxDenominatorMultiplication.Value > MinDenominatorMultiplication.Value _
                And MaxDenominatorDivision.Value > MinDenominatorDivision.Value And MaxNumeratorAdditionSubtraction.Value > MinNumeratorAdditionSubtraction.Value _
                And MaxNumeratorMultiplication.Value > MinNumeratorMultiplication.Value And MaxNumeratorDivision.Value > MinDenominatorDivision.Value) Then
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
                    If (CheckBox1.Checked) Then
                        My.Settings.CommonDenominator = False
                    Else
                        My.Settings.CommonDenominator = True
					End If
					If CheckBox2.Checked Then
						My.Settings.AutomaticComplete = True
					Else
						My.Settings.AutomaticComplete = False
					End If
					If CheckBox3.Checked Then
						My.Settings.ReduceRequired = True
					Else
						My.Settings.ReduceRequired = False
					End If
					My.Settings.Theme = ComboBox1.SelectedItem
                    My.Settings.MaxDenominatorAdditionSubtraction = MaxDenominatorAdditionSubtraction.Value
                    My.Settings.MaxDenominatorMultiplication = MaxDenominatorMultiplication.Value
                    My.Settings.MaxDenominatorDivision = MaxDenominatorDivision.Value
                    My.Settings.MinDenominatorAdditionSubtraction = MinDenominatorAdditionSubtraction.Value
                    My.Settings.MinDenominatorMultiplication = MinDenominatorMultiplication.Value
                    My.Settings.MinDenominatorDivision = MinDenominatorDivision.Value
                    My.Settings.MaxNumeratorAdditionSubtraction = MaxNumeratorAdditionSubtraction.Value
                    My.Settings.MaxNumeratorMultiplication = MaxNumeratorMultiplication.Value
                    My.Settings.MaxNumeratorDivision = MaxNumeratorDivision.Value
                    My.Settings.MinNumeratorAdditionSubtraction = MinNumeratorAdditionSubtraction.Value
                    My.Settings.MinNumeratorMultiplication = MinNumeratorMultiplication.Value
                    My.Settings.MinNumeratorDivision = MinNumeratorDivision.Value
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

    Private Sub MinNumeratorAdditionSubtraction_ValueChanged(sender As Object, e As EventArgs) Handles MinNumeratorAdditionSubtraction.ValueChanged

    End Sub

    Private Sub MaxDenominatorAdditionSubtraction_ValueChanged(sender As Object, e As EventArgs) Handles MaxDenominatorAdditionSubtraction.ValueChanged

    End Sub

    Private Sub MaxNumeratorAdditionSubtraction_ValueChanged(sender As Object, e As EventArgs) Handles MaxNumeratorAdditionSubtraction.ValueChanged

    End Sub
End Class