namespace MiWifiRouter
{
	partial class HotspotForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotspotForm));
			this.label1 = new System.Windows.Forms.Label();
			this.txtNomeRede = new System.Windows.Forms.TextBox();
			this.txtSenha = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboRedes = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.listView1 = new System.Windows.Forms.ListView();
			this.label4 = new System.Windows.Forms.Label();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 63);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Nome da Rede";
			// 
			// txtNomeRede
			// 
			this.txtNomeRede.Location = new System.Drawing.Point(15, 80);
			this.txtNomeRede.Name = "txtNomeRede";
			this.txtNomeRede.Size = new System.Drawing.Size(262, 20);
			this.txtNomeRede.TabIndex = 1;
			// 
			// txtSenha
			// 
			this.txtSenha.Location = new System.Drawing.Point(15, 131);
			this.txtSenha.Name = "txtSenha";
			this.txtSenha.Size = new System.Drawing.Size(262, 20);
			this.txtSenha.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 114);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Senha";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(135, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Rede de Compartilhamento";
			// 
			// comboRedes
			// 
			this.comboRedes.FormattingEnabled = true;
			this.comboRedes.Location = new System.Drawing.Point(15, 27);
			this.comboRedes.Name = "comboRedes";
			this.comboRedes.Size = new System.Drawing.Size(262, 21);
			this.comboRedes.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(64, 167);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(166, 33);
			this.button1.TabIndex = 6;
			this.button1.Text = "Iniciar";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 384);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(293, 22);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(77, 17);
			this.lblStatus.Text = "MiWifiRouter";
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon1.BalloonTipText = "Compartilhamento de internet via Wifi no Windows 10";
			this.notifyIcon1.BalloonTipTitle = "MiWifiRouter 1.0";
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "MiWifiRouter";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
			// 
			// listView1
			// 
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(15, 241);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(262, 128);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.listView1.TabIndex = 8;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 225);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(122, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Dispositivos conectados";
			// 
			// HotspotForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(293, 406);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboRedes);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtSenha);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtNomeRede);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(309, 445);
			this.MinimumSize = new System.Drawing.Size(309, 445);
			this.Name = "HotspotForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MiWifiRouter";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HotspotForm_FormClosing);
			this.Resize += new System.EventHandler(this.HotspotForm_Resize);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtNomeRede;
		private System.Windows.Forms.TextBox txtSenha;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboRedes;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label label4;
	}
}

