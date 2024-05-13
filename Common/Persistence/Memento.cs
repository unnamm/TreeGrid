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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Common.Persistence
{
	public class Memento : IXmlLineInfo
	{
		private const string InvalidNameError     = "The name cannot be null or empty.";
		private const string InvalidKeyError      = "The value key cannot be null or empty.";
		private const string InvalidEnumTypeError = "The operation must be performed on an enumerated type.";
		private const string ValueReadErrorFormat = "There was a problem reading the memento value: {0}. {1}";
		private const string EnumReadErrorFormat  = "The enumerated memento value: {0} is not valid.";
		
		public int    LineNumber   { get; private set; }
		public int    LinePosition { get; private set; }
		public string Name         { get; private set; }

		private Dictionary<string, MementoValue> values;

		public Memento(XElement element) : this()
		{
			// Load the memento
			Load(element);
		}

		public Memento(string name) : this()
		{
			// Is the name valid?
			if (String.IsNullOrEmpty(name))
			{
				// The name is not valid
				throw new ArgumentException(InvalidNameError);
			}

			// Initialize the memento
			Name = name;
		}

		private Memento()
		{
			// Initialize the memento
			values = new Dictionary<string, MementoValue>();
		}

		public void Save(XmlTextWriter writer)
		{
			// Start the memento element
			writer.WriteStartElement(Name);

			// Iterate through all of the values
			foreach (KeyValuePair<string, MementoValue> kvp in values)
			{
				// Write the memento value to the memento
				writer.WriteElementString(kvp.Key, kvp.Value.Text);
			}

			// Close the memento element
			writer.WriteEndElement();
		}

		public void Write(string key, bool value)
		{
			// Write the boolean value to the memento
			Write(key, value.ToString());
		}

		public void Write(string key, long value)
		{
			// Write the long value to the memento
			Write(key, value.ToString());
		}

		public void Write(string key, double value)
		{
			// Write the double value to the memento
			Write(key, value.ToString());
		}

		public void Write(string key, IConvertible value)
		{
			// Write the convertible value to the memento
			Write(key, value.ToString());
		}

		public void Write(string key, string value)
		{
			// Check the value key
			if (String.IsNullOrEmpty(key))
			{
				// The value key is not valid
				throw new ArgumentException(InvalidKeyError);
			}

			// Write the value to the memento
			Write(key, new MementoValue(value));
		}

		public bool HasLineInfo()
		{
			// Return a value indicating if the memento has line information
			return (LineNumber > 0);
		}

		public T ReadEnum<T>(string key, T defaultValue)
		{
			// Get the enumeration type
			Type enumType = typeof(T);

			// Check the type of the operation
			if (!enumType.IsEnum)
			{
				// The type of the operation is not valid
				throw new ArgumentException(InvalidEnumTypeError);
			}

			// Get the memento value
			MementoValue value = (values.ContainsKey(key) ? values[key] : null);

			// Does the value exist?
			if (value == null)
			{
				// Return the default value
				return defaultValue;
			}

			try
			{
				// Convert the value into an enumeration
				return (T)Enum.Parse(enumType, value.Text, true);
			}
			catch(Exception e)
			{
				// Does the value have line information?
				if (value.HasLineInfo())
				{
					// The loaded value was not correct
					throw new XmlLoadException(String.Format(EnumReadErrorFormat, value.Text), value, e);
				}

				// Rethrow the original exception
				throw e;
			}
		}

		public T Read<T>(string key, T defaultValue)
		{
			// Get the memento value
			MementoValue value = (values.ContainsKey(key) ? values[key] : null);

			// Does the value exist?
			if (value == null)
			{
				// Return the default value
				return defaultValue;
			}

			try
			{
				// Parse the numeric value
				return NumericValue.Parse<T>(value.Text);
			}
			catch(Exception e)
			{
				// Does the value have line information?
				if (value.HasLineInfo())
				{
					// The loaded value was not correct
					throw new XmlLoadException(String.Format(ValueReadErrorFormat, value.Text, e.Message), value, e);
				}

				// Rethrow the original exception
				throw e;
			}
		}

		public string Read(string key)
		{
			// Return the value associated with the given key
			return (values.ContainsKey(key) ? values[key].Text : null);
		}

		private void Load(XElement element)
		{
			// Set the name of the memento
			Name         = element.Name.LocalName;
			LineNumber   = element.GetLine();
			LinePosition = element.GetPosition();

			// Iterate through the values of the memento element
			foreach (XElement child in element.Elements())
			{
				// Write the value into the memento
				Write(child.Name.LocalName, child.Value);
			}
		}

		private void LoadValues(XElement element)
		{
			// Iterate through the values of the memento element
			foreach (XElement child in element.Elements())
			{
				// Write the value to the memento
				Write(child.Name.LocalName, new MementoValue(element));
			}
		}

		private void Write(string key, MementoValue value)
		{
			// Does the value already exist?
			if (values.ContainsKey(key))
			{
				// Remove the existing value
				values.Remove(key);
			}

			// Write the value to the memento
			values.Add(key, value);
		}
	}
}