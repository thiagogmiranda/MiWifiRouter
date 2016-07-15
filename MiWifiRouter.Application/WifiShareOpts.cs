using System.Net.NetworkInformation;

namespace MiWifiRouter
{
	public class WifiShareOpts
	{
		public NetworkInterface Source { get; set; }
		public string SSID { get; set; }
		public string Password { get; set; }
	}
}
