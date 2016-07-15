using System.Diagnostics;

namespace MiWifiRouter
{
	public class CommandLine
	{
		public static int ExecuteCommand(string command)
		{
			ProcessStartInfo info = new ProcessStartInfo();
			info.Arguments = command;
			info.FileName = "cmd.exe";
			info.WindowStyle = ProcessWindowStyle.Hidden;

			var process = Process.Start(info);
			process.WaitForExit();

			return process.ExitCode;
		}
	}
}
