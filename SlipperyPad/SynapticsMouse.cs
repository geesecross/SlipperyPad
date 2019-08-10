using System;
using System.Diagnostics;
using SYNCTRLLib;

namespace SlipperyPad
{
	class SynapticsMouse
	{
		public readonly string VendorID;
		public readonly string DeviceID;
		public readonly string Name;
		SynDeviceCtrl synDeviceCtrl = new SynDeviceCtrl();
		SynPacketCtrl synPacketCtrl = new SynPacketCtrl();
		bool hasMotion;

		public event EventHandler OnMoveStart;
		public event EventHandler OnMoveEnd;

		public SynapticsMouse(int deviceHandle) {
			if (-1 == deviceHandle)
				throw new Exception();

			this.synDeviceCtrl.Select(deviceHandle);

			string hardwareID = synDeviceCtrl.GetStringProperty((SynDeviceProperty) SynDeviceStringProperty.SP_PnPID);
			Debug.Print("SynapticsMouse HardwareID={0}", hardwareID);
			string[] token = hardwareID.Split(new char[] { '&' });
			this.VendorID = token[0];
			this.DeviceID = token[1];

			this.Name = synDeviceCtrl.GetStringProperty((SynDeviceProperty) SynDeviceStringProperty.SP_ModelString);
			Debug.Print("Device name={0}", this.Name);

			this.synDeviceCtrl.Activate();
			this.synDeviceCtrl.OnPacket += new _ISynDeviceCtrlEvents_OnPacketEventHandler(OnPacket);
		}

		void OnPacket() {
			this.synDeviceCtrl.LoadPacket(synPacketCtrl);

			bool hasMotion = 0 != (this.synPacketCtrl.FingerState & (int) SynFingerFlags.SF_FingerMotion);

			if (hasMotion && !this.hasMotion) {
				if (this.OnMoveStart != null) {
					this.OnMoveStart.Invoke(this, EventArgs.Empty);
				}
			}

			if (!hasMotion && this.hasMotion) {
				if (this.OnMoveEnd != null) {
					this.OnMoveEnd.Invoke(this, EventArgs.Empty);
				}
			}

			this.hasMotion = hasMotion;
		}
	}
}
