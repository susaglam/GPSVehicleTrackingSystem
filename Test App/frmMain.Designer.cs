using System.Windows.Forms;

namespace ATSServer
{
    partial class frmMain
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
            this.Gunluk = new System.Windows.Forms.TextBox();
            this.KomutMetni = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PortDeger = new System.Windows.Forms.TextBox();
            this.PortBaslik = new System.Windows.Forms.Label();
            this.dgmPortDegistir = new System.Windows.Forms.Button();
            this.dgmKomutGonder = new System.Windows.Forms.Button();
            this.dgmSunucuyuKapat = new System.Windows.Forms.Button();
            this.sncDrmBaslik = new System.Windows.Forms.Label();
            this.PortDurumu = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIdleTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMaxThreads = new System.Windows.Forms.TextBox();
            this.KomutDenemeBaslik = new System.Windows.Forms.Label();
            this.KomutDenemeSayisi = new System.Windows.Forms.TextBox();
            this.BagliCihazSayisi = new System.Windows.Forms.Label();
            this.BagliCihazBaslik = new System.Windows.Forms.Label();
            this.Sunucu1 = new ytsSunucu.TcpServer(this.components);
            this.Zamanlayici = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtValidateInterval = new System.Windows.Forms.TextBox();
            this.CihazListesi = new System.Windows.Forms.ListBox();
            this.KomutZamanlayici = new System.Windows.Forms.Timer(this.components);
            this.gapi = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.aistenen = new System.Windows.Forms.Label();
            this.aislenen = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.vislenen = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.vistenen = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.adresislenen = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.adresistenen = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Gunluk
            // 
            this.Gunluk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gunluk.Location = new System.Drawing.Point(12, 25);
            this.Gunluk.Multiline = true;
            this.Gunluk.Name = "Gunluk";
            this.Gunluk.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Gunluk.Size = new System.Drawing.Size(987, 359);
            this.Gunluk.TabIndex = 1;
            // 
            // KomutMetni
            // 
            this.KomutMetni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KomutMetni.Location = new System.Drawing.Point(557, 403);
            this.KomutMetni.Multiline = true;
            this.KomutMetni.Name = "KomutMetni";
            this.KomutMetni.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.KomutMetni.Size = new System.Drawing.Size(361, 35);
            this.KomutMetni.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Günlük";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(554, 387);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Komut";
            // 
            // PortDeger
            // 
            this.PortDeger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PortDeger.Location = new System.Drawing.Point(67, 429);
            this.PortDeger.Name = "PortDeger";
            this.PortDeger.Size = new System.Drawing.Size(58, 20);
            this.PortDeger.TabIndex = 6;
            this.PortDeger.Text = "7018";
            // 
            // PortBaslik
            // 
            this.PortBaslik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PortBaslik.AutoSize = true;
            this.PortBaslik.Location = new System.Drawing.Point(13, 432);
            this.PortBaslik.Name = "PortBaslik";
            this.PortBaslik.Size = new System.Drawing.Size(26, 13);
            this.PortBaslik.TabIndex = 5;
            this.PortBaslik.Text = "Port";
            // 
            // dgmPortDegistir
            // 
            this.dgmPortDegistir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgmPortDegistir.Location = new System.Drawing.Point(442, 457);
            this.dgmPortDegistir.Name = "dgmPortDegistir";
            this.dgmPortDegistir.Size = new System.Drawing.Size(74, 23);
            this.dgmPortDegistir.TabIndex = 13;
            this.dgmPortDegistir.Text = "DEĞİŞTİR";
            this.dgmPortDegistir.UseVisualStyleBackColor = true;
            this.dgmPortDegistir.Click += new System.EventHandler(this.PortDegistir);
            // 
            // dgmKomutGonder
            // 
            this.dgmKomutGonder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dgmKomutGonder.Location = new System.Drawing.Point(924, 403);
            this.dgmKomutGonder.Name = "dgmKomutGonder";
            this.dgmKomutGonder.Size = new System.Drawing.Size(75, 91);
            this.dgmKomutGonder.TabIndex = 21;
            this.dgmKomutGonder.Text = "GÖNDER";
            this.dgmKomutGonder.UseVisualStyleBackColor = true;
            this.dgmKomutGonder.Click += new System.EventHandler(this.KomutuGonder);
            // 
            // dgmSunucuyuKapat
            // 
            this.dgmSunucuyuKapat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dgmSunucuyuKapat.Location = new System.Drawing.Point(924, 529);
            this.dgmSunucuyuKapat.Name = "dgmSunucuyuKapat";
            this.dgmSunucuyuKapat.Size = new System.Drawing.Size(75, 23);
            this.dgmSunucuyuKapat.TabIndex = 22;
            this.dgmSunucuyuKapat.Text = "ÇIKIŞ";
            this.dgmSunucuyuKapat.UseVisualStyleBackColor = true;
            this.dgmSunucuyuKapat.Click += new System.EventHandler(this.SunucuyuKapat);
            // 
            // sncDrmBaslik
            // 
            this.sncDrmBaslik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sncDrmBaslik.AutoSize = true;
            this.sncDrmBaslik.Location = new System.Drawing.Point(13, 403);
            this.sncDrmBaslik.Name = "sncDrmBaslik";
            this.sncDrmBaslik.Size = new System.Drawing.Size(87, 13);
            this.sncDrmBaslik.TabIndex = 23;
            this.sncDrmBaslik.Text = "Sunucu Durumu:";
            // 
            // PortDurumu
            // 
            this.PortDurumu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PortDurumu.AutoSize = true;
            this.PortDurumu.BackColor = System.Drawing.Color.Red;
            this.PortDurumu.Location = new System.Drawing.Point(113, 403);
            this.PortDurumu.Name = "PortDurumu";
            this.PortDurumu.Size = new System.Drawing.Size(77, 13);
            this.PortDurumu.TabIndex = 24;
            this.PortDurumu.Text = "PORT KAPALI";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(131, 432);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Idle Time";
            // 
            // txtIdleTime
            // 
            this.txtIdleTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtIdleTime.Location = new System.Drawing.Point(187, 429);
            this.txtIdleTime.Name = "txtIdleTime";
            this.txtIdleTime.Size = new System.Drawing.Size(47, 20);
            this.txtIdleTime.TabIndex = 26;
            this.txtIdleTime.Text = "50";
            this.txtIdleTime.TextChanged += new System.EventHandler(this.txtIdleTime_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 432);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Max Threads";
            // 
            // txtMaxThreads
            // 
            this.txtMaxThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMaxThreads.Location = new System.Drawing.Point(315, 429);
            this.txtMaxThreads.Name = "txtMaxThreads";
            this.txtMaxThreads.Size = new System.Drawing.Size(50, 20);
            this.txtMaxThreads.TabIndex = 28;
            this.txtMaxThreads.Text = "100";
            this.txtMaxThreads.TextChanged += new System.EventHandler(this.txtMaxThreads_TextChanged);
            // 
            // KomutDenemeBaslik
            // 
            this.KomutDenemeBaslik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KomutDenemeBaslik.AutoSize = true;
            this.KomutDenemeBaslik.Location = new System.Drawing.Point(371, 403);
            this.KomutDenemeBaslik.Name = "KomutDenemeBaslik";
            this.KomutDenemeBaslik.Size = new System.Drawing.Size(86, 13);
            this.KomutDenemeBaslik.TabIndex = 29;
            this.KomutDenemeBaslik.Text = "Komut Deneme :";
            // 
            // KomutDenemeSayisi
            // 
            this.KomutDenemeSayisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KomutDenemeSayisi.Location = new System.Drawing.Point(476, 400);
            this.KomutDenemeSayisi.Name = "KomutDenemeSayisi";
            this.KomutDenemeSayisi.Size = new System.Drawing.Size(40, 20);
            this.KomutDenemeSayisi.TabIndex = 30;
            this.KomutDenemeSayisi.Text = "3";
            this.KomutDenemeSayisi.TextChanged += new System.EventHandler(this.txtAttempts_TextChanged);
            // 
            // BagliCihazSayisi
            // 
            this.BagliCihazSayisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BagliCihazSayisi.AutoSize = true;
            this.BagliCihazSayisi.BackColor = System.Drawing.SystemColors.Control;
            this.BagliCihazSayisi.Location = new System.Drawing.Point(312, 403);
            this.BagliCihazSayisi.Name = "BagliCihazSayisi";
            this.BagliCihazSayisi.Size = new System.Drawing.Size(13, 13);
            this.BagliCihazSayisi.TabIndex = 32;
            this.BagliCihazSayisi.Text = "0";
            // 
            // BagliCihazBaslik
            // 
            this.BagliCihazBaslik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BagliCihazBaslik.AutoSize = true;
            this.BagliCihazBaslik.Location = new System.Drawing.Point(209, 403);
            this.BagliCihazBaslik.Name = "BagliCihazBaslik";
            this.BagliCihazBaslik.Size = new System.Drawing.Size(92, 13);
            this.BagliCihazBaslik.TabIndex = 31;
            this.BagliCihazBaslik.Text = "Bağlı Cihaz Sayısı:";
            // 
            // Sunucu1
            // 
            this.Sunucu1.Encoding = null;
            this.Sunucu1.IdleTime = 50;
            this.Sunucu1.IsOpen = false;
            this.Sunucu1.MaxCallbackThreads = 100;
            this.Sunucu1.MaxSendAttempts = 3;
            this.Sunucu1.Port = -1;
            this.Sunucu1.VerifyConnectionInterval = 0;
            this.Sunucu1.OnConnect += new ytsSunucu.tcpServerConnectionChanged(this.tcpServer1_OnConnect);
            this.Sunucu1.OnDataAvailable += new ytsSunucu.tcpServerConnectionChanged(this.Sunucu1VeriVarsa);
            // 
            // Zamanlayici
            // 
            this.Zamanlayici.Tick += new System.EventHandler(this.ZamanlayiciSayim);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(371, 432);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Validate Interval";
            // 
            // txtValidateInterval
            // 
            this.txtValidateInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtValidateInterval.Location = new System.Drawing.Point(476, 429);
            this.txtValidateInterval.Name = "txtValidateInterval";
            this.txtValidateInterval.Size = new System.Drawing.Size(40, 20);
            this.txtValidateInterval.TabIndex = 34;
            this.txtValidateInterval.Text = "100";
            this.txtValidateInterval.TextChanged += new System.EventHandler(this.txtValidateInterval_TextChanged);
            // 
            // CihazListesi
            // 
            this.CihazListesi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CihazListesi.FormattingEnabled = true;
            this.CihazListesi.Location = new System.Drawing.Point(561, 447);
            this.CihazListesi.Name = "CihazListesi";
            this.CihazListesi.Size = new System.Drawing.Size(357, 95);
            this.CihazListesi.TabIndex = 35;
            // 
            // KomutZamanlayici
            // 
            this.KomutZamanlayici.Interval = 1000;
            this.KomutZamanlayici.Tick += new System.EventHandler(this.komutzamanlayici_Tick);
            // 
            // gapi
            // 
            this.gapi.AutoSize = true;
            this.gapi.Location = new System.Drawing.Point(14, 462);
            this.gapi.Name = "gapi";
            this.gapi.Size = new System.Drawing.Size(86, 13);
            this.gapi.TabIndex = 36;
            this.gapi.Text = "Google Api Key :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 481);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Ayrıştırılan İstenen :";
            // 
            // aistenen
            // 
            this.aistenen.AutoSize = true;
            this.aistenen.Location = new System.Drawing.Point(155, 481);
            this.aistenen.Name = "aistenen";
            this.aistenen.Size = new System.Drawing.Size(13, 13);
            this.aistenen.TabIndex = 38;
            this.aistenen.Text = "0";
            // 
            // aislenen
            // 
            this.aislenen.AutoSize = true;
            this.aislenen.Location = new System.Drawing.Point(295, 481);
            this.aislenen.Name = "aislenen";
            this.aislenen.Size = new System.Drawing.Size(13, 13);
            this.aislenen.TabIndex = 40;
            this.aislenen.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(229, 481);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "İşlenen  :";
            // 
            // vislenen
            // 
            this.vislenen.AutoSize = true;
            this.vislenen.Location = new System.Drawing.Point(293, 529);
            this.vislenen.Name = "vislenen";
            this.vislenen.Size = new System.Drawing.Size(13, 13);
            this.vislenen.TabIndex = 44;
            this.vislenen.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(227, 529);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "İşlenen  :";
            // 
            // vistenen
            // 
            this.vistenen.AutoSize = true;
            this.vistenen.Location = new System.Drawing.Point(153, 529);
            this.vistenen.Name = "vistenen";
            this.vistenen.Size = new System.Drawing.Size(13, 13);
            this.vistenen.TabIndex = 42;
            this.vistenen.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 529);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Veri Tabanına Gönderilen :";
            // 
            // adresislenen
            // 
            this.adresislenen.AutoSize = true;
            this.adresislenen.Location = new System.Drawing.Point(293, 504);
            this.adresislenen.Name = "adresislenen";
            this.adresislenen.Size = new System.Drawing.Size(13, 13);
            this.adresislenen.TabIndex = 48;
            this.adresislenen.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(227, 504);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "İşlenen  :";
            // 
            // adresistenen
            // 
            this.adresistenen.AutoSize = true;
            this.adresistenen.Location = new System.Drawing.Point(153, 504);
            this.adresistenen.Name = "adresistenen";
            this.adresistenen.Size = new System.Drawing.Size(13, 13);
            this.adresistenen.TabIndex = 46;
            this.adresistenen.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(50, 504);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 13);
            this.label13.TabIndex = 45;
            this.label13.Text = "Adres İstenen :";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(442, 494);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 49;
            this.button1.Text = "TEMİZLE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 564);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.adresislenen);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.adresistenen);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.vislenen);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.vistenen);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.aislenen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.aistenen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gapi);
            this.Controls.Add(this.CihazListesi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtValidateInterval);
            this.Controls.Add(this.BagliCihazSayisi);
            this.Controls.Add(this.BagliCihazBaslik);
            this.Controls.Add(this.KomutDenemeBaslik);
            this.Controls.Add(this.KomutDenemeSayisi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtMaxThreads);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIdleTime);
            this.Controls.Add(this.PortDurumu);
            this.Controls.Add(this.sncDrmBaslik);
            this.Controls.Add(this.dgmSunucuyuKapat);
            this.Controls.Add(this.dgmKomutGonder);
            this.Controls.Add(this.dgmPortDegistir);
            this.Controls.Add(this.PortBaslik);
            this.Controls.Add(this.PortDeger);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KomutMetni);
            this.Controls.Add(this.Gunluk);
            this.MinimumSize = new System.Drawing.Size(696, 461);
            this.Name = "frmMain";
            this.Text = "ATS Sunucu 1.2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private TextBox Gunluk;
        private TextBox KomutMetni;
        private Label label1;
        private Label label2;
        private ytsSunucu.TcpServer Sunucu1;
        private TextBox PortDeger;
        private Label PortBaslik;
        private Button dgmPortDegistir;
        private Button dgmKomutGonder;
        private Button dgmSunucuyuKapat;
        private Label sncDrmBaslik;
        private Label PortDurumu;
        private Label label5;
        private TextBox txtIdleTime;
        private Label label6;
        private TextBox txtMaxThreads;
        private Label KomutDenemeBaslik;
        private TextBox KomutDenemeSayisi;
        private Label BagliCihazSayisi;
        private Label BagliCihazBaslik;
        private Timer Zamanlayici;
        private Label label8;
        private TextBox txtValidateInterval;
        private ListBox CihazListesi;
        private Timer KomutZamanlayici;
        private Label gapi;
        private Label label3;
        private Label aistenen;
        private Label aislenen;
        private Label label7;
        private Label vislenen;
        private Label label9;
        private Label vistenen;
        private Label label11;
        private Label adresislenen;
        private Label label10;
        private Label adresistenen;
        private Label label13;
        private Button button1;
    }
}

