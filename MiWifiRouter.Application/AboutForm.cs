using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiWifiRouter
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();

			lblAppName.Text += Assembly.GetEntryAssembly().GetName().Version.ToString();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=7WQEYYCTM6854");
		}
	}
}
