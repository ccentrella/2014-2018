Public Class SplashScreen

    Private Sub SplashScreen_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim MyObject As New System.Drawing.Drawing2D.GraphicsPath
        MyObject.AddEllipse(0, 0, Me.Width, Me.Height)
        Me.Region = New System.Drawing.Region(MyObject)
    End Sub

    
End Class