Public Class Exercises
	Public Completed As Boolean = False
	Public REDO As Boolean = False
	Dim AutomaticComplete As Boolean = My.Settings.AutomaticComplete
	Dim MainRandom As New Random()
	Dim MaxAddend2AdditionSubtraction As Integer = My.Settings.MaxAddend2AdditionSubtraction
	Dim MaxFactor1Multiplication As Integer = My.Settings.MaxFactor1Multiplication
	Dim MaxDividendDivision As Integer = My.Settings.MaxDividendDivision
	Dim MinAddend2AdditionSubtraction As Integer = My.Settings.MinAddend2AdditionSubtraction
	Dim MinFactor1Multiplication As Integer = My.Settings.MinFactor1Multiplication
	Dim MinDividendDivision As Integer = My.Settings.MinDividendDivision
	Dim MaxAddend1AdditionSubtraction As Integer = My.Settings.MaxAddend1AdditionSubtraction
	Dim MaxFactor2Multiplication As Integer = My.Settings.MaxFactor2Multiplication
	Dim MaxDivisorDivision As Integer = My.Settings.MaxDivisorDivision
	Dim MinAddend1AdditionSubtraction As Integer = My.Settings.MinAddend1AdditionSubtraction
	Dim MinFactor2Multiplication As Integer = My.Settings.MinFactor2Multiplication
	Dim MinDivisorDivision As Integer = My.Settings.MinDivisorDivision
	Dim IncorrectAnswers As Integer = 0
	Dim AnswersCompleted As Boolean = False
	Dim NewForeColor As New Color
	Dim NewBackColor As New Color
	Dim MainBackColor As New Color

	Protected Overrides Function IsInputKey(ByVal KeyData As System.Windows.Forms.Keys) As Boolean
		If KeyData = Keys.Escape Then
			Return True
		Else
			Return KeyData
		End If
	End Function

	Private Sub Exercises_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
		If REDO Then
			e.Cancel = True
			MessageBox.Show("Please complete all problems.", "Complete Problems", MessageBoxButtons.OK, MessageBoxIcon.Error)
		ElseIf Completed = False AndAlso MessageBox.Show("Are you sure you would like to exit?", "Warning (Auto-Helper)", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.No Then
			e.Cancel = True
		End If
	End Sub

	Private Sub Exercises_Load(sender As Object, e As EventArgs) Handles Me.Load
		InitalizeExercises()
		InitializeHandlers()
		InitializeForm()
	End Sub

	Private Sub InitializeForm()

		Select Case My.Settings.Theme
			Case "Blue"
				NewForeColor = Color.White
				NewBackColor = Color.RoyalBlue
				MainBackColor = Color.DarkBlue
			Case "Dark"
				NewForeColor = Color.Black
				NewBackColor = Color.Gray
				MainBackColor = Color.Silver
			Case "Light"
				NewForeColor = Color.Black
				NewBackColor = Color.Silver
				MainBackColor = Color.LightGray
			Case "Standard"
				NewForeColor = Color.White
				NewBackColor = Color.Gray
				MainBackColor = Color.Silver
		End Select
		Me.BackColor = MainBackColor
		Label1.ForeColor = NewForeColor
		For Each Control As Control In Me.Controls
			If TypeOf Control Is Panel Then
				Control.ForeColor = NewForeColor
				Control.BackColor = NewBackColor
			End If
		Next
	End Sub


	Private Sub InitializeHandlers()
		Dim NewArray As Object() = {Panel1.Controls, Panel2.Controls, Panel3.Controls, Panel4.Controls, Panel5.Controls, Panel6.Controls, Panel7.Controls, Panel8.Controls, Panel9.Controls, Panel10.Controls, Panel11.Controls, Panel12.Controls, _
				Panel13.Controls, Panel14.Controls, Panel15.Controls, Panel16.Controls, Panel17.Controls, Panel18.Controls, Panel20.Controls, Panel23.Controls, Panel25.Controls, _
				Panel27.Controls, Panel29.Controls, Panel31.Controls}
		Dim NewList As New List(Of Object)(NewArray)
		For Each PanelCollection As Object In NewList
			For Each Control As Control In PanelCollection
				AddHandler Control.Click, AddressOf Item_Clicked
			Next
		Next
	End Sub

	Private Sub InitalizeExercises()
		'This initializes the signs.
		Dim NewArray As String() = {"+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", _
				"+", "+", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–"}
		Dim NewArray2 As String() = {"×", "×", "×", "×", "×", "×", "×", "×", "×", "×", "×", "×", "×", "×", "×", "×", "×", "×", _
				"×", "×", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷", "÷"}
		Dim NewList As New List(Of String)(NewArray)
		Dim NewList2 As New List(Of String)(NewArray2)
		Dim SignArray As Label() = {Sign1, Sign2, Sign3, Sign4, Sign5, Sign6, Sign7, Sign8, Sign9, Sign10, Sign11, Sign12, Sign13, Sign14, Sign15, Sign16, _
							Sign17, Sign18, Sign19, Sign20, Sign21, Sign22, Sign23, Sign24}
		'This initializes the values for all upper numbers.
		Dim LabelA_Array As Label() = {Addend1a, Addend2a, Addend3a, Addend4a, Addend5a, Addend6a, Addend7a, Addend8a, _
		Addend9a, Addend10a, Addend11a, Addend12a, Addend13a, Addend14a, Addend15a, Addend16a, Addend17a, Addend18a, Addend19a, _
		Addend20a, Addend21a, Addend22a, Addend23a, Addend24a}
		'This initializes the values for all lower numbers.
		Dim LabelB_Array As Label() = {Addend1b, Addend2b, Addend3b, Addend4b, Addend5b, Addend6b, Addend7b, Addend8b, _
		Addend9b, Addend10b, Addend11b, Addend12b, Addend13b, Addend14b, Addend15b, Addend16b, Addend17b, Addend18b, Addend19b, _
		Addend20b, Addend21b, Addend22b, Addend23b, Addend24b}
		Dim SignList As New List(Of Label)(SignArray)
		Dim NewLabelListA As New List(Of Label)(LabelA_Array) 'This creates the upper number list.
		Dim NewLabelListB As New List(Of Label)(LabelB_Array) 'This creates the lower number list.
		If MultiplicationDivision = False Then
			Select Case My.Settings.AdditionSubtraction
				Case "Addition"
					For Each Sign As Label In SignList
						Sign.Text = "+"
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim Addend1 As Label = NewLabelListA.Item(Index)
						Addend1.Text = MainRandom.Next(MinAddend1AdditionSubtraction, MaxAddend1AdditionSubtraction) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
					Next
					For Each Sign As Label In SignList
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim Addend2 As Label = NewLabelListB.Item(Index)
						Addend2.Text = MainRandom.Next(MinAddend2AdditionSubtraction, MaxAddend2AdditionSubtraction) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
					Next
				Case "Subtraction"
					For Each Sign As Label In SignList
						Dim Value1 As Decimal = MainRandom.Next(MinAddend1AdditionSubtraction, MaxAddend1AdditionSubtraction) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
						Dim Value2 As Decimal = MainRandom.Next(MinAddend2AdditionSubtraction, MaxAddend2AdditionSubtraction) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim Addend1 As Label = NewLabelListA.Item(Index)
						Dim Addend2 As Label = NewLabelListB.Item(Index)
						Sign.Text = "–"
						If (Value1 >= Value2) Then
							Addend1.Text = Value1
							Addend2.Text = Value2
						Else
							Addend1.Text = Value2
							Addend2.Text = Value1
						End If
					Next
				Case "Both"
					'This occurs for each problem.
					For Each CurrentSign As Label In SignList
						'This randomly selects which problems will be subtraction.
						Dim Int As Integer = MainRandom.Next(0, NewList.Count - 1)
						CurrentSign.Text = NewList.Item(Int)
						NewList.RemoveAt(Int)
					Next
					For Each CurrentLabelA As Label In NewLabelListA
						Dim Index As Integer = NewLabelListA.IndexOf(CurrentLabelA)	'This represents the index of the new label.
						Dim CurrentSign As Label = SignList.Item(Index)
						If CurrentSign.Text = "+" Then
							'This occurs if the problem is an addition problem.
							CurrentLabelA.Text = MainRandom.Next(MinAddend1AdditionSubtraction, MaxAddend1AdditionSubtraction) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
						Else
							'This occurs if the problem is a subtraction problem.
							Dim CurrentLabelB As Label = NewLabelListB(Index)
							Dim Value1 As Decimal = MainRandom.Next(MinAddend1AdditionSubtraction, MaxAddend1AdditionSubtraction) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
							Dim Value2 As Decimal = MainRandom.Next(MinAddend2AdditionSubtraction, MaxAddend2AdditionSubtraction) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
							If (Value1 >= Value2) Then
								CurrentLabelA.Text = Value1
								CurrentLabelB.Text = Value2
							Else
								CurrentLabelA.Text = Value2
								CurrentLabelB.Text = Value1
							End If
						End If
					Next
					For Each CurrentSign In SignList
						If CurrentSign.Text = "+" Then
							Dim Index As Integer = SignList.IndexOf(CurrentSign)
							Dim CurrentlabelB As Label = NewLabelListB(Index)
							CurrentlabelB.Text = MainRandom.Next(MinAddend2AdditionSubtraction, MaxAddend2AdditionSubtraction) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
						End If
					Next
			End Select
		Else
			'This occurs for each problem.
			Select My.Settings.MultiplicationDivision
				Case "Multiplication"
					For Each Sign As Label In SignList
						Sign.Text = "×"
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim Addend1 As Label = NewLabelListA.Item(Index)
						Addend1.Text = MainRandom.Next(MinFactor1Multiplication, MaxFactor1Multiplication) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
					Next
					For Each Sign As Label In SignList
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim Addend2 As Label = NewLabelListB.Item(Index)
						Addend2.Text = MainRandom.Next(MinFactor2Multiplication, MaxFactor2Multiplication) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
					Next
				Case "Division"
					For Each Sign As Label In SignList
						Dim Value1 As Decimal = MainRandom.Next(MinDividendDivision, MaxDividendDivision) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
						Dim Value2 As Decimal = MainRandom.Next(MinDivisorDivision, MaxDivisorDivision) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim Addend1 As Label = NewLabelListA.Item(Index)
						Dim Addend2 As Label = NewLabelListB.Item(Index)
						Sign.Text = "÷"
						If (Value1 >= Value2) Then
							Addend1.Text = Value1
							Addend2.Text = Value2
						Else
							Addend1.Text = Value2
							Addend2.Text = Value1
						End If
					Next
				Case "Both"
					For Each CurrentSign As Label In SignList
						'This randomly selects which problems will be subtraction.
						Dim Int As Integer = MainRandom.Next(0, NewList2.Count - 1)
						CurrentSign.Text = NewList2.Item(Int)
						NewList2.RemoveAt(Int)
					Next
					For Each Label In NewLabelListA
						Dim CurrentLabelA As Label = Label 'This represents the new label.
						Dim Index As Integer = NewLabelListA.IndexOf(CurrentLabelA)	'This represents the index of the new label.
						Dim CurrentSign As Label = SignList.Item(Index)
						If CurrentSign.Text = "×" Then
							'This occurs if the problem is a multiplication problem.
							CurrentLabelA.Text = MainRandom.Next(MinFactor1Multiplication, MaxFactor1Multiplication) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
						Else
							'This occurs if the problem is a division problem.
							Dim CurrentLabelB As Label = NewLabelListB(Index)
							Dim Value1 As Decimal = MainRandom.Next(MinDividendDivision, MaxDividendDivision) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
							Dim Value2 As Decimal = MainRandom.Next(MinDivisorDivision, MaxDivisorDivision) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
							If (Value1 >= Value2) Then
								CurrentLabelA.Text = Value1
								CurrentLabelB.Text = Value2
							Else
								CurrentLabelA.Text = Value2
								CurrentLabelB.Text = Value1
							End If
						End If
					Next
					For Each CurrentSign In SignList
						If CurrentSign.Text = "×" Then
							Dim Index As Integer = SignList.IndexOf(CurrentSign)
							Dim CurrentlabelB As Label = NewLabelListB(Index)
							CurrentlabelB.Text = MainRandom.Next(MinFactor2Multiplication, MaxFactor2Multiplication) & "." & MainRandom.Next(0, 9) & MainRandom.Next(0, 9)
						End If
					Next
			End Select
		End If
	End Sub

	Private Sub CheckAnswers()
		Try
			'This initializes the signs.
			Dim NewArray As String() = {"+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", "+", _
					"+", "+", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–", "–"}
			Dim NewList As New List(Of String)(NewArray)
			Dim SignArray As Label() = {Sign1, Sign2, Sign3, Sign4, Sign5, Sign6, Sign7, Sign8, Sign9, Sign10, Sign11, Sign12, _
			Sign13, Sign14, Sign15, Sign16, Sign17, Sign18, Sign19, Sign20, Sign21, Sign22, Sign23, Sign24}
			'This initializes the values for all upper numbers.
			Dim LabelA_Array As Label() = {Addend1a, Addend2a, Addend3a, Addend4a, Addend5a, Addend6a, Addend7a, Addend8a, _
			Addend9a, Addend10a, Addend11a, Addend12a, Addend13a, Addend14a, Addend15a, Addend16a, Addend17a, Addend18a, Addend19a, _
			Addend20a, Addend21a, Addend22a, Addend23a, Addend24a}
			'This initializes the values for all lower numbers.
			Dim LabelB_Array As Label() = {Addend1b, Addend2b, Addend3b, Addend4b, Addend5b, Addend6b, Addend7b, Addend8b, _
			Addend9b, Addend10b, Addend11b, Addend12b, Addend13b, Addend14b, Addend15b, Addend16b, Addend17b, Addend18b, Addend19b, _
			Addend20b, Addend21b, Addend22b, Addend23b, Addend24b}
			'This initializes the values for all check marks.
			Dim CheckArray As Label() = {Check1, Check2, Check3, Check4, Check5, Check6, Check7, Check8, Check9, Check10, Check11, Check12, _
			Check13, Check14, Check15, Check16, Check17, Check18, Check19, Check20, Check21, Check22, Check23, Check24}
			'This initializes the values for all answers.
			Dim AnswerArray As TextBox() = {Answer1, Answer2, Answer3, Answer4, Answer5, Answer6, Answer7, Answer8, Answer9, Answer10, Answer11, Answer12, _
			Answer13, Answer14, Answer15, Answer16, Answer17, Answer18, Answer19, Answer20, Answer21, Answer22, Answer23, Answer24}
			Dim SignList As New List(Of Label)(SignArray)
			Dim NewLabelListA As New List(Of Label)(LabelA_Array) 'This creates the upper number list.
			Dim NewLabelListB As New List(Of Label)(LabelB_Array) 'This creates the lower number list.
			Dim CheckList As New List(Of Label)(CheckArray)	'This creates the check mark list.
			Dim AnswerList As New List(Of TextBox)(AnswerArray)	'This creates the answer list.
			Dim MainDecimal As New Decimal
			'Disables the answers from being edited.
			For Each Answer As TextBox In AnswerList
				Answer.Enabled = False
			Next
			SuspendLayout()
			For Each CurrentSign As Label In SignList
				Dim Index As Integer = SignList.IndexOf(CurrentSign)
				Dim CurrentLabelA As Label = NewLabelListA(Index)
				Dim CurrentLabelB As Label = NewLabelListB(Index)
				Dim CurrentAnswer As TextBox = AnswerList(Index)
				Dim CurrentCheckMark As Label = CheckList(Index)
				Dim Correct As Boolean = False
				Dim Result As Decimal
				Select Case CurrentSign.Text
					Case "+" : Result = Convert.ToDecimal(CurrentLabelA.Text) + Convert.ToDecimal(CurrentLabelB.Text)
					Case "–" : Result = Convert.ToDecimal(CurrentLabelA.Text) - Convert.ToDecimal(CurrentLabelB.Text)
					Case "×" : Result = Convert.ToDecimal(CurrentLabelA.Text) * Convert.ToDecimal(CurrentLabelB.Text)
					Case "÷" : Result = Convert.ToDecimal(CurrentLabelA.Text) / Convert.ToDecimal(CurrentLabelB.Text)
				End Select
				If Decimal.TryParse(CurrentAnswer.Text, MainDecimal) AndAlso Math.Round(Result, 2, MidpointRounding.AwayFromZero) = Math.Round(Convert.ToDecimal(CurrentAnswer.Text), 2, MidpointRounding.AwayFromZero) Then Correct = True
				If Correct Then
					CurrentCheckMark.ForeColor = Color.DarkGreen
					CurrentCheckMark.Text = ""
				Else
					CurrentCheckMark.ForeColor = Color.DarkRed
					CurrentCheckMark.Text = ""
					IncorrectAnswers += 1
				End If
			Next
			'Shows all marks.
			For Each CheckMark As Label In CheckList
				CheckMark.Show()
			Next
			ResumeLayout()
			Dim CorrectAnswers As Integer
			'Determine how many answers are correct.
			CorrectAnswers = 24 - IncorrectAnswers
			'Determine the Grade.
			Grade = Math.Round(CorrectAnswers / 24 * 100)
			'Change the EndScreen to match the grade.
			Congratulations.GradeLabel.Text = Grade.ToString
			Dim Input As String = "ERROR | Information not available"
			If REDO = False And MultiplicationDivision = False Then
				Input = DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & CurrentUser & "  " & "Grade: " & Grade.ToString & "  " & "(Addition/Subtraction)" & vbNewLine
			ElseIf REDO = True And MultiplicationDivision = False Then
				Input = DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & CurrentUser & "  " & "Grade: " & Grade.ToString & "  " & "(Addition/Subtraction) - Re-taken" & vbNewLine
			ElseIf REDO = False And MultiplicationDivision Then
				Input = DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & CurrentUser & "  " & "Grade: " & Grade.ToString & "  " & "(Multiplication/Division)" & vbNewLine
			Else
				Input = DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & CurrentUser & "  " & "Grade: " & Grade.ToString & "  " & "(Multiplication/Division) - Re-taken" & vbNewLine
			End If
			My.Computer.FileSystem.WriteAllText(File, Input, True)
		Catch ex As Exception
			If MultiplicationDivision = False Then
				My.Computer.FileSystem.WriteAllText(File, DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & CurrentUser & "  " & "ERROR: Graded Answers " & "(Addition/Subtraction)" & " " & ex.Message & vbNewLine, True)
			Else
				My.Computer.FileSystem.WriteAllText(File, DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & CurrentUser & "  " & "ERROR: Graded Answers " & "(Multiplication/Division)" & " " & ex.Message & vbNewLine, True)
			End If
		End Try
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		AutoSketch.Show()
		AutoSketch.BringToFront()
	End Sub

	Private Sub CompleteButton_Click(sender As Object, e As EventArgs) Handles CompleteButton.Click
		Complete()
	End Sub

	Private Sub Complete()
		Dim Blank As Boolean = False
		Dim AnswerArray As TextBox() = {Answer1, Answer2, Answer3, Answer4, Answer5, Answer6, Answer7, Answer8, Answer9, Answer10, Answer11, Answer12, _
			Answer13, Answer14, Answer15, Answer16, Answer17, Answer18, Answer19, Answer20, Answer21, Answer22, Answer23, Answer24}
		Dim AnswerList As New List(Of TextBox)(AnswerArray)	'This creates the answer list.
		For Each Answer As TextBox In AnswerList
			If Answer.Text = "" Then
				Dim MyPen As New Pen(Brushes.DarkRed, 2)
				Dim Graphics As Graphics = Answer.CreateGraphics()
				Graphics.DrawRectangle(MyPen, Answer.ClientRectangle)
				Blank = True
				Graphics.Dispose()
				MyPen.Dispose()
				Blank = True
			End If
		Next
		If Blank Then
			MessageBox.Show("One or more of the answers is empty. Please complete all problems.", "Complete Problems", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End If
		Timer2.Stop()
		CompleteButton.Hide()
		If CompleteButton.Text = "&Complete" Then
			CheckAnswers()
			Timer1.Enabled = True
		Else
			ShowCongratulationsScreen()
		End If
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		If Me.Opacity > 0 Then
			Me.Opacity -= 0.001
		Else
			ShowCongratulationsScreen()
		End If
	End Sub

	Private Sub ShowCongratulationsScreen()
		Timer1.Enabled = False
		Completed = True
		Congratulations.Show()
		If Grade >= 70 Then
			Me.Close()
		Else
			Me.Hide()
			SuspendLayout()
			'This initializes the values for all check marks.
			Dim CheckArray As Label() = {Check1, Check2, Check3, Check4, Check5, Check6, Check7, Check8, Check9, Check10, Check11, Check12, _
			Check13, Check14, Check15, Check16, Check17, Check18, Check19, Check20, Check21, Check22, Check23, Check24}
			'This initializes the values for all answers.
			Dim AnswerArray As TextBox() = {Answer1, Answer2, Answer3, Answer4, Answer5, Answer6, Answer7, Answer8, Answer9, Answer10, Answer11, Answer12, _
			Answer13, Answer14, Answer15, Answer16, Answer17, Answer18, Answer19, Answer20, Answer21, Answer22, Answer23, Answer24}
			Dim CheckList As New List(Of Label)(CheckArray)	'This creates the check mark list.
			Dim AnswerList As New List(Of TextBox)(AnswerArray)	'This creates the answer list.
			'Initializes all answers.
			For Each Answer In AnswerList
				Answer.Clear()
				Answer.Enabled = True
			Next
			'Hides all marks.
			For Each CheckMark In CheckList
				CheckMark.Hide()
				CheckMark.Text = ""
				CheckMark.ForeColor = Color.Black
			Next
			'Initializes other features
			CompleteButton.Text = "&Complete"
			CompleteButton.Show()
			Me.Opacity = 100
			Answer1.Select()
			IncorrectAnswers = 0
			RedoIndicator.Show() 'Lets the user show that this is a previous assignment.
			ResumeLayout()
		End If

	End Sub

	Private Sub Exercises_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown, CompleteButton.KeyDown, Button1.KeyDown, _
	Answer1.KeyDown, Answer2.KeyDown, Answer3.KeyDown, Answer4.KeyDown, Answer5.KeyDown, Answer6.KeyDown, Answer7.KeyDown, _
	Answer8.KeyDown, Answer9.KeyDown, Answer10.KeyDown, Answer11.KeyDown, Answer12.KeyDown, Answer13.KeyDown, Answer14.KeyDown, _
	Answer15.KeyDown, Answer16.KeyDown, Answer17.KeyDown, Answer18.KeyDown, Answer19.KeyDown, Answer20.KeyDown, Answer21.KeyDown, _
	Answer22.KeyDown, Answer23.KeyDown, Answer24.KeyDown
		If e.KeyCode = Keys.Escape And e.Shift AndAlso InputBox("Please enter the password.", "Enter Password (Auto-Helper)") = My.Settings.Password Then
			REDO = False
			Me.Close()
		End If
	End Sub

	Private Sub Item_Clicked(sender As Object, e As EventArgs) Handles Panel1.Click, Panel2.Click, Panel3.Click, Panel4.Click, _
		Panel5.Click, Panel6.Click, Panel7.Click, Panel8.Click, Panel9.Click, Panel10.Click, Panel11.Click, Panel12.Click, _
		Panel13.Click, Panel14.Click, Panel15.Click, Panel16.Click, Panel17.Click, Panel18.Click, Panel20.Click, _
		 Panel23.Click, Panel25.Click, Panel29.Click, Panel31.Click, MyBase.Click, Label1.Click
		If Timer1.Enabled Then
			Timer1.Enabled = False
			Me.Opacity = 100
			CompleteButton.Show()
			CompleteButton.Text = "&OK"
		End If
	End Sub

	Private Sub Answers_Changed(sender As Object, e As EventArgs) Handles Answer1.TextChanged, Answer2.TextChanged, Answer3.TextChanged, Answer4.TextChanged, Answer5.TextChanged, Answer6.TextChanged, Answer7.TextChanged, Answer8.TextChanged, Answer9.TextChanged, Answer10.TextChanged, Answer11.TextChanged, Answer12.TextChanged, _
				Answer13.TextChanged, Answer14.TextChanged, Answer15.TextChanged, Answer16.TextChanged, Answer17.TextChanged, Answer18.TextChanged, Answer19.TextChanged, Answer20.TextChanged, Answer21.TextChanged, Answer22.TextChanged, Answer23.TextChanged, Answer24.TextChanged
		If AutomaticComplete Then
			Timer2.Stop()
			Timer2.Start()
		End If
	End Sub

	Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
		Dim AnswerArray As TextBox() = {Answer1, Answer2, Answer3, Answer4, Answer5, Answer6, Answer7, Answer8, Answer9, Answer10, Answer11, Answer12, _
				Answer13, Answer14, Answer15, Answer16, Answer17, Answer18, Answer19, Answer20, Answer21, Answer22, Answer23, Answer24}
		Dim AnswerList As New List(Of TextBox)(AnswerArray)	'This creates the answer list.
		For Each Answer As TextBox In AnswerList
			If Answer.Text = "" Then
				Timer2.Stop()
				Exit Sub
			End If
		Next
		Complete()
	End Sub

End Class