using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace EnglishMate
{
	public static class MainStaticClass
	{
		public static string ApplicationDataLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
			+ "\\Autosoft\\Auto-Helper\\2016\\English Mate\\";
		public static string CurrentUserName;
		public static string MainStudentGrammarLocation = ApplicationDataLocation + "Names\\Grammar\\";
		public static string MainStudentCompositionLocation = ApplicationDataLocation + "Names\\Composition\\";
		public static string GrammarGradeLocation = ApplicationDataLocation + "Grades\\Grammar\\";
		public static string CompositionGradeLocation = ApplicationDataLocation + "Grades\\Composition\\";
		public static string GrammarDirectory = ApplicationDataLocation + "Grammar\\";
		public static string CompositionDirectory = ApplicationDataLocation + "Composition\\";
		public static string AssignmentLocation = ApplicationDataLocation + "Assignments\\";
		public static string TempDirectory = ApplicationDataLocation + "Temp";
		public static string TempGrammarDirectory = ApplicationDataLocation + "Temp\\Grammar";
		public static string TempCompositionDirectory = ApplicationDataLocation + "Temp\\Composition";
		public static string GrammarLogFile = ApplicationDataLocation + "Grammar Log.txt";
		public static string CompositionLogFile = ApplicationDataLocation + "Composition Log.txt";
		public static string WelcomeMessageLocation = ApplicationDataLocation + "Welcome Message.txt";
		public static string currentGradeFile;
		public static string currentStudentFile;
		public static string CompositionAssignmentLocation;
		public const string newLine = "\r\n";
		public static decimal Grade;
		public static string WelcomeMessage;
		public static string defaultWelcomeMessage =
@"Welcome to English Mate™, the fifth Auto-Helper™ program.
English Mate™ offers great flexibility and an abundance of features.
	
Some top features are highlighted here:

1.	All exercises are customizable. In fact, individual exercise types can be added and removed.
To change exercises and exercise types, login, click settings, and navigate to the grammar or composition area.
Next, click the exercise, and edit the text after the sentences label.
When you're done, just click OK.

2.	The students individual grades and average grades will automatically be reported.
To clear the grade or log, login, and then click the appropriate clear button.

3.	When the student has completed the assignment, the student will have the opportunity to see which answers were correct, and can then view his/her grade in the Congratulations window.

4.	Help is also customizable.
To customize help, follow the same procedure as with exercises, except change the text under the help label instead of the sentences label.

5.	To setup a creative writing assignment, follow the same steps as changing the assignment (composition), but type ""Assignment"" into the answer section.
The assignment will then be recorded in a log file, where the teacher can grade the work.

6.	If the student's grade is below 70, the student will be prompted to redo the assignment.

7.	In addition to custom categories, Autosoft offers definition file downloads that will soon be available.";

		
	}
}
