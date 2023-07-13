using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace EnglishMate
{
	public partial class CompositionExercises : Form
	{
		public string FileLocation;
		List<string> completeCompositionList = new List<string>();
		List<string> completeAnswerList = new List<string>();
		List<string> answerLabelList = new List<string>(12);
		Random mainRandom = new Random();
		List<string> gradingFilter = new List<string>();
		bool isCaseSensitive = false;
		bool requireCommas = true;
		public bool REDO = false;
		bool Completed = false;
		string compositionData;
		int incorrectAnswers = 0;
		int totalAnswers = 12;
		string incorrectSentences;
		Color NewForeColor;
		Color NewBackColor;
		Color MainBackColor;
		bool AutomaticComplete = EnglishMate.Properties.Settings.Default.AutoCompleteEnabled;
		bool AllowBlankSpaces = EnglishMate.Properties.Settings.Default.AllowBlankAnswers;
		public CompositionExercises()
		{
			InitializeComponent();
		}
		// We're going to update the form here.
		private void CompositionExercises_Shown(object sender, EventArgs e)
		{
			InitializeExerciseLists();
			InitializeExercises();
			InitializeForm();
		}

		protected override bool IsInputKey(Keys KeyData)
		{
			if (KeyData == Keys.Escape)
				return true;
			else
				return false;
		}
		private void InitializeExerciseLists()
		{
			// We're ensuring the exercise definitions file exist.
			if (!File.Exists(FileLocation))
			{
				MessageBox.Show("The file could not be found. Please select a different exercise.",
					"File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Completed = true;
				foreach (Form form in Application.OpenForms)
					if (form.Name == "Start") form.Show();
				this.Close();
				return;
			}
			// If the file exists, we'll now continue.
			try
			{
				// This represents the file-data, which contains all elements, including the exercises and instructions.
				var Data = File.ReadAllText(FileLocation);
				var dataIndex = Data.IndexOf("<data>");
				var dataEndIndex = Data.IndexOf("</data>");
				var instructionsIndex = Data.IndexOf("<instructions");
				var instructionsEndIndex = Data.IndexOf("</instructions>");
				var caseSensitiveIndex = Data.IndexOf("<caseSensitive>") + 15;
				var caseSensitiveEndIndex = Data.IndexOf("</caseSensitive>");
				var commaRequiredIndex = Data.IndexOf("<commaRequired>") + 15;
				var commaRequiredEndIndex = Data.IndexOf("</commaRequired>");
				var filterIndex = Data.IndexOf("<filter>") + 8;
				var filterEndIndex = Data.IndexOf("</filter>");
				// If the document is not properly set up, get out.
				if (dataIndex == -1 | dataEndIndex == -1 | instructionsIndex == -1 |
					 caseSensitiveIndex == 14 | caseSensitiveEndIndex == -1
						| commaRequiredIndex == 14 | commaRequiredEndIndex == -1
						| filterIndex == 7 | filterEndIndex == -1)
				{
					MessageBox.Show("The definitions file contains errors. Please select a different exercise.",
						"File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
					return;
				}
				// If the file exists, and the document has been properly set up, we'll now continue.
				// These variables represent specific elements.
				var caseSensitive = Data.Substring(caseSensitiveIndex, caseSensitiveEndIndex - caseSensitiveIndex);
				var commaRequired = Data.Substring(commaRequiredIndex, commaRequiredEndIndex - commaRequiredIndex);
				var filterData = Data.Substring(filterIndex, filterEndIndex - filterIndex);
				var ExerciseData = Data.Substring(dataIndex + 6, dataEndIndex - dataIndex - 6);
				var InstructionsData = Data.Substring(instructionsIndex + 14, instructionsEndIndex
					- instructionsIndex - 14);
				// Perform work on basic elements.
				if (!string.IsNullOrEmpty(filterData) & filterData.LastIndexOf(",") != filterData.Length - 1)
					filterData += ","; // Automatically end filterData with a comma if necessary.
				if (caseSensitive == "true") isCaseSensitive = true; // Sets the case-sensitive values if necessary.
				if (commaRequired == "false") requireCommas = false; // Sets the require-comma value if necessary.
				if (ExerciseData.ElementAt(ExerciseData.Length - 1).ToString() != ";") ExerciseData += ";";
				if (!string.IsNullOrWhiteSpace(InstructionsData)) Title.Text = InstructionsData;
				// Adds all filters to the filter array.
				var filterCheckComplete = false;
				var startIndex = 0;
				while (!filterCheckComplete)
				{
					var commaIndex = filterData.IndexOf(",", startIndex); // This represents the index of the current comma.
					if (commaIndex != -1) // If the filter-data contains a comma, we're now continue.
					{
						if (commaIndex != 0)
						{
							var filterString = filterData.Substring(startIndex, commaIndex - startIndex);
							gradingFilter.Add(filterString);
						}
						startIndex = commaIndex + 1;
					}
					else
						filterCheckComplete = true;
				}
				// Strip out a space between all colons and semicolons not enclosed in quotation marks.
				ExerciseData = ExerciseData.Replace(" :", ":").Replace(": ", ":").Replace(" ;", ";").Replace("; ", ";");
				// We're now starting the major task of loading each and every question into the total question list.
				var sIndex = 0;
				var complete = false;
				// This is the point at which each iteration starts.
				while (!complete)
				{
					// This represents the index of the semi-colon.
					var semiColonInt = ExerciseData.IndexOf(";", sIndex);
					// This searches for the next semi-colon (if necessary).
					// var nextSemiColonInt = ExerciseData.IndexOf(";", semiColonInt + 1);
					if (semiColonInt == -1)
					{
						semiColonInt = ExerciseData.Length - 1;
						complete = true;
					}
					else if (semiColonInt + 1 == ExerciseData.Length | semiColonInt <= 0
					   || ExerciseData.ElementAt(semiColonInt + 1).ToString() != "\"" ||
				  ExerciseData.ElementAt(semiColonInt - 1).ToString() != "\"")
					{
						string currentSubstring;
						currentSubstring = ExerciseData.Substring(sIndex, semiColonInt - sIndex);
						var instanceFound = false;
						string currentQuestion;
						string currentAnswer;
						var colonindex = 0;
						var startLocation = 0;
						// Determines the question versus the answer.
						while (!instanceFound)
						{
							var colonInt = currentSubstring.IndexOf(":", startLocation);
							if (colonInt == -1)
							{
								colonindex = currentSubstring.Length - 1;
								instanceFound = true;
							}
							else if (colonInt + 1 == ExerciseData.Length | colonInt <= 0
						|| ExerciseData.ElementAt(colonInt + 1).ToString() != "\"" ||
				   ExerciseData.ElementAt(colonInt - 1).ToString() != "\"")
							{
								colonindex = colonInt;
								instanceFound = true;
							}
							// Let's begin searching for the next colon.
							else startLocation++;
						}
						if (colonindex > 0) currentQuestion = currentSubstring.Substring(0, colonindex);
						else currentQuestion = "";
						if (colonindex > 0 & colonindex < currentSubstring.Length - 1)
							currentAnswer = currentSubstring.Substring(colonindex + 1);
						else currentAnswer = "";
						completeCompositionList.Add(currentQuestion);
						completeAnswerList.Add(currentAnswer);
						sIndex = semiColonInt + 1;
					}
					// If the semicolon is surrounded with quotation marks, it won't count.
					else
						sIndex++;
				}
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
				MessageBox.Show("An error has occurred. Please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: CEXPLD03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void InitializeExercises()
		{
			try
			{
				if (completeCompositionList.Count < 1)
				{
					var result = MessageBox.Show("The file contains no entries. Please select a different exercise.",
					"File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Completed = true;
					foreach (Form form in Application.OpenForms)
						if (form.Name == "Start") form.Show();
					this.Close();
					return;
				}
				var problemLabelList = new List<Label>(){Sentence1,Sentence2,Sentence3,Sentence4,
			Sentence5,Sentence6,Sentence7,Sentence8,Sentence9,Sentence10,Sentence11,Sentence12,
			};
				var MyTextboxList = new List<TextBox>(){Answer1,Answer2,Answer3,Answer4,Answer5,
					Answer6,Answer7,Answer8,Answer9,Answer10,Answer11,Answer12};
				SuspendLayout();
				problemLabelList.ForEach(delegate(Label currentLabel)
				{
					var currentIndex = problemLabelList.IndexOf(currentLabel);
					var index = mainRandom.Next(0, completeCompositionList.Count);
					var newProblem = completeCompositionList.ElementAt(index);
					var newAnswer = completeAnswerList.ElementAt(index);
					currentLabel.Text = newProblem;
					var currentAnswerTextBox = MyTextboxList.ElementAt(currentIndex);
					currentAnswerTextBox.Height = currentLabel.Height;
					answerLabelList.Add(newAnswer);
					if (completeCompositionList.Count >= 12)
					{
						completeCompositionList.RemoveAt(index);
						completeAnswerList.RemoveAt(index);
					}
				}
					);
				ResumeLayout();
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error has occurred. Please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: GREXLD03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void InitializeForm()
		{
			switch (EnglishMate.Properties.Settings.Default.Theme)
			{
				case "Blue":
					{
						NewForeColor = Color.White;
						NewBackColor = Color.RoyalBlue;
						MainBackColor = Color.DarkBlue;
						break;
					}

				case "Dark":
					{
						NewForeColor = Color.Black;
						NewBackColor = Color.DimGray;
						MainBackColor = Color.Gray;
						break;
					}
				case "Light":
					{
						NewForeColor = Color.Black;
						NewBackColor = Color.Silver;
						MainBackColor = Color.LightGray;
						break;
					}
				case "Standard":
					{
						NewForeColor = Color.White;
						NewBackColor = Color.DimGray;
						MainBackColor = Color.Gray;
						break;
					}
			}
			panel1.BackColor = MainBackColor;
			MainPanel.BackColor = MainBackColor;
			Title.ForeColor = NewForeColor;
			foreach (Control CurrentControl in MainPanel.Controls)
			{
				if (CurrentControl.GetType() == typeof(Panel))
				{
					CurrentControl.ForeColor = NewForeColor;
					CurrentControl.BackColor = NewBackColor;
				}
				else if (CurrentControl.GetType() == typeof(Label)) CurrentControl.ForeColor = NewForeColor;
			}



		}

		private void completeButton_Click(object sender, EventArgs e)
		{
			Complete();
		}

		private void CheckAnswers()
		{

			TextBox[] MyTextboxArray = new TextBox[]{ Answer1,Answer2,Answer3,Answer4,Answer5,Answer6,Answer7,Answer8,Answer9,Answer10,
				Answer11,Answer12};
			List<TextBox> MyTextboxList = new List<TextBox>(MyTextboxArray);
			Label[] MyCheckArray = new Label[]{ Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,
				Check11,Check12};
			List<Label> MyCheckList = new List<Label>(MyCheckArray);

			SuspendLayout();
			// Disables each textbox, checks each answer, shows each check-mark, and populates the grade.
			MyTextboxList.ForEach(delegate(TextBox currentTextBox)
			{
				currentTextBox.Enabled = false;
				var elementLocation = MyTextboxList.IndexOf(currentTextBox);
				string answer = answerLabelList.ElementAt(elementLocation); // Represents the correct answer.
				var currentCheck = MyCheckList.ElementAt(elementLocation);
				var questionLabelList = new List<Label>(){Sentence1,Sentence2,Sentence3,Sentence4,
			Sentence5,Sentence6,Sentence7,Sentence8,Sentence9,Sentence10,Sentence11,Sentence12};
				if (answer.ToUpper() != "ASSIGNMENT") // Evaluates to true if the current question is not an assignment.
				{
					if (answer.ToUpper() == "\"ASSIGNMENT\"") answer = answer.Replace("\"", "");
					string studentAnswer = currentTextBox.Text; // Represents the student answer.
					if (!isCaseSensitive)
					{
						answer = answer.ToUpper();
						studentAnswer = studentAnswer.ToUpper();
					}
					if (requireCommas)
					{
						answer = answer.Replace(",", "");
						studentAnswer = studentAnswer.Replace(",", "");
					}
					var answerBuilder = new StringBuilder(answer); // Allows for filters.
					var studentAnswerBuilder = new StringBuilder(studentAnswer);
					studentAnswerBuilder.Replace("\";\"", ";");
					studentAnswerBuilder.Replace("\":\"", ":");
					gradingFilter.ForEach(delegate(string currentFilter)
				{
					answerBuilder.Replace(currentFilter, "");
					studentAnswerBuilder.Replace(currentFilter, "");
				});
					answer = answerBuilder.ToString();
					studentAnswer = studentAnswerBuilder.ToString();
					if (studentAnswer == answer)
					{
						currentCheck.ForeColor = Color.DarkGreen;
						currentCheck.Text = "";
					}
					else
					{
						currentCheck.ForeColor = Color.DarkRed;
						currentCheck.Text = "";
						incorrectAnswers += 1;
						incorrectSentences += "Question:\r\n\r\n" + questionLabelList.ElementAt(elementLocation)
						+ "\r\n(" + DateTime.Now + ")\r\n\r\nStudent Answer:\r\n\r\n" + currentTextBox.Text + "\r\n\r\n";

					}
				}
				else
				{
					currentCheck.ForeColor = Color.Blue;
					currentCheck.Text = "";
					totalAnswers--;
					compositionData += "Question:\r\n\r\n" + questionLabelList.ElementAt(elementLocation)
						+ "\r\n(" + DateTime.Now + ")\r\n\r\nStudent Answer:\r\n\r\n" + currentTextBox.Text + "\r\n\r\n";
				}
				currentCheck.Show();
			});
			ResumeLayout();
			int CorrectAnswers = totalAnswers - incorrectAnswers;
			decimal FullGrade = decimal.Divide(CorrectAnswers, totalAnswers) * 100;
			MainStaticClass.Grade = Math.Round(FullGrade, MidpointRounding.AwayFromZero);
			// Writes to the log file.
			try
			{
				string Input = "ERROR | Information not available";
				if (!REDO) Input = DateTime.Now + "  " + MainStaticClass.CurrentUserName + "  " + "Grade: " + MainStaticClass.Grade.ToString();
				else Input = DateTime.Now + "  " + MainStaticClass.CurrentUserName + "  " + "Grade: " + MainStaticClass.Grade.ToString()
					+ " - Retaken";
				StreamWriter MainWriter = new StreamWriter(MainStaticClass.CompositionLogFile, true);
				MainWriter.WriteLine(Input);
				MainWriter.Close();
				StreamWriter NewWriter = new StreamWriter(MainStaticClass.currentStudentFile, true);
				NewWriter.Write(incorrectSentences);
				NewWriter.Close();
				// Writes to the composition file.
				var NewAssignmentWriter = new StreamWriter(MainStaticClass.CompositionAssignmentLocation, true);
				NewAssignmentWriter.Write(compositionData);
				NewAssignmentWriter.Close();
				// Updates the Average Grade
				StreamWriter GradeStreamWriter = new StreamWriter(MainStaticClass.currentGradeFile, true);
				string input = MainStaticClass.Grade.ToString();
				GradeStreamWriter.WriteLine(input);
				GradeStreamWriter.Close();
			}
			catch (Exception ex)
			{
				StreamWriter MainWriter = new StreamWriter(MainStaticClass.CompositionLogFile, true);
				MainWriter.WriteLine(DateTime.Now + "  " + MainStaticClass.CurrentUserName + "  "
					+ "ERROR: Graded Answers " + ex.Message);
				MainWriter.Close();
			}

		}
		private void Item_Click(object sender, EventArgs e)
		{
			if (timer1.Enabled)
			{
				timer1.Enabled = false;
				this.Opacity = 100;
				completeButton.Show();
				completeButton.Text = "&OK";
			}
		}

		private void Answers_TextChanged(object sender, EventArgs e)
		{
			if (AutomaticComplete)
			{
				timer2.Stop();
				timer2.Start();
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.Opacity > 0) this.Opacity -= 0.001;
			else ShowCongratulationsScreen();
		}

		private void ShowCongratulationsScreen()
		{
			timer1.Enabled = false;
			Completed = true;
			Congratulations NewCongratulations = new Congratulations() { Composition = false };
			NewCongratulations.Show();
			if (MainStaticClass.Grade >= 70) this.Close();
			else
			{
				REDO = true;
				this.Hide();
				SuspendLayout();
				//This initializes the values for all check marks.
				Label[] MyCheckArray = new Label[]{ Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,
				Check11,Check12};
				//This initializes the values for all answers.
				TextBox[] AnswerArray = new TextBox[]{ Answer1,Answer2,Answer3,Answer4,Answer5,Answer6,Answer7,Answer8,Answer9,Answer10,
				Answer11,Answer12};
				List<TextBox> AnswerList = new List<TextBox>(AnswerArray);
				List<Label> CheckList = new List<Label>(MyCheckArray);	//This creates the check-mark list.
				//Initializes all answers.
				foreach (TextBox Answer in AnswerList)
				{
					Answer.Clear();
					Answer.Enabled = true;
				}
				//Hides all marks.
				foreach (Label Checkmark in CheckList)
				{
					Checkmark.Hide();
					Checkmark.Text = "";
					Checkmark.ForeColor = Color.Black;
				}
				// Initializes other features
				completeButton.Text = "&Complete";
				completeButton.Show();
				this.Opacity = 100;
				incorrectAnswers = 0;
				totalAnswers = 12;
				redoIndicator.Show(); // Lets the user show that this is a previous assignment.
				ResumeLayout();
			}

		}

		private void Exercises_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (REDO)
			{
				e.Cancel = true;
				MessageBox.Show("Please complete all problems.", "Complete Problems", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (Completed == false)
			{
				DialogResult Result = MessageBox.Show("Are you sure you would like to exit?", "Warning (Auto-Helper)",
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (Result == DialogResult.No) e.Cancel = true;
				else
				{
					Completed = true;
					Application.Exit();
				}
			}
		}

		private void Answers_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape & e.Shift &&
				Microsoft.VisualBasic.Interaction.InputBox("Please enter the password.",
				"Enter Password (Auto-Helper)") == EnglishMate.Properties.Settings.Default.Password)
			{
				REDO = false;
				Application.Exit();
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			// This initializes the values for all answers.
			TextBox[] AnswerArray = new TextBox[]{ Answer1,Answer2,Answer3,Answer4,Answer5,Answer6,Answer7,Answer8,Answer9,Answer10,
				Answer11,Answer12};
			List<TextBox> AnswerList = new List<TextBox>(AnswerArray);
			bool Blank = false;
			foreach (TextBox Answer in AnswerList)
			{
				if (Answer.Text == "")
				{
					timer2.Stop();
					Blank = true;
				}
			}
			if (!Blank) Complete(); // Grades the answers if none are blank.
		}

		private void Complete()
		{
			bool Blank = false;
			TextBox[] MyTextboxArray = new TextBox[]{ Answer1,Answer2,Answer3,Answer4,Answer5,Answer6,Answer7,Answer8,Answer9,Answer10,
				Answer11,Answer12};
			List<TextBox> MyTextboxList = new List<TextBox>(MyTextboxArray);
			foreach (TextBox Answer in MyTextboxList)
			{
				if (string.IsNullOrWhiteSpace(Answer.Text))
				{
					Graphics NewGraphics = Answer.CreateGraphics();
					Pen MyPen = new Pen(Brushes.DarkRed, 2);
					NewGraphics.DrawRectangle(MyPen, Answer.ClientRectangle);
					Blank = true;
					NewGraphics.Dispose();
					MyPen.Dispose();
				}
			}
			if (Blank && completeButton.Text == "&Complete" && MessageBox.Show("One or more of the answers is empty. Are you sure you want to continue?",
					"Complete Problems", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
				return;
			// If the user has selected yes, or all answers have been completed, we'll now continue.

			timer2.Stop();
			completeButton.Hide();
			if (completeButton.Text == "&Complete")
			{
				CheckAnswers();
				timer1.Enabled = true;
			}
			else
				ShowCongratulationsScreen();
		}

		
	}
}
