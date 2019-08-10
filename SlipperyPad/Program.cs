using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace SlipperyPad
{
	static class Program
	{
		static int normalSpeed = 10;
		static int synapticsSpeed = 16;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			try {
				if (args.Length != 2) {
					throw new Exception();
				}

				normalSpeed = ClampMouseSpeed(Int32.Parse(args[0]));
				synapticsSpeed = ClampMouseSpeed(Int32.Parse(args[1]));
			}
			catch(Exception) {
				MessageBox.Show(
					"SilpperyPad.exe {normal_speed} {touchpad_speed}\n" +
					"  Note that the speed value should be from 1 to 20 (Default value of Windows is 10)",
					"Usage",
					MessageBoxButtons.OK
				);
				return;
			}

			AppDomain.CurrentDomain.ProcessExit += OnApplicationExit;
			SystemEvents.PowerModeChanged += OnPowerModeChanged;

			InitSynaptics();

			while (true) {
				Thread.CurrentThread.Join();
			}
		}

		private static void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e) {
			switch (e.Mode) {
				case PowerModes.Resume:
					InitSynaptics();
					break;
			}
		}

		private static void InitSynaptics() {
			SetMouseSpeed(normalSpeed);
			SynapticsAPI synapticsAPI = new SynapticsAPI();

			foreach (SynapticsMouse mouse in synapticsAPI.DeviceList) {
				Console.WriteLine(String.Format("Found Synaptics Mouse Device: Name={0}", mouse.Name));
				mouse.OnMoveStart += OnSynapticsMoveStart;
				mouse.OnMoveEnd += OnSynapticsMoveEnd;
			}
		}

		private static void OnApplicationExit(object sender, EventArgs e) {
			SetMouseSpeed(normalSpeed);
		}

		private static void OnSynapticsMoveEnd(object sender, EventArgs e) {
			SetMouseSpeed(normalSpeed);
		}

		private static void OnSynapticsMoveStart(object sender, EventArgs e) {
			SetMouseSpeed(synapticsSpeed);
		}

		private static int ClampMouseSpeed(int speed) {
			return Math.Max(0, Math.Min(speed, 20));
		}

		private static void SetMouseSpeed(int sensitivity) {
			IntPtr pvParam = new IntPtr(sensitivity);
			User32.SystemParametersInfo(User32.SPI.SPI_SETMOUSESPEED, 0, pvParam, User32.SPIF.SPIF_UPDATEINIFILE);
		}
	}
}
