Public Class Memorizer
    Public Division As Boolean
    Dim MyFont1 As New Font("Calibri", 36)
    Dim MyFont2 As New Font("Calibri", 28)
    Dim Down As Boolean
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Total.Visible = False Then
            Total.Visible = True
            Button3.Visible = False
        End If
    End Sub

    Public Sub DownCLick()
        Try
            If Division = False Then
                If Factor2.Text <> 0 Then
                    Factor2.Text -= 1
                Else
                    Factor1.Text -= 1
                    Factor2.Text = 12
                End If
                Dim NewTotal As String = Convert.ToInt32(Factor1.Text) * Convert.ToInt32(Factor2.Text)
                Total.Text = NewTotal
                If Factor1.Text = 0 And Factor2.Text = 0 Then
                    If Button2.Enabled = True Then Button2.Enabled = False
                End If
            Else
                Dim Value As Integer
                Dim value1 As Integer = Convert.ToInt32(Factor1.Text)
                Dim value2 As Integer = Convert.ToInt32(Factor2.Text)
                If value2 >= 2 Then
                    value2 -= 1
                    Value = value1 Mod value2
                Else
                    If value2 = 1 Then
                        value2 = 0
                        Value = 0
                        If value1 = 0 Then Button2.Enabled = False
                    Else
                        value1 -= 1
                        value2 = 12
                        Value = value1 Mod value2
                    End If
                End If

                While Value > 0
                    If value2 > 0 Then
                        value2 -= 1
                        Value = value1 Mod value2
                    Else
                        value1 -= 1
                        value2 = 12
                        Exit While
                    End If
                End While



                Factor1.Text = Convert.ToString(value1)
                Factor2.Text = Convert.ToString(value2)
                Dim NewTotal As String = Convert.ToInt32(Factor1.Text) / Convert.ToInt32(Factor2.Text)
                If Factor2.Text = 0 Then
                    Total.Text = "Undefined"
                Else
                    Total.Text = NewTotal
                End If
            End If

            If Total.Visible = True Then
                Total.Visible = False
                Button3.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show("The number selected is outside of this range. Please select a new value.", "Number Outside of Range", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub UpClick()
        Try
            If Division = False Then
                If Factor2.Text < 12 Then
                    Factor2.Text += 1
                Else
                    Factor1.Text += 1
                    Factor2.Text = 0
                End If

                Dim NewTotal As String = Convert.ToInt32(Factor1.Text) * Convert.ToInt32(Factor2.Text)
                Total.Text = NewTotal
            Else
                Dim Value As Integer
                Dim value1 As Integer = Convert.ToInt32(Factor1.Text)
                Dim value2 As Integer = Convert.ToInt32(Factor2.Text + 1)
                Value = value1 Mod value2
                While Value > 0
                    If value2 < 12 Then
                        value2 += 1
                        Value = value1 Mod value2
                    Else
                        value1 += 1
                        value2 = 0
                        Exit While
                    End If
                End While
                Factor1.Text = Convert.ToString(value1)
                Factor2.Text = Convert.ToString(value2)

                Dim NewTotal As String = Convert.ToInt32(Factor1.Text) / Convert.ToInt32(Factor2.Text)
                If Factor2.Text = 0 Then
                    Total.Text = "Undefined"
                Else
                    Total.Text = NewTotal
                End If
            End If
            If Total.Visible = True Then
                Total.Visible = False
                Button3.Visible = True
            End If
            If Button2.Enabled = False Then Button2.Enabled = True
        Catch ex As Exception
            MessageBox.Show("The number selected is outside of this range. Please select a new value.", "Number Outside of Range", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Down = False
        Timer1.Enabled = False
        Timer1.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Down = True
        Timer1.Enabled = False
        Timer1.Enabled = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Division = False Then
            Factor1.Text = 0
            Factor2.Text = 0
            Total.Text = 0
        Else
            Factor1.Text = 144
            Factor2.Text = 12
            Total.Text = 12
        End If
        If Total.Visible = True Then
            Total.Visible = False
            Button3.Visible = True
        End If
        If Button2.Enabled = True Then Button2.Enabled = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Total_Click(sender As Object, e As EventArgs) Handles Total.Click

    End Sub

    Private Sub Total_TextChanged(sender As Object, e As EventArgs) Handles Total.TextChanged
        If Total.Text = "Undefined" Then
            Total.Font = MyFont2
        Else
            Total.Font = MyFont1
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If Down Then
            DownCLick()
        Else
            UpClick()
        End If
    End Sub
End Class