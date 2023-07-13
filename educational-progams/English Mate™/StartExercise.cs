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
	public partial class StartExercise : Form
	{
		public bool Composition = false;
		public StartExercise()
		{
			InitializeComponent();
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void StartExercise_Shown(object sender, EventArgs e)
		{
			string searchPath;
			if (!Composition) searchPath = MainStaticClass.GrammarDirectory;
			else searchPath = MainStaticClass.CompositionDirectory;
			foreach (var file in Directory.EnumerateFiles(searchPath))
				listBox1.Items.Add(Path.GetFileNameWithoutExtension(file));
			if (listBox1.Items.Count == 0) OKButton.Enabled = false;
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			var invalidName = false;
			// If an invalid name has been entered, get out.
			Array CheckArray = System.IO.Path.GetInvalidFileNameChars();
			foreach (char item in CheckArray)
			{
				if (UserName.Text.Contains(item))
				{
					invalidName = true;
					break;
				}
			}
			if (string.IsNullOrWhiteSpace(UserName.Text)) invalidName = true;
			if (invalidName)
			{
				if (MessageBox.Show("An invalid name has been entered. Please enter a valid name.", "Invalid Name",
				MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel) this.Close();
				return;
			}
			MainStaticClass.CurrentUserName = UserName.Text;

			{
				// If no item has been selected, we're assume it's the first one.
				if (listBox1.SelectedItem == null & listBox1.Items.Count > 0) listBox1.SelectedIndex = 0;
				// If the grammar button was clicked, we're open grammar exercises.
				if (!Composition)
				{
					var NewExercises = new GrammarExercises() { FileLocation = MainStaticClass.GrammarDirectory + "\\" + listBox1.SelectedItem + ".xml" };
					NewExercises.Show();
					// If no issues have been found, we're initialize the student.
					MainStaticClass.currentGradeFile = MainStaticClass.GrammarGradeLocation + UserName.Text + ".txt";
					MainStaticClass.currentStudentFile = MainStaticClass.MainStudentGrammarLocation + UserName.Text + ".txt";
					try
					{
						if (!File.Exists(MainStaticClass.currentGradeFile))
							File.Create(MainStaticClass.currentGradeFile);
						if (!File.Exists(MainStaticClass.currentStudentFile))
							File.Create(MainStaticClass.currentStudentFile);
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
						MessageBox.Show("An error has occurred. Make sure the document has properly been formatted."
										+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
										"\n\nError Code: STEXCGF03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				// If any other exercises button was clicked, we're open composition exercises.
				else
				{
					var NewExercises = new CompositionExercises() { FileLocation = MainStaticClass.CompositionDirectory + "\\" + listBox1.SelectedItem + ".xml" };
					NewExercises.Show();
					// If no issues have been found, we're initialize the student.
					MainStaticClass.currentGradeFile = MainStaticClass.CompositionGradeLocation + UserName.Text + ".txt";
					MainStaticClass.currentStudentFile = MainStaticClass.MainStudentCompositionLocation + UserName.Text + ".txt";
					MainStaticClass.CompositionAssignmentLocation = MainStaticClass.AssignmentLocation + UserName.Text + ".txt";

					try
					{
						if (!File.Exists(MainStaticClass.currentGradeFile))
							File.Create(MainStaticClass.currentGradeFile);
						if (!File.Exists(MainStaticClass.currentStudentFile))
							File.Create(MainStaticClass.currentStudentFile);
						if (!File.Exists(MainStaticClass.CompositionAssignmentLocation))
							File.Create(MainStaticClass.CompositionAssignmentLocation);
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
						MessageBox.Show("An error has occurred. Make sure the document has properly been formatted."
										+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
										"\n\nError Code: STEXCCF03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}

				// We're now hide the Start form and close this form.
				foreach (Form form in Application.OpenForms)
				{
					if (form.Name == "Start") form.Hide();
					this.Close();
				}

			}
		}

	}
}