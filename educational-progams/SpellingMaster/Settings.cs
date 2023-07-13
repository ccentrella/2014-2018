using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Speech.Synthesis;
namespace SpellingMaster
{
	public partial class Settings : Form
	{
		public Settings()
		{
			InitializeComponent();
		}

		private void Settings_Load(object sender, EventArgs e)
		{
			try
			{
				// Loads all words.
				textBox1.Text = System.IO.File.ReadAllText(MainStaticClass.SpellingWordFileLocation);
				// Loads all settings.
				ComboBox1.SelectedItem = SpellingMaster.Properties.Settings.Default.Theme;
				if (SpellingMaster.Properties.Settings.Default.HelpEnabled) checkBox1.Checked = true;
				else checkBox1.Checked = false;
				if (SpellingMaster.Properties.Settings.Default.ExercisesEnabled) checkBox2.Checked = true;
				else checkBox2.Checked = false;
				if (SpellingMaster.Properties.Settings.Default.GamesEnabled) checkBox3.Checked = true;
				else checkBox3.Checked = false;
				if (SpellingMaster.Properties.Settings.Default.AutoCompleteEnabled) checkBox4.Checked = true;
				else checkBox4.Checked = false;
				if (SpellingMaster.Properties.Settings.Default.AllowRepetition) checkBox6.Checked = true;
				else checkBox6.Checked = false;
				if (SpellingMaster.Properties.Settings.Default.AutoPopulate) checkBox7.Checked = true;
				else checkBox7.Checked = false;
				trackBar1.Value = SpellingMaster.Properties.Settings.Default.CurrentRate;
				UserName.Text = SpellingMaster.Properties.Settings.Default.UserName;
				Password.Text = SpellingMaster.Properties.Settings.Default.Password;
				ConfirmPassword.Text = SpellingMaster.Properties.Settings.Default.Password;
			
			}
			catch (Exception)
			{
				MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);	
			}
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (Password.Text != ConfirmPassword.Text)
				{
					DialogResult Result = MessageBox.Show("The passwords do not match. Please enter valid passwords.", "Invalid Passwords", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
					if (Result == DialogResult.Cancel) this.Close();
				}
					// If repetition is not allowed and the number of elements is less than 30, the user will be notified.
				else if (textBox1.Lines.Count() == 0)
				{
					DialogResult Result = MessageBox.Show("There are no words in the word-list. Please add words.", "Incorrect Word Count", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
					if (Result == DialogResult.Cancel) this.Close();
				}
				else if (!checkBox6.Checked & textBox1.Lines.Count() < 30)
				{
					DialogResult Result = MessageBox.Show("There are less than thirty elements and repetition is disabled.\nPlease either increase the amount of elements, or enable repetition.", "Incorrect Word Count", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
					if (Result == DialogResult.Cancel) this.Close();
				}
				else
				{
					// Updates all words.
					System.IO.File.WriteAllText(MainStaticClass.SpellingWordFileLocation,textBox1.Text);
					// Updates all settings.
					SpellingMaster.Properties.Settings.Default.Theme = (string)ComboBox1.SelectedItem;
					if (checkBox1.Checked) SpellingMaster.Properties.Settings.Default.HelpEnabled = true;
					else SpellingMaster.Properties.Settings.Default.HelpEnabled = false;
					if (checkBox2.Checked) SpellingMaster.Properties.Settings.Default.ExercisesEnabled = true;
					else SpellingMaster.Properties.Settings.Default.ExercisesEnabled = false;
					if (checkBox3.Checked) SpellingMaster.Properties.Settings.Default.GamesEnabled = true;
					else SpellingMaster.Properties.Settings.Default.GamesEnabled = false;
					if (checkBox4.Checked) SpellingMaster.Properties.Settings.Default.AutoCompleteEnabled = true;
					else SpellingMaster.Properties.Settings.Default.AutoCompleteEnabled = false;
					if (checkBox6.Checked) SpellingMaster.Properties.Settings.Default.AllowRepetition = true;
					else SpellingMaster.Properties.Settings.Default.AllowRepetition = false;
					if (checkBox7.Checked) SpellingMaster.Properties.Settings.Default.AutoPopulate = true;
					else SpellingMaster.Properties.Settings.Default.AutoPopulate = false;
					SpellingMaster.Properties.Settings.Default.CurrentRate = trackBar1.Value;
					SpellingMaster.Properties.Settings.Default.UserName = UserName.Text;
					SpellingMaster.Properties.Settings.Default.Password = Password.Text;
					SpellingMaster.Properties.Settings.Default.Save();
					this.Close();
				}
			}
			catch (Exception)
			{
				MessageBox.Show("An important file could not be accessed.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);	
			}
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Basic_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("This will reset any custom words. Are you sure you want to continue?", "Warning (Auto-Helper)",
				MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes) 
			{
				if (!File.Exists(MainStaticClass.BasicWordLocation))
				{
					MessageBox.Show("The file does not exist. Try restarting the program.",
						"File Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return;	
				}
				try{
				string input = File.ReadAllText(MainStaticClass.BasicWordLocation);
				textBox1.Text = input;
					}
				catch (Exception)
					{
					MessageBox.Show("The file does not exist. Try restarting the program.",
						"File Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
			}
		}

		private void Intermediate_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("This will reset any custom words. Are you sure you want to continue?", "Warning (Auto-Helper)",
				MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				if (!File.Exists(MainStaticClass.IntermediateWordLocation))
				{
					MessageBox.Show("The file does not exist. Try restarting the program.",
						"File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				try
			{
				string input = File.ReadAllText(MainStaticClass.IntermediateWordLocation);
				textBox1.Text = input;
							}
			catch (Exception)
					{
					MessageBox.Show("The file does not exist. Try restarting the program.",
						"File Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
		}

		private void Advanced_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("This will reset any custom words. Are you sure you want to continue?", "Warning (Auto-Helper)",
				MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				if (!File.Exists(MainStaticClass.AdvancedWordLocation))
				{
					MessageBox.Show("The file does not exist. Try restarting the program.",
						"File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				try
			{
				string input = File.ReadAllText(MainStaticClass.AdvancedWordLocation);
				textBox1.Text = input;
			}
			catch (Exception)
					{
					MessageBox.Show("The file does not exist. Try restarting the program.",
						"File Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
		}

		private void button1_Click(object sender, EventArgs e)
		{


			string Input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the string to test", "Enter String", textBox1.SelectedText);
			if (Input != "")
			{
				var NewWord = new SpeechSynthesizer();
				// Initializes the Speech Synthesizer.
				NewWord.Rate = trackBar1.Value - 10;
				NewWord.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
				NewWord.Volume = 100;
				NewWord.SpeakAsync(Input);
			}
		}

		}
}
