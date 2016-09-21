using System;
using System.Configuration;

namespace MiWifiRouter
{
	public class MiWifiConfiguration
	{
		public static string SSID
		{
			get { return GetString("ssid"); }
			set { Update("ssid", value.ToString()); }
		}

		public static string Password
		{
			get { return GetString("password"); }
			set { Update("password", value.ToString()); }
		}

		public static int DeviceListDelay
		{
			get { return GetInt("devicelistdelay"); }
			set { Update("devicelistdelay", value.ToString()); }
		}

		private static void Update(string key, string value)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings[key].Value = value;
			config.Save(ConfigurationSaveMode.Modified);
		}

		public static string GetString(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		public static int GetInt(string key)
		{
			return Convert.ToInt32(GetString(key));
		}
	}
}
