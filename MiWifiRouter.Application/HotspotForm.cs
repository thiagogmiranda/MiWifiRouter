using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace MiWifiRouter
{
	public partial class HotspotForm : Form
	{
		private Hotspot HotSpot;

		public HotspotForm()
		{
			InitializeComponent();

			button2.Enabled = false;

			HotSpot = new Hotspot();
			HotSpot.SearchDevicesCompleted += HotSpot_SearchDevicesCompleted;

			CarregarRedesDisponiveis();

			txtNomeRede.Text = ConfigurationManager.AppSettings["ssid"] ?? string.Empty;
			txtSenha.Text = ConfigurationManager.AppSettings["password"] ?? string.Empty;
		}

		private void HotSpot_SearchDevicesCompleted(List<Device> devices)
		{
			this.Invoke(new Action(() => {
				listView1.Clear();
				foreach (var item in devices)
				{
					listView1.Items.Add(new ListViewItem(new string[] {
					item.IpAddress,
					item.Hostname,
					item.MacAddress
				}));
				}
			}));
		}

		private void CarregarRedesDisponiveis()
		{
			NetworkInterface[] redes = NetworkInterface.GetAllNetworkInterfaces();
			redes = redes
				.Where(rede => rede.OperationalStatus == OperationalStatus.Up && rede.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
				.ToArray();

			comboRedes.DataSource = redes;
			comboRedes.DisplayMember = "Name";
			comboRedes.ValueMember = "Id";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			button1.Enabled = false;

			if (HotSpot.IsSharing)
			{
				DesabilitarSharing();
			}
			else
			{
				WifiShareOpts opts = new WifiShareOpts();
				opts.SSID = txtNomeRede.Text;
				opts.Password = txtSenha.Text;
				opts.Source = (NetworkInterface)comboRedes.SelectedItem;

				if (OptsValidas(opts))
				{
					Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
					config.AppSettings.Settings["ssid"].Value = opts.SSID;
					config.AppSettings.Settings["password"].Value = opts.Password;
					config.Save(ConfigurationSaveMode.Modified);

					AtualizarStatus("Iniciando HotSpot.");

					HotSpot.ShareWifi(opts);

					DesabilitarComponentes();

					button2.Enabled = true;

					AtualizarStatus("HotSpot iniciado.");
				}
			}

			button1.Enabled = true;
		}

		private void DesabilitarSharing()
		{
			AtualizarStatus("Desligando HotSpot.");

			HotSpot.StopSharing((NetworkInterface)comboRedes.SelectedItem);

			HabilitarComponentes();

			AtualizarStatus("HotSpot desligado.");
		}

		public void HabilitarComponentes()
		{
			comboRedes.Enabled = true;
			txtNomeRede.Enabled = true;
			txtSenha.Enabled = true;
			button1.Text = "Iniciar";
		}

		public void DesabilitarComponentes()
		{
			comboRedes.Enabled = false;
			txtNomeRede.Enabled = false;
			txtSenha.Enabled = false;
			button1.Text = "Parar";
		}

		private bool OptsValidas(WifiShareOpts opts)
		{
			bool ok = true;

			if (string.IsNullOrEmpty(opts.SSID))
			{
				ExibirAlerta("Informe um SSID.");
				ok = false;
			}
			if (string.IsNullOrEmpty(opts.Password))
			{
				ExibirAlerta("Informe uma Senha.");
				ok = false;
			}
			else if (opts.Password.Length < 8)
			{
				ExibirAlerta("A senha deve conter pelo menos 8 caracteres.");
				ok = false;
			}

			return ok;
		}

		private void ExibirAlerta(string mensagem)
		{
			MessageBox.Show(this, mensagem, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void AtualizarStatus(string status)
		{
			lblStatus.Text = status;
		}

		private void HotspotForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (HotSpot.IsSharing)
			{
				DesabilitarSharing();
			}
		}

		private void HotspotForm_Resize(object sender, EventArgs e)
		{
			if (FormWindowState.Minimized == this.WindowState)
			{
				notifyIcon1.Visible = true;
				notifyIcon1.ShowBalloonTip(500);
				this.Hide();
			}
			else if (FormWindowState.Normal == this.WindowState)
			{
				notifyIcon1.Visible = false;
			}
		}

		private void notifyIcon1_Click(object sender, EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Task.Run(() => {
				HotSpot.SearchConnectedDevices();
			});
		}
	}
}
