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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button2 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 67);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Rede sem fio";
			// 
			// txtNomeRede
			// 
			this.txtNomeRede.Location = new System.Drawing.Point(118, 64);
			this.txtNomeRede.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtNomeRede.Name = "txtNomeRede";
			this.txtNomeRede.Size = new System.Drawing.Size(234, 22);
			this.txtNomeRede.TabIndex = 1;
			// 
			// txtSenha
			// 
			this.txtSenha.Location = new System.Drawing.Point(118, 94);
			this.txtSenha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtSenha.Name = "txtSenha";
			this.txtSenha.Size = new System.Drawing.Size(234, 22);
			this.txtSenha.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 97);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "Senha";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 32);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "Rede local";
			// 
			// comboRedes
			// 
			this.comboRedes.FormattingEnabled = true;
			this.comboRedes.Location = new System.Drawing.Point(118, 32);
			this.comboRedes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.comboRedes.Name = "comboRedes";
			this.comboRedes.Size = new System.Drawing.Size(234, 24);
			this.comboRedes.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(223, 126);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(129, 31);
			this.button1.TabIndex = 6;
			this.button1.Text = "Iniciar";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 447);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(395, 25);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(97, 20);
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
			this.listView1.Location = new System.Drawing.Point(12, 28);
			this.listView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(340, 166);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.listView1.TabIndex = 8;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtNomeRede);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtSenha);
			this.groupBox1.Controls.Add(this.comboRedes);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Location = new System.Drawing.Point(13, 47);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox1.Size = new System.Drawing.Size(371, 172);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Hotspot";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.listView1);
			this.groupBox2.Location = new System.Drawing.Point(13, 227);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox2.Size = new System.Drawing.Size(371, 207);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Dispositivos Conectados";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::MiWifiRouter.Properties.Resources.miwifirouter;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(13, 6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(36, 38);
			this.pictureBox1.TabIndex = 16;
			this.pictureBox1.TabStop = false;
			// 
			// button2
			// 
			this.button2.AutoSize = true;
			this.button2.BackgroundImage = global::MiWifiRouter.Properties.Resources.donate_button;
			this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button2.CausesValidation = false;
			this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button2.FlatAppearance.BorderSize = 0;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(291, 12);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(93, 32);
			this.button2.TabIndex = 15;
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(53, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(150, 29);
			this.label4.TabIndex = 17;
			this.label4.Text = "Mi wifi router";
			// 
			// HotspotForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 472);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.Name = "HotspotForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HotspotForm_FormClosing);
			this.Resize += new System.EventHandler(this.HotspotForm_Resize);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label4;
	}
}

