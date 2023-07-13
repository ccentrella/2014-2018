using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnglishMate
{
	public partial class Congratulations : Form
	{
		public bool Composition;
		public Congratulations()
		{
			InitializeComponent();
		}

		private void Congratulations_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MainStaticClass.Grade > 70)
			{
				foreach (Form Form in Application.OpenForms)
				{
					if (Form.Name == "Start") Form.Show();
				}
			}
			else
			{
				foreach (Form Form in Application.OpenForms)
				{
					if (!Composition & Form.Name == "GrammarExercises")
					{
						var CurrentForm = (GrammarExercises)Form;
						CurrentForm.Show();
						CurrentForm.Answer1.Select();
					}
					else if (Composition & Form.Name == "CompositionExercises")
					{
						var CurrentForm = (CompositionExercises)Form;
						CurrentForm.Show();
						CurrentForm.Answer1.Select();
					}
				}
			}


		}

		private void Congratulations_Load(object sender, EventArgs e)
		{
			if (MainStaticClass.Grade < 70) this.Text = "Oops! (Auto-Helper)";
			GradeLabel.Text = MainStaticClass.Grade.ToString();
		}

		private void Congratulations_Shown(object sender, EventArgs e)
		{
			timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.Opacity > 0) this.Opacity -= 0.002;
			else this.Close();
		}

		private void Congratulations_Paint(object sender, PaintEventArgs e)
		{
			if (e.ClipRectangle.Width != 0 & e.ClipRectangle.Height != 0) 
				{
					Point MyStartPoint = new Point();
					Point MyEndPoint = new Point();
					Point MyStartPoint2 = new Point();
					Point MyEndPoint2 = new Point();
					Pen MyPen = new Pen(Brushes.DeepSkyBlue,3);
					Font MyFont = new Font("Cambria",24,FontStyle.Regular,GraphicsUnit.Point);
					MyStartPoint.X = 80;
					MyStartPoint.Y = 247;
					MyEndPoint.Y = MyStartPoint.Y;
					MyEndPoint.X = MyStartPoint.X + 75;
					MyStartPoint2.X = MyStartPoint.X + 575;
					MyStartPoint2.Y = MyStartPoint.Y;
					MyEndPoint2.Y = MyStartPoint.Y;
					MyEndPoint2.X = MyStartPoint2.X + 75;
				
					if (SystemInformation.UIEffectsEnabled & SystemInformation.PrimaryMonitorSize.Height >= 600 & Width >= 1024 &
					System.Environment.SystemPageSize >= 500 & System.Environment.WorkingSet >= 250000 & System.Environment.ProcessorCount > 1 & MainStaticClass.Grade >= 70)
						{
							Brush Mybrush = new LinearGradientBrush(e.ClipRectangle, Color.DeepSkyBlue, Color.DarkBlue, 90);
							this.BackColor = Color.DarkBlue;
							e.Graphics.FillRectangle(Mybrush, e.ClipRectangle);
							e.Graphics.DrawString("Congratulations!", MyFont, Brushes.DeepSkyBlue, MyStartPoint.X + 200, MyStartPoint.Y - 25);
							Mybrush.Dispose();
						}
					else if (MainStaticClass.Grade < 70) 
						{
							this.BackColor = Color.DarkRed;
							MyPen.Color = Color.White;
							e.Graphics.DrawString("Oops! That's a Re-do!", MyFont, Brushes.White, MyStartPoint.X + 200, MyStartPoint.Y - 25);
						}
					else
						{
							this.BackColor = Color.DarkBlue;
							MyPen.Color = Color.White;
							e.Graphics.DrawString("Congratulations!", MyFont, Brushes.DeepSkyBlue, MyStartPoint.X + 200, MyStartPoint.Y - 25);
						}
					e.Graphics.DrawLine(MyPen, MyStartPoint, MyEndPoint);
					e.Graphics.DrawLine(MyPen, MyStartPoint2, MyEndPoint2);
					MyPen.Dispose();
					e.Graphics.Dispose();
					}
		

		}

	}
}
