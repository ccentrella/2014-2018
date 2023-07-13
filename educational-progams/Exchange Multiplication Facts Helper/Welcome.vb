Public Class Welcome

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Memorizer.Division = False
        Memorizer.Button2.Enabled = False
        Memorizer.Factor1.Text = 0
        Memorizer.Factor2.Text = 0
        Memorizer.Total.Text = 0
        Memorizer.Sign1.Text = "×"
        Memorizer.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Memorizer.Division = True
        Memorizer.Button2.Enabled = True
        Memorizer.Factor1.Text = 144
        Memorizer.Factor2.Text = 12
        Memorizer.Total.Text = 12
        Memorizer.Sign1.Text = "÷"
        Memorizer.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Exercises.Visible = False Then
            If Label2.Visible = True Then Label2.Visible = False
            Exercises.Division = False
            If Panel1.Visible = False Then Panel1.Visible = True
        Else
            If Label2.Visible = False Then Label2.Visible = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Exercises.Visible = False Then
            If Label2.Visible = True Then Label2.Visible = False
            Exercises.Division = True
            If Panel1.Visible = False Then Panel1.Visible = True
        Else
            If Label2.Visible = False Then Label2.Visible = True
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If ComboBox1.SelectedItem <> "Select One" Then
            Exercises.UserName = ComboBox1.SelectedItem.ToString
            Exercises.Hours.Text = "00"
            Exercises.Seconds.Text = "00"
            Exercises.Minutes.Text = "00"
            Exercises.Show()
            Panel1.Visible = False
        End If
    End Sub

    Private Sub Panel1_VisibleChanged(sender As Object, e As EventArgs) Handles Panel1.VisibleChanged
        ComboBox1.SelectedItem = "Select One"
    End Sub

    Private Sub Welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
