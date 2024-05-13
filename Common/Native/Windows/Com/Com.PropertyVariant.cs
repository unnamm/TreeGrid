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
using System.Runtime.InteropServices;

namespace Common.Native.Windows.Com
{
	[StructLayout(LayoutKind.Explicit, Pack = 1)]
	public struct PropertyVariant
	{
		private const ushort Empty           = 0;
		private const ushort LPWSTR          = 31;
		private const string TypeErrorFormat = "The variant type: {0} is not supported.";

		private static readonly Dictionary<Type, VariantType> TypeHandlers = new Dictionary<Type, VariantType>()
		{
			{ typeof(string), new VariantType(InitUnicodeString, ClearUnicodeString, GetUnicodeString) }
		};

		private static readonly Dictionary<ushort, Type> Types = new Dictionary<ushort, Type>()
		{
			{ LPWSTR, typeof(string) }
		};

		[FieldOffset(0)]
		private ushort varType;

		[FieldOffset(8)]
		private IntPtr pUnicodeString;

		[FieldOffset(16)]
		private Type managedType;

		public PropertyVariant(object value = null) : this()
		{
			// Initialize the variant
			Init(value);
		}

		private void Init(object value = null)
		{
			// Is the value valid?
			if (value == null)
			{
				// The type is empty
				varType     = (ushort)VarEnum.VT_EMPTY;
				managedType = null;
			}
			
			// Get the type of the value
			Type type = value.GetType();

			// Is the type valid?
			if (!TypeHandlers.ContainsKey(type))
			{
				// The type is not supported
				throw new ArgumentException(String.Format(TypeErrorFormat, managedType));
			}

			// Set the managed type
			managedType = type;

			// Initialize the value
			TypeHandlers[type].Init(ref this, value);
		}

		public void Clear()
		{
			// Is the value cleared?
			if (varType == (ushort)VarEnum.VT_EMPTY)
			{
				// The type is already cleared
				return;
			}

			// Clear the variant
			TypeHandlers[managedType].Clear(ref this);

			// Clear the variant type
			varType     = (ushort)VarEnum.VT_EMPTY;
			managedType = null;
		}

		private static void InitUnicodeString(ref PropertyVariant variant, object value)
		{
			// Get a pointer to the string
			variant.pUnicodeString = Marshal.StringToCoTaskMemUni(value as string);

			// Set the variant type
			variant.varType = (ushort)VarEnum.VT_LPWSTR;
		}

		private static void ClearUnicodeString(ref PropertyVariant variant)
		{
			// Free the memory associated with the string
			Marshal.FreeCoTaskMem(variant.pUnicodeString);
		}

		private static object GetUnicodeString(ref PropertyVariant variant)
		{
			// Return the unicode string
			return Marshal.PtrToStringUni(variant.pUnicodeString);
		}

		public object Value
		{
			get
			{
				// Is the variant cleared?
				if (varType == (ushort)VarEnum.VT_EMPTY)
				{
					// The variant is cleared
					return null;
				}

				// Make sure the managed type exist
				else if (!Types.ContainsKey(varType))
				{
					// The type is not supported
					throw new InvalidOperationException(String.Format(TypeErrorFormat, varType));
				}

				// Set the managed type
				managedType = Types[varType];

				// Get the value associated with the variant
				return TypeHandlers[managedType].Get(ref this);
			}

			set { Init(value); }
		}

		private class VariantType
		{
			public delegate void   InitHandler(ref PropertyVariant variant, object value);
			public delegate void   ClearHandler(ref PropertyVariant variant);
			public delegate object GetHandler(ref PropertyVariant variant);

			public InitHandler  Init  { get; private set; }
			public ClearHandler Clear { get; private set; }
			public GetHandler   Get   { get; private set; }

			public VariantType(InitHandler init, ClearHandler clear, GetHandler get)
			{
				// Initialize the type
				Init  = init;
				Clear = clear;
				Get   = get;
			}
		}
	}
}