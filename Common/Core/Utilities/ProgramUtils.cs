// ------------------------------------------------------
// ---------- Copyright (c) 2017 Colton Murphy ----------
// ------------------------------------------------------
// ------------------------------------------------------
// ------------------------------------------------------
// ------------------------------------------------------
// ------------------------------------------------------
// ------------------------------------------------------
// ------------------------------------------------------
// ------------------------------------------------------
// ------------------------------------------------------
// ------------------------------------------------------
// ------------------------------------------------------

using Common.Native.Windows;
using System;
using System.IO;

namespace Common
{
	public static class ProgramUtils
	{
		public static readonly string Location;

		static ProgramUtils()
		{
			// Get the program location
			Location = AppDomain.CurrentDomain.BaseDirectory;
		}

		public static void SetId(string appId)
		{
			// Set the program Id
			Win32.SetCurrentProcessExplicitAppUserModelID(appId);
		}

		public static string WorkingDirectory
		{
			get { return Directory.GetCurrentDirectory(); }
			set { Directory.SetCurrentDirectory(value); }
		}
	}
}