Public Class Exercises
	Public MultiplicationDivision As Boolean
	Public DoneClicked As Boolean = False
	Dim MainRandom As New Random
	Dim Completed As Boolean = False
	Dim REDO As Boolean = False
	Dim AutomaticComplete As Boolean = My.Settings.AutomaticComplete
	Dim CommonDenominator As Boolean = My.Settings.CommonDenominator
	Dim ReduceRequired As Boolean = My.Settings.ReduceRequired
	Dim MaxDenominatorAdditionSubtraction As Integer = My.Settings.MaxDenominatorAdditionSubtraction
	Dim MaxDenominatorMultiplication As Integer = My.Settings.MaxDenominatorMultiplication
	Dim MaxDenominatorDivision As Integer = My.Settings.MaxDenominatorDivision
	Dim MinDenominatorAdditionSubtraction As Integer = My.Settings.MinDenominatorAdditionSubtraction
	Dim MinDenominatorMultiplication As Integer = My.Settings.MinDenominatorMultiplication
	Dim MinDenominatorDivision As Integer = My.Settings.MinDenominatorDivision
	Dim MaxNumeratorAdditionSubtraction As Integer = My.Settings.MaxNumeratorAdditionSubtraction
	Dim MaxNumeratorMultiplication As Integer = My.Settings.MaxNumeratorMultiplication
	Dim MaxNumeratorDivision As Integer = My.Settings.MaxNumeratorDivision
	Dim MinNumeratorAdditionSubtraction As Integer = My.Settings.MinNumeratorAdditionSubtraction
	Dim MinNumeratorMultiplication As Integer = My.Settings.MinNumeratorMultiplication
	Dim MinNumeratorDivision As Integer = My.Settings.MinNumeratorDivision
	Dim IncorrectAnswers As Integer = 0
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
				Panel13.Controls, Panel14.Controls, Panel15.Controls, Panel16.Controls, Panel17.Controls, Panel18.Controls, Panel19.Controls, Panel20.Controls, Panel21.Controls, Panel22.Controls, _
				Panel23.Controls, Panel24.Controls}
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
		'This represents all signs.
		Dim SignArray As Label() = {Sign1, Sign2, Sign3, Sign4, Sign5, Sign6, Sign7, Sign8, Sign9, Sign10, Sign11, Sign12, Sign13, Sign14, Sign15, Sign16, _
									Sign17, Sign18, Sign19, Sign20, Sign21, Sign22, Sign23, Sign24}
		'This represents all leftmost numerators.
		Dim NumeratorA_Array As Label() = {Numerator1a, Numerator2a, Numerator3a, Numerator4a, Numerator5a, Numerator6a, Numerator7a, Numerator8a, _
										   Numerator9a, Numerator10a, Numerator11a, Numerator12a, Numerator13a, Numerator14a, Numerator15a, Numerator16a, _
										   Numerator17a, Numerator18a, Numerator19a, Numerator20a, Numerator21a, Numerator22a, Numerator23a, Numerator24a}
		'This represents all rightmost numerators.
		Dim NumeratorB_Array As Label() = {Numerator1b, Numerator2b, Numerator3b, Numerator4b, Numerator5b, Numerator6b, Numerator7b, Numerator8b, _
										   Numerator9b, Numerator10b, Numerator11b, Numerator12b, Numerator13b, Numerator14b, Numerator15b, Numerator16b, _
										   Numerator17b, Numerator18b, Numerator19b, Numerator20b, Numerator21b, Numerator22b, Numerator23b, Numerator24b}

		'This represents all leftmost denominators.
		Dim DenominatorA_Array As Label() = {Denominator1a, Denominator2a, Denominator3a, Denominator4a, Denominator5a, Denominator6a, Denominator7a, Denominator8a, _
										   Denominator9a, Denominator10a, Denominator11a, Denominator12a, Denominator13a, Denominator14a, Denominator15a, Denominator16a, _
										   Denominator17a, Denominator18a, Denominator19a, Denominator20a, Denominator21a, Denominator22a, Denominator23a, Denominator24a}
		'This represents all rightmost denominators.
		Dim DenominatorB_Array As Label() = {Denominator1b, Denominator2b, Denominator3b, Denominator4b, Denominator5b, Denominator6b, Denominator7b, Denominator8b, _
										   Denominator9b, Denominator10b, Denominator11b, Denominator12b, Denominator13b, Denominator14b, Denominator15b, Denominator16b, _
										   Denominator17b, Denominator18b, Denominator19b, Denominator20b, Denominator21b, Denominator22b, Denominator23b, Denominator24b}
		'This represents all numerator answers.
		Dim NumeratorAnswerArray As TextBox() = {NumeratorAnswer1, NumeratorAnswer2, NumeratorAnswer3, NumeratorAnswer4, NumeratorAnswer5, NumeratorAnswer6, NumeratorAnswer7, NumeratorAnswer8, _
										   NumeratorAnswer9, NumeratorAnswer10, NumeratorAnswer11, NumeratorAnswer12, NumeratorAnswer13, NumeratorAnswer14, NumeratorAnswer15, NumeratorAnswer16, _
										   NumeratorAnswer17, NumeratorAnswer18, NumeratorAnswer19, NumeratorAnswer20, NumeratorAnswer21, NumeratorAnswer22, NumeratorAnswer23, NumeratorAnswer24}
		'This represents all denominator answers.
		Dim DenominatorAnswerArray As TextBox() = {DenominatorAnswer1, DenominatorAnswer2, DenominatorAnswer3, DenominatorAnswer4, DenominatorAnswer5, DenominatorAnswer6, DenominatorAnswer7, DenominatorAnswer8, _
								   DenominatorAnswer9, DenominatorAnswer10, DenominatorAnswer11, DenominatorAnswer12, DenominatorAnswer13, DenominatorAnswer14, DenominatorAnswer15, DenominatorAnswer16, _
								   DenominatorAnswer17, DenominatorAnswer18, DenominatorAnswer19, DenominatorAnswer20, DenominatorAnswer21, DenominatorAnswer22, DenominatorAnswer23, DenominatorAnswer24}
		Dim SignList As New List(Of Label)(SignArray)
		Dim NumeratorA_List As New List(Of Label)(NumeratorA_Array)
		Dim NumeratorB_List As New List(Of Label)(NumeratorB_Array)
		Dim DenominatorA_List As New List(Of Label)(DenominatorA_Array)
		Dim DenominatorB_List As New List(Of Label)(DenominatorB_Array)
		Dim NumeratorAnswerList As New List(Of TextBox)(NumeratorAnswerArray)
		Dim DenominatorAnswerList As New List(Of TextBox)(DenominatorAnswerArray)
		If MultiplicationDivision = False Then
			Select Case My.Settings.AdditionSubtraction
				Case "Addition"
					For Each Sign As Label In SignList
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim NumeratorA As Label = NumeratorA_List(Index)
						Sign.Text = "+"
						NumeratorA.Text = MainRandom.Next(MinNumeratorAdditionSubtraction, MaxNumeratorAdditionSubtraction)
					Next
					For Each NumeratorB As Label In NumeratorB_List
						NumeratorB.Text = MainRandom.Next(MinNumeratorAdditionSubtraction, MaxNumeratorAdditionSubtraction)
					Next
					For Each DenominatorA As Label In DenominatorA_List
						DenominatorA.Text = MainRandom.Next(MinDenominatorAdditionSubtraction, MaxDenominatorAdditionSubtraction)
					Next
					For Each SignB As Label In DenominatorB_List
						If CommonDenominator Then
							Dim Index As Integer = DenominatorB_List.IndexOf(SignB)
							Dim SignA As Label = DenominatorA_List(Index)
							SignB.Text = SignA.Text
						Else
							SignB.Text = MainRandom.Next(MinDenominatorAdditionSubtraction, MaxDenominatorAdditionSubtraction)
						End If
					Next
				Case "Subtraction"
					For Each Sign As Label In SignList
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim NumeratorA As Label = NumeratorA_List(Index)
						Dim NumeratorB As Label = NumeratorB_List(Index)
						Dim Value1 As Integer = MainRandom.Next(MinNumeratorAdditionSubtraction, MaxNumeratorAdditionSubtraction)
						Dim Value2 As Integer = MainRandom.Next(MinNumeratorAdditionSubtraction, MaxNumeratorAdditionSubtraction)
						Sign.Text = "–"
						If Value1 >= Value2 Then
							NumeratorA.Text = Value1
							NumeratorB.Text = Value2
						Else
							NumeratorA.Text = Value2
							NumeratorB.Text = Value1
						End If
						NumeratorA.Text = MainRandom.Next(MinNumeratorAdditionSubtraction, MaxNumeratorAdditionSubtraction)
					Next
					For Each DenominatorA As Label In DenominatorA_List
						DenominatorA.Text = MainRandom.Next(MinDenominatorAdditionSubtraction, MaxDenominatorAdditionSubtraction)
					Next
					For Each DenominatorB As Label In DenominatorB_List
						If CommonDenominator Then
							Dim Index As Integer = DenominatorB_List.IndexOf(DenominatorB)
							Dim DenominatorA As Label = DenominatorA_List(Index)
							DenominatorB.Text = DenominatorA.Text
						Else
							DenominatorB.Text = MainRandom.Next(MinDenominatorAdditionSubtraction, MaxDenominatorAdditionSubtraction)
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
					For Each NumeratorA As Label In NumeratorA_List
						Dim Index As Integer = NumeratorA_List.IndexOf(NumeratorA)
						Dim CurrentSign As Label = SignList(Index)
						If CurrentSign.text = "+" Then
							NumeratorA.Text = MainRandom.Next(MinNumeratorAdditionSubtraction, MaxNumeratorAdditionSubtraction)
						ElseIf CurrentSign.Text = "–" Then
							Dim NumeratorB As Label = NumeratorB_List(Index)
							Dim Value1 As Integer = MainRandom.Next(MinNumeratorAdditionSubtraction, MaxNumeratorAdditionSubtraction)
							Dim Value2 As Integer = MainRandom.Next(MinNumeratorAdditionSubtraction, MaxNumeratorAdditionSubtraction)
							If Value1 >= Value2 Then
								NumeratorA.Text = Value1
								NumeratorB.Text = Value2
							Else
								NumeratorA.Text = Value2
								NumeratorB.Text = Value1
							End If
						End If
					Next
					For Each CurrentSign As Label In SignList
						If CurrentSign.Text = "+" Then
							Dim Index As Integer = SignList.IndexOf(CurrentSign)
							Dim NumeratorB As Label = NumeratorB_List(Index)
							NumeratorB.Text = MainRandom.Next(MinNumeratorAdditionSubtraction, MaxNumeratorAdditionSubtraction)
						End If
					Next
					For Each DenominatorA As Label In DenominatorA_List
						DenominatorA.Text = MainRandom.Next(MinDenominatorAdditionSubtraction, MaxDenominatorAdditionSubtraction)
					Next
					For Each DenominatorB As Label In DenominatorB_List
						If CommonDenominator Then
							Dim Index As Integer = DenominatorB_List.IndexOf(DenominatorB)
							Dim DenominatorA As Label = DenominatorA_List(Index)
							DenominatorB.Text = DenominatorA.Text
						Else
							DenominatorB.Text = MainRandom.Next(MinDenominatorAdditionSubtraction, MaxDenominatorAdditionSubtraction)
						End If
					Next
			End Select
		Else
			Select Case My.Settings.MultiplicationDivision
				Case "Multiplication"
					For Each Sign As Label In SignList
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim NumeratorA As Label = NumeratorA_List(Index)
						Sign.Text = "×"
						NumeratorA.Text = MainRandom.Next(MinNumeratorMultiplication, MaxNumeratorMultiplication)
					Next
					For Each NumeratorB As Label In NumeratorB_List
						NumeratorB.Text = MainRandom.Next(MinNumeratorMultiplication, MaxNumeratorMultiplication)
					Next
					For Each DenominatorA As Label In DenominatorA_List
						DenominatorA.Text = MainRandom.Next(MinDenominatorMultiplication, MaxDenominatorMultiplication)
					Next

					For Each SignB As Label In DenominatorB_List
						SignB.Text = MainRandom.Next(MinDenominatorMultiplication, MaxDenominatorMultiplication)
					Next
				Case "Division"
					For Each Sign As Label In SignList
						Dim Index As Integer = SignList.IndexOf(Sign)
						Dim NumeratorA As Label = NumeratorA_List(Index)
						Sign.Text = "÷"
						NumeratorA.Text = MainRandom.Next(MinNumeratorDivision, MaxNumeratorDivision)
					Next
					For Each NumeratorB As Label In NumeratorB_List
						NumeratorB.Text = MainRandom.Next(MinNumeratorDivision, MaxNumeratorDivision)
					Next
					For Each DenominatorA As Label In DenominatorA_List
						DenominatorA.Text = MainRandom.Next(MinDenominatorDivision, MaxDenominatorDivision)
					Next

					For Each SignB As Label In DenominatorB_List
						SignB.Text = MainRandom.Next(MinDenominatorDivision, MaxDenominatorDivision)
					Next
				Case "Both"
					'This occurs for each problem.
					For Each CurrentSign As Label In SignList
						'This randomly selects which problems will be subtraction.
						Dim Int As Integer = MainRandom.Next(0, NewList2.Count - 1)
						CurrentSign.Text = NewList2.Item(Int)
						NewList2.RemoveAt(Int)
						Next
					For Each NumeratorA As Label In NumeratorA_List
						Dim Index As Integer = NumeratorA_List.IndexOf(NumeratorA)
						Dim CurrentSign As Label = SignList(Index)
						If CurrentSign.Text = "×" Then
							NumeratorA.Text = MainRandom.Next(MinNumeratorMultiplication, MaxNumeratorMultiplication)
						ElseIf CurrentSign.Text = "÷" Then
							NumeratorA.Text = MainRandom.Next(MinNumeratorDivision, MaxNumeratorDivision)
						End If
					Next
					For Each NumeratorB As Label In NumeratorB_List
						Dim Index As Integer = NumeratorB_List.IndexOf(NumeratorB)
						Dim CurrentSign As Label = SignList(Index)
						If CurrentSign.Text = "×" Then
							NumeratorB.Text = MainRandom.Next(MinDenominatorMultiplication, MaxDenominatorMultiplication)
						ElseIf CurrentSign.Text = "÷" Then
							NumeratorB.Text = MainRandom.Next(MinNumeratorDivision, MaxNumeratorDivision)
						End If
					Next
					For Each DenominatorA As Label In DenominatorA_List
						Dim Index As Integer = DenominatorA_List.IndexOf(DenominatorA)
						Dim CurrentSign As Label = SignList(Index)
						If CurrentSign.Text = "×" Then
							DenominatorA.Text = MainRandom.Next(MinDenominatorMultiplication, MaxDenominatorMultiplication)
						ElseIf CurrentSign.Text = "÷" Then
							DenominatorA.Text = MainRandom.Next(MinDenominatorDivision, MaxDenominatorDivision)
						End If
					Next
					For Each DenominatorB As Label In DenominatorB_List
						Dim Index As Integer = DenominatorB_List.IndexOf(DenominatorB)
						Dim CurrentSign As Label = SignList(Index)
						If CurrentSign.Text = "×" Then
							DenominatorB.Text = MainRandom.Next(MinDenominatorMultiplication, MaxDenominatorMultiplication)
						ElseIf CurrentSign.Text = "÷" Then
							DenominatorB.Text = MainRandom.Next(MinDenominatorDivision, MaxDenominatorDivision)
						End If
					Next
			End Select
		End If
	End Sub

	Private Sub CheckAnswers()
		Try
			'This represents all signs.
			Dim SignArray As Label() = {Sign1, Sign2, Sign3, Sign4, Sign5, Sign6, Sign7, Sign8, Sign9, Sign10, Sign11, Sign12, Sign13, Sign14, Sign15, Sign16, _
										Sign17, Sign18, Sign19, Sign20, Sign21, Sign22, Sign23, Sign24}
			'This represents all leftmost numerators.
			Dim NumeratorA_Array As Label() = {Numerator1a, Numerator2a, Numerator3a, Numerator4a, Numerator5a, Numerator6a, Numerator7a, Numerator8a, _
											   Numerator9a, Numerator10a, Numerator11a, Numerator12a, Numerator13a, Numerator14a, Numerator15a, Numerator16a, _
											   Numerator17a, Numerator18a, Numerator19a, Numerator20a, Numerator21a, Numerator22a, Numerator23a, Numerator24a}
			'This represents all rightmost numerators.
			Dim NumeratorB_Array As Label() = {Numerator1b, Numerator2b, Numerator3b, Numerator4b, Numerator5b, Numerator6b, Numerator7b, Numerator8b, _
											   Numerator9b, Numerator10b, Numerator11b, Numerator12b, Numerator13b, Numerator14b, Numerator15b, Numerator16b, _
											   Numerator17b, Numerator18b, Numerator19b, Numerator20b, Numerator21b, Numerator22b, Numerator23b, Numerator24b}
			'This represents all leftmost denominators.
			Dim DenominatorA_Array As Label() = {Denominator1a, Denominator2a, Denominator3a, Denominator4a, Denominator5a, Denominator6a, Denominator7a, Denominator8a, _
											   Denominator9a, Denominator10a, Denominator11a, Denominator12a, Denominator13a, Denominator14a, Denominator15a, Denominator16a, _
											   Denominator17a, Denominator18a, Denominator19a, Denominator20a, Denominator21a, Denominator22a, Denominator23a, Denominator24a}
			'This represents all rightmost denominators.
			Dim DenominatorB_Array As Label() = {Denominator1b, Denominator2b, Denominator3b, Denominator4b, Denominator5b, Denominator6b, Denominator7b, Denominator8b, _
											   Denominator9b, Denominator10b, Denominator11b, Denominator12b, Denominator13b, Denominator14b, Denominator15b, Denominator16b, _
											   Denominator17b, Denominator18b, Denominator19b, Denominator20b, Denominator21b, Denominator22b, Denominator23b, Denominator24b}
			'This initializes the values for all check marks.
			Dim CheckArray As Label() = {Check1, Check2, Check3, Check4, Check5, Check6, Check7, Check8, Check9, Check10, Check11, Check12, _
			Check13, Check14, Check15, Check16, Check17, Check18, Check19, Check20, Check21, Check22, Check23, Check24}
			'This represents all numerator answers.
			Dim NumeratorAnswerArray As TextBox() = {NumeratorAnswer1, NumeratorAnswer2, NumeratorAnswer3, NumeratorAnswer4, NumeratorAnswer5, NumeratorAnswer6, NumeratorAnswer7, NumeratorAnswer8, _
											   NumeratorAnswer9, NumeratorAnswer10, NumeratorAnswer11, NumeratorAnswer12, NumeratorAnswer13, NumeratorAnswer14, NumeratorAnswer15, NumeratorAnswer16, _
											   NumeratorAnswer17, NumeratorAnswer18, NumeratorAnswer19, NumeratorAnswer20, NumeratorAnswer21, NumeratorAnswer22, NumeratorAnswer23, NumeratorAnswer24}
			'This represents all denominator answers.
			Dim DenominatorAnswerArray As TextBox() = {DenominatorAnswer1, DenominatorAnswer2, DenominatorAnswer3, DenominatorAnswer4, DenominatorAnswer5, DenominatorAnswer6, DenominatorAnswer7, DenominatorAnswer8, _
									   DenominatorAnswer9, DenominatorAnswer10, DenominatorAnswer11, DenominatorAnswer12, DenominatorAnswer13, DenominatorAnswer14, DenominatorAnswer15, DenominatorAnswer16, _
									   DenominatorAnswer17, DenominatorAnswer18, DenominatorAnswer19, DenominatorAnswer20, DenominatorAnswer21, DenominatorAnswer22, DenominatorAnswer23, DenominatorAnswer24}
			Dim SignList As New List(Of Label)(SignArray)
			Dim NumeratorA_List As New List(Of Label)(NumeratorA_Array)
			Dim NumeratorB_List As New List(Of Label)(NumeratorB_Array)
			Dim DenominatorA_List As New List(Of Label)(DenominatorA_Array)
			Dim DenominatorB_List As New List(Of Label)(DenominatorB_Array)
			Dim NumeratorAnswerList As New List(Of TextBox)(NumeratorAnswerArray)
			Dim DenominatorAnswerList As New List(Of TextBox)(DenominatorAnswerArray)
			Dim CheckList As New List(Of Label)(CheckArray)
			'Disables the numerator answers from being edited.
			For Each Answer As TextBox In NumeratorAnswerList
				Answer.Enabled = False
			Next
			'Disables the denominator answers from being edited.
			For Each Answer As TextBox In DenominatorAnswerList
				Answer.Enabled = False
			Next
			SuspendLayout()
			For Each Sign As Label In SignList
				Dim Index As Integer = SignList.IndexOf(Sign)
				Dim NumeratorAnswer As TextBox = NumeratorAnswerList(Index)
				Dim DenominatorAnswer As TextBox = DenominatorAnswerList(Index)
				Dim NumeratorA As Label = NumeratorA_List(Index)
				Dim DenominatorA As Label = DenominatorA_List(Index)
				Dim NumeratorB As Label = NumeratorB_List(Index)
				Dim DenominatorB As Label = DenominatorB_List(Index)
				Dim CurrentCheckMark As Label = CheckList(Index)
				Dim Correct As Boolean = False
				Dim Value1ChangeAmount As Integer
				Dim Value2ChangeAmount As Integer
				Dim DenominatorValue As Integer
				Dim NumeratorValue As Integer
				Dim NumeratorAnswerValue As Integer
				Dim DenominatorAnswerValue As Integer
				Dim MaxDenominator As Integer = CInt(DenominatorA.Text) * CInt(DenominatorB.Text)
				If Not IsNumeric(NumeratorAnswer.Text) Or Not IsNumeric(DenominatorAnswer.Text) Then GoTo Check
				NumeratorAnswerValue = CInt(NumeratorAnswer.Text)
				DenominatorAnswerValue = CInt(DenominatorAnswer.Text)
				Select Case Sign.Text
					Case "+"
						For Value As Integer = 1 To MaxDenominator Step 1
							If Value Mod CInt(DenominatorA.Text) = 0 And Value Mod CInt(DenominatorB.Text) = 0 Then
								DenominatorValue = Value
								If CInt(DenominatorA.Text) <> 0 Then
									Value1ChangeAmount = Value / CInt(DenominatorA.Text)
								Else
									Value1ChangeAmount = 0
								End If
								If CInt(DenominatorB.Text) <> 0 Then
									Value2ChangeAmount = Value / CInt(DenominatorB.Text)
								Else
									Value2ChangeAmount = 0
								End If
								NumeratorValue = (CInt(NumeratorA.Text) * Value1ChangeAmount) + (CInt(NumeratorB.Text) * Value2ChangeAmount)
								Exit For
							End If
						Next
					Case "–"
						For Value As Integer = 1 To MaxDenominator Step 1
							If Value Mod CInt(DenominatorA.Text) = 0 And Value Mod CInt(DenominatorB.Text) = 0 Then
								DenominatorValue = Value
								If CInt(DenominatorA.Text) <> 0 Then
									Value1ChangeAmount = Value / CInt(DenominatorA.Text)
								Else
									Value1ChangeAmount = 0
								End If
								If CInt(DenominatorB.Text) <> 0 Then
									Value2ChangeAmount = Value / CInt(DenominatorB.Text)
								Else
									Value2ChangeAmount = 0
								End If
								NumeratorValue = (CInt(NumeratorA.Text) * Value1ChangeAmount) - (CInt(NumeratorB.Text) * Value2ChangeAmount)
								Exit For
							End If
						Next
					Case "×"
						NumeratorValue = CInt(NumeratorA.Text) * CInt(NumeratorB.Text)
						DenominatorValue = CInt(DenominatorA.Text) * CInt(DenominatorB.Text)
					Case "÷"
						NumeratorValue = CInt(NumeratorA.Text) * CInt(DenominatorB.Text)
						DenominatorValue = CInt(DenominatorA.Text) * CInt(NumeratorB.Text)
				End Select
				If NumeratorValue = 0 Then DenominatorValue = 0 'This changes the denominator to reflect all changes.
				'This reduces the answers.
				For Value As Integer = DenominatorValue To 1 Step -1
					If NumeratorValue Mod Value = 0 And DenominatorValue Mod Value = 0 Then
						NumeratorValue /= Value
						DenominatorValue /= Value
						Exit For
					End If
				Next
				'This will occur if ReduceRequired is set to false.
				If ReduceRequired = False Then
					For Value As Integer = DenominatorAnswerValue To 1 Step -1
						If NumeratorAnswerValue Mod Value = 0 And DenominatorAnswerValue Mod Value = 0 Then
							NumeratorAnswerValue /= Value
							DenominatorAnswerValue /= Value
							Exit For
						End If
					Next
				End If
				'This determines if the answer is correct.
				If NumeratorAnswerValue = NumeratorValue And DenominatorAnswerValue = DenominatorValue Then Correct = True
Check:
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
			Grade = Math.Round(CorrectAnswers / 24 * 100, MidpointRounding.AwayFromZero)
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

		Private Sub CompleteButton_Click(sender As Object, e As EventArgs) Handles CompleteButton.Click
		Complete()
	End Sub

	Private Sub Complete()
		Dim Blank As Boolean = False
		Dim AnswerArray As TextBox() = {NumeratorAnswer1, NumeratorAnswer2, NumeratorAnswer3, NumeratorAnswer4, NumeratorAnswer5, NumeratorAnswer6, NumeratorAnswer7, NumeratorAnswer8, NumeratorAnswer9, NumeratorAnswer10, NumeratorAnswer11, NumeratorAnswer12, _
			NumeratorAnswer13, NumeratorAnswer14, NumeratorAnswer15, NumeratorAnswer16, NumeratorAnswer17, NumeratorAnswer18, NumeratorAnswer19, NumeratorAnswer20, NumeratorAnswer21, NumeratorAnswer22, NumeratorAnswer23, NumeratorAnswer24, DenominatorAnswer1, DenominatorAnswer2, DenominatorAnswer3, DenominatorAnswer4, DenominatorAnswer5, DenominatorAnswer6, DenominatorAnswer7, DenominatorAnswer8, DenominatorAnswer9, DenominatorAnswer10, DenominatorAnswer11, DenominatorAnswer12, _
			DenominatorAnswer13, DenominatorAnswer14, DenominatorAnswer15, DenominatorAnswer16, DenominatorAnswer17, DenominatorAnswer18, DenominatorAnswer19, DenominatorAnswer20, DenominatorAnswer21, DenominatorAnswer22, DenominatorAnswer23, DenominatorAnswer24}
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
			Dim AnswerArray As TextBox() = {NumeratorAnswer1, NumeratorAnswer2, NumeratorAnswer3, NumeratorAnswer4, NumeratorAnswer5, NumeratorAnswer6, NumeratorAnswer7, NumeratorAnswer8, NumeratorAnswer9, NumeratorAnswer10, NumeratorAnswer11, NumeratorAnswer12, _
			NumeratorAnswer13, NumeratorAnswer14, NumeratorAnswer15, NumeratorAnswer16, NumeratorAnswer17, NumeratorAnswer18, NumeratorAnswer19, NumeratorAnswer20, NumeratorAnswer21, NumeratorAnswer22, NumeratorAnswer23, NumeratorAnswer24, DenominatorAnswer1, DenominatorAnswer2, DenominatorAnswer3, DenominatorAnswer4, DenominatorAnswer5, DenominatorAnswer6, DenominatorAnswer7, DenominatorAnswer8, DenominatorAnswer9, DenominatorAnswer10, DenominatorAnswer11, DenominatorAnswer12, _
			DenominatorAnswer13, DenominatorAnswer14, DenominatorAnswer15, DenominatorAnswer16, DenominatorAnswer17, DenominatorAnswer18, DenominatorAnswer19, DenominatorAnswer20, DenominatorAnswer21, DenominatorAnswer22, DenominatorAnswer23, DenominatorAnswer24}
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
			NumeratorAnswer1.Select()
			IncorrectAnswers = 0
			RedoIndicator.Show() 'Lets the user show that this is a previous assignment.
			ResumeLayout()
		End If

	End Sub

	Private Sub Exercises_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown, CompleteButton.KeyDown, Button1.KeyDown, _
	NumeratorAnswer1.KeyDown, NumeratorAnswer2.KeyDown, NumeratorAnswer3.KeyDown, NumeratorAnswer4.KeyDown, NumeratorAnswer5.KeyDown, NumeratorAnswer6.KeyDown, NumeratorAnswer7.KeyDown, _
	NumeratorAnswer8.KeyDown, NumeratorAnswer9.KeyDown, NumeratorAnswer10.KeyDown, NumeratorAnswer11.KeyDown, NumeratorAnswer12.KeyDown, NumeratorAnswer13.KeyDown, NumeratorAnswer14.KeyDown, _
	NumeratorAnswer15.KeyDown, NumeratorAnswer16.KeyDown, NumeratorAnswer17.KeyDown, NumeratorAnswer18.KeyDown, NumeratorAnswer19.KeyDown, NumeratorAnswer20.KeyDown, NumeratorAnswer21.KeyDown, _
	NumeratorAnswer22.KeyDown, NumeratorAnswer23.KeyDown, NumeratorAnswer24.KeyDown, DenominatorAnswer1.KeyDown, DenominatorAnswer2.KeyDown, DenominatorAnswer3.KeyDown, DenominatorAnswer4.KeyDown, _
	DenominatorAnswer5.KeyDown, DenominatorAnswer6.KeyDown, DenominatorAnswer7.KeyDown, DenominatorAnswer8.KeyDown, DenominatorAnswer9.KeyDown, DenominatorAnswer10.KeyDown, DenominatorAnswer11.KeyDown, _
	DenominatorAnswer12.KeyDown, DenominatorAnswer13.KeyDown, DenominatorAnswer14.KeyDown, DenominatorAnswer15.KeyDown, DenominatorAnswer16.KeyDown, DenominatorAnswer17.KeyDown, DenominatorAnswer18.KeyDown, _
	DenominatorAnswer19.KeyDown, DenominatorAnswer20.KeyDown, DenominatorAnswer21.KeyDown, DenominatorAnswer22.KeyDown, DenominatorAnswer23.KeyDown, DenominatorAnswer24.KeyDown
		If e.KeyCode = Keys.Escape And e.Shift AndAlso InputBox("Please enter the password.", "Enter Password (Auto-Helper)") = My.Settings.Password Then
			REDO = False
			Me.Close()
		End If
	End Sub

	Private Sub Item_Clicked(sender As Object, e As EventArgs) Handles Panel1.Click, Panel2.Click, Panel3.Click, Panel4.Click, _
		Panel5.Click, Panel6.Click, Panel7.Click, Panel8.Click, Panel9.Click, Panel10.Click, Panel11.Click, Panel12.Click, _
		Panel13.Click, Panel14.Click, Panel15.Click, Panel16.Click, Panel17.Click, Panel18.Click, Panel19.Click, Panel20.Click, _
		 Panel21.Click, Panel22.Click, Panel23.Click, Panel24.Click, MyBase.Click, Label1.Click
		If Timer1.Enabled Then
			Timer1.Enabled = False
			Me.Opacity = 100
			CompleteButton.Show()
			CompleteButton.Text = "&OK"
		End If
	End Sub

	Private Sub Answers_Changed(sender As Object, e As EventArgs) Handles NumeratorAnswer1.TextChanged, NumeratorAnswer2.TextChanged, NumeratorAnswer3.TextChanged, NumeratorAnswer4.TextChanged, NumeratorAnswer5.TextChanged, NumeratorAnswer6.TextChanged, NumeratorAnswer7.TextChanged, NumeratorAnswer8.TextChanged, NumeratorAnswer9.TextChanged, NumeratorAnswer10.TextChanged, NumeratorAnswer11.TextChanged, NumeratorAnswer12.TextChanged, _
				NumeratorAnswer13.TextChanged, NumeratorAnswer14.TextChanged, NumeratorAnswer15.TextChanged, NumeratorAnswer16.TextChanged, NumeratorAnswer17.TextChanged, NumeratorAnswer18.TextChanged, NumeratorAnswer19.TextChanged, NumeratorAnswer20.TextChanged, NumeratorAnswer21.TextChanged, NumeratorAnswer22.TextChanged, NumeratorAnswer23.TextChanged, NumeratorAnswer24.TextChanged, DenominatorAnswer1.TextChanged, _
				DenominatorAnswer2.TextChanged, DenominatorAnswer3.TextChanged, DenominatorAnswer4.TextChanged, DenominatorAnswer5.TextChanged, DenominatorAnswer6.TextChanged, DenominatorAnswer7.TextChanged, DenominatorAnswer8.TextChanged, DenominatorAnswer9.TextChanged, DenominatorAnswer10.TextChanged, DenominatorAnswer11.TextChanged, DenominatorAnswer12.TextChanged, _
				DenominatorAnswer13.TextChanged, DenominatorAnswer14.TextChanged, DenominatorAnswer15.TextChanged, DenominatorAnswer16.TextChanged, DenominatorAnswer17.TextChanged, DenominatorAnswer18.TextChanged, DenominatorAnswer19.TextChanged, DenominatorAnswer20.TextChanged, DenominatorAnswer21.TextChanged, DenominatorAnswer22.TextChanged, DenominatorAnswer23.TextChanged, DenominatorAnswer24.TextChanged
		If AutomaticComplete Then
			Timer2.Stop()
			Timer2.Start()
		End If
	End Sub

	Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
		'This initializes the values for all answers.
		Dim AnswerArray As TextBox() = {NumeratorAnswer1, NumeratorAnswer2, NumeratorAnswer3, NumeratorAnswer4, NumeratorAnswer5, NumeratorAnswer6, NumeratorAnswer7, NumeratorAnswer8, NumeratorAnswer9, NumeratorAnswer10, NumeratorAnswer11, NumeratorAnswer12, _
		NumeratorAnswer13, NumeratorAnswer14, NumeratorAnswer15, NumeratorAnswer16, NumeratorAnswer17, NumeratorAnswer18, NumeratorAnswer19, NumeratorAnswer20, NumeratorAnswer21, NumeratorAnswer22, NumeratorAnswer23, NumeratorAnswer24, DenominatorAnswer1, DenominatorAnswer2, DenominatorAnswer3, DenominatorAnswer4, DenominatorAnswer5, DenominatorAnswer6, DenominatorAnswer7, DenominatorAnswer8, DenominatorAnswer9, DenominatorAnswer10, DenominatorAnswer11, DenominatorAnswer12, _
		DenominatorAnswer13, DenominatorAnswer14, DenominatorAnswer15, DenominatorAnswer16, DenominatorAnswer17, DenominatorAnswer18, DenominatorAnswer19, DenominatorAnswer20, DenominatorAnswer21, DenominatorAnswer22, DenominatorAnswer23, DenominatorAnswer24}
		Dim AnswerList As New List(Of TextBox)(AnswerArray)	'This creates the answer list.
		For Each Answer As TextBox In AnswerList
			If Answer.Text = "" Then
				Timer2.Stop()
				Exit Sub
			End If
		Next
		Complete()
	End Sub

	Private Sub Panels_Paint(CurrentPanel As Object, e As PaintEventArgs) Handles Panel1.Paint, Panel2.Paint, Panel8.Paint, Panel7.Paint, Panel6.Paint, Panel5.Paint, Panel4.Paint, Panel3.Paint, Panel9.Paint, Panel16.Paint, Panel15.Paint, Panel14.Paint, Panel13.Paint, Panel12.Paint, Panel11.Paint, Panel10.Paint, Panel24.Paint, Panel23.Paint, Panel22.Paint, Panel21.Paint, Panel20.Paint, Panel19.Paint, Panel18.Paint, Panel17.Paint
		Dim NewPen As New Pen(NewForeColor)
		For Each Control As Control In CurrentPanel.controls
			If Microsoft.VisualBasic.Left(Control.Name, 9) = "Numerator" Then
				Dim MainPoint As New Point(Control.Location.X, 34)
				Dim MainPoint2 As New Point(Control.Location.X + Control.Width, 34)
				e.Graphics.DrawLine(NewPen, MainPoint, MainPoint2)
			End If
		Next
		NewPen.Dispose()
		e.Dispose()
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		AutoSketch.Show()
		AutoSketch.BringToFront()
	End Sub
End Class