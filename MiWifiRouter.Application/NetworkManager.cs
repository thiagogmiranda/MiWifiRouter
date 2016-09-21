using NETCONLib;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace MiWifiRouter
{
	public class NetworkManager
	{
		private static readonly INetSharingManager SharingManager = new NetSharingManager();

		public static INetConnection EnableHostedNetwork(string ssid, string password)
		{
			CommandLine.ExecuteCommand(string.Format("/C netsh wlan set hostednetwork mode=allow ssid={0} key={1}", ssid, password));
			CommandLine.ExecuteCommand("/C netsh wlan start hostednetwork");

			return GetConnectionByDescription("Microsoft Hosted Network Virtual Adapter");
		}

		public static void DisableHostedNetwork()
		{
			CommandLine.ExecuteCommand("/C netsh wlan stop hostednetwork");
			CommandLine.ExecuteCommand("/C netsh wlan set hostednetwork mode=disallow");
		}

		[HandleProcessCorruptedStateExceptions]
		public static bool EnableShare(INetConnection connection, tagSHARINGCONNECTIONTYPE shareType, int tentativa = 1)
		{
			try
			{
				if (shareType == tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC)
				{
					INetSharingPublicConnectionCollection cons = SharingManager.get_EnumPublicConnections(tagSHARINGCONNECTION_ENUM_FLAGS.ICSSC_ENABLED);

					foreach (INetConnection con in cons)
					{
						DisableShare(con);
					}
				}

				GetShareConfiguration(connection).EnableSharing(shareType);

				return true;
			}
			catch (COMException ex)
			{
				var props = SharingManager.get_NetConnectionProps(connection);

				Debug.WriteLine("Tentativa {0} de 5.", tentativa);
				Debug.WriteLine("Ocorreu um erro ao ativar o compartilhamento de rede '{0}' para '{1}'. Mensagem de Erro: {2}",
					shareType.ToString(), props.Name, ex.Message);

				int novaTentativa = tentativa + 1;

				if (novaTentativa <= 5)
				{
					Debug.WriteLine("Executanto nova tentativa.");
					EnableShare(connection, shareType, novaTentativa);
				}
				else
				{
					Debug.WriteLine("Não foi possível ativar o compartilhamento de rede '{0}' para '{1}.'", shareType.ToString(), props.Name);
				}
			}

			return false;
		}

		[HandleProcessCorruptedStateExceptions]
		public static bool DisableShare(INetConnection connection, int tentativa = 1)
		{
			try
			{
				GetShareConfiguration(connection).DisableSharing();

				return true;
			}
			catch (Exception ex)
			{
				var props = SharingManager.get_NetConnectionProps(connection);

				Debug.WriteLine("Tentativa {0} de 5.", tentativa);
				Debug.WriteLine("Erro ao desabilitar o compartilhamento de rede para '{0}'. Erro: ", props.Name, ex.Message);

				int novaTentativa = tentativa + 1;

				if (novaTentativa <= 5)
				{
					Debug.WriteLine("Executanto nova tentativa.");
					DisableShare(connection, novaTentativa);
				}
				else
				{
					Debug.WriteLine("Não foi possível desabilitar o compartilhamento de rede para '{0}'.", props.Name);
				}
			}

			return false;
		}

		public static INetSharingConfiguration GetShareConfiguration(INetConnection connection)
		{
			return SharingManager.get_INetSharingConfigurationForINetConnection(connection);
		}

		public static INetConnection GetConnectionById(string id)
		{
			return (from INetConnection c
				in SharingManager.EnumEveryConnection
					where SharingManager.get_NetConnectionProps(c).Guid == id
					select c).DefaultIfEmpty(null).First();
		}

		public static INetConnection GetConnectionByDescription(string desc)
		{
			var networkInterface = NetworkInterface.GetAllNetworkInterfaces().Where(n => n.Description.Contains(desc)).DefaultIfEmpty(null).First();

			return GetConnectionById(networkInterface.Id);
		}

		public static bool IsSharingEnabled(INetConnection connection)
		{
			return connection != null && GetShareConfiguration(connection).SharingEnabled;
		}

		public static string GetRootIP(INetConnection connection)
		{
			var props = SharingManager.get_NetConnectionProps(connection);
			var networkInterface = NetworkInterface.GetAllNetworkInterfaces()
										.Where(i => i.Id == props.Guid)
										.DefaultIfEmpty(null)
										.First();

			var addressInfo = networkInterface.GetIPProperties().UnicastAddresses.
				Where(i => i.Address.AddressFamily == AddressFamily.InterNetwork)
				.DefaultIfEmpty(null)
				.First();

			if (addressInfo == null)
			{
				return null;
			}

			return addressInfo.Address.ToString();
		}

		public static string GetHostName(string ipAddress)
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

		public static string GetMacAddress(string ipAddress)
		{
			Process Process = new Process();
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
				string macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
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
	}
}
