using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SYNCTRLLib;

namespace SlipperyPad
{
	class SynapticsAPI
	{
        private readonly SynAPICtrl synAPICtrl;

        public SynapticsAPI()
		{
			synAPICtrl = new SynAPICtrl();
			synAPICtrl.Initialize();
			synAPICtrl.Activate();
			CreateDeviceList();
		}

		private void CreateDeviceList()
		{
			int deviceHandle = -1;
			while(true)
			{
				deviceHandle = synAPICtrl.FindDevice(SynConnectionType.SE_ConnectionAny, SynDeviceType.SE_DeviceTouchPad, deviceHandle);
				
				if(-1 == deviceHandle)
					break;

				DeviceList.Add(new SynapticsMouse(deviceHandle));
			}
		}

        public List<SynapticsMouse> DeviceList { get; } = new List<SynapticsMouse>();
    }
}