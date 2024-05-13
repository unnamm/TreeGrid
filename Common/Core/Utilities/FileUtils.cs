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
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Common
{
	public static class FileUtils
	{
		public static readonly Encoding DefaultEncoding = Encoding.UTF8;

		private const string PathError          = "The path could not be resolved.";
		private const string XmlLoadErrorFormat = "There was a problem loading the Xml document: \"{0}\". {1} Line {2}, Column {3}";

		private static readonly string[] ExecutableSelectors = new string[] { "/", @"\" };

		public static XDocument LoadXmlDocument(string filepath)
		{
			try
			{
				// Load the document
				return XDocument.Load(filepath, (LoadOptions.SetBaseUri | LoadOptions.SetLineInfo));
			}
			catch(XmlException e)
			{
				// The was an error loading the file
				throw new XmlLoadException(e);
			}
		}

		public static XDocument LoadXmlDocument(string filepath, string schemapath)
		{
			// Load the document
			XDocument document = LoadXmlDocument(filepath);

			// Load schema
			XmlReader    reader    = XmlReader.Create(schemapath);
			XmlSchemaSet schemaSet = new XmlSchemaSet();

			// Add schema
			schemaSet.Add(String.Empty, reader);

			try
			{
				// Validate input XML file
				document.Validate(schemaSet, null);
			}
			catch (XmlSchemaValidationException e)
			{
				// There was an error loading the file
				throw new XmlLoadException(e);
			}

			// Return the loaded document
			return document;
		}

		public static void WriteFileText(string filepath, string text)
		{
			// Write the file text
			WriteFileText(filepath, text, DefaultEncoding);
		}

		public static void WriteFileText(string filepath, string text, Encoding encoding)
		{
			// Compare the text content in the file with the input text
			if (CompareFileText(filepath, text))
			{
				// We do not need to write the file text, because the context is the same
				return;
			}

			// Get the directory for the filepath
			string directory = Path.GetDirectoryName(filepath);

			// Is a directory specified?
			if (!String.IsNullOrEmpty(directory))
			{
				// Create the directory if it does not exist
				Directory.CreateDirectory(directory);
			}

			// Write the file text
			File.WriteAllText(filepath, text, DefaultEncoding);
		}

		public static bool CompareFileText(string filepath, string text)
		{
			// Compare the file text
			return CompareFileText(filepath, text, DefaultEncoding);
		}

		public static bool TryWriteFileText(string filepath, string text)
		{
			// Try to write the file text
			return TryWriteFileText(filepath, text, DefaultEncoding);
		}

		public static bool TryWriteFileText(string filepath, string text, Encoding encoding)
		{
			try
			{
				// Write the file text
				WriteFileText(filepath, text, encoding);
			}
			catch
			{
				// The operation failed
				return false;
			}

			// The operation was successful
			return true;
		}

		public static bool CompareFileText(string filepath, string text, Encoding encoding)
		{
			try
			{
				// Read all of the text in the file
				string fileText = File.ReadAllText(filepath, encoding);

				// Compare the file text to the input text
				return (fileText == text);
			}
			catch { }

			// The comparison failed
			return false;
		}

		public static string ResolvePath(string path)
		{
			// Initialize the base path
			string basePath = ProgramUtils.WorkingDirectory;

			// Iterate through the executable selectors
			foreach (string selector in ExecutableSelectors)
			{
				// Does the path start with a selector?
				if (path.StartsWith(selector))
				{
					// Remove the selector from the path
					path = path.Substring(selector.Length);

					// Get a path relative to the executable
					basePath = ProgramUtils.Location;

					// A selector was found
					break;
				}
			}

			// Return the path relative to the base path
			return ResolveRelativePath(basePath, path);
		}

		private static string ResolveRelativePath(string basePath, string path)
		{
			try
			{
				// Combine the base path and the input path
				path = Path.Combine(basePath, path);

				// Resolve the absolute path
				return Path.GetFullPath(path);
			}
			catch
			{
				// The path could not be resolved
				throw new Exception(PathError);
			}
		}
	}
}