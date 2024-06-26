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

namespace Common.CodeDOM.C
{
    public static class DefaultValues
	{
		public static CodeObject True  { get; private set; }
		public static CodeObject False { get; private set; }
		public static CodeObject Null  { get; private set; }

		static DefaultValues()
		{
			True  = new TrueValue();
			False = new FalseValue();
			Null  = new NullValue();
		}
	}
}