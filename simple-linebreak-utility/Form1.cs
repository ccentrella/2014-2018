using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace Html_to_CSharp_and_VB_Converter
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
			{
				string ReplaceString = "\"" + Constants.vbNewLine + "+ \"";
				if (checkBox1.Checked) ReplaceString = ReplaceString.Insert(0, " <br/>");
				if (checkBox2.Checked) ReplaceString = ReplaceString.Insert(0,"\" + Environment.NewLine + \"");
				string Input = "\"" + textBox1.Text.Replace("\"", "\\\"").Replace(Constants.vbNewLine, ReplaceString) + "\"";
				textBox2.Text = Input.Replace(" + \"\"","");
			}
			else if (radioButton2.Checked)
			{
				string ReplaceString = "\" _" + Constants.vbNewLine + "& \"";
				if (checkBox1.Checked) ReplaceString = ReplaceString.Insert(0," <br/>");
				if (checkBox2.Checked) ReplaceString = ReplaceString.Insert(0,"\" & vbNewLine & \"");
				string Input = "\"" + textBox1.Text.Replace("\"", "\"\"").Replace(Constants.vbNewLine, ReplaceString) + "\"";
				textBox2.Text = Input.Replace("& \"\" _"," _").Replace("\" <br/>","\"<br/>");
			}
		}
	}
}
