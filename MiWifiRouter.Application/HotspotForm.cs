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
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;

namespace MiWifiRouter
{
	public partial class HotspotForm : Form
	{
		private Hotspot Hotspot;
		public bool _closing;

		public HotspotForm()
		{
			InitializeComponent();

			Text = string.Format("MiWifi Router {0}", Program.GetVersion());

			Hotspot = new Hotspot();
			Hotspot.OnDeviceFound += Hotspot_DeviceFound;

			CarregarRedesDisponiveis();

			txtNomeRede.Text = ConfigurationManager.AppSettings["ssid"] ?? string.Empty;
			txtSenha.Text = ConfigurationManager.AppSettings["password"] ?? string.Empty;

			InicializarListViewDispositivos();
		}

		private void InicializarListViewDispositivos()
		{
			listView1.View = View.Tile;
			listView1.TileSize = new Size(250, 60);
			listView1.Columns.AddRange(new ColumnHeader[] { new ColumnHeader(), new ColumnHeader(), new ColumnHeader() });

			var imgList = new ImageList();
			imgList.ImageSize = new Size(36, 36);

			Bitmap bmpAndroid = new Bitmap(Image.FromFile(Application.StartupPath + "\\android_device.png")); // ImageIndex = 0
			Bitmap bmpComputer = new Bitmap(Image.FromFile(Application.StartupPath + "\\computer_device.png")); // ImageIndex = 1

			ImageHelper helper = new ImageHelper();
			helper.AddImageToImageList(imgList, bmpAndroid, "android_device");
			helper.AddImageToImageList(imgList, bmpComputer, "computer_device");

			listView1.LargeImageList = imgList;
		}

		private void Hotspot_DeviceFound(Device device)
		{
			if (!_closing)
			{
				Invoke(new Action(() =>
				{
					bool hasItem = listView1.Items.Cast<ListViewItem>().Any(item => item.SubItems[0].Text == device.Hostname);

					if (!hasItem)
					{
						var listViewItem = new ListViewItem(new string[] { device.Hostname, device.IpAddress, device.MacAddress });
						string hostname = device.Hostname.ToLower();

						listViewItem.ImageIndex = hostname.Contains("android") ? 0 : 1;

						listView1.Items.Add(listViewItem);
					}
				}));
			}
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

			if (Hotspot.Started)
			{
				PararHotSpot();
			}
			else
			{
				Hotspot.Name = txtNomeRede.Text;
				Hotspot.Password = txtSenha.Text;
				Hotspot.LocalNetwork = (NetworkInterface)comboRedes.SelectedItem;

				if (DadosValidosParaIniciarHotspot())
				{
					AtualizarStatus("Iniciando Hotspot.");

					MiWifiConfiguration.SSID = Hotspot.Name;
					MiWifiConfiguration.Password = Hotspot.Password;

					try
					{
						Hotspot.Start();
						DesabilitarComponentes();
						AtualizarStatus("Hotspot iniciado.");
					}
					catch (Exception ex)
					{
						AtualizarStatus("Erro ao iniciar o Hotspot.");
						MessageBox.Show(this, ex.Message, "Ops... Ocorreu um erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			button1.Enabled = true;
		}

		private void DesabilitarSharing()
		{
			AtualizarStatus("Desligando HotSpot.");

			Hotspot.Stop();

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

		private bool DadosValidosParaIniciarHotspot()
		{
			bool ok = true;

			if (string.IsNullOrEmpty(Hotspot.Name))
			{
				ExibirAlerta("Informe um nome para rede sem fio.");
				ok = false;
			}
			else if (Hotspot.Name.Split(' ').Length > 1)
			{
				ExibirAlerta("O Nome da rede sem fio não pode conter espaços.");
				ok = false;
			}
			if (string.IsNullOrEmpty(Hotspot.Password))
			{
				ExibirAlerta("Informe uma senha.");
				ok = false;
			}
			else if (Hotspot.Password.Length < 8)
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
			_closing = true;

			if (Hotspot.Started)
			{
				DesabilitarSharing();
			}
		}

		private void HotspotForm_Resize(object sender, EventArgs e)
		{
			if (FormWindowState.Minimized == this.WindowState)
			{
				ExibirIconeNotificacao();
			}
		}

		private void notifyIcon1_Click(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ExibirForm();
			}
			else if (e.Button == MouseButtons.Right)
			{
				contextMenuStripIcon.Show();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=7WQEYYCTM6854");
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			PararHotSpot();

			toolStripMenuItem1.Enabled = false;
		}

		private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExibirForm();
		}

		private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new AboutForm().ShowDialog(this);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			listView1.Items.Clear();

			if (Hotspot.Started)
			{
				Hotspot.SearchConnectedDevicesAsync();
			}
			else
			{
				MessageBox.Show(this, "O Hotspot não foi iniciado!", "Ops...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void ExibirIconeNotificacao()
		{
			this.Hide();

			notifyIcon1.Visible = true;
			notifyIcon1.ShowBalloonTip(500);

			toolStripMenuItem1.Enabled = Hotspot.Started;
		}

		private void ExibirForm()
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
			notifyIcon1.Visible = false;
		}

		private void PararHotSpot()
		{
			DesabilitarSharing();

			listView1.Items.Clear();
		}
	}
}
