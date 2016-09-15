using NETCONLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MiWifiRouter
{
	public class Hotspot
	{
		public delegate void SearchDevicesCompletedEvent(List<Device> devices);
		public event SearchDevicesCompletedEvent SearchDevicesCompleted;

		private static readonly INetSharingManager SharingManager = new NetSharingManager();

		public bool IsSharing
		{
			get
			{
				return SharingEnabled(LocalConnection) && SharingEnabled(WifiConnection);
			}
		}

		private INetConnection LocalConnection;
		private INetConnection WifiConnection;

		private static object syncDeviceSearch = new object();
		private List<Device> DevicesFound = new List<Device>();

		public void ShareWifi(WifiShareOpts options)
		{
			if (!IsSharing)
			{
				CommandLine.ExecuteCommand(string.Format("/C netsh wlan set hostednetwork mode=allow ssid={0} key={1}", options.SSID, options.Password));
				CommandLine.ExecuteCommand("/C netsh wlan start hostednetwork");

				LocalConnection = GetConnectionById(options.Source.Id);
				WifiConnection = GetConnectionByDescription("Microsoft Hosted Network Virtual Adapter");

				NetworkManager.EnableShare(LocalConnection, tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
				NetworkManager.EnableShare(WifiConnection, tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);
			}
		}

		public void StopSharing(NetworkInterface network)
		{
			if (IsSharing)
			{
				NetworkManager.DisableShare(WifiConnection);
				NetworkManager.DisableShare(LocalConnection);

				CommandLine.ExecuteCommand("/C netsh wlan stop hostednetwork");
				CommandLine.ExecuteCommand("/C netsh wlan set hostednetwork mode=disallow");

				LocalConnection = null;
				WifiConnection = null;
			}
		}

		public void SearchConnectedDevices()
		{
			DevicesFound.Clear();

			if (IsSharing)
			{
				string gatewayIp = GetSharePointNetworkGatewayIP();
				string[] array = gatewayIp.Split('.');
				string ipBase = string.Format("{0}.{1}.{2}", array[0], array[1], array[2]);

				var tasks = new List<Task>();

				for (int i = 2; i <= 255; i++)
				{
					string enderecoIp = string.Format("{0}.{1}", ipBase, i);

					tasks.AddRange(Lookup(enderecoIp, 4, 4000));
				}

				Task.WaitAll(tasks.ToArray());

				if (SearchDevicesCompleted != null)
				{
					SearchDevicesCompleted(DevicesFound);
				}
			}
		}

		public Task[] Lookup(string host, int attempts, int timeout)
		{
			var threads = new Task[attempts];

			for (int i = 0; i < attempts; i++)
			{
				var t = Task.Factory.StartNew(delegate()
				{
					try
					{
						System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
						ping.PingCompleted += new PingCompletedEventHandler(PingCompleted);
						ping.SendAsync(host, timeout, host);

						GC.Collect();
					}
					catch
					{
						// Do nothing and let it try again until the attempts are exausted.
						// Exceptions are thrown for normal ping failurs like address lookup
						// failed.  For this reason we are supressing errors.
					}
				});

				threads[i] = t;
			}

			return threads;
		}

		private void PingCompleted(object sender, PingCompletedEventArgs e)
		{
			if (e.Reply != null && e.Reply.Status == IPStatus.Success)
			{
				string ip = (string)e.UserState;

				Device device = new Device();
				device.Hostname = GetHostName(ip);
				device.MacAddress = GetMacAddress(ip);
				device.IpAddress = ip;

				lock (syncDeviceSearch)
				{
					if (!DevicesFound.Any(d => d.MacAddress == device.MacAddress))
					{
						DevicesFound.Add(device);
					}
				}
			}
		}

		public string GetHostName(string ipAddress)
		{
			try
			{
				IPHostEntry entry = Dns.GetHostEntry(ipAddress);
				if (entry != null)
				{
					return entry.HostName;
				}
			}
			catch (SocketException)
			{
			}

			return null;
		}

		public string GetMacAddress(string ipAddress)
		{
			string macAddress = string.Empty;
			System.Diagnostics.Process Process = new System.Diagnostics.Process();
			Process.StartInfo.FileName = "arp";
			Process.StartInfo.Arguments = "-a " + ipAddress;
			Process.StartInfo.UseShellExecute = false;
			Process.StartInfo.RedirectStandardOutput = true;
			Process.StartInfo.CreateNoWindow = true;
			Process.Start();
			string strOutput = Process.StandardOutput.ReadToEnd();
			string[] substrings = strOutput.Split('-');
			if (substrings.Length >= 8)
			{
				macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
						 + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
						 + "-" + substrings[7] + "-"
						 + substrings[8].Substring(0, 2);
				return macAddress;
			}
			else
			{
				return "OWN Machine";
			}
		}

		private string GetSharePointNetworkGatewayIP()
		{
			var sharePointProps = SharingManager.get_NetConnectionProps(WifiConnection);
			var sharePointInterface = NetworkInterface.GetAllNetworkInterfaces().Where(i => i.Id == sharePointProps.Guid).DefaultIfEmpty(null).First();
			string ip = null;

			foreach (UnicastIPAddressInformation d in sharePointInterface.GetIPProperties().UnicastAddresses)
			{
				if (d.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					ip = d.Address.ToString();
					break;
				}
			}

			return ip;
		}

		private bool SharingEnabled(INetConnection con)
		{
			return con != null && GetConfiguration(con).SharingEnabled;
		}

		private INetSharingConfiguration GetConfiguration(INetConnection con)
		{
			return SharingManager.get_INetSharingConfigurationForINetConnection(con);
		}

		private INetConnection GetConnectionById(string id)
		{
			return (from INetConnection c
				in SharingManager.EnumEveryConnection
					where SharingManager.get_NetConnectionProps(c).Guid == id
					select c).DefaultIfEmpty(null).First();
		}

		private INetConnection GetConnectionByDescription(string desc)
		{
			var inter = NetworkInterface.GetAllNetworkInterfaces().Where(n => n.Description.Contains(desc)).DefaultIfEmpty(null).First();

			return GetConnectionById(inter.Id);
		}
	}
}
