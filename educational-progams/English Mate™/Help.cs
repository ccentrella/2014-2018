using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
namespace EnglishMate
{
	public partial class Help : Form
	{
		public bool Composition = false;
		public Help()
		{
			InitializeComponent();
		}

		private void Help_Load(object sender, EventArgs e)
		{
			try
			{
				string WebbrowserHelp; // This is used for most help.
				string GrammarWordList = "";
				string CompositionWordList = "";
				string GrammarNavLinks = "<div style=\"background-color:darkgray\">\n"; // This is used to allow the user to navigate to different sections.
				string CompositionNavLinks = "<div style=\"background-color:darkgray\">\n"; // This is used to allow the user to navigate to different sections.
				// This is used to indicate the grammar help.
				if (!Composition)
				{
					foreach (var file in Directory.EnumerateFiles(MainStaticClass.GrammarDirectory))
					{
						var Data = File.ReadAllText(file);
						var Title = Path.GetFileNameWithoutExtension(file);
						var helpIndex = Data.IndexOf("<help>");
						var helpEndIndex = Data.IndexOf("</help>");
						// If the document is not properly set up, get out.
						if (helpIndex == -1 | helpEndIndex == -1)
						{
							continue;
						}
						var HelpData = Data.Substring(helpIndex + 6, helpEndIndex - helpIndex - 6);
						if (!HelpData.Contains(">") | !HelpData.Contains(">"))
						{
							HelpData = "<p>" + HelpData.Replace("\n", "<br/>") + "</p>";
						}
						if (string.IsNullOrWhiteSpace(HelpData)) continue;
						GrammarNavLinks += "<a href=\"#G" + Title + "\">" + Title + "</a>";
						GrammarWordList += "<h2 id =\"G" + Title + "\"" + ">" + Title + "</h2>" + HelpData + MainStaticClass.newLine + MainStaticClass.newLine;
					}
				}
				else
				{
					// This is used to indicate the composition help.
					foreach (var file in Directory.EnumerateFiles(MainStaticClass.CompositionDirectory))
					{

						var Data = File.ReadAllText(file);
						var Title = Path.GetFileNameWithoutExtension(file);
						var helpIndex = Data.IndexOf("<help>");
						var helpEndIndex = Data.IndexOf("</help>");
						// If the document is not properly set up, get out.
						if (helpIndex == -1 | helpEndIndex == -1)
						{
							continue;
						}
						var HelpData = Data.Substring(helpIndex + 6, helpEndIndex - helpIndex - 6);
						if (!HelpData.Contains(">") | !HelpData.Contains(">"))
						{
							HelpData = "<p>" + HelpData.Replace("\n", "<br/>") + "</p>";
						}
						if (string.IsNullOrWhiteSpace(HelpData)) continue;
						CompositionNavLinks += "<a href=\"#C" + Title + "\">" + Title + "</a>";
						CompositionWordList += "<h2 id =\"C" + Title + "\"" + ">" + Title + "</h2>" + HelpData + MainStaticClass.newLine + MainStaticClass.newLine;
					}
				}
				GrammarNavLinks += "</div>";
				CompositionNavLinks += "</div>";
				string Links;
				string WordLists;
				if (!Composition)
				{
					Links = GrammarNavLinks;
					WordLists = GrammarWordList;
				}
				else
				{
					Links = CompositionNavLinks;
					WordLists = CompositionWordList;
				}
				WebbrowserHelp = "<!DOCTYPE html>"
				+ "<html>"
				+ "<head>"
				+ "<title>Help (Auto-Helper)</title>"
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
				+ "<h2 style = \"font-family:Cambria\">Shortcuts</h2>"
				+ Links
				+ "<h2 style = \"font-family:Cambria\">Help</h2>"
				+ WordLists
				+ "\n<a style = \"bottom:15px;right:10px;position:fixed;"
				+ "font-family:wingdings\" href = \"#\">é</a>"
				+ "\n</body></html>";
				webBrowser1.DocumentText = WebbrowserHelp;
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
				MessageBox.Show("An error has occurred. Please report this problem to Autosoft. We are very sorry for the inconvenience. \n\nError Code: HLD03\n\n" + ex.Message, "Error (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Help_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (Form Form in Application.OpenForms)
			{
				if (Form.Name == "Start") Form.Show();
			}
		}
	}
}
