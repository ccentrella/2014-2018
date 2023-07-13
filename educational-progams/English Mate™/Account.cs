using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnglishMate
{
	public partial class Account : Form
	{
		public Account()
		{
			InitializeComponent();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			var NewSettings = new Settings().ShowDialog();
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
			DialogResult Result = MessageBox.Show("The logs will be deleted permanently. Are you sure you would like to continue?", "Delete Log?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (Result == DialogResult.Yes)
				try
				{
					System.IO.File.WriteAllText(MainStaticClass.GrammarLogFile, "");
					System.IO.File.WriteAllText(MainStaticClass.CompositionLogFile, "");

					WebbrowserLoad();
				}
				catch (IOException)
				{
					MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (UnauthorizedAccessException)
				{
					MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error has occurred. Please report this problem to Autosoft. We are very sorry for the inconvenience. \n\nError Code: ACCDL03\n\n" + ex.Message, "Error (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				var compCompleteFiles = new List<string>();
				string Grammarinput = ""; // This is used for input for the user profiles.
				string Compositioninput = ""; // This is used for input for the user profiles.
				// This is used to allow the user to navigate to different sections.
				string GrammarNavLinks = "<div style=\"background-color:darkgray\">\n";
				// This is used to allow the user to navigate to different sections.
				string CompositionNavLinks = "<div style=\"background-color:darkgray\">\n";
				// This is used to display the grammar history log.
				string GrammarLogInput = System.IO.File.ReadAllText(MainStaticClass.GrammarLogFile);
				// This is used to display the grammar history log.
				string CompositionLogInput = System.IO.File.ReadAllText(MainStaticClass.CompositionLogFile);
				// View grammar files.
				foreach (string CurrentGrammarFile in System.IO.Directory.EnumerateFiles(MainStaticClass.MainStudentGrammarLocation))
				{
					string Title = System.IO.Path.GetFileNameWithoutExtension(CurrentGrammarFile);
					string CurrentGradeFile = MainStaticClass.GrammarGradeLocation + Title + ".txt";
					string CompositionCurrentGradeFile = MainStaticClass.CompositionGradeLocation + Title + ".txt";
					string CurrentCompositionFile = MainStaticClass.MainStudentCompositionLocation + Title + ".txt";
					compCompleteFiles.Add(CurrentCompositionFile.ToUpper());
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
					System.IO.StreamReader NewGrammarReader = new System.IO.StreamReader(CurrentGrammarFile);
					Grammarinput += "\n<h3 id=\"G" + Title + "\" style = \"font-family:Cambria\">" + Title + "</h3>\n";
					Grammarinput += "<h4>Grammar Grade: " + Grade.ToString() + " (" + Lines.ToString() + ")";
					if (Lines < 15) Grammarinput += " NOT SET";
					Grammarinput += "</h4>";
					if (File.Exists(CurrentCompositionFile))
					{
						var CompositionGradeReader = new StreamReader(CompositionCurrentGradeFile);
						decimal CompositionGradeInput = 0;
						while (CompositionGradeReader.Peek() > -1)
						{
							CompositionGradeInput += Convert.ToInt32(CompositionGradeReader.ReadLine().Replace("\n", ""));
							Lines++;
						}
						CompositionGradeReader.Close();
						decimal CompositionGrade = 0;
						if (Lines > 0) CompositionGrade = decimal.Divide(CompositionGradeInput, Lines);
						CompositionGrade = Math.Round(CompositionGrade);
						Grammarinput += "<h4>Composition Grade: " + CompositionGrade.ToString() + " (" + Lines.ToString() + ")";
						if (Lines < 15) Grammarinput += " NOT SET";
						Grammarinput += "</h4>";
					}
					GrammarNavLinks += "<a href=\"#G" + Title + "\">" + Title + "</a>";
					Grammarinput += "\n<div><h3 style = \"font-family:Cambria\">Grammar Errors</h3><p>";
					while (NewGrammarReader.Peek() > -1)
					{
						Grammarinput += NewGrammarReader.ReadLine() + "</br>";
					}
					Grammarinput += "</p>";
					if (File.Exists(CurrentCompositionFile))
					{
						CompositionNavLinks += "<a href=\"#G" + Title + "\">" + Title + "</a>";
						var NewCompositionReader = new System.IO.StreamReader(CurrentCompositionFile);
						Grammarinput += "<h3 style = \"font-family:Cambria\">Composition Errors</h3><p>";
						while (NewCompositionReader.Peek() > -1)
						{
							Grammarinput += NewCompositionReader.ReadLine() + "<br/>";
						}
						var currentAssignmentUrl = MainStaticClass.AssignmentLocation + Title + ".txt";
						var currentAssignmentData = File.ReadAllText(currentAssignmentUrl);
						Grammarinput += "</p>\n<h3 style = \"font-family:Cambria\">Assignments</h3><p>" + currentAssignmentData.Replace("\n", "\n<br/>") + "</p>";
						NewCompositionReader.Close();
					}
					Grammarinput += "</div>";
					NewGrammarReader.Close();
				}

				GrammarNavLinks += "\n<a href=\"#GLog\">Grammar Log</a></div>\n";
				// View composition files.
				foreach (string CurrentCompositionFile in System.IO.Directory.EnumerateFiles(MainStaticClass.MainStudentCompositionLocation))
				{
					if (compCompleteFiles.Contains(CurrentCompositionFile.ToUpper())) continue;
					string Title = System.IO.Path.GetFileNameWithoutExtension(CurrentCompositionFile);
					string CurrentGradeFile = MainStaticClass.CompositionGradeLocation + Title + ".txt";
					decimal GradeInput = 0;
					int Lines = 0;
					var GradeReader = new StreamReader(CurrentGradeFile);
					var currentAssignmentUrl = MainStaticClass.AssignmentLocation + Title + ".txt";
					var currentAssignmentData = File.ReadAllText(currentAssignmentUrl);
					while (GradeReader.Peek() > -1)
					{
						GradeInput += Convert.ToInt32(GradeReader.ReadLine().Replace("\n", ""));
						Lines++;
					}
					decimal Grade = 0;
					if (Lines > 0) Grade = decimal.Divide(GradeInput, Lines);
					Grade = Math.Round(Grade);
					var NewCompositionReader = new System.IO.StreamReader(CurrentCompositionFile);
					Compositioninput += "\n<h3 id=\"C" + Title + "\" style = \"font-family:Cambria\">" + Title + "</h3>\n";
					Compositioninput += "<h4>Composition Grade: " + Grade.ToString() + " (" + Lines.ToString() + ")";
					if (Lines < 15) Compositioninput += " NOT SET";
					Compositioninput += "</h4>\n<div>\n<h3 style = \"font-family:Cambria\">Composition Errors</h3><p>";
					CompositionNavLinks += "<a href=\"#C" + Title + "\">" + Title + "</a>";
					while (NewCompositionReader.Peek() > -1)
					{
						Compositioninput += NewCompositionReader.ReadLine() + "<br/>";
					}
					Compositioninput += "</p>\n<h3 style = \"font-family:Cambria\">Assignments</h3><p>" + currentAssignmentData.Replace("\n", "\n<br/>") + "</p></div>";
					NewCompositionReader.Close();
				}
				CompositionNavLinks += "\n<a href=\"#CLog\">Composition Log</a></div>";
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
				+ "<h2>Grammar Shortcuts</h2>"
				+ GrammarNavLinks
				+ "<h2>Composition Shortcuts</h2>"
				+ CompositionNavLinks + Grammarinput + Compositioninput + "\n<h2 id = \"GLog\">Grammar Log</h2>\n"
				+ GrammarLogInput.Replace("\n", "<br/>") + "\n<h2 id = \"CLog\">Composition Log</h2>\n"
				+ CompositionLogInput.Replace("\n", "<br/>")
				+ "<a style = \"bottom:15px;right:10px;position:fixed;font-family:wingdings\" href = \"#\">é</a>"
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
			catch (Exception ex)
			{
				MessageBox.Show("An error has occurred. Please report this problem to Autosoft. "
				+ "We are very sorry for the inconvenience. \n\nError Code: ACCLD03\n\n"
				+ ex.Message, "Error (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			DialogResult Result = MessageBox.Show("The word-lists and grade data will be deleted permanently. "
			+ "Are you sure you would like to continue?", "Delete Personal Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (Result == DialogResult.Yes)
			{
				try
				{
					foreach (string PersonalFile in Directory.EnumerateFiles(MainStaticClass.MainStudentGrammarLocation))
						File.Delete(PersonalFile);
					foreach (string PersonalFile in Directory.EnumerateFiles(MainStaticClass.MainStudentCompositionLocation))
						File.Delete(PersonalFile);
					foreach (string PersonalFile in Directory.EnumerateFiles(MainStaticClass.GrammarGradeLocation))
						File.Delete(PersonalFile);
					foreach (string PersonalFile in Directory.EnumerateFiles(MainStaticClass.CompositionGradeLocation))
						File.Delete(PersonalFile);
					WebbrowserLoad();
				}
				catch (IOException)
				{
					MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				catch (UnauthorizedAccessException)
				{
					MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error has occurred. Please report this problem to Autosoft. "
					 + "We are very sorry for the inconvenience. \n\nError Code: ACCDF03\n\n"
					 + ex.Message, "Error (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			MessageBox.Show(MainStaticClass.WelcomeMessage, "Welcome to English Mate™",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

	}
}

