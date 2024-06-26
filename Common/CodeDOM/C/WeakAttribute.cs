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
    public class WeakAttribute : CodeAttribute
	{
		private const string WeakAttributeKeyword = "__weak";

		public WeakAttribute() : base(WeakAttributeKeyword) { }
	}
}