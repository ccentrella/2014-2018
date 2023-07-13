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
namespace SpellingMaster
{
	public partial class Help : Form
	{
		public Help()
		{
			InitializeComponent();
		}

		private void Help_Load(object sender, EventArgs e)
		{
			try
			{
				string WebbrowserHelp; // This is used for most help.
				// This is used to indicate the  spelling words.
				StreamReader MainStreamReader = new StreamReader(MainStaticClass.SpellingWordFileLocation);
				string WordList = "";

				while (MainStreamReader.Peek() > -1)
				{
					string CurrentInput = MainStreamReader.ReadLine() + "\n";
					CurrentInput = CurrentInput.Insert(0, "<b>").Replace(" :", ":").Replace(": ", ":").Replace(":", "</b> For example, ");
					if (!CurrentInput.Contains(":")) CurrentInput += " </b>";
					CurrentInput = CurrentInput.Replace("\n", "\n <br/>");
					WordList += CurrentInput;
				}
				MainStreamReader.Close();
				WebbrowserHelp = File.ReadAllText(MainStaticClass.HelpLocation) + WordList + File.ReadAllText(MainStaticClass.RulesLocation)
				+ "\n<p style=\"font-weight:bold;font-size:10px\">Word-List Version: " + MainStaticClass.WordListVersion + "</p></body></html>";
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
			catch (Exception)
			{
				MessageBox.Show("An error has occurred. Please report this problem to Autosoft. We are very sorry for the inconvenience. \n\nError Code: HLD03", "Error (Auto-Helper)", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
