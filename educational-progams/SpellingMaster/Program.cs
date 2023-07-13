using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace SpellingMaster
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				if (!System.IO.Directory.Exists(MainStaticClass.MainDirectory)) System.IO.Directory.CreateDirectory(MainStaticClass.MainDirectory);
				if (!System.IO.Directory.Exists(MainStaticClass.GradeLocation)) System.IO.Directory.CreateDirectory(MainStaticClass.GradeLocation);
				if (!System.IO.Directory.Exists(MainStaticClass.MainFileLocation)) System.IO.Directory.CreateDirectory(MainStaticClass.MainFileLocation);
				if (!System.IO.Directory.Exists(MainStaticClass.WordListLocation)) System.IO.Directory.CreateDirectory(MainStaticClass.WordListLocation);
				if (!System.IO.File.Exists(MainStaticClass.SpellingWordFileLocation)) System.IO.File.WriteAllText(MainStaticClass.SpellingWordFileLocation, SpellingDefinitions.IntermediateWords);
				if (!System.IO.File.Exists(MainStaticClass.LogFileLocation)) System.IO.File.Create(MainStaticClass.LogFileLocation);
				if (!System.IO.File.Exists(MainStaticClass.WordListVersionLocation)) File.WriteAllText(MainStaticClass.WordListVersionLocation, "1.0.1.3");
				if (!System.IO.File.Exists(MainStaticClass.BasicWordLocation)) File.WriteAllText(MainStaticClass.BasicWordLocation, SpellingDefinitions.BeginningWords);
				if (!System.IO.File.Exists(MainStaticClass.IntermediateWordLocation)) File.WriteAllText(MainStaticClass.IntermediateWordLocation, SpellingDefinitions.IntermediateWords);
				if (!System.IO.File.Exists(MainStaticClass.AdvancedWordLocation)) File.WriteAllText(MainStaticClass.AdvancedWordLocation, SpellingDefinitions.AdvancedWords);
				if (!System.IO.File.Exists(MainStaticClass.HelpLocation)) File.WriteAllText(MainStaticClass.HelpLocation, SpellingDefinitions.Help);
				if (!System.IO.File.Exists(MainStaticClass.RulesLocation)) File.WriteAllText(MainStaticClass.RulesLocation, SpellingDefinitions.Rules);
				MainStaticClass.WordListVersion = File.ReadAllText(MainStaticClass.WordListVersionLocation);
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
				MessageBox.Show("An error has occurred. Please report this problem to Autosoft. We are very sorry for the inconvenience. \n\nError Code: PSLD03", "Error (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Start());

		}

	
	}
}
