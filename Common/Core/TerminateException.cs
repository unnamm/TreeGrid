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

namespace Common
{
	public class TerminateException : Exception
	{
		public int ExitCode { get; private set; }

		public TerminateException(Enum value, string message)                  : this(value, message, null) { }
		public TerminateException(Enum value, string message, Exception inner) : this(((IConvertible)value).ToInt32(null), message, inner) { }
		public TerminateException(int exitCode, string message)                : this(exitCode, message, null) { }

		public TerminateException(int exitCode, string message, Exception inner) : base(message, inner)
		{
			// Initialize the exception
			ExitCode = exitCode;
		}
	}
}