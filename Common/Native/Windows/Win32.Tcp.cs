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
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Common.Native.Windows
{
	public static partial class Win32
	{
		public enum TcpTableClass
		{
			BasicListener,
			BasicConnections,
			BasicAll,
			OwnerPIDListener,
			OwnerPIDConnections,
			OwnerPIDAll,
			OwnerModuleListener,
			OwnerModuleConnections,
			OwnerModuleAll
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TcpTableRow
		{
			public TcpState State;
			public uint     LocalAddress;
			public ushort   LocalPort;
			private ushort  reserved;
			public uint     RemoteAddress;
			public ushort   RemotePort;
			private ushort  reserved2;
			public uint     PID;  
		}

		[DllImport("iphlpapi.dll", SetLastError = true)]
		public static extern uint GetExtendedTcpTable(IntPtr pTable, ref int tableSize, bool sort, AddressFamily family, TcpTableClass tableClass,
			                                          uint reserved = 0);
	}
}