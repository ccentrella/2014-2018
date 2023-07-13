<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Start
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Start))
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
		Me.AdditionSubtractionQuiz = New System.Windows.Forms.Button()
		Me.MultiplicationDivisionQuiz = New System.Windows.Forms.Button()
		Me.MultiplicationDivisionHelp = New System.Windows.Forms.Button()
		Me.AdditionSubtractionHelp = New System.Windows.Forms.Button()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Password = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.UserName = New System.Windows.Forms.TextBox()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Panel1.SuspendLayout()
		Me.Panel2.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
		Me.Panel1.Controls.Add(Me.LinkLabel1)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(649, 30)
		Me.Panel1.TabIndex = 4
		'
		'LinkLabel1
		'
		Me.LinkLabel1.ActiveLinkColor = System.Drawing.SystemColors.GrayText
		Me.LinkLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.LinkLabel1.AutoSize = True
		Me.LinkLabel1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
		Me.LinkLabel1.LinkColor = System.Drawing.SystemColors.ControlText
		Me.LinkLabel1.Location = New System.Drawing.Point(596, 4)
		Me.LinkLabel1.Name = "LinkLabel1"
		Me.LinkLabel1.Size = New System.Drawing.Size(49, 21)
		Me.LinkLabel1.TabIndex = 0
		Me.LinkLabel1.TabStop = True
		Me.LinkLabel1.Text = "Login"
		'
		'AdditionSubtractionQuiz
		'
		Me.AdditionSubtractionQuiz.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AdditionSubtractionQuiz.Location = New System.Drawing.Point(112, 94)
		Me.AdditionSubtractionQuiz.Name = "AdditionSubtractionQuiz"
		Me.AdditionSubtractionQuiz.Size = New System.Drawing.Size(424, 41)
		Me.AdditionSubtractionQuiz.TabIndex = 0
		Me.AdditionSubtractionQuiz.Text = "Take a Quiz (&Adding and Subtracting Decimals)"
		Me.AdditionSubtractionQuiz.UseVisualStyleBackColor = True
		'
		'MultiplicationDivisionQuiz
		'
		Me.MultiplicationDivisionQuiz.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.MultiplicationDivisionQuiz.Location = New System.Drawing.Point(112, 150)
		Me.MultiplicationDivisionQuiz.Name = "MultiplicationDivisionQuiz"
		Me.MultiplicationDivisionQuiz.Size = New System.Drawing.Size(424, 41)
		Me.MultiplicationDivisionQuiz.TabIndex = 1
		Me.MultiplicationDivisionQuiz.Text = "Take a Quiz (&Multiplying and Dividing Decimals)"
		Me.MultiplicationDivisionQuiz.UseVisualStyleBackColor = True
		'
		'MultiplicationDivisionHelp
		'
		Me.MultiplicationDivisionHelp.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.MultiplicationDivisionHelp.Location = New System.Drawing.Point(112, 264)
		Me.MultiplicationDivisionHelp.Name = "MultiplicationDivisionHelp"
		Me.MultiplicationDivisionHelp.Size = New System.Drawing.Size(424, 41)
		Me.MultiplicationDivisionHelp.TabIndex = 3
		Me.MultiplicationDivisionHelp.Text = "Learn how to &Multiply and Divide Decimals"
		Me.MultiplicationDivisionHelp.UseVisualStyleBackColor = True
		'
		'AdditionSubtractionHelp
		'
		Me.AdditionSubtractionHelp.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AdditionSubtractionHelp.Location = New System.Drawing.Point(112, 208)
		Me.AdditionSubtractionHelp.Name = "AdditionSubtractionHelp"
		Me.AdditionSubtractionHelp.Size = New System.Drawing.Size(424, 41)
		Me.AdditionSubtractionHelp.TabIndex = 2
		Me.AdditionSubtractionHelp.Text = "Learn how to &Add and Subtract Decimals"
		Me.AdditionSubtractionHelp.UseVisualStyleBackColor = True
		'
		'Panel2
		'
		Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
		Me.Panel2.Controls.Add(Me.Button2)
		Me.Panel2.Controls.Add(Me.Label2)
		Me.Panel2.Controls.Add(Me.Password)
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.UserName)
		Me.Panel2.Controls.Add(Me.Button1)
		Me.Panel2.Location = New System.Drawing.Point(424, 28)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(225, 116)
		Me.Panel2.TabIndex = 5
		Me.Panel2.Visible = False
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(65, 83)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(88, 25)
		Me.Button2.TabIndex = 2
		Me.Button2.Text = "&Login"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(10, 59)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(59, 13)
		Me.Label2.TabIndex = 5
		Me.Label2.Text = "Password:"
		'
		'Password
		'
		Me.Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Password.Location = New System.Drawing.Point(73, 55)
		Me.Password.Name = "Password"
		Me.Password.Size = New System.Drawing.Size(145, 22)
		Me.Password.TabIndex = 1
		Me.Password.UseSystemPasswordChar = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 31)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(66, 13)
		Me.Label1.TabIndex = 4
		Me.Label1.Text = "User-Name:"
		'
		'UserName
		'
		Me.UserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UserName.Location = New System.Drawing.Point(73, 27)
		Me.UserName.Name = "UserName"
		Me.UserName.Size = New System.Drawing.Size(145, 22)
		Me.UserName.TabIndex = 0
		'
		'Button1
		'
		Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Button1.BackColor = System.Drawing.Color.DarkGray
		Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray
		Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed
		Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(140, Byte), Integer))
		Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.Button1.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Button1.ForeColor = System.Drawing.Color.Black
		Me.Button1.Location = New System.Drawing.Point(196, 1)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(28, 20)
		Me.Button1.TabIndex = 3
		Me.Button1.Text = "X"
		Me.Button1.UseVisualStyleBackColor = False
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.ForeColor = System.Drawing.Color.DodgerBlue
		Me.Label3.Location = New System.Drawing.Point(523, 341)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(115, 29)
		Me.Label3.TabIndex = 7
		Me.Label3.Text = "Autosoft™"
		'
		'Start
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.WhiteSmoke
		Me.ClientSize = New System.Drawing.Size(649, 383)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.MultiplicationDivisionHelp)
		Me.Controls.Add(Me.AdditionSubtractionHelp)
		Me.Controls.Add(Me.MultiplicationDivisionQuiz)
		Me.Controls.Add(Me.AdditionSubtractionQuiz)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.Name = "Start"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Welcome | Auto-Helper"
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents AdditionSubtractionQuiz As System.Windows.Forms.Button
    Friend WithEvents MultiplicationDivisionQuiz As System.Windows.Forms.Button
    Friend WithEvents MultiplicationDivisionHelp As System.Windows.Forms.Button
    Friend WithEvents AdditionSubtractionHelp As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Password As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents UserName As System.Windows.Forms.TextBox
	Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
