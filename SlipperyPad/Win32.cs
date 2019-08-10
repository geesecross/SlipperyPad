using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SlipperyPad
{
	class User32
	{
		[Flags]
		public enum SPI
		{
			/// <summary>Retrieves the current mouse speed. The mouse speed determines how far the pointer will move based on the distance the mouse moves. The pvParam parameter must point to an integer that receives a value which ranges between 1 (slowest) and 20 (fastest). A value of 10 is the default. The value can be set by an end-user using the mouse control panel application or by an application using SPI_SETMOUSESPEED.</summary>
			SPI_GETMOUSESPEED = 0x0070,
			/// <summary>Sets the current mouse speed. The pvParam parameter is an integer between 1 (slowest) and 20 (fastest). A value of 10 is the default. This value is typically set using the mouse control panel application.</summary>
			SPI_SETMOUSESPEED = 0x0071,
		}

		[Flags]
		public enum SPIF
		{
			None = 0x00,
			/// <summary>Writes the new system-wide parameter setting to the user profile.</summary>
			SPIF_UPDATEINIFILE = 0x01,
			/// <summary>Broadcasts the WM_SETTINGCHANGE message after updating the user profile.</summary>
			SPIF_SENDCHANGE = 0x02,
			/// <summary>Same as SPIF_SENDCHANGE.</summary>
			SPIF_SENDWININICHANGE = 0x02
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SystemParametersInfo(SPI uiAction, uint uiParam, IntPtr pvParam, SPIF fWinIni);

		// For setting a string parameter
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, SPIF fWinIni);

		// For reading a string parameter
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, StringBuilder pvParam, SPIF fWinIni);
	}
}
