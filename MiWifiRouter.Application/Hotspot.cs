using NETCONLib;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace MiWifiRouter
{
	public delegate void DeviceFoundEvent(Device device);

	public class Hotspot
	{
		public event DeviceFoundEvent OnDeviceFound;

		public bool Started
		{
			get
			{
				return NetworkManager.IsSharingEnabled(LocalConnection) 
					&& NetworkManager.IsSharingEnabled(WifiConnection);
			}
		}

		private INetConnection LocalConnection;
		private INetConnection WifiConnection;

		public string Name { get; set; }
		public string Password { get; set; }
		public NetworkInterface LocalNetwork { get; set; }

		public void Start()
		{
			if (!Started)
			{
				LocalConnection = NetworkManager.GetConnectionById(LocalNetwork.Id);
				WifiConnection = NetworkManager.EnableHostedNetwork(this.Name, this.Password);

				if (WifiConnection == null)
				{
					throw new Exception("Verifique se o adaptador de rede sem fio está ativado!");
				}

				NetworkManager.EnableShare(LocalConnection, tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
				NetworkManager.EnableShare(WifiConnection, tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);
			}
		}

		public void Stop()
		{
			if (Started)
			{
				NetworkManager.DisableShare(WifiConnection);
				NetworkManager.DisableShare(LocalConnection);

				NetworkManager.DisableHostedNetwork();

				LocalConnection = null;
				WifiConnection = null;
			}
		}

		public void SearchConnectedDevicesAsync()
		{
			if (Started)
			{
				Task.Factory.StartNew(() => {
					string gateway = NetworkManager.GetRootIP(WifiConnection);
					string[] array = gateway.Split('.');
					string ipBase = string.Format("{0}.{1}.{2}", array[0], array[1], array[2]);

					for (int ipEnd = 2, i = 0; ipEnd <= 255; ipEnd++, i++)
					{
						string addr = string.Format("{0}.{1}", ipBase, ipEnd);

						Task.Factory.StartNew(() => { TryPing(addr); });
					}

					GC.Collect();
				});
			}
		}

		public void TryPing(string addr)
		{
			int attempts = 2;
			int timeout = 4000;

			for (int i = 0; i < attempts; i++)
			{
				Task.Factory.StartNew(() =>
				{
					try
					{
						Ping ping = new Ping();
						ping.PingCompleted += new PingCompletedEventHandler(PingCompleted);
						ping.SendAsync(addr, timeout, addr);

						GC.Collect();
					}
					catch
					{
						// Do nothing and let it try again until the attempts are exausted.
						// Exceptions are thrown for normal ping failurs like address lookup
						// failed.  For this reason we are supressing errors.
					}
				});
			}
		}

		public void PingCompleted(object sender, PingCompletedEventArgs e)
		{
			if (e.Reply != null && e.Reply.Status == IPStatus.Success)
			{
				string ip = (string)e.UserState;

				Device device = new Device();
				device.Hostname = NetworkManager.GetHostName(ip);
				device.MacAddress = NetworkManager.GetMacAddress(ip);
				device.IpAddress = ip;

				if (OnDeviceFound != null)
				{
					OnDeviceFound(device);
				}

				GC.Collect();
			}
		}
	}
}
