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
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace Common.Native.Windows
{
	public static partial class Win32
	{
		public const int False = 0;
		public const int True  = 1;

		public enum ErrorCodes : uint
		{
			Ok = 0
		}

		[Flags]
		public enum DuplicateOptions : uint
		{
			CloseSource = 0x01,
			SameAccess  = 0x02,
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SecurityAttributes
		{
			public int    Length;
			public IntPtr pSecurityDescriptor;
			public int    InheritHandle;
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr hObject);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DuplicateHandle(IntPtr hSourceProc, IntPtr hSource, IntPtr hTargetProc, out IntPtr hTarget,
												  DuplicateOptions desiredAccess, [MarshalAs(UnmanagedType.Bool)] bool inheritHandle,
												  DuplicateOptions options);
	}
}