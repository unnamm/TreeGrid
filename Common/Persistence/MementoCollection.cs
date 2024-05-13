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

using Common.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Common.Persistence
{
	public class MementoCollection : IList<Memento>
	{
		private const int    Indentation         = 1;
		private const string InvalidMementoError = "The memento cannot be null.";
		private const string DocumentRootEltTag  = "Configuration";

		private List<Memento> mementos;

		public MementoCollection(string filepath) : this()
		{
			// Load the collection
			Load(filepath);
		}

		public MementoCollection()
		{
			// Initialize the collection
			mementos = new List<Memento>();
		}

		public void Save(string filepath)
		{
			// Create the output
			EncodedStringWriter output = new EncodedStringWriter();

			// Create the writer
			XmlTextWriter writer = new XmlTextWriter(output)
			{
				Formatting  = Formatting.Indented,
				Indentation = Indentation,
				IndentChar  = Characters.Tab
			};

			// Create the root element
			writer.WriteStartElement(DocumentRootEltTag);

			// Iterate through all of the mementos
			foreach (Memento memento in mementos)
			{
				// Save the memento
				memento.Save(writer);
			}

			// Close the root element
			writer.WriteEndElement();

			// Save the output to file
			FileUtils.WriteFileText(filepath, output.ToString());
		}

		public void CopyTo(Memento[] array, int index)
		{
			// Copy the collection to the array
			mementos.CopyTo(array, index);
		}

		public void Add(Memento memento)
		{
			// Insert the memento at the end of the collection
			Insert(mementos.Count, memento);
		}

		public void Insert(int index, Memento memento)
		{
			// Validate the memento
			ValidateMemento(memento);

			// Insert the memento into the collection
			mementos.Insert(index, memento);
		}

		public void Clear()
		{
			// Clear the mementos in the collection
			mementos.Clear();
		}

		public void RemoveAt(int index)
		{
			// Remove the memento from the collection
			mementos.RemoveAt(index);
		}

		public bool Remove(Memento memento)
		{
			// Get the index of the memento to remove
			int index = IndexOf(memento);

			// Is the memento in the collection?
			if (index < 0)
			{
				// The memento is not in the collection
				return false;
			}

			// Remove the memento from the collection
			RemoveAt(index);

			// The operation was successful
			return true;
		}

		public bool Contains(Memento memento)
		{
			// Return a value indicating if the memento is in the collection
			return mementos.Contains(memento);
		}

		public int IndexOf(Memento memento)
		{
			// Return the index of the memento in the collection
			return mementos.IndexOf(memento);
		}

		public IEnumerator<Memento> GetEnumerator()
		{
			// Return an enumerator for the collection
			return mementos.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			// Return an enumerator for the collection
			return GetEnumerator();
		}

		private void ValidateMemento(Memento memento)
		{
			// Is the memento valid?
			if (memento == null)
			{
				// The memento is not valid
				throw new ArgumentNullException(InvalidMementoError);
			}
		}

		private void Load(string filepath)
		{
			// Load the memento document
			XDocument document = FileUtils.LoadXmlDocument(filepath);

			// Load the mementos from the document
			LoadMementos(document.Root);
		}

		private void LoadMementos(XElement root)
		{
			// Iterate through all of the mementos to load
			foreach (XElement element in root.Elements())
			{
				// Load the memento
				Add(new Memento(element));
			}
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public int Count
		{
			get { return mementos.Count; }
		}

		public Memento this[int index]
		{
			get { return mementos[index]; }
			set
			{
				// Validate the new memento
				ValidateMemento(value);

				// Replace the existing memento
				mementos[index] = value;
			}
		}
	}
}