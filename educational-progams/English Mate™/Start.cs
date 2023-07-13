using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace EnglishMate
{
	public partial class Start : Form
	{
		public Start()
		{
			InitializeComponent();
		}

		private void Start_Paint(object sender, PaintEventArgs e)
		{
			if (e.ClipRectangle.Height == 0 | e.ClipRectangle.Width == 0) return;
			// Enhances the form's appearance.
			Pen Mypen = new Pen(Brushes.WhiteSmoke, 3);
			e.Graphics.DrawLine(Mypen, 0, this.Height - 95, this.Width, this.Height - 95);
			Mypen.Dispose();
			e.Dispose();
		}

		private void Start_Load(object sender, EventArgs e)
		{
			// Hides impertinent buttons.
			if (EnglishMate.Properties.Settings.Default.GrammarHelpEnabled == false) GrammarHelp.Hide();
			if (EnglishMate.Properties.Settings.Default.CompositionExercisesEnabled == false) CompositionHelp.Hide();
			if (EnglishMate.Properties.Settings.Default.GrammarExercisesEnabled == false) GrammarExercises.Hide();
			if (EnglishMate.Properties.Settings.Default.CompositionExercisesEnabled == false) CompositionExercises.Hide();

		}

		private void Button2_Click(object sender, EventArgs e)
		{
			Login();
		}

		private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Panel2.Show();
			UserName.Select();
		}

		private void Password_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) Login();
		}

		private void Login()
		{
			if (string.IsNullOrWhiteSpace(UserName.Text))
			{
				Password.Clear();
				MessageBox.Show("No user-name has been entered.\nPlease enter a valid user-name and password.", "Insert User-Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
				UserName.Select();
			}
			else if (string.IsNullOrWhiteSpace(Password.Text))
			{
				MessageBox.Show("No password has been entered.\nPlease enter a valid user-name and password.", "Insert Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Password.Select();
			}
			else if (UserName.Text.ToUpper() == EnglishMate.Properties.Settings.Default.UserName.ToUpper()
				& Password.Text == EnglishMate.Properties.Settings.Default.Password)
			{
				Account NewAccount = new Account();
				NewAccount.Show();
				Panel2.Hide();
				UserName.Clear();
				Password.Clear();
				this.Hide();
			}
			else
			{
				UserName.Clear();
				Password.Clear();
				MessageBox.Show("An invalid user-name/password combination has been entered.\nPlease enter a valid user-name and password.", "Invalid User-Name/Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
				UserName.Select();
			}
		}

		private void Start_Click(object sender, EventArgs e)
		{
			HidePanel();
		}
		private void HidePanel()
		{
			Panel2.Hide();
			UserName.Clear();
			Password.Clear();
		}

		private void GrammarExercises_Click(object sender, EventArgs e)
		{
			var newStartExercise = new StartExercise().ShowDialog();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			HidePanel();
		}

		private void CompositionExercises_Click(object sender, EventArgs e)
		{
			var newStartExercise = new StartExercise() { Composition = true }.ShowDialog();
		}

		private void GrammarHelp_Click(object sender, EventArgs e)
		{
			var newHelp = new Help().ShowDialog();
		}

		private void CompositionHelp_Click(object sender, EventArgs e)
		{
			var newHelp = new Help() { Composition = true }.ShowDialog();
		}
	}
}
