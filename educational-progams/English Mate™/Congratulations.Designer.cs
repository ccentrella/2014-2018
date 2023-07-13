namespace EnglishMate
{
	partial class Congratulations
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Congratulations));
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.GradeLabel = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupBox1
			// 
			this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.GroupBox1.Controls.Add(this.GradeLabel);
			this.GroupBox1.ForeColor = System.Drawing.Color.White;
			this.GroupBox1.Location = new System.Drawing.Point(346, 373);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(112, 84);
			this.GroupBox1.TabIndex = 1;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Your Grade";
			// 
			// GradeLabel
			// 
			this.GradeLabel.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GradeLabel.ForeColor = System.Drawing.Color.White;
			this.GradeLabel.Location = new System.Drawing.Point(4, 18);
			this.GradeLabel.Name = "GradeLabel";
			this.GradeLabel.Size = new System.Drawing.Size(106, 57);
			this.GradeLabel.TabIndex = 0;
			this.GradeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Congratulations
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(831, 520);
			this.Controls.Add(this.GroupBox1);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Congratulations";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Congratulations (Auto-Helper)";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Congratulations_FormClosing);
			this.Load += new System.EventHandler(this.Congratulations_Load);
			this.Shown += new System.EventHandler(this.Congratulations_Shown);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Congratulations_Paint);
			this.GroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Label GradeLabel;
		private System.Windows.Forms.Timer timer1;
	}
}