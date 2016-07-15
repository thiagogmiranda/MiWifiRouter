using NETCONLib;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;

namespace MiWifiRouter
{
	public class Hotspot
	{
		private static readonly INetSharingManager SharingManager = new NetSharingManager();

		public bool IsSharing
		{
			get
			{
				return SharingEnabled(ToShare) && SharingEnabled(SharePoint);
			}
		}

		private INetConnection ToShare;
		private INetConnection SharePoint;

		public void ShareWifi(WifiShareOpts options)
		{
			try
			{
				if (!IsSharing)
				{
					CommandLine.ExecuteCommand(string.Format("/C netsh wlan set hostednetwork mode=allow ssid={0} key={1}", options.SSID, options.Password));
					CommandLine.ExecuteCommand("/C netsh wlan start hostednetwork");

					Thread.Sleep(3000);

					ToShare = GetConnectionById(options.Source.Id);
					SharePoint = GetConnectionByDescription("Microsoft Hosted Network Virtual Adapter");

					var hc = GetConfiguration(ToShare);
					var wc = GetConfiguration(SharePoint);

					hc.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
					wc.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);
				}
			}
			catch (COMException)
			{
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void StopSharing(NetworkInterface network)
		{
			if (IsSharing)
			{
				INetConnection wifi = GetConnectionByDescription("Microsoft Hosted Network Virtual Adapter");
				var wc = GetConfiguration(wifi);
				wc.DisableSharing();

				INetConnection homeCon = GetConnectionById(network.Id);
				var hc = GetConfiguration(homeCon);
				hc.DisableSharing();

				CommandLine.ExecuteCommand("/C netsh wlan stop hostednetwork");
				CommandLine.ExecuteCommand("/C netsh wlan set hostednetwork mode=disallow");

				ToShare = null;
				SharePoint = null;
			}
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
