Imports System.IO
Public Class Exercises
	Public Division As Boolean
	Public UserName As String
	Dim IncorrectAnswers As Integer = 0
	Dim Message As String
	Dim TextBox As Integer
	Dim Random As New Random
	Private Sub SendMessage()
		Const IsparkMessageLocation As String = "HKEY_Current_User\Software\Autosoft\ISpark\login"
		Dim FileLocation As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\" & "Autosoft\ISpark\Messages\Christopher\"
		Dim NewMessage As String = FileLocation & "MFH Grade - " & UserName & ".html"
		Dim NewMessageData As String = Message
		Dim NewName As String = "MFH Grade - " & UserName
		Try
			If File.Exists(FileLocation & "MFH Grade - " & UserName & ".html") Then
				For Int As Integer = 1 To 1000000 Step 1
					If File.Exists(FileLocation & "MFH Grade - " & UserName & Int.ToString & ".html") = False Then
						NewMessage = FileLocation & "MFH Grade - " & UserName & Int.ToString & ".html"
						NewName = "MFH Grade - " & UserName & Int.ToString()
						Exit For
					Else
						If Int = 1000000 Then MessageBox.Show("The Automatic Messaging System (""AMS"") failed to work correctly. If the problem persists, please contact Autosoft Corporation. We are very sorry for the inconvenience.", "Error - Exchange", MessageBoxButtons.OK, MessageBoxIcon.Error)
					End If
				Next
			End If
			File.WriteAllText(FileLocation & NewName & ".html", NewMessageData)
			My.Computer.Registry.SetValue(IsparkMessageLocation, "CMC" & NewName & "Name", "AMS")
			My.Computer.Registry.SetValue(IsparkMessageLocation, "CMC" & NewName & "UserName", "AMS@ispark.com")
		Catch ex As Exception
			MessageBox.Show("The Automatic Messaging System (""AMS"") failed to work correctly. If the problem persists, please contact Autosoft Corporation. We are very sorry for the inconvenience.", "Error - Exchange", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub Exercises_Load(sender As Object, e As EventArgs) Handles Me.Load

		If Division = False Then

			Factor1a.Text = Random.Next(13)
			Factor2a.Text = Random.Next(13)
			Factor3a.Text = Random.Next(13)
			Factor4a.Text = Random.Next(13)
			Factor5a.Text = Random.Next(13)
			Factor6a.Text = Random.Next(13)
			Factor7a.Text = Random.Next(13)
			Factor8a.Text = Random.Next(13)
			Factor9a.Text = Random.Next(13)
			Factor10a.Text = Random.Next(13)
			Factor11a.Text = Random.Next(13)
			Factor12a.Text = Random.Next(13)
			Factor13a.Text = Random.Next(13)
			Factor14a.Text = Random.Next(13)
			Factor15a.Text = Random.Next(13)
			Factor16a.Text = Random.Next(13)
			Factor17a.Text = Random.Next(13)
			Factor18a.Text = Random.Next(13)
			Factor19a.Text = Random.Next(13)
			Factor20a.Text = Random.Next(13)
			Factor21a.Text = Random.Next(13)
			Factor22a.Text = Random.Next(13)
			Factor23a.Text = Random.Next(13)
			Factor24a.Text = Random.Next(13)

			Factor1b.Text = Random.Next(13)
			Factor2b.Text = Random.Next(13)
			Factor3b.Text = Random.Next(13)
			Factor4b.Text = Random.Next(13)
			Factor5b.Text = Random.Next(13)
			Factor6b.Text = Random.Next(13)
			Factor7b.Text = Random.Next(13)
			Factor8b.Text = Random.Next(13)
			Factor9b.Text = Random.Next(13)
			Factor10b.Text = Random.Next(13)
			Factor11b.Text = Random.Next(13)
			Factor12b.Text = Random.Next(13)
			Factor13b.Text = Random.Next(13)
			Factor14b.Text = Random.Next(13)
			Factor15b.Text = Random.Next(13)
			Factor16b.Text = Random.Next(13)
			Factor17b.Text = Random.Next(13)
			Factor18b.Text = Random.Next(13)
			Factor19b.Text = Random.Next(13)
			Factor20b.Text = Random.Next(13)
			Factor21b.Text = Random.Next(13)
			Factor22b.Text = Random.Next(13)
			Factor23b.Text = Random.Next(13)
			Factor24b.Text = Random.Next(13)

		Else
			Dim Result As Integer
			Dim Result2 As Integer
			Dim Zero As Boolean = False
			Sign1.Text = "÷"
			Sign2.Text = "÷"
			Sign3.Text = "÷"
			Sign4.Text = "÷"
			Sign5.Text = "÷"
			Sign6.Text = "÷"
			Sign7.Text = "÷"
			Sign8.Text = "÷"
			Sign9.Text = "÷"
			Sign10.Text = "÷"
			Sign11.Text = "÷"
			Sign12.Text = "÷"
			Sign13.Text = "÷"
			Sign14.Text = "÷"
			Sign15.Text = "÷"
			Sign16.Text = "÷"
			Sign17.Text = "÷"
			Sign18.Text = "÷"
			Sign19.Text = "÷"
			Sign20.Text = "÷"
			Sign21.Text = "÷"
			Sign22.Text = "÷"
			Sign23.Text = "÷"
			Sign24.Text = "÷"
			'Answer 1
			Result = Random.Next(145)
			Result2 = Random.Next(0, 13)
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						Result2 = Random.Next(0, 13)
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						Result2 = Random.Next(0, 13)
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor1a.Text = Result
				Factor1b.Text = Result2
			Else
				Factor1a.Text = Result2
				Factor1b.Text = Result
			End If
			If Factor1b.Text = 0 Then Zero = True
			'Answer 2
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor2a.Text = Result
				Factor2b.Text = Result2

			Else
				Factor2a.Text = Result2
				Factor2b.Text = Result
			End If
			If Factor2b.Text = 0 Then Zero = True
			'Answer 3
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor3a.Text = Result
				Factor3b.Text = Result2

			Else
				Factor3a.Text = Result2
				Factor3b.Text = Result
			End If
			If Factor3b.Text = 0 Then Zero = True
			'Answer 4
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor4a.Text = Result
				Factor4b.Text = Result2

			Else
				Factor4a.Text = Result2
				Factor4b.Text = Result
			End If
			If Factor4b.Text = 0 Then Zero = True
			'Answer 5
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor5a.Text = Result
				Factor5b.Text = Result2

			Else
				Factor5a.Text = Result2
				Factor5b.Text = Result
			End If
			If Factor5b.Text = 0 Then Zero = True
			'Answer 6
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor6a.Text = Result
				Factor6b.Text = Result2

			Else
				Factor6a.Text = Result2
				Factor6b.Text = Result
			End If
			If Factor7b.Text = 0 Then Zero = True
			'Answer 7
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor7a.Text = Result
				Factor7b.Text = Result2

			Else
				Factor7a.Text = Result2
				Factor7b.Text = Result
			End If
			If Factor7b.Text = 0 Then Zero = True
			'Answer 8
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor8a.Text = Result
				Factor8b.Text = Result2

			Else
				Factor8a.Text = Result2
				Factor8b.Text = Result
			End If
			If Factor8b.Text = 0 Then Zero = True
			'Answer 9
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor9a.Text = Result
				Factor9b.Text = Result2

			Else
				Factor9a.Text = Result2
				Factor9b.Text = Result
			End If
			If Factor9b.Text = 0 Then Zero = True
			'Answer 10
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor10a.Text = Result
				Factor10b.Text = Result2

			Else
				Factor10a.Text = Result2
				Factor10b.Text = Result
			End If
			If Factor10b.Text = 0 Then Zero = True
			'Answer 11
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor11a.Text = Result
				Factor11b.Text = Result2

			Else
				Factor11a.Text = Result2
				Factor11b.Text = Result
			End If
			If Factor11b.Text = 0 Then Zero = True
			'Answer 12
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor12a.Text = Result
				Factor12b.Text = Result2

			Else
				Factor12a.Text = Result2
				Factor12b.Text = Result
			End If
			If Factor12b.Text = 0 Then Zero = True
			'Answer 13
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor13a.Text = Result
				Factor13b.Text = Result2

			Else
				Factor13a.Text = Result2
				Factor13b.Text = Result
			End If
			If Factor13b.Text = 0 Then Zero = True
			'Answer 14
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor14a.Text = Result
				Factor14b.Text = Result2

			Else
				Factor14a.Text = Result2
				Factor14b.Text = Result
			End If
			If Factor14b.Text = 0 Then Zero = True
			'Answer 15
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor15a.Text = Result
				Factor15b.Text = Result2

			Else
				Factor15a.Text = Result2
				Factor15b.Text = Result
			End If
			If Factor15b.Text = 0 Then Zero = True
			'Answer 16
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor16a.Text = Result
				Factor16b.Text = Result2

			Else
				Factor16a.Text = Result2
				Factor16b.Text = Result
			End If
			If Factor16b.Text = 0 Then Zero = True
			'Answer 17
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor17a.Text = Result
				Factor17b.Text = Result2

			Else
				Factor17a.Text = Result2
				Factor17b.Text = Result
			End If
			If Factor17b.Text = 0 Then Zero = True
			'Answer 18
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor18a.Text = Result
				Factor18b.Text = Result2

			Else
				Factor18a.Text = Result2
				Factor18b.Text = Result
			End If
			If Factor18b.Text = 0 Then Zero = True
			'Answer 19
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor19a.Text = Result
				Factor19b.Text = Result2

			Else
				Factor19a.Text = Result2
				Factor19b.Text = Result
			End If
			If Factor15b.Text = 0 Then Zero = True
			'Answer 20
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor20a.Text = Result
				Factor20b.Text = Result2

			Else
				Factor20a.Text = Result2
				Factor20b.Text = Result
			End If
			If Factor20b.Text = 0 Then Zero = True
			'Answer 21
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor21a.Text = Result
				Factor21b.Text = Result2

			Else
				Factor21a.Text = Result2
				Factor21b.Text = Result
			End If
			If Factor21b.Text = 0 Then Zero = True
			'Answer 22
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor22a.Text = Result
				Factor22b.Text = Result2

			Else
				Factor22a.Text = Result2
				Factor22b.Text = Result
			End If
			If Factor22b.Text = 0 Then Zero = True
			'Answer 23
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor23a.Text = Result
				Factor23b.Text = Result2

			Else
				Factor23a.Text = Result2
				Factor23b.Text = Result
			End If
			If Factor23b.Text = 0 Then Zero = True
			'Answer 24
			Result = Random.Next(145)
			If Zero = True Then
				Result2 = Random.Next(1, 13)
			Else
				Result2 = Random.Next(13)
			End If
			If Result >= Result2 Then
				If Result2 <> 0 Then
					Dim Value As Integer = Result Mod Result2
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			Else
				If Result <> 0 Then
					Dim Value As Integer = Result2 Mod Result
					While Value > 0
						Result = Random.Next(145)
						If Zero = True Then
							Result2 = Random.Next(1, 13)
						Else
							Result2 = Random.Next(13)
						End If
						If Result >= Result2 Then
							If Result2 <> 0 Then
								Value = Result Mod Result2
							Else
								Exit While
							End If
						Else
							If Result <> 0 Then
								Value = Result2 Mod Result
							Else
								Exit While
							End If
						End If
					End While
				End If
			End If
			If Result >= Result2 Then
				Factor24a.Text = Result
				Factor24b.Text = Result2

			Else
				Factor24a.Text = Result2
				Factor24b.Text = Result
			End If

		End If
	End Sub

	Private Sub CheckAnswers()
		Message = "<!DOCTYPE HTML> <html> <head> <title>Details - Exchange</title> </head> <body style = ""font-family: Calibri; font-size: 12px""> <h2 style = ""color:green"">" _
		  & "Exchange </h2> <br><b>Exchange Multiplication Facts Helper</b><br><br> User: " & UserName & "<br><br> Date: " & DateTime.Today & "<br><br> Time: " & DateAndTime.TimeOfDay _
		  & "<br><br> Total Length:" & " " & Hours.Text & Label9.Text & Minutes.Text & Label13.Text & Seconds.Text & "<br><br>"
		Dim Answer As String
		IncorrectAnswers = 0
		If Division = False Then
			Answer = Convert.ToInt32(Factor1a.Text) * Convert.ToInt32(Factor1b.Text)
			Message &= "<br>1: "
			Message &= Factor1a.Text
			Message &= " × "
			Message &= Factor1b.Text
			Message &= " = "
			Message &= TextBox1.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox1.Text <> Answer Then
				Check1.ForeColor = Color.DarkRed
				Check1.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor2a.Text) * Convert.ToInt32(Factor2b.Text)
			Message &= "<br>2: "
			Message &= Factor2a.Text
			Message &= " × "
			Message &= Factor2b.Text
			Message &= " = "
			Message &= TextBox2.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox2.Text <> Answer Then
				Check2.ForeColor = Color.DarkRed
				Check2.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor3a.Text) * Convert.ToInt32(Factor3b.Text)
			Message &= "<br>3: "
			Message &= Factor3a.Text
			Message &= " × "
			Message &= Factor3b.Text
			Message &= " = "
			Message &= TextBox3.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox3.Text <> Answer Then
				Check3.ForeColor = Color.DarkRed
				Check3.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor4a.Text) * Convert.ToInt32(Factor4b.Text)
			Message &= "<br>4: "
			Message &= Factor4a.Text
			Message &= " × "
			Message &= Factor4b.Text
			Message &= " = "
			Message &= TextBox4.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox4.Text <> Answer Then
				Check4.ForeColor = Color.DarkRed
				Check4.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor5a.Text) * Convert.ToInt32(Factor5b.Text)
			Message &= "<br>5: "
			Message &= Factor5a.Text
			Message &= " × "
			Message &= Factor5b.Text
			Message &= " = "
			Message &= TextBox5.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox5.Text <> Answer Then
				Check5.ForeColor = Color.DarkRed
				Check5.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor6a.Text) * Convert.ToInt32(Factor6b.Text)
			Message &= "<br>6: "
			Message &= Factor6a.Text
			Message &= " × "
			Message &= Factor6b.Text
			Message &= " = "
			Message &= TextBox6.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox6.Text <> Answer Then
				Check6.ForeColor = Color.DarkRed
				Check6.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor7a.Text) * Convert.ToInt32(Factor7b.Text)
			Message &= "<br>7: "
			Message &= Factor7a.Text
			Message &= " × "
			Message &= Factor7b.Text
			Message &= " = "
			Message &= TextBox7.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox7.Text <> Answer Then
				Check7.ForeColor = Color.DarkRed
				Check7.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Message &= "<br>8: "
			Answer = Convert.ToInt32(Factor8a.Text) * Convert.ToInt32(Factor8b.Text)
			Message &= Factor8a.Text
			Message &= " × "
			Message &= Factor8b.Text
			Message &= " = "
			Message &= TextBox8.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox8.Text <> Answer Then
				Check8.ForeColor = Color.DarkRed
				Check8.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor9a.Text) * Convert.ToInt32(Factor9b.Text)
			Message &= "<br>9: "
			Message &= Factor9a.Text
			Message &= " × "
			Message &= Factor9b.Text
			Message &= " = "
			Message &= TextBox9.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox9.Text <> Answer Then
				Check9.ForeColor = Color.DarkRed
				Check9.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor10a.Text) * Convert.ToInt32(Factor10b.Text)
			Message &= "<br>10: "
			Message &= Factor10a.Text
			Message &= " × "
			Message &= Factor10b.Text
			Message &= " = "
			Message &= TextBox10.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox10.Text <> Answer Then
				Check10.ForeColor = Color.DarkRed
				Check10.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor11a.Text) * Convert.ToInt32(Factor11b.Text)
			Message &= "<br>11: "
			Message &= Factor11a.Text
			Message &= " × "
			Message &= Factor11b.Text
			Message &= " = "
			Message &= TextBox11.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox11.Text <> Answer Then
				Check11.ForeColor = Color.DarkRed
				Check11.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor12a.Text) * Convert.ToInt32(Factor12b.Text)
			Message &= "<br>12: "
			Message &= Factor12a.Text
			Message &= " × "
			Message &= Factor12b.Text
			Message &= " = "
			Message &= TextBox12.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox12.Text <> Answer Then
				Check12.ForeColor = Color.DarkRed
				Check12.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor13a.Text) * Convert.ToInt32(Factor13b.Text)
			Message &= "<br>13: "
			Message &= Factor13a.Text
			Message &= " × "
			Message &= Factor13b.Text
			Message &= " = "
			Message &= TextBox13.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox13.Text <> Answer Then
				Check13.ForeColor = Color.DarkRed
				Check13.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor14a.Text) * Convert.ToInt32(Factor14b.Text)
			Message &= "<br>14: "
			Message &= Factor14a.Text
			Message &= " × "
			Message &= Factor14b.Text
			Message &= " = "
			Message &= TextBox14.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox14.Text <> Answer Then
				Check14.ForeColor = Color.DarkRed
				Check14.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor15a.Text) * Convert.ToInt32(Factor15b.Text)
			Message &= "<br>15: "
			Message &= Factor15a.Text
			Message &= " × "
			Message &= Factor15b.Text
			Message &= " = "
			Message &= TextBox15.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox15.Text <> Answer Then
				Check15.ForeColor = Color.DarkRed
				Check15.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor16a.Text) * Convert.ToInt32(Factor16b.Text)
			Message &= "<br>16: "
			Message &= Factor16a.Text
			Message &= " × "
			Message &= Factor16b.Text
			Message &= " = "
			Message &= TextBox16.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox16.Text <> Answer Then
				Check16.ForeColor = Color.DarkRed
				Check16.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor17a.Text) * Convert.ToInt32(Factor17b.Text)
			Message &= "<br>17: "
			Message &= Factor17a.Text
			Message &= " × "
			Message &= Factor17b.Text
			Message &= " = "
			Message &= TextBox17.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox17.Text <> Answer Then
				Check17.ForeColor = Color.DarkRed
				Check17.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor18a.Text) * Convert.ToInt32(Factor18b.Text)
			Message &= "<br>18: "
			Message &= Factor18a.Text
			Message &= " × "
			Message &= Factor18b.Text
			Message &= " = "
			Message &= TextBox18.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox18.Text <> Answer Then
				Check18.ForeColor = Color.DarkRed
				Check18.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor19a.Text) * Convert.ToInt32(Factor19b.Text)
			Message &= "<br>19: "
			Message &= Factor19a.Text
			Message &= " × "
			Message &= Factor19b.Text
			Message &= " = "
			Message &= TextBox19.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox19.Text <> Answer Then
				Check19.ForeColor = Color.DarkRed
				Check19.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor20a.Text) * Convert.ToInt32(Factor20b.Text)
			Message &= "<br>20: "
			Message &= Factor20a.Text
			Message &= " × "
			Message &= Factor20b.Text
			Message &= " = "
			Message &= TextBox20.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox20.Text <> Answer Then
				Check20.ForeColor = Color.DarkRed
				Check20.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor21a.Text) * Convert.ToInt32(Factor21b.Text)
			Message &= "<br>21: "
			Message &= Factor21a.Text
			Message &= " × "
			Message &= Factor21b.Text
			Message &= " = "
			Message &= TextBox21.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox21.Text <> Answer Then
				Check21.ForeColor = Color.DarkRed
				Check21.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor22a.Text) * Convert.ToInt32(Factor22b.Text)
			Message &= "<br>22: "
			Message &= Factor22a.Text
			Message &= " × "
			Message &= Factor22b.Text
			Message &= " = "
			Message &= TextBox22.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox22.Text <> Answer Then
				Check22.ForeColor = Color.DarkRed
				Check22.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor23a.Text) * Convert.ToInt32(Factor23b.Text)
			Message &= "<br>23: "
			Message &= Factor23a.Text
			Message &= " × "
			Message &= Factor23b.Text
			Message &= " = "
			Message &= TextBox23.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox23.Text <> Answer Then
				Check23.ForeColor = Color.DarkRed
				Check23.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If
			Answer = Convert.ToInt32(Factor24a.Text) * Convert.ToInt32(Factor24b.Text)
			Message &= "<br>24: "
			Message &= Factor24a.Text
			Message &= " × "
			Message &= Factor24b.Text
			Message &= " = "
			Message &= TextBox24.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox24.Text <> Answer Then
				Check24.ForeColor = Color.DarkRed
				Check24.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			Else
				Message &= "CORRECT"
			End If


		Else
			Answer = Convert.ToInt32(Factor1a.Text) / Convert.ToInt32(Factor1b.Text)
			Message &= "<br>1: "
			Message &= Factor1a.Text
			Message &= " ÷ "
			Message &= Factor1b.Text
			Message &= " = "
			Message &= TextBox1.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox1.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox1.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check1.ForeColor = Color.DarkRed
				Check1.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor2a.Text) / Convert.ToInt32(Factor2b.Text)
			Message &= "<br>2: "
			Message &= Factor2a.Text
			Message &= " ÷ "
			Message &= Factor2b.Text
			Message &= " = "
			Message &= TextBox2.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox2.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox2.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check2.ForeColor = Color.DarkRed
				Check2.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor3a.Text) / Convert.ToInt32(Factor3b.Text)
			Message &= "<br>3: "
			Message &= Factor3a.Text
			Message &= " ÷ "
			Message &= Factor3b.Text
			Message &= " = "
			Message &= TextBox3.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox3.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox3.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check3.ForeColor = Color.DarkRed
				Check3.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor4a.Text) / Convert.ToInt32(Factor4b.Text)
			Message &= "<br>4: "
			Message &= Factor4a.Text
			Message &= " ÷ "
			Message &= Factor4b.Text
			Message &= " = "
			Message &= TextBox4.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox4.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox4.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check4.ForeColor = Color.DarkRed
				Check4.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor5a.Text) / Convert.ToInt32(Factor5b.Text)
			Message &= "<br>5: "
			Message &= Factor5a.Text
			Message &= " ÷ "
			Message &= Factor5b.Text
			Message &= " = "
			Message &= TextBox5.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox5.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox5.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check5.ForeColor = Color.DarkRed
				Check5.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor6a.Text) / Convert.ToInt32(Factor6b.Text)
			Message &= "<br>6: "
			Message &= Factor6a.Text
			Message &= " ÷ "
			Message &= Factor6b.Text
			Message &= " = "
			Message &= TextBox6.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox6.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox6.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check6.ForeColor = Color.DarkRed
				Check6.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor7a.Text) / Convert.ToInt32(Factor7b.Text)
			Message &= "<br>7: "
			Message &= Factor7a.Text
			Message &= " ÷ "
			Message &= Factor7b.Text
			Message &= " = "
			Message &= TextBox7.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox7.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox7.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check7.ForeColor = Color.DarkRed
				Check7.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Message &= "<br>8: "
			Answer = Convert.ToInt32(Factor8a.Text) / Convert.ToInt32(Factor8b.Text)
			Message &= Factor8a.Text
			Message &= " ÷ "
			Message &= Factor8b.Text
			Message &= " = "
			Message &= TextBox8.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox8.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox8.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check8.ForeColor = Color.DarkRed
				Check8.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor9a.Text) / Convert.ToInt32(Factor9b.Text)
			Message &= "<br>9: "
			Message &= Factor9a.Text
			Message &= " ÷ "
			Message &= Factor9b.Text
			Message &= " = "
			Message &= TextBox9.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox9.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox9.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check9.ForeColor = Color.DarkRed
				Check9.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor10a.Text) / Convert.ToInt32(Factor10b.Text)
			Message &= "<br>10: "
			Message &= Factor10a.Text
			Message &= " ÷ "
			Message &= Factor10b.Text
			Message &= " = "
			Message &= TextBox10.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox10.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox10.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check10.ForeColor = Color.DarkRed
				Check10.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor11a.Text) / Convert.ToInt32(Factor11b.Text)
			Message &= "<br>11: "
			Message &= Factor11a.Text
			Message &= " ÷ "
			Message &= Factor11b.Text
			Message &= " = "
			Message &= TextBox11.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox11.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox11.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check11.ForeColor = Color.DarkRed
				Check11.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor12a.Text) / Convert.ToInt32(Factor12b.Text)
			Message &= "<br>12: "
			Message &= Factor12a.Text
			Message &= " ÷ "
			Message &= Factor12b.Text
			Message &= " = "
			Message &= TextBox12.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox12.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox12.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check12.ForeColor = Color.DarkRed
				Check12.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor13a.Text) / Convert.ToInt32(Factor13b.Text)
			Message &= "<br>13: "
			Message &= Factor13a.Text
			Message &= " ÷ "
			Message &= Factor13b.Text
			Message &= " = "
			Message &= TextBox13.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox13.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox13.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check13.ForeColor = Color.DarkRed
				Check13.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor14a.Text) / Convert.ToInt32(Factor14b.Text)
			Message &= "<br>14: "
			Message &= Factor14a.Text
			Message &= " ÷ "
			Message &= Factor14b.Text
			Message &= " = "
			Message &= TextBox14.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox14.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox14.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check14.ForeColor = Color.DarkRed
				Check14.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor15a.Text) / Convert.ToInt32(Factor15b.Text)
			Message &= "<br>15: "
			Message &= Factor15a.Text
			Message &= " ÷ "
			Message &= Factor15b.Text
			Message &= " = "
			Message &= TextBox15.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox15.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox15.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check15.ForeColor = Color.DarkRed
				Check15.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor16a.Text) / Convert.ToInt32(Factor16b.Text)
			Message &= "<br>16: "
			Message &= Factor16a.Text
			Message &= " ÷ "
			Message &= Factor16b.Text
			Message &= " = "
			Message &= TextBox16.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox16.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox16.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check16.ForeColor = Color.DarkRed
				Check16.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor17a.Text) / Convert.ToInt32(Factor17b.Text)
			Message &= "<br>17: "
			Message &= Factor17a.Text
			Message &= " ÷ "
			Message &= Factor17b.Text
			Message &= " = "
			Message &= TextBox17.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox17.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox17.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check17.ForeColor = Color.DarkRed
				Check17.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor18a.Text) / Convert.ToInt32(Factor18b.Text)
			Message &= "<br>18: "
			Message &= Factor18a.Text
			Message &= " ÷ "
			Message &= Factor18b.Text
			Message &= " = "
			Message &= TextBox18.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox18.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox18.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check18.ForeColor = Color.DarkRed
				Check18.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor19a.Text) / Convert.ToInt32(Factor19b.Text)
			Message &= "<br>19: "
			Message &= Factor19a.Text
			Message &= " ÷ "
			Message &= Factor19b.Text
			Message &= " = "
			Message &= TextBox19.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox19.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox19.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check19.ForeColor = Color.DarkRed
				Check19.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor20a.Text) / Convert.ToInt32(Factor20b.Text)
			Message &= "<br>20: "
			Message &= Factor20a.Text
			Message &= " ÷ "
			Message &= Factor20b.Text
			Message &= " = "
			Message &= TextBox20.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox20.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox20.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check20.ForeColor = Color.DarkRed
				Check20.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor21a.Text) / Convert.ToInt32(Factor21b.Text)
			Message &= "<br>21: "
			Message &= Factor21a.Text
			Message &= " ÷ "
			Message &= Factor21b.Text
			Message &= " = "
			Message &= TextBox21.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox21.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox21.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check21.ForeColor = Color.DarkRed
				Check21.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor22a.Text) / Convert.ToInt32(Factor22b.Text)
			Message &= "<br>22: "
			Message &= Factor22a.Text
			Message &= " ÷ "
			Message &= Factor22b.Text
			Message &= " = "
			Message &= TextBox22.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox22.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox22.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check22.ForeColor = Color.DarkRed
				Check22.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor23a.Text) / Convert.ToInt32(Factor23b.Text)
			Message &= "<br>23: "
			Message &= Factor23a.Text
			Message &= " ÷ "
			Message &= Factor23b.Text
			Message &= " = "
			Message &= TextBox23.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox23.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox23.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check23.ForeColor = Color.DarkRed
				Check23.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If
			Answer = Convert.ToInt32(Factor24a.Text) / Convert.ToInt32(Factor24b.Text)
			Message &= "<br>24: "
			Message &= Factor24a.Text
			Message &= " ÷ "
			Message &= Factor24b.Text
			Message &= " = "
			Message &= TextBox24.Text
			Message &= "&nbsp;&nbsp;&nbsp;&nbsp;"
			If TextBox24.Text = Answer Then
				Message &= "CORRECT"
			ElseIf Answer = "Infinity" And TextBox24.Text.ToUpper = "Undefined".ToUpper Then
				Message &= "CORRECT"
			Else
				Check24.ForeColor = Color.DarkRed
				Check24.Text = ""
				IncorrectAnswers += 1
				Message &= "INCORRECT"
			End If

		End If
	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		Try
			Timer1.Enabled = False
			Button5.Visible = False
			Button1.Visible = False
			CheckAnswers()
			Check1.Visible = True
			Check2.Visible = True
			Check3.Visible = True
			Check4.Visible = True
			Check5.Visible = True
			Check6.Visible = True
			Check7.Visible = True
			Check8.Visible = True
			Check9.Visible = True
			Check10.Visible = True
			Check11.Visible = True
			Check12.Visible = True
			Check13.Visible = True
			Check14.Visible = True
			Check15.Visible = True
			Check16.Visible = True
			Check17.Visible = True
			Check18.Visible = True
			Check19.Visible = True
			Check20.Visible = True
			Check21.Visible = True
			Check22.Visible = True
			Check23.Visible = True
			Check24.Visible = True
			TextBox1.Enabled = False
			TextBox2.Enabled = False
			TextBox3.Enabled = False
			TextBox4.Enabled = False
			TextBox5.Enabled = False
			TextBox6.Enabled = False
			TextBox7.Enabled = False
			TextBox8.Enabled = False
			TextBox9.Enabled = False
			TextBox10.Enabled = False
			TextBox11.Enabled = False
			TextBox12.Enabled = False
			TextBox13.Enabled = False
			TextBox14.Enabled = False
			TextBox15.Enabled = False
			TextBox16.Enabled = False
			TextBox17.Enabled = False
			TextBox18.Enabled = False
			TextBox19.Enabled = False
			TextBox20.Enabled = False
			TextBox21.Enabled = False
			TextBox22.Enabled = False
			TextBox23.Enabled = False
			TextBox24.Enabled = False
			Dim Total As String = (24 - IncorrectAnswers) / 24 * 100

			Message &= "<br><br> <h3> Grade: " & Total & "</h3>"

			If Total <= 75 Then
				Message &= "<br><h3 style = ""color:darkred""> This assignment should be re-done. </h3>"
			ElseIf Total >= 90 Then
				Message &= "<br><h3 style = ""color:darkgreen""> Congratulations! The student" & " (" & UserName & ") " & "has excelled! </h3>"
			Else
				Message &= "<br><h3 style = ""color:darkgreen""> This assignment has been completed successfully. </h3>"
			End If
			Message &= "</body>"

			Message &= "</html>"
			SendMessage()
			Grade.Text = Total
			GroupBox1.Visible = True
			Label4.Visible = True
			CloseButton.Visible = True

		Catch ex As DivideByZeroException

		Catch ex As Exception
			MessageBox.Show("An error has occurred. The quiz could not be graded. We are very sorry for the inconvenience. If the problem persists, please contact Autosoft Corporation. We are very sorry for the inconvenience.", "Error - Exchange", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Me.Close()
		End Try
	End Sub

	Private Sub Close_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
		Me.Close()
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		If Seconds.Text <> 59 Then
			Seconds.Text += 1
			If Seconds.Text < 10 Then Seconds.Text = "0" & Seconds.Text
		Else
			Seconds.Text = "00"
			If Minutes.Text <> 59 Then
				Minutes.Text += 1
				If Minutes.Text < 10 Then Minutes.Text = "0" & Minutes.Text
			Else
				Minutes.Text = "00"
				Hours.Text += 1
			End If
		End If
	End Sub


	Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, _
		TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox8.KeyPress, _
		TextBox9.KeyPress, TextBox10.KeyPress, TextBox11.KeyPress, TextBox12.KeyPress, TextBox13.KeyPress, TextBox14.KeyPress, _
		TextBox15.KeyPress, TextBox16.KeyPress, TextBox17.KeyPress, TextBox18.KeyPress, TextBox19.KeyPress, TextBox20.KeyPress, _
		TextBox21.KeyPress, TextBox22.KeyPress, TextBox23.KeyPress, TextBox24.KeyPress
		If e.KeyChar = "." Then e.Handled = True
	End Sub


	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If TextBox = 1 Then
			TextBox1.Text = "Undefined"
			TextBox2.Focus()
		ElseIf TextBox = 2 Then
			TextBox2.Text = "Undefined"
			TextBox3.Focus()
		ElseIf TextBox = 3 Then
			TextBox3.Text = "Undefined"
			TextBox4.Focus()
		ElseIf TextBox = 4 Then
			TextBox4.Text = "Undefined"
			TextBox5.Focus()
		ElseIf TextBox = 5 Then
			TextBox5.Text = "Undefined"
			TextBox6.Focus()
		ElseIf TextBox = 6 Then
			TextBox6.Text = "Undefined"
			TextBox7.Focus()
		ElseIf TextBox = 7 Then
			TextBox7.Text = "Undefined"
			TextBox8.Focus()
		ElseIf TextBox = 8 Then
			TextBox8.Text = "Undefined"
			TextBox9.Focus()
		ElseIf TextBox = 9 Then
			TextBox9.Text = "Undefined"
			TextBox10.Focus()
		ElseIf TextBox = 10 Then
			TextBox10.Text = "Undefined"
			TextBox11.Focus()
		ElseIf TextBox = 11 Then
			TextBox11.Text = "Undefined"
			TextBox12.Focus()
		ElseIf TextBox = 12 Then
			TextBox12.Text = "Undefined"
			TextBox13.Focus()
		ElseIf TextBox = 13 Then
			TextBox13.Text = "Undefined"
			TextBox14.Focus()
		ElseIf TextBox = 14 Then
			TextBox14.Text = "Undefined"
			TextBox15.Focus()
		ElseIf TextBox = 15 Then
			TextBox15.Text = "Undefined"
			TextBox16.Focus()
		ElseIf TextBox = 16 Then
			TextBox16.Text = "Undefined"
			TextBox17.Focus()
		ElseIf TextBox = 17 Then
			TextBox17.Text = "Undefined"
			TextBox18.Focus()
		ElseIf TextBox = 18 Then
			TextBox18.Text = "Undefined"
			TextBox19.Focus()
		ElseIf TextBox = 19 Then
			TextBox19.Text = "Undefined"
			TextBox20.Focus()
		ElseIf TextBox = 20 Then
			TextBox20.Text = "Undefined"
			TextBox21.Focus()
		ElseIf TextBox = 21 Then
			TextBox21.Text = "Undefined"
			TextBox22.Focus()
		ElseIf TextBox = 22 Then
			TextBox22.Text = "Undefined"
			TextBox23.Focus()
		ElseIf TextBox = 23 Then
			TextBox23.Text = "Undefined"
			TextBox24.Focus()
		ElseIf TextBox = 24 Then
			TextBox24.Text = "Undefined"
			Button5.Focus()
		End If
	End Sub

	Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
		TextBox = "1"
	End Sub

	Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
		TextBox = "2"
	End Sub

	Private Sub TextBox3_GotFocus(sender As Object, e As EventArgs) Handles TextBox3.GotFocus
		TextBox = "3"
	End Sub

	Private Sub TextBox4_GotFocus(sender As Object, e As EventArgs) Handles TextBox4.GotFocus
		TextBox = "4"
	End Sub

	Private Sub TextBox5_GotFocus(sender As Object, e As EventArgs) Handles TextBox5.GotFocus
		TextBox = "5"
	End Sub

	Private Sub TextBox6_GotFocus(sender As Object, e As EventArgs) Handles TextBox6.GotFocus
		TextBox = "6"
	End Sub

	Private Sub TextBox7_GotFocus(sender As Object, e As EventArgs) Handles TextBox7.GotFocus
		TextBox = "7"
	End Sub

	Private Sub TextBox8_GotFocus(sender As Object, e As EventArgs) Handles TextBox8.GotFocus
		TextBox = "8"
	End Sub

	Private Sub TextBox9_GotFocus(sender As Object, e As EventArgs) Handles TextBox9.GotFocus
		TextBox = "9"
	End Sub

	Private Sub TextBox10_GotFocus(sender As Object, e As EventArgs) Handles TextBox10.GotFocus
		TextBox = "10"
	End Sub

	Private Sub TextBox11_GotFocus(sender As Object, e As EventArgs) Handles TextBox11.GotFocus
		TextBox = "11"
	End Sub

	Private Sub TextBox12_GotFocus(sender As Object, e As EventArgs) Handles TextBox12.GotFocus
		TextBox = "12"
	End Sub

	Private Sub TextBox13_GotFocus(sender As Object, e As EventArgs) Handles TextBox13.GotFocus
		TextBox = "13"
	End Sub

	Private Sub TextBox14_GotFocus(sender As Object, e As EventArgs) Handles TextBox14.GotFocus
		TextBox = "14"
	End Sub

	Private Sub TextBox15_GotFocus(sender As Object, e As EventArgs) Handles TextBox15.GotFocus
		TextBox = "15"
	End Sub

	Private Sub TextBox16_GotFocus(sender As Object, e As EventArgs) Handles TextBox16.GotFocus
		TextBox = "16"
	End Sub

	Private Sub TextBox17_GotFocus(sender As Object, e As EventArgs) Handles TextBox17.GotFocus
		TextBox = "17"
	End Sub

	Private Sub TextBox18_GotFocus(sender As Object, e As EventArgs) Handles TextBox18.GotFocus
		TextBox = "18"
	End Sub

	Private Sub TextBox19_GotFocus(sender As Object, e As EventArgs) Handles TextBox19.GotFocus
		TextBox = "19"
	End Sub

	Private Sub TextBox20_GotFocus(sender As Object, e As EventArgs) Handles TextBox20.GotFocus
		TextBox = "20"
	End Sub

	Private Sub TextBox21_GotFocus(sender As Object, e As EventArgs) Handles TextBox21.GotFocus
		TextBox = "21"
	End Sub

	Private Sub TextBox22_GotFocus(sender As Object, e As EventArgs) Handles TextBox22.GotFocus
		TextBox = "22"
	End Sub

	Private Sub TextBox23_GotFocus(sender As Object, e As EventArgs) Handles TextBox23.GotFocus
		TextBox = "23"
	End Sub

	Private Sub TextBox24_GotFocus(sender As Object, e As EventArgs) Handles TextBox24.GotFocus
		TextBox = "24"
	End Sub

End Class