Imports System.Drawing.Drawing2D
Public Class Congratulations

	Private Sub Congratulations_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
		If Grade > 70 Then
			Start.Show()
		Else
			Exercises.Show()
		End If

	End Sub

	Private Sub Congratulations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		If Grade < 70 Then Me.Text = "Oops! (Auto-Helper)"
	End Sub

	Private Sub Congratulations_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
		If e.ClipRectangle.Width = 0 Or e.ClipRectangle.Height = 0 Then Exit Sub
		Dim MyStartPoint As New Point
		Dim MyEndPoint As New Point
		Dim MyStartPoint2 As Point
		Dim MyEndPoint2 As Point
		Dim MyPen As New Pen(Brushes.DeepSkyBlue, 3)
		Dim MyFont As New Font("Cambria", 24, FontStyle.Regular, GraphicsUnit.Point)
		MyStartPoint.X = 80
		MyStartPoint.Y = 247
		MyEndPoint.Y = MyStartPoint.Y
		MyEndPoint.X = MyStartPoint.X + 75
		MyStartPoint2.X = MyStartPoint.X + 575
		MyStartPoint2.Y = MyStartPoint.Y
		MyEndPoint2.Y = MyStartPoint.Y
		MyEndPoint2.X = MyStartPoint2.X + 75
		If SystemInformation.UIEffectsEnabled = True And SystemInformation.PrimaryMonitorSize.Height >= 600 And Width >= 1024 And _
			System.Environment.SystemPageSize >= 500 And System.Environment.WorkingSet >= 250000 And System.Environment.ProcessorCount > 1 And Grade >= 70 Then
			Dim Mybrush As New LinearGradientBrush(e.ClipRectangle, Color.DeepSkyBlue, Color.DarkBlue, 90)
			Me.BackColor = Color.DarkBlue
			e.Graphics.FillRectangle(Mybrush, e.ClipRectangle)
			e.Graphics.DrawString("Congratulations!", MyFont, Brushes.DeepSkyBlue, MyStartPoint.X + 200, MyStartPoint.Y - 25)
			Mybrush.Dispose()
		ElseIf Grade < 70 Then
			Me.BackColor = Color.DarkRed
			MyPen.Color = Color.White
			e.Graphics.DrawString("Oops! That's a Re-do!", MyFont, Brushes.White, MyStartPoint.X + 200, MyStartPoint.Y - 25)
		Else
			Me.BackColor = Color.DarkBlue
			MyPen.Color = Color.White
			e.Graphics.DrawString("Congratulations!", MyFont, Brushes.DeepSkyBlue, MyStartPoint.X + 200, MyStartPoint.Y - 25)
		End If
		e.Graphics.DrawLine(MyPen, MyStartPoint, MyEndPoint)
		e.Graphics.DrawLine(MyPen, MyStartPoint2, MyEndPoint2)
		MyPen.Dispose()
		e.Graphics.Dispose()

	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles timer1.tick
		If Me.Opacity > 0 Then
			Me.Opacity -= 0.002
		Else
			
			Me.Close()
		End If
	End Sub

	Private Sub Congratulations_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
		Timer1.Start()
	End Sub
End Class