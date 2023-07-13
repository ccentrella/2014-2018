using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpellingMaster
{
	public partial class Account : Form
	{
		public Account()
		{
			InitializeComponent();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Settings NewSettings = new Settings();
			NewSettings.ShowDialog();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// This will delete all personal files.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, EventArgs e)
		{
			DialogResult Result = MessageBox.Show("The log will be deleted permanently. Are you sure you would like to continue?", "Delete Log?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if(Result == DialogResult.Yes)
			{
				System.IO.File.WriteAllText(MainStaticClass.LogFileLocation, "");
				WebbrowserLoad();
			}
		}

		private void Account_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (Form Form in Application.OpenForms)
			{
				if (Form.Name == "Start") Form.Show();
			}	
		}

		private void WebbrowserLoad()
		{
			try
			{
				string input = ""; // This is used for input for the user profiles.
				string NavLinks = "<div style=\"background-color:darkgray\">\n"; // This is used to allow the user to navigate to different sections.
				string LogInput = System.IO.File.ReadAllText(MainStaticClass.LogFileLocation); // This is used to display the history log.
				foreach (string CurrentFile in System.IO.Directory.EnumerateFiles(MainStaticClass.MainFileLocation))
				{
					string Title = System.IO.Path.GetFileNameWithoutExtension(CurrentFile);
					string CurrentGradeFile = MainStaticClass.GradeLocation + "\\" + Title + ".txt";
					decimal GradeInput = 0;
					int Lines = 0;
					StreamReader GradeReader = new StreamReader(CurrentGradeFile);
					while (GradeReader.Peek() > -1)
					{
						GradeInput += Convert.ToInt32(GradeReader.ReadLine().Replace("\n", ""));
						Lines++;
					}
					GradeReader.Close();
					decimal Grade = 0;
					if (Lines > 0) Grade = decimal.Divide(GradeInput, Lines);
					Grade = Math.Round(Grade);
					System.IO.StreamReader NewStreamReader = new System.IO.StreamReader(CurrentFile);
					input += "\n<h3 id=\"" + Title + "\">" + Title + "</h3>\n";
					input += "<h4>Grade: " + Grade.ToString() + " (" + Lines.ToString() + ")";
					if (Lines < 15) input += " NOT SET";
					input += "</h4>\n";
						input += "<div>\n<ul>\n";
					NavLinks += "<a href=\"#" + Title + "\">" + Title + "</a>";
					while (NewStreamReader.Peek() > -1)
					{
						input += "<li>" + NewStreamReader.ReadLine() + "</li>";
					}
					input += "\n</ul></div>";
					NewStreamReader.Close();
				}
				NavLinks += "\n<a href=\"#Log\">Log</a></div>\n<h2>Misspelled Words</h2>\n";
				webBrowser1.DocumentText = "<!DOCTYPE html>"
	+ "<html>"
	+ "<head>"
	+ "<title>Account (Auto-Helper)</title>"
	+ "<style type=\"text/css\">"
	+ "h1{"
	+ "color:blueviolet;"
	+ "font-family:Cambria"
	+ "}"
	+ "div{"
	+ "border: 2px solid black;"
	+ "padding:5px;"
	+ "font-weight:bold;"
	+ "}"
	+ "a{"
	+ "color:black;"
	+ "text-decoration:none;"
	+ "padding:5px;"
	+ "font-weight:bold;"
	+ "}"
	+ "a:hover{"
	+ "background-color:gray;"
	+ "color:white"
	+ "}"
	+ "</style>"
	+ "</head>"
	+ "<body style = \"font-family: Calibri; font-size: 12px;background-color:darkgray\">"
	+ "<h1>Auto-Helper</h1>"
	+ "<h2>Shortcuts</h2>"
	+ NavLinks + input + "\n<h2 id = \"Log\">Log</h2>" + LogInput.Replace("\n", "<br/>") + "<a style = \"bottom:15px;right:10px;position:fixed;font-family:wingdings\" href = \"#\">é</a>"
	+ "\n</body>\n</html>";

			}

			catch (System.IO.IOException)
			{
				MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception)
			{
				MessageBox.Show("An error has occurred. Please report this problem to Autosoft. We are very sorry for the inconvenience. \n\nError Code: ACCLD03", "Error (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void Account_Load(object sender, EventArgs e)
		{
			WebbrowserLoad();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			WebbrowserLoad();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			DialogResult Result = MessageBox.Show("The word-lists and grade data will be deleted permanently. Are you sure you would like to continue?", "Delete Personal Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (Result == DialogResult.Yes)
			{
				foreach (string PersonalFile in Directory.EnumerateFiles(MainStaticClass.MainFileLocation))
				{
					File.Delete(PersonalFile);
				}
				foreach (string PersonalFile in Directory.EnumerateFiles(MainStaticClass.GradeLocation))
					File.Delete(PersonalFile);
				WebbrowserLoad();
			}
			}

		private void button3_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("If the program doesn't appear to be working:\n\n1. Ensure that your speakers are connected and turned on."
					+ "\n2. Ensure that Windows® detects your speakers.\n3. Ensure that your speakers are not muted. If your speakers are muted:"
				+ "\nClick the volume icon in the taskbar and then click the red circle.\n4. Ensure that the volume is set reasonably high, not very low."
				+ "\nClick the volume icon in the taskbar and slide the bar up.\nIf your speakers have a volume switch, ensure that the switch is"
				+ " set to at least the middle setting.\n\nDo you want to see this message next time?", "Speaker Reminder",
				MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
				SpellingMaster.Properties.Settings.Default.ShowSpeakerReminder = true;
			else
				SpellingMaster.Properties.Settings.Default.ShowSpeakerReminder = false;
			SpellingMaster.Properties.Settings.Default.Save();
		}
		}
	}

