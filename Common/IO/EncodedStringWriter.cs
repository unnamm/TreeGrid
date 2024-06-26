﻿// ------------------------------------------------------
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

using System.IO;
using System.Text;

namespace Common.IO
{
	public class EncodedStringWriter : StringWriter
	{
		private Encoding encoding;

		public EncodedStringWriter() : this(FileUtils.DefaultEncoding) { }

		public EncodedStringWriter(Encoding encoding)
		{
			// Initialize the writer
			this.encoding = encoding;
		}

		public override Encoding Encoding
		{
			get { return encoding; }
		}
	}
}