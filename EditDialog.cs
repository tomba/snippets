using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Snippets
{
	public partial class EditDialog : Form
	{
		public EditDialog()
		{
			InitializeComponent();
		}

		public string EditText
		{
			get { return textBox.Text; }
			set { textBox.Text = value; }
		}

		public Font EditFont
		{
			set { textBox.Font = value; }
		}

		private void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && e.Shift)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
				this.DialogResult = DialogResult.OK;
			}
		}
	}
}