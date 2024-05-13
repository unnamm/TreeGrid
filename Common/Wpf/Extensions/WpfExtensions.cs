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

using Common.Native.Windows;
using Common.Native.Windows.Com;
using System.Windows;
using System.Windows.Interop;

namespace Common.Wpf
{
	public static class WpfExtensions
	{
		public static void SetId(this Window window, string Id)
		{
			// Create the property variant for the window
			PropertyKey     key      = new PropertyKey(PropertyId.AppUserModel, FormatId.AppUserModel);
			PropertyVariant property = new PropertyVariant(Id);
			IPropertyStore  store    = null;

			// Get the property store for the window
			Win32.SHGetPropertyStoreForWindow(new WindowInteropHelper(window).Handle, ref InterfaceId.IPropertyStore, out store);

			// Set the property value for the window
			store.SetValue(ref key, ref property);

			// Clean up the property
			property.Clear();
		}

		public static string GetId(this Window window)
		{
			// Create the property variant for the window
			PropertyVariant property;
			PropertyKey     key   = new PropertyKey(PropertyId.AppUserModel, FormatId.AppUserModel);
			IPropertyStore  store = null;

			// Get the property store for the window
			Win32.SHGetPropertyStoreForWindow(new WindowInteropHelper(window).Handle, ref InterfaceId.IPropertyStore, out store);

			// Get the value for the property
			store.GetValue(ref key, out property);

			// Convert the property value to an Id
			string Id = (string)property.Value;

			// Clean up the property
			property.Clear();

			// Return the Id of the window
			return Id;
		}
	}
}