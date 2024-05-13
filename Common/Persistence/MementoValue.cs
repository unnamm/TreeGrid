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

namespace Common.Persistence
{
	internal class MementoValue : IXmlLineInfo
	{
		private const string InvalidValueError = "The value cannot be null or empty.";
		private const string ValueLoadError    = "The memento value cannot be empty.";

		public int    LineNumber   { get; private set; }
		public int    LinePosition { get; private set; }
		public string Text         { get; private set; }

		public MementoValue(XElement element)
		{
			// Initialize the information
			LineNumber   = element.GetLine();
			LinePosition = element.GetPosition();

			// Load the value
			Load(element);
		}

		public MementoValue(string text)
		{
			// Make sure the value is valid
			if (String.IsNullOrEmpty(text))
			{
				// The value is not valid
				throw new ArgumentException(InvalidValueError);
			}

			// Initialize the value
			Text = text;
		}

		public bool HasLineInfo()
		{
			// Return a value indicating if the element has line information
			return (LineNumber > 0);
		}

		private void Load(XElement element)
		{
			// Load the value text
			Text = element.Value;

			// Is the value text valid?
			if (String.IsNullOrEmpty(Text))
			{
				// The value is not valid
				throw new XmlLoadException(ValueLoadError, element);
			}
		}
	}
}