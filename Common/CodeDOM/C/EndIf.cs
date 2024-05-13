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

using System;

namespace Common.CodeDOM.C
{
    public class EndIf : CodeObject
	{
		public EndIf() { }

		public override string ToString()
		{
			return GetString();
		}

		internal override string GetString(int indentation = 0)
		{
			// Print end if
			return (GetIndentation(indentation) + PoundChar + EndIfKeyword + Environment.NewLine);
		}
	}
}