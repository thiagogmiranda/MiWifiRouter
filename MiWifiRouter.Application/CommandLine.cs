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

		public static string Execute(string command)
		{
			ProcessStartInfo info = new ProcessStartInfo();
			info.Arguments = "/C " + command;
			info.FileName = "cmd.exe";
			info.WindowStyle = ProcessWindowStyle.Hidden;
			info.CreateNoWindow = true;
			info.UseShellExecute = false;
			info.RedirectStandardOutput = true;

			var process = Process.Start(info);

			return process.StandardOutput.ReadToEnd();
		}
	}
}
