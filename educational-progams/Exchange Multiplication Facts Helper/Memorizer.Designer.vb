<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Memorizer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Memorizer))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Total = New System.Windows.Forms.Label()
        Me.Sign1 = New System.Windows.Forms.Label()
        Me.Factor2 = New System.Windows.Forms.Label()
        Me.Factor1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Total)
        Me.Panel1.Controls.Add(Me.Sign1)
        Me.Panel1.Controls.Add(Me.Factor2)
        Me.Panel1.Controls.Add(Me.Factor1)
        Me.Panel1.Location = New System.Drawing.Point(61, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(209, 285)
        Me.Panel1.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(2, 188)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(202, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "_______________________________________"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Total
        '
        Me.Total.Font = New System.Drawing.Font("Calibri", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total.Location = New System.Drawing.Point(-1, 224)
        Me.Total.Name = "Total"
        Me.Total.Size = New System.Drawing.Size(209, 59)
        Me.Total.TabIndex = 6
        Me.Total.Text = "Undefined"
        Me.Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Total.Visible = False
        '
        'Sign1
        '
        Me.Sign1.AutoSize = True
        Me.Sign1.Font = New System.Drawing.Font("Calibri", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Sign1.Location = New System.Drawing.Point(3, 62)
        Me.Sign1.Name = "Sign1"
        Me.Sign1.Size = New System.Drawing.Size(33, 39)
        Me.Sign1.TabIndex = 5
        Me.Sign1.Text = "×"
        '
        'Factor2
        '
        Me.Factor2.Font = New System.Drawing.Font("Calibri", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Factor2.Location = New System.Drawing.Point(-1, 99)
        Me.Factor2.Name = "Factor2"
        Me.Factor2.Size = New System.Drawing.Size(209, 59)
        Me.Factor2.TabIndex = 4
        Me.Factor2.Text = "0"
        Me.Factor2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Factor1
        '
        Me.Factor1.Font = New System.Drawing.Font("Calibri", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Factor1.Location = New System.Drawing.Point(3, -1)
        Me.Factor1.Name = "Factor1"
        Me.Factor1.Size = New System.Drawing.Size(205, 59)
        Me.Factor1.TabIndex = 3
        Me.Factor1.Text = "0"
        Me.Factor1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Wingdings", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Button1.Location = New System.Drawing.Point(292, 54)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(53, 48)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = ""
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Font = New System.Drawing.Font("Wingdings", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Button2.Location = New System.Drawing.Point(292, 108)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(53, 48)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = ""
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(292, 218)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(53, 41)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "Reset"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(292, 265)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(53, 41)
        Me.Button5.TabIndex = 5
        Me.Button5.Text = "Done"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = Global.Exchange_Multiplication_Facts_Helper.My.Resources.Resources.Help___02
        Me.Button3.Location = New System.Drawing.Point(292, 165)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(53, 47)
        Me.Button3.TabIndex = 3
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'Memorizer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(374, 349)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(390, 387)
        Me.Name = "Memorizer"
        Me.Text = "Learn Facts - Exchange"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Factor1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Total As System.Windows.Forms.Label
    Friend WithEvents Sign1 As System.Windows.Forms.Label
    Friend WithEvents Factor2 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
