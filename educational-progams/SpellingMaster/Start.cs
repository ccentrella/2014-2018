using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace SpellingMaster
{
	public partial class Start : Form
	{
		public Start()
		{
			InitializeComponent();
		}

		private void Start_Paint(object sender, PaintEventArgs e)
		{
			// Enhances the form's appearance.
			Pen Mypen = new Pen(Brushes.WhiteSmoke, 3);
			e.Graphics.DrawLine(Mypen, 0, 258, this.Width, 258);
			Mypen.Dispose();
			e.Dispose();
		}

		private void Start_Load(object sender, EventArgs e)
		{
			if (SpellingMaster.Properties.Settings.Default.HelpEnabled == false) Help.Hide();
			if (SpellingMaster.Properties.Settings.Default.ExercisesEnabled == false) ExercisesButton.Hide();
			if (SpellingMaster.Properties.Settings.Default.GamesEnabled == false) Games.Hide();
			if (SpellingMaster.Properties.Settings.Default.ShowSpeakerReminder)
			{
				if (MessageBox.Show("If the program doesn't appear to be working:\n\n1. Ensure that your speakers are connected and turned on."
					+ "\n2. Ensure that Windows® detects your speakers.\n3. Ensure that your speakers are not muted. If your speakers are muted:"
				+ "\nClick the volume icon in the taskbar and then click the red circle.\n4. Ensure that the volume is set reasonably high, not very low."
				+ "\nClick the volume icon in the taskbar and slide the bar up.\nIf your speakers have a volume switch, ensure that the switch is"
				+ " set to at least the middle setting.\n\nDo you want to see this message next time?", "Speaker Reminder",
				MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
				{
					SpellingMaster.Properties.Settings.Default.ShowSpeakerReminder = false;
					SpellingMaster.Properties.Settings.Default.Save();
				}
			}
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

		private void Button1_Click_1(object sender, EventArgs e)
		{
			HidePanel();
		}

		private void Password_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) Login();
		}

		private void Login()
		{
			if (string.IsNullOrWhiteSpace(UserName.Text))
			{
				MessageBox.Show("No user-name has been entered.\nPlease enter a valid user-name and password.", "Insert User-Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Password.Clear();
				Password.ClearUndo();
				UserName.Select();
			}
			else if (string.IsNullOrWhiteSpace(Password.Text))
			{
				MessageBox.Show("No password has been entered.\nPlease enter a valid user-name and password.", "Insert Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Password.Select();
			}
			else if (UserName.Text.ToUpper() == SpellingMaster.Properties.Settings.Default.UserName.ToUpper()
				& Password.Text == SpellingMaster.Properties.Settings.Default.Password)
			{
				Account NewAccount = new Account();
				NewAccount.Show();
				Panel2.Hide();
				UserName.Clear();
				UserName.ClearUndo();
				Password.Clear();
				Password.ClearUndo();
				this.Hide();
			}
			else
			{
				UserName.Clear();
				UserName.ClearUndo();
				Password.Clear();
				Password.ClearUndo();
				MessageBox.Show("An invalid user-name/password combination has been entered.\nPlease enter a valid user-name and password.", "Invalid User-Name/Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
				UserName.Select();
			}
		}

		private void Help_Click(object sender, EventArgs e)
		{
			Help HelpForm = new Help();
			HelpForm.Show();
			this.Hide();
		}

		private void Exercises_Click(object sender, EventArgs e)
		{
			string Input = Interaction.InputBox("Please enter your name.", "Enter Name (Auto-Helper)");
			bool InvalidCharacters = false;
			if (Input != "")
			{
				Array CheckArray = System.IO.Path.GetInvalidFileNameChars();
				foreach (char item in CheckArray)
				{
					if (Input.Contains(item)) InvalidCharacters = true;
				}
				if (!InvalidCharacters)
				{
					Exercises NewExercises = new Exercises();
					NewExercises.Show();
					MainStaticClass.CurrentUserName = Input.Trim();
					MainStaticClass.MainFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\SpellingNames\\" + Input.Trim() + ".txt";
					MainStaticClass.GradeFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\Grades\\" + Input.Trim() + ".txt";

					this.Hide();
				}
			}
		}

		private void Games_Click(object sender, EventArgs e)
		{
			string Input = Interaction.InputBox("Please enter your name.", "Enter Name (Auto-Helper)");
			bool InvalidCharacters = false;
			if (Input != "")
			{
				Array CheckArray = System.IO.Path.GetInvalidFileNameChars();
				foreach (char item in CheckArray)
				{
					if (Input.Contains(item)) InvalidCharacters = true;
				}
				if (!InvalidCharacters)
				{
					Games NewGames = new Games();
					NewGames.Show();
					MainStaticClass.CurrentUserName = Input.Trim();
					this.Hide();
				}
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
			UserName.ClearUndo();
			Password.Clear();
			Password.ClearUndo();
		}
	}
}
