using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Collections;
using System.Runtime.InteropServices;

namespace Snippets
{
	static class Program
	{
		[DllImport("Kernel32.dll")]
		static extern bool AllocConsole();

		[DllImport("Kernel32.dll")]
		static extern bool FreeConsole();


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//AllocConsole();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			MainForm f = new MainForm();
			//f.Show();

			Application.Run();

			Properties.Settings.Default.Save();

			//FreeConsole();
		}
	}
}