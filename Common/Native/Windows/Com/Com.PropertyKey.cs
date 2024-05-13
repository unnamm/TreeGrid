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

namespace Common.Native.Windows.Com
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PropertyKey
	{
		public Guid FormatId   { get; private set; }
		public uint PropertyId { get; private set; }

		public PropertyKey(PropertyId propertyId, Guid formatId)
		{
			// Initialize the key
			PropertyId = (uint)propertyId;
			FormatId   = formatId;
		}
	}
}