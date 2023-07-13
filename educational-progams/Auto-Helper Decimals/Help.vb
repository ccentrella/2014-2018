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
                           & "Auto-Helper </h2> <br> <b> <h3>Adding & Subtracting Decimals</b> </h3> <br><p style = ""font-size:18px"">To add and subtract decimals, we simply " _
                           & "add and subtract normally — as if we are adding and subtracting normal numbers. There's just one note: Don't forget the Decimal Point! The decimal point goes in the exact place where " _
                           & "it is shown. To make this easier, imagine the decimal point is falling down. It's that easy! From what we have learned, we now realize that it is quite easy to add and subtract decimals. </p> </body>"
                Initialized = True
            End If
        Else
            If Initialized = False Then
                WebBrowser1.DocumentText = "<!DOCTYPE HTML> <html> <head> <title>Help | Auto-Helper</title> </head> <body style = ""font-family: Calibri; font-size: 12px""> <h2 style = ""color:darkgreen"">" _
                           & "Auto-Helper </h2> <br> <b> <h3>Multiplying & Dividing Decimals</b> </h3> <br><p style = ""font-size:18px"">To multiply decimals, we must first count the amount of places <i> <b> after </b> </i> the decimal point on <i> <b> both </b> </i> sides. We will apply this number later. Then, we simply " _
                           & "multiply normally — as if we are adding and subtracting normal numbers. The decimal is then placed at the number of places to the right of the numbers — the number of places we counted before." _
                           & "We have now learned how to multiply decimals. We must now learn how to divide decimals. To divide decimals, we first multiply the quotient by a power of 10 — such that the quotient contains no decimals. We then do the same for the divisor. We now divide as usual. We have now learned how to divide decimals. Thus, dividing decimals is very easy."
                Initialized = True
            End If
        End If


    End Sub
End Class