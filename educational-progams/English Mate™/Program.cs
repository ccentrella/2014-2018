using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace EnglishMate
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

				// If the temp directory exists, we're delete it.
				if (Directory.Exists(MainStaticClass.TempDirectory))
					Directory.Delete(MainStaticClass.TempDirectory, true);
				// If the appropriate files and directories don't exist, we'll create them.
				if (!Directory.Exists(MainStaticClass.GrammarDirectory))
					Directory.CreateDirectory(MainStaticClass.GrammarDirectory);
				if (!Directory.Exists(MainStaticClass.CompositionDirectory))
					Directory.CreateDirectory(MainStaticClass.CompositionDirectory);
				if (!Directory.Exists(MainStaticClass.MainStudentGrammarLocation))
					Directory.CreateDirectory(MainStaticClass.MainStudentGrammarLocation);
				if (!Directory.Exists(MainStaticClass.MainStudentCompositionLocation))
					Directory.CreateDirectory(MainStaticClass.MainStudentCompositionLocation);
				if (!Directory.Exists(MainStaticClass.ApplicationDataLocation))
					Directory.CreateDirectory(MainStaticClass.ApplicationDataLocation);
				if (!Directory.Exists(MainStaticClass.GrammarGradeLocation))
					Directory.CreateDirectory(MainStaticClass.GrammarGradeLocation);
				if (!Directory.Exists(MainStaticClass.CompositionGradeLocation))
					Directory.CreateDirectory(MainStaticClass.CompositionGradeLocation);
				if (!Directory.Exists(MainStaticClass.AssignmentLocation))
					Directory.CreateDirectory(MainStaticClass.AssignmentLocation);
				if (!File.Exists(MainStaticClass.GrammarLogFile))
					File.Create(MainStaticClass.GrammarLogFile);
				if (!File.Exists(MainStaticClass.CompositionLogFile))
					File.Create(MainStaticClass.CompositionLogFile);
				if (!File.Exists(MainStaticClass.WelcomeMessageLocation))
					File.WriteAllText(MainStaticClass.WelcomeMessageLocation, MainStaticClass.defaultWelcomeMessage);
				MainStaticClass.WelcomeMessage = File.ReadAllText(MainStaticClass.WelcomeMessageLocation);
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
								"\n\nError Code: PRGST03", "Error - English Mate™", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			if (Properties.Settings.Default.FirstStartup)
			{
				MessageBox.Show(MainStaticClass.WelcomeMessage, "Welcome to English Mate™",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				Properties.Settings.Default.FirstStartup = false;
				Properties.Settings.Default.Save();
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Start());
		}
	}
}
