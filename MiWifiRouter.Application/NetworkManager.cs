using NETCONLib;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace MiWifiRouter
{
	public class NetworkManager
	{
		private static readonly INetSharingManager SharingManager = new NetSharingManager();

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
	}
}
