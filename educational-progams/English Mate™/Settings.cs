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
				// Updates the user's form settings.
				grammarOptionsPanel2.SplitterDistance = Properties.Settings.Default.grammarOptionsPanel2Splitter;
				mainGrammarSplitContainer.SplitterDistance = Properties.Settings.Default.mainGrammarSplitContainerSplitter;
				compositionOptionsPanel2.SplitterDistance = Properties.Settings.Default.compositionOptionsPanel2Splitter;
				mainCompositionSplitContainer.SplitterDistance = Properties.Settings.Default.mainCompositionSplitContainerSplitter;
				// Create temporary files and folders.
				Directory.CreateDirectory(MainStaticClass.TempGrammarDirectory);
				Directory.CreateDirectory(MainStaticClass.TempCompositionDirectory);
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.GrammarDirectory))
				{
					var TempFile = MainStaticClass.TempGrammarDirectory + "\\" + System.IO.Path.GetFileName(file);
					File.Copy(file, TempFile);
				}
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.CompositionDirectory))
				{
					var TempFile = MainStaticClass.TempCompositionDirectory + "\\" + System.IO.Path.GetFileName(file);
					File.Copy(file, TempFile);
				}
				// Loads all grammar categories.
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.GrammarDirectory))
					grammarCategories.Items.Add(Path.GetFileNameWithoutExtension(file));
				// Loads all composition categories.
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.CompositionDirectory))
					compositionCategories.Items.Add(Path.GetFileNameWithoutExtension(file));
				// Loads all settings.
				ComboBox1.SelectedItem = EnglishMate.Properties.Settings.Default.Theme;
				if (EnglishMate.Properties.Settings.Default.GrammarHelpEnabled) grammarHelpCheckBox.Checked = true;
				else grammarHelpCheckBox.Checked = false;
				if (EnglishMate.Properties.Settings.Default.CompositionHelpEnabled) compositionHelpCheckBox.Checked = true;
				else compositionHelpCheckBox.Checked = false;
				if (EnglishMate.Properties.Settings.Default.GrammarExercisesEnabled) grammarExercisesCheckBox.Checked = true;
				else grammarExercisesCheckBox.Checked = false;
				if (EnglishMate.Properties.Settings.Default.CompositionExercisesEnabled) compositionExercisesCheckBox.Checked = true;
				else compositionExercisesCheckBox.Checked = false;
				if (EnglishMate.Properties.Settings.Default.AutoCompleteEnabled) autoCompleteCheckBox.Checked = true;
				else autoCompleteCheckBox.Checked = false;
				if (EnglishMate.Properties.Settings.Default.AllowBlankAnswers) allowBlankAnswers.Checked = true;
				else allowBlankAnswers.Checked = false;
				UserName.Text = EnglishMate.Properties.Settings.Default.UserName;
				Password.Text = EnglishMate.Properties.Settings.Default.Password;
				ConfirmPassword.Text = EnglishMate.Properties.Settings.Default.Password;
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGLD03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			if (Password.Text != ConfirmPassword.Text)
			{
				DialogResult Result = MessageBox.Show("The passwords do not match. Please enter valid passwords."
					, "Invalid Passwords", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
				if (Result == DialogResult.Cancel) this.Close();
				return;
			}
			try
			{
				// Ensure that all files contain at least one entry.
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.TempGrammarDirectory))
				{
					var Data = File.ReadAllText(file);
					var dataIndex = Data.IndexOf("<data>");
					var dataEndIndex = Data.IndexOf("</data>");
					var instructionsIndex = Data.IndexOf("<instructions>");
					var instructionsEndIndex = Data.IndexOf("</instructions>");
					var helpIndex = Data.IndexOf("<help>");
					var helpEndIndex = Data.IndexOf("</help>");
					var caseSensitiveIndex = Data.IndexOf("<caseSensitive>");
					var caseSensitiveEndIndex = Data.IndexOf("</caseSensitive>");
					var commaRequiredIndex = Data.IndexOf("<commaRequired");
					var commaRequiredEndIndex = Data.IndexOf("</commaRequired");
					var filterIndex = Data.IndexOf("<filter>");
					var filterEndIndex = Data.IndexOf("</filter>");
					// If the document is not properly set up, get out.
					if (dataIndex == -1 | dataEndIndex == -1 | instructionsIndex == -1
						| instructionsEndIndex == -1 | helpIndex == -1 | helpEndIndex == -1
						| caseSensitiveIndex == -1 | caseSensitiveEndIndex == -1
						| commaRequiredIndex == -1 | commaRequiredEndIndex == -1
						| filterIndex == -1 | filterEndIndex == -1)
					{
						MessageBox.Show("One or more of the categories is not properly setup.",
							"Category Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					string FileData = Data.Substring(dataIndex + 6, dataEndIndex - dataIndex - 6);
					if (string.IsNullOrWhiteSpace(FileData.Replace(":","")))
					{
						MessageBox.Show("One or more of the categories contains no sentences. Each category must contain at least one sentence.",
													"Category Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.TempCompositionDirectory))
				{
					var Data = File.ReadAllText(file);
					var dataIndex = Data.IndexOf("<data>");
					var dataEndIndex = Data.IndexOf("</data>");
					var instructionsIndex = Data.IndexOf("<instructions>");
					var instructionsEndIndex = Data.IndexOf("</instructions>");
					var helpIndex = Data.IndexOf("<help>");
					var helpEndIndex = Data.IndexOf("</help>");
					var caseSensitiveIndex = Data.IndexOf("<caseSensitive>");
					var caseSensitiveEndIndex = Data.IndexOf("</caseSensitive>");
					var commaRequiredIndex = Data.IndexOf("<commaRequired");
					var commaRequiredEndIndex = Data.IndexOf("</commaRequired");
					var filterIndex = Data.IndexOf("<filter>");
					var filterEndIndex = Data.IndexOf("</filter>");
					// If the document is not properly set up, get out.
					if (dataIndex == -1 | dataEndIndex == -1 | instructionsIndex == -1
						| instructionsEndIndex == -1 | helpIndex == -1 | helpEndIndex == -1
						| caseSensitiveIndex == -1 | caseSensitiveEndIndex == -1
						| commaRequiredIndex == -1 | commaRequiredEndIndex == -1
						| filterIndex == -1 | filterEndIndex == -1)
					{
						MessageBox.Show("One or more of the categories is not properly setup.",
							"Category Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					string FileData = Data.Substring(dataIndex + 6, dataEndIndex - dataIndex - 6);
					if (string.IsNullOrWhiteSpace(FileData.Replace(":","").Replace(";","")))
					{
						MessageBox.Show("One or more of the categories contains no exercises. Each category must contain at least one exercise.",
													"Category Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				// Updates all settings.
				if (grammarHelpCheckBox.Checked) EnglishMate.Properties.Settings.Default.GrammarHelpEnabled = true;
				else EnglishMate.Properties.Settings.Default.GrammarHelpEnabled = false;
				if (compositionHelpCheckBox.Checked) EnglishMate.Properties.Settings.Default.CompositionHelpEnabled = true;
				else EnglishMate.Properties.Settings.Default.CompositionHelpEnabled = false;
				if (grammarExercisesCheckBox.Checked) EnglishMate.Properties.Settings.Default.GrammarExercisesEnabled = true;
				else EnglishMate.Properties.Settings.Default.GrammarExercisesEnabled = false;
				if (compositionExercisesCheckBox.Checked) EnglishMate.Properties.Settings.Default.CompositionExercisesEnabled = true;
				else EnglishMate.Properties.Settings.Default.CompositionExercisesEnabled = false;
				if (autoCompleteCheckBox.Checked) EnglishMate.Properties.Settings.Default.AutoCompleteEnabled = true;
				else EnglishMate.Properties.Settings.Default.AutoCompleteEnabled = false;
				if (allowBlankAnswers.Checked) EnglishMate.Properties.Settings.Default.AllowBlankAnswers = true;
				else EnglishMate.Properties.Settings.Default.AllowBlankAnswers = false;
				EnglishMate.Properties.Settings.Default.Theme = (string)ComboBox1.SelectedItem;
				EnglishMate.Properties.Settings.Default.UserName = UserName.Text;
				EnglishMate.Properties.Settings.Default.Password = Password.Text;
				// Updates grammar category files.
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.GrammarDirectory))
				{
					var TempGrammarFileName = MainStaticClass.TempGrammarDirectory + "\\" + Path.GetFileName(file);
					if (!File.Exists(TempGrammarFileName)) File.Delete(file);
				}
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.TempGrammarDirectory))
				{
					var GrammarFileName = MainStaticClass.GrammarDirectory + "\\" + Path.GetFileName(file);
					if (!File.Exists(GrammarFileName) || File.ReadAllText(file) != File.ReadAllText(GrammarFileName))
						File.Copy(file, GrammarFileName, true); // If a file needs to be created or updated, we'll do it now.
				}
				// Updates composition category files.
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.CompositionDirectory))
				{
					var TempCompositionFileName = MainStaticClass.TempCompositionDirectory + "\\" + Path.GetFileName(file);
					if (!File.Exists(TempCompositionFileName)) File.Delete(file);
				}
				foreach (var file in Directory.EnumerateFiles(MainStaticClass.TempCompositionDirectory))
				{
					var CompositionFileName = MainStaticClass.CompositionDirectory + "\\" + Path.GetFileName(file);
					if (!File.Exists(CompositionFileName) || File.ReadAllText(file) != File.ReadAllText(CompositionFileName))
						File.Copy(file, CompositionFileName, true); // If a file needs to be created or updated, we'll do it now.
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGOK03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			EnglishMate.Properties.Settings.Default.Save();
			this.Close();

		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void grammarCategories_SelectedValueChanged(object sender, EventArgs e)
		{
			// If no item is selected, get out.
			if (grammarCategories.SelectedItem == null)
			{
				grammarRemoveButton.Hide();
				grammarOptionsPanel.Hide();
				grammarOptionsPanel2.Hide();
				grammarHelp.Clear();
				grammarSentences.Clear();
				grammarInstructions.Clear();
				grammarCategoryName.Clear();
				return;
			}
			//  We're updating the control panel to enable the user to change category settings.
			try
			{
				var FileLocation = MainStaticClass.TempGrammarDirectory + "\\" + grammarCategories.SelectedItem + ".xml";
				grammarCategoryName.Text = (string)grammarCategories.SelectedItem;
				if (!File.Exists(FileLocation))
				{
					MessageBox.Show("The definitions file does not exist. The entry will now be deleted.",
						"File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				var Data = File.ReadAllText(FileLocation);
				var dataIndex = Data.IndexOf("<data>") + 6;
				var dataEndIndex = Data.IndexOf("</data>");
				var instructionsIndex = Data.IndexOf("<instructions>") + 14;
				var instructionsEndIndex = Data.IndexOf("</instructions>");
				var helpIndex = Data.IndexOf("<help>") + 6;
				var helpEndIndex = Data.IndexOf("</help>");
				var caseSensitiveIndex = Data.IndexOf("<caseSensitive>") + 15;
				var caseSensitiveEndIndex = Data.IndexOf("</caseSensitive>");
				var commaRequiredIndex = Data.IndexOf("<commaRequired>") + 15;
				var commaRequiredEndIndex = Data.IndexOf("</commaRequired>");
				var filterIndex = Data.IndexOf("<filter>") + 8;
				var filterEndIndex = Data.IndexOf("</filter>");
				// If the document is not properly set up, get out.
				if (dataIndex == 5 | dataEndIndex == -1 | instructionsIndex == 13
						| instructionsEndIndex == -1 | helpIndex == 3 | helpEndIndex == -1
						| caseSensitiveIndex == 14 | caseSensitiveEndIndex == -1
						| commaRequiredIndex == 14 | commaRequiredEndIndex == -1
						| filterIndex == 7 | filterEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				string caseSensitive = Data.Substring(caseSensitiveIndex, caseSensitiveEndIndex - caseSensitiveIndex);
				string commaRequired = Data.Substring(commaRequiredIndex, commaRequiredEndIndex - commaRequiredIndex);
				if (caseSensitive == "true") grammarCaseSensitive.Checked = true;
				else grammarCaseSensitive.Checked = false;
				if (commaRequired == "true") grammarRequireCommas.Checked = true;
				else grammarRequireCommas.Checked = false;
				grammarSentences.Text = Data.Substring(dataIndex, dataEndIndex - dataIndex);
				grammarInstructions.Text = Data.Substring(instructionsIndex, instructionsEndIndex - instructionsIndex);
				grammarHelp.Text = Data.Substring(helpIndex, helpEndIndex - helpIndex);
				grammarFilter.Text = Data.Substring(filterIndex, filterEndIndex - filterIndex);
				grammarRemoveButton.Show();
				grammarOptionsPanel.Show();
				grammarOptionsPanel2.Show();
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGGSVC03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void compositionCategories_SelectedValueChanged(object sender, EventArgs e)
		{
			// If no item is selected, get out.
			if (compositionCategories.SelectedItem == null)
			{
				compositionRemoveButton.Hide();
				compositionOptionsPanel.Hide();
				compositionOptionsPanel2.Hide();
				compositionHelp.Clear();
				compositionSentences.Clear();
				compositionInstructions.Clear();
				compositionCategoryName.Clear();
				return;
			}
			//  We're updating the control panel to enable the user to change category settings.
			try
			{
				var FileLocation = MainStaticClass.TempCompositionDirectory + "\\" + compositionCategories.SelectedItem + ".xml";
				compositionCategoryName.Text = (string)compositionCategories.SelectedItem;
				if (!File.Exists(FileLocation))
				{
					MessageBox.Show("The definitions file does not exist. The entry will now be deleted.",
						"File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteCompositionItem();
					return;
				}
				var Data = File.ReadAllText(FileLocation);
				var dataIndex = Data.IndexOf("<data>") + 6;
				var dataEndIndex = Data.IndexOf("</data>");
				var instructionsIndex = Data.IndexOf("<instructions>") + 14;
				var instructionsEndIndex = Data.IndexOf("</instructions>");
				var helpIndex = Data.IndexOf("<help>") + 6;
				var helpEndIndex = Data.IndexOf("</help>");
				var caseSensitiveIndex = Data.IndexOf("<caseSensitive>") + 15;
				var caseSensitiveEndIndex = Data.IndexOf("</caseSensitive>");
				var commaRequiredIndex = Data.IndexOf("<commaRequired>") + 15;
				var commaRequiredEndIndex = Data.IndexOf("</commaRequired>");
				var filterIndex = Data.IndexOf("<filter>") + 8;
				var filterEndIndex = Data.IndexOf("</filter>");
				// If the document is not properly set up, get out.
				if (dataIndex == 5 | dataEndIndex == -1 | instructionsIndex == 13
						| instructionsEndIndex == -1 | helpIndex == 3 | helpEndIndex == -1
						| caseSensitiveIndex == 14 | caseSensitiveEndIndex == -1
						| commaRequiredIndex == 14 | commaRequiredEndIndex == -1
						| filterIndex == 7 | filterEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteCompositionItem();
					return;
				}
				string caseSensitive = Data.Substring(caseSensitiveIndex, caseSensitiveEndIndex - caseSensitiveIndex);
				string commaRequired = Data.Substring(commaRequiredIndex, commaRequiredEndIndex - commaRequiredIndex);
				if (caseSensitive == "true") compositionCaseSensitive.Checked = true;
				else compositionCaseSensitive.Checked = false;
				if (commaRequired == "true") compositionRequireCommas.Checked = true;
				else compositionRequireCommas.Checked = false;
				compositionSentences.Text = Data.Substring(dataIndex, dataEndIndex - dataIndex);
				compositionInstructions.Text = Data.Substring(instructionsIndex, instructionsEndIndex - instructionsIndex);
				compositionHelp.Text = Data.Substring(helpIndex, helpEndIndex - helpIndex);
				compositionFilter.Text = Data.Substring(filterIndex, filterEndIndex - filterIndex);
				compositionRemoveButton.Show();
				compositionOptionsPanel.Show();
				compositionOptionsPanel2.Show();
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGCSVC03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void addGrammarCategory()
		{
			// We're ask the user to insert the new name.
			var input = Microsoft.VisualBasic.Interaction.InputBox("Please insert the category name.", "Insert Category Name", "Default");
			// We're ensuring that the user hasn't entered an invalid name.			
			Array CheckArray = System.IO.Path.GetInvalidFileNameChars();
			foreach (char item in CheckArray)
			{
				if (grammarCategoryName.Text.Contains(item))
				{
					var result = MessageBox.Show("An invalid category name has been entered. "
						+ "Please enter a valid category name.", "Invalid Category Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
					if (result == DialogResult.OK) addGrammarCategory(); // If the user clicks OK, they'll be prompted again.
					return;
				}
			}
			// If no name has been entered, or the cancel button has been pressed, get out.
			if (string.IsNullOrWhiteSpace(input)) return;
			// If no issues have been found, we're now attempt to create the new category.
			try
			{
				string NewFileLocation = MainStaticClass.TempGrammarDirectory + "\\" + input + ".xml";
				if (File.Exists(NewFileLocation) && MessageBox.Show("The file already exists. Do you want to overwrite it?",
						"Overwrite File?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
				File.WriteAllText(NewFileLocation, @"<data></data><instructions></instructions><help></help>
			<caseSensitive>false</caseSensitive><commaRequired>true</commaRequired><filter></filter>");
				if (!grammarCategories.Items.Contains(input)) grammarCategories.Items.Add(input);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGAGC03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void addCompositionCategory()
		{
			// We're ask the user to insert the new name.
			var input = Microsoft.VisualBasic.Interaction.InputBox("Please insert the category name.", "Insert Category Name", "Default");
			// We're ensuring that the user hasn't entered an invalid name.			
			Array CheckArray = System.IO.Path.GetInvalidFileNameChars();
			foreach (char item in CheckArray)
			{
				if (compositionCategoryName.Text.Contains(item))
				{
					var result = MessageBox.Show("An invalid category name has been entered. "
						+ "Please enter a valid category name.", "Invalid Category Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
					if (result == DialogResult.OK) addCompositionCategory(); // If the user clicks OK, they'll be prompted again.
					return;
				}
			}
			// If no name has been entered, or the cancel button has been pressed, get out.
			if (string.IsNullOrWhiteSpace(input)) return;
			// If no issues have been found, we're now attempt to create the new category.
			try
			{
				string NewFileLocation = MainStaticClass.TempCompositionDirectory + "\\" + input + ".xml";
				if (File.Exists(NewFileLocation) && MessageBox.Show("The file already exists. Do you want to overwrite it?",
						"Overwrite File?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
				File.WriteAllText(NewFileLocation, @"<data></data><instructions></instructions><help></help>
			<caseSensitive>false</caseSensitive><commaRequired>true</commaRequired><filter></filter>");
				if (!compositionCategories.Items.Contains(input)) compositionCategories.Items.Add(input);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGACC03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void grammarRemoveButton_Click(object sender, EventArgs e)
		{
			deleteGrammarItem();
		}
		private void compositionRemoveButton_Click(object sender, EventArgs e)
		{
			deleteCompositionItem();
		}
		private void grammarCategoryName_Leave(object sender, EventArgs e)
		{
			grammarCategoryRename();
		}

		private void grammarCategoryName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) grammarCategoryRename();
		}

		private void compositionCategoryName_Leave(object sender, EventArgs e)
		{
			compositionCategoryRename();
		}
		private void compositionCategoryName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) compositionCategoryRename();
		}
		private void grammarCategoryRename()
		{
			var newName = grammarCategoryName.Text;
			if (newName == (string)grammarCategories.SelectedItem) return; // If the name has not changed, get out.
			/*We're ensuring the file-name contains only characters. 
			 * If the file-name is empty, consists of only spaces, or contains invalid characters, we're notify the user.*/
			Array CheckArray = System.IO.Path.GetInvalidFileNameChars();
			foreach (char item in CheckArray)
			{
				if (newName.Contains(item))
				{
					var result = MessageBox.Show("An invalid category name has been entered. "
						+ "Please enter a valid category name.", "Invalid Category Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
					if (result == DialogResult.OK) grammarCategoryRename(); // If the user clicks OK, they'll be prompted again.
					return;
				}
			}
			if (string.IsNullOrWhiteSpace(newName))
			{
				var result = MessageBox.Show("An invalid category name has been entered. "
					+ "Please enter a valid category name.", "Invalid Category Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
				if (result == DialogResult.OK) grammarCategoryRename(); // If the user clicks OK, they'll be prompted again.
				return;
			}
			// We're preparing to rename the file.
			var FileLocation = MainStaticClass.TempGrammarDirectory + "\\" + grammarCategories.SelectedItem + ".xml";
			var newFileLocation = MainStaticClass.TempGrammarDirectory + "\\" + newName + ".xml";
			try
			{
				// If the name has changed in casing only, create a temporary file.
				if (newName.ToUpper() == (string)grammarCategories.SelectedItem)
				{
					var TempLocation = MainStaticClass.TempDirectory + "\\TEMPFILE.xml";
					File.Move(FileLocation, TempLocation);
					FileLocation = TempLocation; // FileLocation now represents the temporary file.
				}
				// We're now attempting to rename the file.
				File.Move(FileLocation, newFileLocation);
				// We will now reflect the changes made to the list-box.
				grammarCategories.Items.Add(newName);
				grammarCategories.Items.Remove(grammarCategories.SelectedItem);
				grammarCategories.SelectedItem = newName;
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGRGI03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void compositionCategoryRename()
		{
			var newName = compositionCategoryName.Text;
			if (newName == (string)compositionCategories.SelectedItem) return; // If the name has not changed, get out.
			/*We're ensuring the file-name contains only characters. 
			 * If the file-name is empty, consists of only spaces, or contains invalid characters, we're notify the user.*/
			Array CheckArray = System.IO.Path.GetInvalidFileNameChars();
			foreach (char item in CheckArray)
			{
				if (newName.Contains(item))
				{
					var result = MessageBox.Show("An invalid category name has been entered. "
						+ "Please enter a valid category name.", "Invalid Category Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
					if (result == DialogResult.OK) compositionCategoryRename(); // If the user clicks OK, they'll be prompted again.
					return;
				}
			}
			if (string.IsNullOrWhiteSpace(newName))
			{
				var result = MessageBox.Show("An invalid category name has been entered. "
					+ "Please enter a valid category name.", "Invalid Category Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
				if (result == DialogResult.OK) compositionCategoryRename(); // If the user clicks OK, they'll be prompted again.
				return;
			}
			// We're preparing to rename the file.
			var FileLocation = MainStaticClass.TempCompositionDirectory + "\\" + compositionCategories.SelectedItem + ".xml";
			var newFileLocation = MainStaticClass.TempCompositionDirectory + "\\" + newName + ".xml";
			try
			{
				// If the name has changed in casing only, create a temporary file.
				if (newName.ToUpper() == (string)compositionCategories.SelectedItem)
				{
					var TempLocation = MainStaticClass.TempDirectory + "\\TEMPFILE.xml";
					File.Move(FileLocation, TempLocation);
					FileLocation = TempLocation; // FileLocation now represents the temporary file.
				}
				// We're now attempting to rename the file.
				File.Move(FileLocation, newFileLocation);
				// We will now reflect the changes made to the list-box.
				compositionCategories.Items.Add(newName);
				compositionCategories.Items.Remove(compositionCategories.SelectedItem);
				compositionCategories.SelectedItem = newName;
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGRIC03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void deleteGrammarItem()
		{
			var FileLocation = MainStaticClass.TempGrammarDirectory + "\\" + grammarCategories.SelectedItem + ".xml";
			try
			{
				if (File.Exists(FileLocation)) File.Delete(FileLocation);
				grammarCategories.Items.Remove(grammarCategories.SelectedItem);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGDGI03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void deleteCompositionItem()
		{
			var FileLocation = MainStaticClass.TempCompositionDirectory + "\\" + compositionCategories.SelectedItem + ".xml";
			try
			{
				if (File.Exists(FileLocation)) File.Delete(FileLocation);
				compositionCategories.Items.Remove(compositionCategories.SelectedItem);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGDCI03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void grammarDataLeave(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (grammarCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempGrammarDirectory + "\\" + grammarCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var dataIndex = Data.IndexOf("<data>") + 6;
				var dataEndIndex = Data.IndexOf("</data>") - dataIndex;
				if (dataIndex == 5 | dataEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				var newData = Data.Remove(dataIndex, dataEndIndex).Insert(dataIndex, grammarSentences.Text);
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGCHGD03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void compositionDataLeave(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (compositionCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempCompositionDirectory + "\\" + compositionCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var dataIndex = Data.IndexOf("<data>") + 6;
				var dataEndIndex = Data.IndexOf("</data>") - dataIndex;
				if (dataIndex == 5 | dataEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				var newData = Data.Remove(dataIndex, dataEndIndex).Insert(dataIndex, compositionSentences.Text);
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGCHCD03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void grammarInstructions_Leave(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (grammarCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempGrammarDirectory + "\\" + grammarCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var instructionsIndex = Data.IndexOf("<instructions>") + 14;
				var instructionsEndIndex = Data.IndexOf("</instructions>") - instructionsIndex;
				if (instructionsIndex == 13 | instructionsEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				var newData = Data.Remove(instructionsIndex, instructionsEndIndex).Insert(instructionsIndex, grammarInstructions.Text);
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGCHCI03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void compositionInstructions_Leave(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (compositionCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempCompositionDirectory + "\\" + compositionCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var instructionsIndex = Data.IndexOf("<instructions>") + 14;
				var instructionsEndIndex = Data.IndexOf("</instructions>") - instructionsIndex;
				if (instructionsIndex == 13 | instructionsEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				var newData = Data.Remove(instructionsIndex, instructionsEndIndex).Insert(instructionsIndex, compositionInstructions.Text);
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
								+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
								"\n\nError Code: SGCHCI03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void grammarHelp_Leave(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (grammarCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempGrammarDirectory + "\\" + grammarCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var startIndex = Data.IndexOf("<help>") + 6;
				var startEndIndex = Data.IndexOf("</help>") - startIndex;
				if (startIndex == 5 | startEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				var newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, grammarHelp.Text);
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
				+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
				"\n\nError Code: SGCHGH03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void compositionHelp_Leave(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (compositionCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempCompositionDirectory + "\\" + compositionCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var startIndex = Data.IndexOf("<help>") + 6;
				var startEndIndex = Data.IndexOf("</help>") - startIndex;
				if (startIndex == 5 | startEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				var newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, compositionHelp.Text);
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
				+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
				"\n\nError Code: SGCHCH03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void grammarAddButton_Click(object sender, EventArgs e)
		{
			addGrammarCategory();
		}

		private void compositionAddButton_Click(object sender, EventArgs e)
		{
			addCompositionCategory();
		}

		// We're updating the grammar options splitter.
		private void grammarOptionsPanel2_SplitterMoved(object sender, SplitterEventArgs e)
		{
			Properties.Settings.Default.grammarOptionsPanel2Splitter = grammarOptionsPanel2.SplitterDistance;
			Properties.Settings.Default.Save();
		}
		// We're updating the main grammar splitter.
		private void mainGrammarSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			Properties.Settings.Default.mainGrammarSplitContainerSplitter = mainGrammarSplitContainer.SplitterDistance;
			Properties.Settings.Default.Save();
		}
		// We're updating the composition options splitter.
		private void compositionOptionsPanel2_SplitterMoved(object sender, SplitterEventArgs e)
		{
			Properties.Settings.Default.compositionOptionsPanel2Splitter = compositionOptionsPanel2.SplitterDistance;
			Properties.Settings.Default.Save();
		}
		// We're updating the main composition splitter.
		private void mainCompositionSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			Properties.Settings.Default.mainCompositionSplitContainerSplitter = mainCompositionSplitContainer.SplitterDistance;
			Properties.Settings.Default.Save();
		}
		private void Settings_FormClosing(object sender, FormClosingEventArgs e)
		{
			// We're going to delete all temporary directories.
			try
			{
				if (Directory.Exists(MainStaticClass.TempDirectory)) Directory.Delete(MainStaticClass.TempDirectory, true);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
				+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
				"\n\nError Code: SGCL03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void grammarCaseSensitive_CheckedChanged(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (grammarCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempGrammarDirectory + "\\" + grammarCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var startIndex = Data.IndexOf("<caseSensitive>") + 15;
				var startEndIndex = Data.IndexOf("</caseSensitive") - startIndex;
				if (startIndex == 14 | startEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				string newData;
				if (grammarCaseSensitive.Checked) newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, "true");
				else newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, "false");
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
				+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
				"\n\nError Code: SGCHGCS03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void compositionCaseSensitive_CheckedChanged(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (compositionCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempCompositionDirectory + "\\" + compositionCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var startIndex = Data.IndexOf("<caseSensitive>") + 15;
				var startEndIndex = Data.IndexOf("</caseSensitive") - startIndex;
				if (startIndex == 14 | startEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteCompositionItem();
					return;
				}
				string newData;
				if (compositionCaseSensitive.Checked) newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, "true");
				else newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, "false");
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
				+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
				"\n\nError Code: SGCHCCS03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void grammarCommaRequired_CheckedChanged(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (grammarCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempGrammarDirectory + "\\" + grammarCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var startIndex = Data.IndexOf("<commaRequired>") + 15;
				var startEndIndex = Data.IndexOf("</commaRequired") - startIndex;
				if (startIndex == 14 | startEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				string newData;
				if (grammarRequireCommas.Checked) newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, "true");
				else newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, "false");
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
				+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
				"\n\nError Code: SGCHGCR03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void compositionCommaRequired_CheckedChanged(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (compositionCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempCompositionDirectory + "\\" + compositionCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var startIndex = Data.IndexOf("<commaRequired>") + 15;
				var startEndIndex = Data.IndexOf("</commaRequired") - startIndex;
				if (startIndex == 14 | startEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteCompositionItem();
					return;
				}
				string newData;
				if (compositionRequireCommas.Checked) newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, "true");
				else newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, "false");
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
				+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
				"\n\nError Code: SGCHCCR03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void grammarFilter_Leave(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (grammarCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempGrammarDirectory + "\\" + grammarCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var startIndex = Data.IndexOf("<filter>") + 8;
				var startEndIndex = Data.IndexOf("</filter") - startIndex;
				if (startIndex == 7 | startEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteGrammarItem();
					return;
				}
				string newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, grammarFilter.Text);
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
				+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
				"\n\nError Code: SGCHGF03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void compositionFilter_Leave(object sender, EventArgs e)
		{
			//  We're updating the temp file to allow the user to switch between different categories.
			if (compositionCategories.SelectedItem == null) return;
			// If the selected item is not null, we're now proceed.
			var FileLocation = MainStaticClass.TempCompositionDirectory + "\\" + compositionCategories.SelectedItem + ".xml";
			try
			{
				var Data = File.ReadAllText(FileLocation);
				var startIndex = Data.IndexOf("<filter>") + 8;
				var startEndIndex = Data.IndexOf("</filter") - startIndex;
				if (startIndex == 7 | startEndIndex == -1)
				{
					MessageBox.Show("The selected category is not properly formatted and will be deleted.",
						"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					deleteCompositionItem();
					return;
				}
				string newData = Data.Remove(startIndex, startEndIndex).Insert(startIndex, compositionFilter.Text);
				File.WriteAllText(FileLocation, newData);
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
				MessageBox.Show("An error has occurred. Make sure the document has properly been formatted. "
				+ "If the problem continues, please report this problem to Autosoft.\n\n" + ex.Message +
				"\n\nError Code: SGCHCF03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void grammarHelp_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.Enter)
				grammarHelp.SelectedText = "<br/>";
		}

		private void compositionHelp_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.Enter)
				compositionHelp.SelectedText = "<br/>";

		}
	}
}
