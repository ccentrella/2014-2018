Public Class Help
    Public MultiplicationDivison As Boolean = False
    Dim Initialized As Boolean = False
    Private Sub Help_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Start.Show()
        Me.Close()
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        If MultiplicationDivison = False Then
            If Initialized = False Then
                WebBrowser1.DocumentText = "<!DOCTYPE HTML> <html> <head> <title>Help | Auto-Helper</title> </head> <body style = ""font-family: Calibri; font-size: 12px""> <h2 style = ""color:darkgreen"">" _
                           & "Auto-Helper </h2> <br> <b> <h3>Adding & Subtracting Fractions</b> </h3> <br><p style = ""font-size:18px""> To add and subtract fractions, we note the following: </p>" _
                           & "<ol style = ""font-size:16px""> <li>If the fractions have common denominators (the bottom-halves of the fraction are the same), simply add and subtract the two top numbers, and keep the bottom number. <b> Do not add and subtract the bottom numbers. </b></li> <br> <br> <li>" _
                           & "If the numbers do not have common denominators (the bottom-halves of the fractions are different from each other), then a number must be found, such that all denominators fit into the specified number. " _
                           & "This number is called the <b> Least Common Multiple (LCM). </b> To find the Least Common Multiple, we must look for a number that is divisible by <b> all </b> denominators. After finding the Least Common Multiple, we " _
                           & "must divide the Least Common Multiple by the first denominator. <b>We must then multiply the first denominator by the result of the division we just performed. We must do this same step for each and every denominator. </b> </li> <br> <br><li>" _
                           & "All fractions must then be <b> reduced. </b> Reducing a fraction helps make readability easier by making both terms smaller. To reduce a fraction, we must find a whole number, such that the numerator (top-half) " _
                           & "and denominator (bottom-half) are evenly divisible by that number. In some cases, reducing is not necessary, due to the fact that the fractions are not divisible by any " _
                           & "whole number, other than one. <b> When trying to reduce a fraction, we must make sure that both the numerator and the denominator are divisible by that fraction — not just one or the other. </<b></li> </ol> </body>"
                Initialized = True
            End If
        Else
            If Initialized = False Then
                WebBrowser1.DocumentText = "<!DOCTYPE HTML> <html> <head> <title>Help | Auto-Helper</title> </head> <body style = ""font-family: Calibri; font-size: 12px""> <h2 style = ""color:darkgreen"">" _
                           & "Auto-Helper </h2> <br> <b> <h3>Multiplying & Dividing Fractions</b> </h3> <br><p style = ""font-size:18px""> To multiply and divide fractions, we note the following: </p>" _
                           & "<ol style = ""font-size:16px"">  <li> To multiply fractions, we multiply the numerators (top-halves) and denominators (bottom-halves). </li> <br> <br><li> To divide fractions, we multiply the " _
                           & "<b> reciprocal </b> of the fraction. <b>To find the reciprocal, we simply flip the second pair around. </b> Thus, the second numerator becomes the second denominator, and vice versa. " _
                           & "We then continue as if we were multiplying the fractions. Thus, we multiply the numerators and the denominators, in order to find the result. </li> <br> <br> <li> All fractions must then be <b> reduced. </b> Reducing a fraction helps make readability easier by " _
                           & "making both terms smaller. To reduce a fraction, we must find a whole number, such that the numerator (top-half) and denominator (bottom-half) are evenly divisible by that number. " _
                           & "In some cases, reducing is not necessary, due to the fact that the fractions are not divisible by any whole number, other than one. <b> When trying to reduce a fraction, we must make " _
                           & "sure that both the numerator and the denominator are divisible by that fraction — not just one or the other. </<b></li> </ol> </body>"
                Initialized = True
            End If
        End If


    End Sub
End Class