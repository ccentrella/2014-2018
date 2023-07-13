namespace SpellingMaster
{
	partial class Settings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
			this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.Cancel = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Password = new System.Windows.Forms.TextBox();
			this.ConfirmPassword = new System.Windows.Forms.TextBox();
			this.UserName = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ThemeLabel = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ComboBox1 = new System.Windows.Forms.ComboBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.Advanced = new System.Windows.Forms.Button();
			this.Intermediate = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.Basic = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.HelpProvider1 = new System.Windows.Forms.HelpProvider();
			this.TableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TableLayoutPanel1
			// 
			this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.WindowFrame;
			resources.ApplyResources(this.TableLayoutPanel1, "TableLayoutPanel1");
			this.TableLayoutPanel1.Controls.Add(this.Cancel, 2, 0);
			this.TableLayoutPanel1.Controls.Add(this.OKButton, 1, 0);
			this.TableLayoutPanel1.Name = "TableLayoutPanel1";
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.Cancel, "Cancel");
			this.Cancel.Name = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// OKButton
			// 
			resources.ApplyResources(this.OKButton, "OKButton");
			this.OKButton.Name = "OKButton";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.groupBox4);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Name = "panel1";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.label2);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.Password);
			this.groupBox4.Controls.Add(this.ConfirmPassword);
			this.groupBox4.Controls.Add(this.UserName);
			resources.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// Password
			// 
			resources.ApplyResources(this.Password, "Password");
			this.Password.Name = "Password";
			this.Password.UseSystemPasswordChar = true;
			// 
			// ConfirmPassword
			// 
			resources.ApplyResources(this.ConfirmPassword, "ConfirmPassword");
			this.ConfirmPassword.Name = "ConfirmPassword";
			this.ConfirmPassword.UseSystemPasswordChar = true;
			// 
			// UserName
			// 
			resources.ApplyResources(this.UserName, "UserName");
			this.UserName.Name = "UserName";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.ThemeLabel);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.ComboBox1);
			this.groupBox2.Controls.Add(this.trackBar1);
			this.groupBox2.Controls.Add(this.checkBox1);
			this.groupBox2.Controls.Add(this.checkBox2);
			this.groupBox2.Controls.Add(this.checkBox4);
			this.groupBox2.Controls.Add(this.checkBox3);
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// ThemeLabel
			// 
			resources.ApplyResources(this.ThemeLabel, "ThemeLabel");
			this.HelpProvider1.SetHelpString(this.ThemeLabel, resources.GetString("ThemeLabel.HelpString"));
			this.ThemeLabel.Name = "ThemeLabel";
			this.HelpProvider1.SetShowHelp(this.ThemeLabel, ((bool)(resources.GetObject("ThemeLabel.ShowHelp"))));
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.HelpProvider1.SetHelpString(this.label4, resources.GetString("label4.HelpString"));
			this.label4.Name = "label4";
			this.HelpProvider1.SetShowHelp(this.label4, ((bool)(resources.GetObject("label4.ShowHelp"))));
			// 
			// ComboBox1
			// 
			this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBox1.FormattingEnabled = true;
			this.HelpProvider1.SetHelpString(this.ComboBox1, resources.GetString("ComboBox1.HelpString"));
			this.ComboBox1.Items.AddRange(new object[] {
            resources.GetString("ComboBox1.Items"),
            resources.GetString("ComboBox1.Items1"),
            resources.GetString("ComboBox1.Items2"),
            resources.GetString("ComboBox1.Items3")});
			resources.ApplyResources(this.ComboBox1, "ComboBox1");
			this.ComboBox1.Name = "ComboBox1";
			this.HelpProvider1.SetShowHelp(this.ComboBox1, ((bool)(resources.GetObject("ComboBox1.ShowHelp"))));
			// 
			// trackBar1
			// 
			resources.ApplyResources(this.trackBar1, "trackBar1");
			this.HelpProvider1.SetHelpString(this.trackBar1, resources.GetString("trackBar1.HelpString"));
			this.trackBar1.Maximum = 20;
			this.trackBar1.Name = "trackBar1";
			this.HelpProvider1.SetShowHelp(this.trackBar1, ((bool)(resources.GetObject("trackBar1.ShowHelp"))));
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBar1.Value = 6;
			// 
			// checkBox1
			// 
			resources.ApplyResources(this.checkBox1, "checkBox1");
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.HelpProvider1.SetHelpString(this.checkBox1, resources.GetString("checkBox1.HelpString"));
			this.checkBox1.Name = "checkBox1";
			this.HelpProvider1.SetShowHelp(this.checkBox1, ((bool)(resources.GetObject("checkBox1.ShowHelp"))));
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			resources.ApplyResources(this.checkBox2, "checkBox2");
			this.checkBox2.Checked = true;
			this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.HelpProvider1.SetHelpString(this.checkBox2, resources.GetString("checkBox2.HelpString"));
			this.checkBox2.Name = "checkBox2";
			this.HelpProvider1.SetShowHelp(this.checkBox2, ((bool)(resources.GetObject("checkBox2.ShowHelp"))));
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			resources.ApplyResources(this.checkBox4, "checkBox4");
			this.checkBox4.Checked = true;
			this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
			this.HelpProvider1.SetHelpString(this.checkBox4, resources.GetString("checkBox4.HelpString"));
			this.checkBox4.Name = "checkBox4";
			this.HelpProvider1.SetShowHelp(this.checkBox4, ((bool)(resources.GetObject("checkBox4.ShowHelp"))));
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			resources.ApplyResources(this.checkBox3, "checkBox3");
			this.checkBox3.Checked = true;
			this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
			this.HelpProvider1.SetHelpString(this.checkBox3, resources.GetString("checkBox3.HelpString"));
			this.checkBox3.Name = "checkBox3";
			this.HelpProvider1.SetShowHelp(this.checkBox3, ((bool)(resources.GetObject("checkBox3.ShowHelp"))));
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox7);
			this.groupBox1.Controls.Add(this.checkBox6);
			this.groupBox1.Controls.Add(this.Advanced);
			this.groupBox1.Controls.Add(this.Intermediate);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.Basic);
			this.groupBox1.Controls.Add(this.textBox1);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// checkBox7
			// 
			resources.ApplyResources(this.checkBox7, "checkBox7");
			this.checkBox7.Checked = true;
			this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
			this.HelpProvider1.SetHelpString(this.checkBox7, resources.GetString("checkBox7.HelpString"));
			this.checkBox7.Name = "checkBox7";
			this.HelpProvider1.SetShowHelp(this.checkBox7, ((bool)(resources.GetObject("checkBox7.ShowHelp"))));
			this.checkBox7.UseVisualStyleBackColor = true;
			// 
			// checkBox6
			// 
			resources.ApplyResources(this.checkBox6, "checkBox6");
			this.HelpProvider1.SetHelpString(this.checkBox6, resources.GetString("checkBox6.HelpString"));
			this.checkBox6.Name = "checkBox6";
			this.HelpProvider1.SetShowHelp(this.checkBox6, ((bool)(resources.GetObject("checkBox6.ShowHelp"))));
			this.checkBox6.UseVisualStyleBackColor = true;
			// 
			// Advanced
			// 
			this.HelpProvider1.SetHelpString(this.Advanced, resources.GetString("Advanced.HelpString"));
			resources.ApplyResources(this.Advanced, "Advanced");
			this.Advanced.Name = "Advanced";
			this.HelpProvider1.SetShowHelp(this.Advanced, ((bool)(resources.GetObject("Advanced.ShowHelp"))));
			this.Advanced.UseVisualStyleBackColor = true;
			this.Advanced.Click += new System.EventHandler(this.Advanced_Click);
			// 
			// Intermediate
			// 
			this.HelpProvider1.SetHelpString(this.Intermediate, resources.GetString("Intermediate.HelpString"));
			resources.ApplyResources(this.Intermediate, "Intermediate");
			this.Intermediate.Name = "Intermediate";
			this.HelpProvider1.SetShowHelp(this.Intermediate, ((bool)(resources.GetObject("Intermediate.ShowHelp"))));
			this.Intermediate.UseVisualStyleBackColor = true;
			this.Intermediate.Click += new System.EventHandler(this.Intermediate_Click);
			// 
			// button1
			// 
			this.HelpProvider1.SetHelpString(this.button1, resources.GetString("button1.HelpString"));
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.HelpProvider1.SetShowHelp(this.button1, ((bool)(resources.GetObject("button1.ShowHelp"))));
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Basic
			// 
			this.HelpProvider1.SetHelpString(this.Basic, resources.GetString("Basic.HelpString"));
			resources.ApplyResources(this.Basic, "Basic");
			this.Basic.Name = "Basic";
			this.HelpProvider1.SetShowHelp(this.Basic, ((bool)(resources.GetObject("Basic.ShowHelp"))));
			this.Basic.UseVisualStyleBackColor = true;
			this.Basic.Click += new System.EventHandler(this.Basic_Click);
			// 
			// textBox1
			// 
			resources.ApplyResources(this.textBox1, "textBox1");
			this.HelpProvider1.SetHelpString(this.textBox1, resources.GetString("textBox1.HelpString"));
			this.textBox1.Name = "textBox1";
			this.HelpProvider1.SetShowHelp(this.textBox1, ((bool)(resources.GetObject("textBox1.ShowHelp"))));
			// 
			// Settings
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.TableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Settings";
			this.Load += new System.EventHandler(this.Settings_Load);
			this.TableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
		internal System.Windows.Forms.Button Cancel;
		internal System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox Password;
		private System.Windows.Forms.TextBox ConfirmPassword;
		private System.Windows.Forms.TextBox UserName;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button Advanced;
		private System.Windows.Forms.Button Intermediate;
		private System.Windows.Forms.Button Basic;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.CheckBox checkBox7;
		internal System.Windows.Forms.Label ThemeLabel;
		internal System.Windows.Forms.HelpProvider HelpProvider1;
		internal System.Windows.Forms.ComboBox ComboBox1;
		private System.Windows.Forms.Button button1;

	}
}