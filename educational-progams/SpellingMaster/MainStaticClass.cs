using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace SpellingMaster
{
	public static class MainStaticClass
	{
	public static string CurrentUserName;
	public static string SpellingWordFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\SpellingWordList.txt";
	public static string LogFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\Log.txt";
	public static string MainFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\SpellingNames";
	public static string MainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016";
	public static string MainFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\SpellingNames\\" + CurrentUserName + ".txt";
	public static string GradeFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\Grades\\" + CurrentUserName + ".txt";
	public static string GradeLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\Grades";
	public static string WordListLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\WordLists";
	public static string BasicWordLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\WordLists\\Basic.txt";
	public static string IntermediateWordLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\WordLists\\Intermediate.txt";
	public static string AdvancedWordLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\WordLists\\Advanced.txt";
	public static string WordListVersionLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\WordLists\\WordListVersion.txt";
	public static string HelpLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\WordLists\\Help.txt";
	public static string RulesLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Autosoft\\Auto-Helper\\2016\\WordLists\\Rules.txt";
	public static string WordListVersion = "1.0.1.3";
	public static decimal Grade;
 
	}
}
