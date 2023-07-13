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
	public partial class Games : Form
	{
		Random NewRandom = new Random(); // This is used for initializing values.
		List<string> NewList = new List<string>(); // This is used to store all possible words.
		// This indicates whether or not duplicate words can be used.
		bool AllowRepetition = SpellingMaster.Properties.Settings.Default.AllowRepetition;
		List<string> WordList = new List<string>(); // This is used to store the current words.
		public bool REDO = false;
		bool Completed = false;
		int IncorrectAnswers = 0;
		Color NewForeColor;
		Color NewBackColor;
		Color MainBackColor;
		bool AutomaticComplete = SpellingMaster.Properties.Settings.Default.AutoCompleteEnabled;
		public Games()
		{
			InitializeComponent();
		}

		protected override bool IsInputKey(Keys KeyData)
		{
			if (KeyData == Keys.Escape)
				return true;
			else
				return false;
		}
		private void InitializeForm()
		{
			switch (SpellingMaster.Properties.Settings.Default.Theme)
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
					this.BackColor = MainBackColor;
					foreach (Control CurrentControl in this.Controls)
					{
						if (CurrentControl.GetType() == typeof(Panel))
						{
							CurrentControl.ForeColor = NewForeColor;
							CurrentControl.BackColor = NewBackColor;
						}
						else if (CurrentControl.GetType() == typeof(Label)) CurrentControl.ForeColor = NewForeColor;

					}
			

		}
		/// <summary>
		/// This occurs when the form is loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Games_Load(object sender, EventArgs e)
		{
			try
			{
			InitializeForm();
			// This adds each entry to the entry list, and each phrase to the phrase list.
			StreamReader CurrentFile = new StreamReader(MainStaticClass.SpellingWordFileLocation);
			while (CurrentFile.Peek() > -1)
			{
				NewList.Add(CurrentFile.ReadLine());
			}
			CurrentFile.Close();
			InitializeExercises(); // Randomly chooses new words.
			}
	catch (IOException)
			{
				MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
	catch (UnauthorizedAccessException)
			{
				MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
	catch (Exception)
			{
				MessageBox.Show("An error has occurred. Please report this problem to Autosoft. We are very sorry for the inconvenience. \n\nError Code: GSLD03", "Error (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void InitializeExercises()
		{
			// Initializes word and phrase elements.
			for (int i = 0; i < 30; i++)
			{
				Label[] WordLabelArray = new Label[] { Word1, Word2, Word3, Word4, Word5, Word6, Word7, Word8,Word9,Word10,Word11,Word12,
			Word13,Word14,Word15,Word16,Word17,Word18,Word19,Word20,Word21,Word22,Word23,Word24,Word25,Word26,Word27,Word28,Word29,Word30};
				List<Label> WordLabelList = new List<Label>(WordLabelArray);
				int IntegerValue = NewRandom.Next(0, NewList.Count);
				string TempWord = NewList.ElementAt(IntegerValue).Replace(" :", ":").Replace(": ", ":");
				if (!AllowRepetition) NewList.RemoveAt(IntegerValue);
				int TempInteger;
				if (TempWord.Contains(":")) TempInteger = TempWord.IndexOf(":");
				else TempInteger = TempWord.Length;
				string Word = TempWord.Substring(0, TempInteger);
				int blankchar = NewRandom.Next(0, Word.Length);
				Word = Word.Insert(blankchar,"_").Remove(blankchar + 1, 1);
				WordList.Add(TempWord.Substring(0, TempInteger));
				Label CurrentLabel = WordLabelList.ElementAt(i);
				CurrentLabel.Text = Word;
			}	
		}

		private void completeButton_Click(object sender, EventArgs e)
		{
			Complete();
		}

		private void Complete()
		{
			bool Blank = false;
			TextBox[] MyTextboxArray = new TextBox[]{ Answer1,Answer2,Answer3,Answer4,Answer5,Answer6,Answer7,Answer8,Answer9,Answer10,
				Answer11,Answer12,Answer13,Answer14,Answer15,Answer16,Answer17,Answer18,Answer19,Answer20,Answer21,Answer22,
				Answer23,Answer24,Answer25,Answer26,Answer27,Answer28,Answer29,Answer30};
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
			if (Blank) MessageBox.Show("One or more of the answers is empty. Please complete all problems.", "Complete Problems", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
			{
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

		private void CheckAnswers()
		{
			TextBox[] MyTextboxArray = new TextBox[]{ Answer1,Answer2,Answer3,Answer4,Answer5,Answer6,Answer7,Answer8,Answer9,Answer10,
				Answer11,Answer12,Answer13,Answer14,Answer15,Answer16,Answer17,Answer18,Answer19,Answer20,Answer21,Answer22,
				Answer23,Answer24,Answer25,Answer26,Answer27,Answer28,Answer29,Answer30};
			List<TextBox> MyTextboxList = new List<TextBox>(MyTextboxArray);
			Label[] MyCheckArray = new Label[]{ Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,
				Check11,Check12,Check13,Check14,Check15,Check16,Check17,Check18,Check19,Check20,Check21,Check22,
				Check23,Check24,Check25,Check26,Check27,Check28,Check29,Check30};
			List<Label> MyCheckList = new List<Label>(MyCheckArray);
			foreach (TextBox Answer in MyTextboxList)
			{
				Answer.Enabled = false;
			}
			SuspendLayout();
			foreach (TextBox Problem in MyTextboxList)
			{
				int ElementLocation = MyTextboxList.IndexOf(Problem);
				string Answer = WordList.ElementAt(ElementLocation);
				Label CurrentCheck = MyCheckList.ElementAt(ElementLocation);
				if (Problem.Text.ToUpper() == Answer.ToUpper())
				{
					CurrentCheck.ForeColor = Color.Green;
					CurrentCheck.Text = "";
				}
				else
				{
					CurrentCheck.ForeColor = Color.DarkRed;
					CurrentCheck.Text = "";
					IncorrectAnswers += 1;
				}
				CurrentCheck.Show();
			}
			ResumeLayout();
			int CorrectAnswers = 30 - IncorrectAnswers;
			decimal FullGrade = decimal.Divide(CorrectAnswers, 30) * 100;
			MainStaticClass.Grade = Math.Round(FullGrade, MidpointRounding.AwayFromZero);
			// Writes to the log file.
			try
			{
				string Input = "ERROR | Information not available";
				if (!REDO) Input = DateTime.Now + "  " + MainStaticClass.CurrentUserName + "  (Game)  " + "Grade: " + MainStaticClass.Grade.ToString();
				else Input = DateTime.Now + "  " + MainStaticClass.CurrentUserName + "  (Game)  " + "Grade: " + MainStaticClass.Grade.ToString() + " - Retaken";
				StreamWriter MainWriter = new StreamWriter(MainStaticClass.LogFileLocation, true);
				MainWriter.WriteLine(Input);
				MainWriter.Close();
			}
			catch (Exception ex)
			{
				StreamWriter MainWriter = new StreamWriter(MainStaticClass.LogFileLocation, true);
				MainWriter.WriteLine(DateTime.Now + "  " + MainStaticClass.CurrentUserName + "  "
					+ "ERROR: Graded Answers " + "Game" + " " + ex.Message);
				MainWriter.Close();

			}
		}

		private void ShowCongratulationsScreen()
		{
			timer1.Enabled = false;
			Completed = true;
			Congratulations NewCongratulations = new Congratulations();
			NewCongratulations.Games = true;
			NewCongratulations.Show();
			if (MainStaticClass.Grade >= 70) this.Close();
			else
			{
				REDO = true;
				this.Hide();
				SuspendLayout();
				//This initializes the values for all check marks.
				Label[] MyCheckArray = new Label[]{ Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,
				Check11,Check12,Check13,Check14,Check15,Check16,Check17,Check18,Check19,Check20,Check21,Check22,
				Check23,Check24,Check25,Check26,Check27,Check28,Check29,Check30};
				//This initializes the values for all answers.
				TextBox[] AnswerArray = new TextBox[]{ Answer1,Answer2,Answer3,Answer4,Answer5,Answer6,Answer7,Answer8,Answer9,Answer10,
				Answer11,Answer12,Answer13,Answer14,Answer15,Answer16,Answer17,Answer18,Answer19,Answer20,Answer21,Answer22,
				Answer23,Answer24,Answer25,Answer26,Answer27,Answer28,Answer29,Answer30};
				List<TextBox> AnswerList = new List<TextBox>(AnswerArray);
				List<Label> CheckList = new List<Label>(MyCheckArray);	//This creates the check mark list.
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
				//Initializes other features
				completeButton.Text = "&Complete";
				completeButton.Show();
				this.Opacity = 100;
				IncorrectAnswers = 0;
				redoIndicator.Show(); // Lets the user show that this is a previous assignment.
				ResumeLayout();
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

		private void Answers_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape & e.Shift && Microsoft.VisualBasic.Interaction.InputBox("Please enter the password.", "Enter Password (Auto-Helper)") == SpellingMaster.Properties.Settings.Default.Password)
			{
				REDO = false;
				Application.Exit();
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.Opacity > 0) this.Opacity -= 0.001;
			else ShowCongratulationsScreen();
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			//This initializes the values for all answers.
			TextBox[] AnswerArray = new TextBox[]{ Answer1,Answer2,Answer3,Answer4,Answer5,Answer6,Answer7,Answer8,Answer9,Answer10,
				Answer11,Answer12,Answer13,Answer14,Answer15,Answer16,Answer17,Answer18,Answer19,Answer20,Answer21,Answer22,
				Answer23,Answer24,Answer25,Answer26,Answer27,Answer28,Answer29,Answer30};
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

		private void Games_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (REDO)
			{
				e.Cancel = true;
				MessageBox.Show("Please complete all problems.", "Complete Problems", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (Completed == false)
			{
				DialogResult Result = MessageBox.Show("Are you sure you would like to exit?", "Warning (Auto-Helper)", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (Result == DialogResult.No) e.Cancel = true;
				else
				{
					Completed = true;
					Application.Exit();
				}
			}
		}
	}
}
