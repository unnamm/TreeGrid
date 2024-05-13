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

using System;
using System.Runtime.InteropServices;

namespace Common.Native.Windows
{
	public static partial class Win32
	{
		[Flags]
		public enum StartupFlags : uint
		{
			UseStandardHandles = 0x00000100
		}

		[Flags]
		public enum CreationFlags : uint
		{
			Suspended           = 0x00000004,
			ExtendedStartupInfo = 0x00080000
		}

		public enum ProcThreadAttribute : uint
		{
			ParentProcess = 0x00020000
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct StartupInfo
		{
			public  uint         Length;            
			private string       reserved;
			public  string       Desktop;
			public  string       Title;
			public  uint         WindowX;
			public  uint         WindowY;
			public  uint         WindowWidth;
			public  uint         WindowHeight;
			public  uint         ConsoleWidthChars;
			public  uint         ConsoleHeightChars;
			public  uint         ConsoleColor;
			public  StartupFlags Flags;
			public  short        ShowWindowValue; 
			private short        reserved2;
			private IntPtr       reserved3;
			public  IntPtr       hStdInput;
			public  IntPtr       hStdOutput;
			public  IntPtr       hStdError;     
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct StartupInfoEx
		{
			public StartupInfo StartupInfo;
			public IntPtr      pAttributeList;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ProcessInformation
		{
			public IntPtr hProcess;
			public IntPtr hThread;
			public int    Id;
			public int    ThreadId;
		}

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern bool CreateProcess(string appName, string commandLine, ref Win32.SecurityAttributes procAttrs,
			                                    ref Win32.SecurityAttributes threadAttrs, bool inheritHandles, CreationFlags creationFlags,
												IntPtr pEnvironment, string currDirectory, [In] ref StartupInfoEx startupInfo,
												out ProcessInformation processInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool InitializeProcThreadAttributeList(IntPtr pAttrList, int numAttrs, int flags, ref IntPtr pSize);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UpdateProcThreadAttribute(IntPtr pAttrList, uint flags, IntPtr attr, IntPtr value, IntPtr size,
			                                                IntPtr previousValue, IntPtr returnSize);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteProcThreadAttributeList(IntPtr pAttrList);

		[DllImport("shell32.dll", SetLastError = true)]
		public static extern void SetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string appId);
	}
}