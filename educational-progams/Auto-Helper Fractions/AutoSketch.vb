Public Class AutoSketch
    Dim MyPoint As New Point
    Shadows MouseDown As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim MyGraphics As Graphics = Panel2.CreateGraphics
        MyGraphics.Clear(Color.Black)
        MyGraphics.Dispose()
    End Sub

    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove
        If MouseDown = True Then
            Dim MyPoint As New Point
            MyPoint.X = e.X + 1
            MyPoint.Y = e.Y + 1
            Dim MyPen As New Pen(Brushes.White, 5)
            Dim MyGraphics As Graphics = Panel2.CreateGraphics
            MyGraphics.DrawLine(MyPen, e.Location, MyPoint)
            MyGraphics.Dispose()
            MyPen.Dispose()
        End If
    End Sub

    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel2.MouseUp
        MouseDown = False
    End Sub

    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then MouseDown = True
    End Sub

   

End Class