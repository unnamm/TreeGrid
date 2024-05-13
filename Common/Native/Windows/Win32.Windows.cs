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
		public enum ShowValue : int
		{
			Normal    = 1,
			Minimized = 2
		}

		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public struct Point
		{
			public int X;
			public int Y;

			public Point(int x, int y)
			{
				// Initialize the point
				X = x;
				Y = y;
			}
		}

		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public struct Rectangle
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;

			public Rectangle(int left, int top, int right, int bottom)
			{
				// Initialize the rectangle
				Left   = left;
				Top    = top;
				Right  = right;
				Bottom = bottom;
			}
		}

		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public struct Placement
		{
			public int       Length;
			public int       Flags;
			public int       ShowCommand;
			public Point     MinPosition;
			public Point     MaxPosition;
			public Rectangle NormalPosition;
		}

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetWindowPlacement(IntPtr hWindow, [In] ref Placement placement);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetWindowPlacement(IntPtr hWindow, out Placement placement);
	}
}