Public Class Help
	Dim Initialized As Boolean = False
	Private Sub Help_Load(sender As Object, e As EventArgs) Handles MyBase.Load

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Start.Show()
		Me.Close()
	End Sub

	Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
		If MultiplicationDivision = False Then
			If Initialized = False Then
				WebBrowser1.DocumentText = "<!DOCTYPE HTML> <html> <head> <title>Help | Auto-Helper</title> </head> <body style = ""font-family: Calibri; font-size: 12px""> <h2 style = ""color:darkgreen"">" _
						   & "Auto-Helper </h2> <br> <b> <h3>Adding & Subtracting</b> </h3> <br><p style = ""font-size:18px"">Adding numbers is the process when one number is <i>added </i> to another number. In <i> addition, </i> we add the <i> value </i> of the first number by the <i>value</i> of the second number. " _
						   & "For example, 8 + 7 = 15, 2 + 5 = 7, and 25 + 47 = 72. <br> <br>Subtracting numbers is similar to adding, but we take the <i>value</i> of the lower number <i> away </i> from the <i> value </i> of the top number. Thus, adding is the <i>opposite</i> of addition. For example, 18 – 6 = 12, 25 – 5 = 20, 35 – 7 = 28, 26 – 13 = 13, and 125 – 33 = 99. <br> <br> From we have seen, it is thus <i> <b> very </i> </b>" _
						   & "easy to add and subtract, and there is <i> <b> no </i> </b> excuse for not knowing how to perform these basic operations.</p> </body>"
				Initialized = True
			End If
		Else
			If Initialized = False Then
				WebBrowser1.DocumentText = "<!DOCTYPE HTML> <html> <head> <title>Help | Auto-Helper</title> </head> <body style = ""font-family: Calibri; font-size: 12px""> <h2 style = ""color:darkgreen"">" _
						& "Auto-Helper </h2> <br> <b> <h3>Multiplying & Dividing</b> </h3> <br><p style = ""font-size:18px"">Multiplying numbers is the process when one number is <i>multiplied</i> to another number. In <i> multiplication, </i> we take the <i> value </i> of the first number and create that many copies of the second number. In multiplication, we <i> evaluate </i> each number digit by digit. " _
						& "This indicates that we multiply <i> each </i> digit on the top by the rightmost bottom digit. We then <i>carry </i> the answer to the next digit. This process can be a bit confusing. When we carry the number, we do <i> <b> not </i> </b> add the value to the next digit. Instead, we multiply the digit as indicated, and then add that number to the answer for that digit. " _
						& "Thus, if we had to multiply 87 by 23, we would first multiply 7 by 3. Because the answer is 21, we write down 1, and insert ""2"" over the 8. We then multiply 8 by 3, and then add 2 to this answer. Because we have multiplied all the digits by the rightmost bottom digit, we then write down the complete answer, 26, to the left of our previous answers. We then follow the same process for the remaining bottom digits – with one exception. " _
						& "Each time we multiply the answer by the next digit on the <i>bottom</i>, we begin writing all answer underneath the previous answer, but one digit to the <i>left</i>. After multiplying <i>all</i> the numbers, on <i> both </i> sides, we then add all of our answers together, keeping note of the position we put each answer  in. For example, suppose all first answer was 26, and our second answer was 35. Do <i> not</i> place the 5 under the 6, but " _
						& "under the 2. The 3 is then placed to the left of the 2. To make things easier, we can fill in all <i> blank </i> spaces with 0. Thus, we would add it like ""350 + 26"", and receive an answer of 376. Some basic multiplication examples are: 8 × 7 = 56, 6 × 5 = 30, 25 × 3 = 75, 9 × 9 = 81, 12 × 3 = 36, 3 × 5 = 15, 10 × 5 = 50, and 20 × 4 = 80. <br> <br> Dividing numbers is the <i>opposite</i> of multiplying. In division, we take the first number and " _
						& "<i>divide</i> it into many smaller numbers. The number we divide it by is, of course, the second number. When we divide, we check to see how many times the second number fits into the first number – but we do so digit by digit. We then <i> carry </i> the remainder. After all the digits have been <i> evaluated </i>, we must then put ""R"" & then insert the <i> " _
						& "remainder</i>. The remainder is the extra number we have after completing all division. For example, 26 ÷ 3 = 8 R 2. In the same way, 33 ÷ 2 = 8 R 1, 65 ÷ 2 = 32 R 1, 25 ÷ 7 = 3 R 4, and 53 = 9 = 5 R 8. If the number has no remainder, just write down the number. For example, 8 ÷ 2 = 4, 25 ÷ 5 = 5, 42 ÷ 6 = 7, 60 ÷ 12 = 5, and 26 ÷ 13 = 2. <br> <br> Although the process of multiplication and division may seem very hard and boring, it is very important that we are able to perform these basic operations. </p> </body>"
				Initialized = True
			End If
		End If


	End Sub
End Class