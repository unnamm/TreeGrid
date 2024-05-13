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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Common
{
	public class XmlLoadException : Exception
	{
		private const string LineInfoKey   = "line";
		private const string MessageFormat = "{0} Line {1}, Position {2}.";

		public int    Line        { get; private set; }
		public int    Position    { get; private set; }
		public string BaseMessage { get; private set; }

		public XmlLoadException(XmlException inner)                                                : this(GetBaseMessage(inner.Message), inner.LineNumber, inner.LinePosition, inner) { }
		public XmlLoadException(XmlSchemaException inner)                                          : this(GetBaseMessage(inner.Message), inner.LineNumber, inner.LinePosition, inner) { }
		public XmlLoadException(string baseMessage, XObject lineInfo, Exception inner = null)      : this(baseMessage, (IXmlLineInfo)lineInfo) { }
		public XmlLoadException(string baseMessage, IXmlLineInfo lineInfo, Exception inner = null) : this(baseMessage, lineInfo.LineNumber, lineInfo.LinePosition, inner) { }

		public XmlLoadException(string baseMessage, int line, int position, Exception inner = null) : base(FormatMessage(baseMessage, line, position), inner)
		{
			// Initialize the exception
			BaseMessage = baseMessage;
			Line        = line;
			Position    = position;
		}

		private static string GetBaseMessage(string message)
		{
			// First find the line information from the message
			int index = message.LastIndexOf(LineInfoKey, StringComparison.OrdinalIgnoreCase);

			// Strip the line information from the message
			message = ((index >= 0) ? message.Substring(0, index) : message);

			// Trim the message to remove whitespace
			return message.Trim();
		}

		private static string FormatMessage(string message, int line, int position)
		{
			// Format the message to include the line information
			return String.Format(MessageFormat, message, line, position);
		}
	}
}