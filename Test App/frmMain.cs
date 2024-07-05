using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using YTSDAL;
using ytsSunucu;

namespace ATSServer
{
    public partial class frmMain : Form
    {
        public delegate void invokeDelegate();

        public frmMain()
        {
            InitializeComponent();
        }

        private void PortDegistir(object sender, EventArgs e)
        {
            try
            {
                PortuAc();
            }
            catch (FormatException)
            {
                MessageBox.Show("Port değeri sadece rakam olabilir", "Geçersiz Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Port değeri çok büyük", "Geçersiz Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            CihazListesi.Items.Clear();
            KomutZamanlayici.Enabled = false;
        }

        private void PortuAc()
        {
            Sunucu1.Close();
            Sunucu1.Port = Convert.ToInt32(PortDeger.Text);
            PortDeger.Text = Sunucu1.Port.ToString();
            Sunucu1.Open();
            SunucuDurumunuGoster();
        }

        private void SunucuDurumunuGoster()
        {
            if (Sunucu1.IsOpen)
            {
                PortDurumu.Text = "PORT AÇIK";
                PortDurumu.BackColor = Color.Lime;
            }
            else
            {
                PortDurumu.Text = "PORT KAPALI";
                PortDurumu.BackColor = Color.Red;
            }
        }

        private void KomutuGonder(object sender, EventArgs e)
        {
            KomutGonder();
        }

        private void SunucuyuKapat(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Sunucu1.Close();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            PortDegistir(null, null);
            Zamanlayici.Enabled = true;
            gapi.Text = "Google Api Key :" + ConfigurationManager.AppSettings.Get("google-apikey");
        }

        private void ZamanlayiciSayim(object sender, EventArgs e)
        {
            SunucuDurumunuGoster();
            BagliCihazSayisi.Text = Sunucu1.Connections.Count.ToString();
            if (Sunucu1.Connections.Count > 0)
            {
                KomutZamanlayici.Enabled = true;
            }
        }

        private void txtIdleTime_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int time = Convert.ToInt32(txtIdleTime.Text);
                Sunucu1.IdleTime = time;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void txtMaxThreads_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int threads = Convert.ToInt32(txtMaxThreads.Text);
                Sunucu1.MaxCallbackThreads = threads;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void txtAttempts_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int attempts = Convert.ToInt32(KomutDenemeSayisi.Text);
                Sunucu1.MaxSendAttempts = attempts;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Zamanlayici.Enabled = false;
            KomutZamanlayici.Enabled = false;
        }

        private void txtValidateInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int interval = Convert.ToInt32(txtValidateInterval.Text);
                Sunucu1.VerifyConnectionInterval = interval;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void KomutGonder()
        {
            string data = "";

            foreach (string line in KomutMetni.Lines)
            {
                data = data + line.Replace("\r", "").Replace("\n", "") + "\r\n";
            }
            data = data.Substring(0, data.Length - 2);

            Sunucu1.Send(data, CihazListesi.SelectedItem.ToString());

            logData(true, data, "");
        }

        private void Sunucu1VeriVarsa(TcpServerConnection connection)
        {
            byte[] data = readStream(connection.Socket);
            if (data != null)
            {
                string dataStr = Encoding.ASCII.GetString(data);
                string ip = ((IPEndPoint)connection.Socket.Client.RemoteEndPoint).ToString();
                invokeDelegate del = () =>
                {
                    try
                    {
                        if (!CihazListesi.Items.Contains(ip))
                        { CihazListesi.Items.Add(ip); }
                        if (dataStr != null)
                        {
                            aistenen.Text = aa++.ToString();

                            logData(false, dataStr, ip);
                            aislenen.Text = ai++.ToString();
                        }

                    }
                    catch (Exception e)
                    {
                        StreamWriter file = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "log.txt", true);
                        file.WriteLine(e);
                        file.Close();
                    }

                };
                Invoke(del);
                data = null;
            }
        }

        public void komutuygula(string gelendata)
        {
            string komut = null;
            int term;
            gelendata = gelendata.TrimStart('@').TrimStart('L').ToString().TrimEnd('!').ToString();
            string imei = gelendata.Substring(3, 17);
            int terminalId = terminalIDbul(imei);
            AracTakipDb db = new AracTakipDb();
            SqlConnection sqlBaglantim = new SqlConnection(db.Database.Connection.ConnectionString);
            string query = string.Format("select TOP(1) * from tbl_komut where sms IS NULL or sms = 0  and TerminalId = {0} order by KomutID desc", terminalId);
            SqlCommand sqlSorgu = new SqlCommand(query, sqlBaglantim);
            try
            {
                if (sqlBaglantim != null)
                {
                    sqlBaglantim.Close();
                }
                sqlBaglantim.Open();
                SqlDataReader sqlOku = sqlSorgu.ExecuteReader();
                while (sqlOku.Read())
                {
                    komut = sqlOku["Komut"].ToString();
                    term = Convert.ToInt16(sqlOku["TerminalId"]);

                    if (komut != null)
                    {

                        if (!cihazArmoli(term))
                        { 
                            DbHelper i = new DbHelper();
                            if (term == i.GetTerminalId(imei))
                            {
                                Sunucu1.Send(komut, i.GetTerminalip(term));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlBaglantim.Close();
            }
        }


        public void komutuygula()
        {
            string komut = null;
            int term;

            AracTakipDb db = new AracTakipDb();
            SqlConnection sqlBaglantim = new SqlConnection(db.Database.Connection.ConnectionString);
            string query = string.Format("select TOP(1) * from tbl_komut where sms IS NULL or sms = 0  order by KomutID desc");
            SqlCommand sqlSorgu = new SqlCommand(query, sqlBaglantim);
            try
            {
                if (sqlBaglantim != null)
                {
                    sqlBaglantim.Close();
                }
                sqlBaglantim.Open();
                SqlDataReader sqlOku = sqlSorgu.ExecuteReader();
                while (sqlOku.Read())
                {
                    komut = sqlOku["Komut"].ToString();
                    term = Convert.ToInt16(sqlOku["TerminalId"]);

                    if (komut != null)
                    {
                        DbHelper i = new DbHelper();
                        Sunucu1.Send(komut, i.GetTerminalip(term));
                        logData(true, komut, i.GetTerminalip(term));

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlBaglantim.Close();
            }
        }

        protected byte[] readStream(TcpClient client)
        {
            byte[] numArray;
            NetworkStream stream = client.GetStream();
            if (!stream.DataAvailable)
            {
                numArray = null;
            }
            else
            {
                byte[] data = new byte[client.Available];
                int bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(data, 0, (int)data.Length);
                }
                catch (IOException oException)
                {
                    throw (oException);
                }
                if (bytesRead < (int)data.Length)
                {
                    byte[] lastData = data;
                    data = new byte[bytesRead];
                    Array.ConstrainedCopy(lastData, 0, data, 0, bytesRead);
                }
                numArray = data;
            }
            return numArray;
        }
        public int aa;
        public int ai;
        public int va;
        public int vi;

        public int adra;
        public int adri;
        private void tcpServer1_OnConnect(TcpServerConnection connection)
        {
            base.Invoke(new frmMain.invokeDelegate(() => BagliCihazSayisi.Text = Sunucu1.Connections.Count.ToString()));
            //invokeDelegate setText = () => lblConnected.Text = tcpServer1.Connections.Count.ToString();
            //Invoke(setText);
        }

        public void logData(bool sent, string text, string ip)
        {
            string[] data = text.Split('!');
            if (this.Gunluk.InvokeRequired)
            {
                Gunluk.Invoke(new MethodInvoker(delegate
                {
                    foreach (var kay in data)
                    {
                        if (kay.Length > 0)
                        {
                            string ss = kay + "!";
                            Gunluk.Text += (sent ? " GİDEN :" : " GELEN :") + ss + Environment.NewLine;
                            base.Invoke(new frmMain.invokeDelegate(() => ParseEt(ss.TrimEnd(), ip)));
                            base.Invoke(new frmMain.invokeDelegate(() => DataSave(ss.TrimEnd())));
                            // base.Invoke(new frmMain.invokeDelegate(() => komutuygula(ss)));
                        }

                        if (Gunluk.Lines.Length > 500)
                        {
                            string[] temp = new string[500];
                            Array.Copy(Gunluk.Lines, Gunluk.Lines.Length - 500, temp, 0, 500);
                            Gunluk.Lines = temp;
                        }
                        Gunluk.SelectionStart = Gunluk.Text.Length;
                        Gunluk.ScrollToCaret();
                    }
                }));
            }
            else
            {
                foreach (var kay in data)
                {
                    if (kay.Length > 0)
                    {
                        string ss = kay + "!";
                        Gunluk.Text += (sent ? " GİDEN_ :" : " GELEN_ :") + ss + Environment.NewLine;
                        ParseEt(ss.TrimEnd(), ip);
                        DataSave(ss.TrimEnd());
                        // komutuygula(ss);
                    }

                    if (Gunluk.Lines.Length > 500)
                    {
                        string[] temp = new string[500];
                        Array.Copy(Gunluk.Lines, Gunluk.Lines.Length - 500, temp, 0, 500);
                        Gunluk.Lines = temp;
                    }
                    Gunluk.SelectionStart = Gunluk.Text.Length;
                    Gunluk.ScrollToCaret();
                }
            }

            base.Invoke(new frmMain.invokeDelegate(() =>
        { }));
        }


        private string VeriOkuKontrol(byte[] data)
        {

            ASCIIEncoding aSciiEncoding;
            string str;
            int i;
            bool flag = false;
            int VeriUzunluk = data.Length;
            int islem = 0;
            StringBuilder gpsdata = new StringBuilder();
            byte[] byte1 = new byte[200000];
            int Bekle = 0;
            gpsdata.Clear();
            byte1 = data;

            var dizi = new byte[6];

            if (VeriUzunluk <= islem)
            {
                VeriUzunluk = 0;
                islem = 0;
            }
            else
            {
                while (true)
                {
                    if (islem != VeriUzunluk)
                    {
                        dizi[0] = byte1[islem];
                        dizi[1] = 0;
                        if (dizi[0] == 64)
                        {
                            if (byte1[islem + 1] == 76)
                            {
                                flag = true;
                                break;
                            }
                            else
                            {
                                gpsdata.Append("@");
                            }
                        }
                        else if (dizi[0] != 33)
                        {
                            aSciiEncoding = new ASCIIEncoding();
                            str = aSciiEncoding.GetString(dizi);
                            gpsdata.Append(str);
                        }
                        else
                        {
                            gpsdata.Append("!");
                            gpsdata.Replace("\0", "");
                            return gpsdata.ToString();
                        }

                        islem = islem + 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (!flag)
                {
                    Bekle = 0;
                }
                else
                {
                    if (VeriUzunluk < islem + 4)
                    {
                        Bekle = Bekle + 1;
                    }
                    else
                    {
                        try
                        {
                            dizi[0] = byte1[islem + 2];
                            dizi[1] = byte1[islem + 3];
                            dizi[2] = byte1[islem + 4];
                            dizi[3] = 0;
                            aSciiEncoding = new ASCIIEncoding();
                            int ınt32 = int.Parse(aSciiEncoding.GetString(dizi), NumberStyles.HexNumber);
                            if ((ınt32 & 2048) <= 0)
                            {
                                gpsdata.Append("@");
                                islem = islem + 1;
                            }
                            else
                            {
                                ınt32 = ınt32 & 2047;
                                if (VeriUzunluk < islem + 21 + ınt32)
                                {
                                    Bekle = Bekle + 1;
                                }
                                else
                                {
                                    int ınt321 = ınt32 * 2;
                                    str = string.Concat("@L", ınt321.ToString("X3")).Replace("\00", "").Replace("\0", "");
                                    gpsdata.Append(str);
                                    islem = islem + 5;
                                    for (i = 0; i != 17; i++)
                                    {
                                        dizi[0] = byte1[islem + i];
                                        dizi[1] = 0;
                                        dizi[2] = 0;
                                        str = aSciiEncoding.GetString(dizi).Replace("\00", "").Replace("\0", "");
                                        gpsdata.Append(str);
                                    }
                                    islem = islem + 17;
                                    for (i = 0; i != ınt32; i++)
                                    {
                                        str = byte1[islem + i].ToString("X2").Replace("\00", "").Replace("\0", "");
                                        gpsdata.Append(str);
                                    }
                                    islem = islem + ınt32;
                                    Bekle = 0;
                                }
                            }
                        }
                        catch
                        {
                            gpsdata.Append("@");
                            islem = islem + 1;
                        }
                    }
                    if (Bekle >= 200)
                    {
                        gpsdata.Append("@");
                        islem = islem + 1;
                    }
                }
            }

            // burada komut göndermeyi düşün            
            gpsdata.Append("!");
            return gpsdata.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gelendata"></param>
        /// <param name="ip"></param>
        public void ParseEt(string gelendata, string ip)
        {
            if (gelendata.StartsWith("@L")) //konum bilgisi
                PeriyodikMesaj(gelendata, ip);
            else if (gelendata.StartsWith("@S"))// sefer bilgisi
                SeferBilgisi(gelendata);
            else if (gelendata.StartsWith("@C"))// register bilgisi
                RegisterBilgisi(gelendata, ip);
            else if (gelendata.StartsWith("@ID"))//motor blokaj dönüşü @ID;86107402543253875;99;1234!
            {
                string[] mesaj = gelendata.Split(';');
                MotorBlokajDurumuGuncelle(mesaj[1], int.Parse(mesaj[2]));
            }
        }



        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        public void DataSave(string data)
        {
            // Terminal Archive tablosuna kayıt işlemleri
            string text = data.TrimStart('(').TrimEnd(')');
            //cihazid onune 3522 eklemesini sildim
            string cihazId = text.Substring(startIndex: 1, length: 12);
            string komut = text.Substring(startIndex: 12, length: 4);
            if (komut != "BR00")
            {
                return;
            }
            string date = text.Substring(startIndex: 16, length: 6);
            string isInternetAvailable = text.Substring(startIndex: 22, length: 1);
            string latitude = text.Substring(startIndex: 23, length: 9);
            string latSymbol = text.Substring(startIndex: 32, length: 1);
            string longlitude = text.Substring(startIndex: 33, length: 10);
            string longlitudeSymbol = text.Substring(startIndex: 43, length: 1);
            string speed = text.Substring(startIndex: 44, length: 5);
            string time = text.Substring(startIndex: 49, length: 6);
            string orientation = text.Substring(startIndex: 55, length: 6);
            string ioState = text.Substring(startIndex: 61, length: 8);
            string milepost = text.Substring(startIndex: 69, length: 1);
            string milage = text.Substring(startIndex: 70, length: 8);

            int year = int.Parse("20" + date.Substring(startIndex: 0, length: 2));
            int month = int.Parse(date.Substring(startIndex: 2, length: 2));
            int day = int.Parse(date.Substring(startIndex: 4));
            int hh = int.Parse(time.Substring(startIndex: 0, length: 2));
            int mm = int.Parse(time.Substring(startIndex: 2, length: 2));
            int ss = int.Parse(time.Substring(startIndex: 4));

            var dt = new DateTime(year, month, day, hh, mm, ss);
            //3819.5026
            string degree = latitude.Substring(startIndex: 0, length: 2);
            string min = latitude.Substring(startIndex: 2, length: 2);
            string sec1 = latitude.Substring(startIndex: 4).TrimStart(trimChars: new char[] { '.' });
            decimal lat = decimal.Parse(degree) + decimal.Parse(string.Format("{0},{1}", min, sec1)) / 60;

            degree = longlitude.Substring(startIndex: 0, length: 3);
            min = longlitude.Substring(startIndex: 3, length: 2);
            sec1 = longlitude.Substring(startIndex: 5).TrimStart('.');
            decimal lon = decimal.Parse(degree) + decimal.Parse(string.Format("{0},{1}", min, sec1)) / 60;

            var b = new TBL_ARCHIVE();
            b.Altitude = 0;
            b.CoordinateX = lon;
            b.CoordinateY = lat;
            b.DateTimeGps = dt.AddHours(2);
            b.DateTimeLocal = DateTime.Now;
            b.Direction = 0;
            b.Distance = decimal.Parse(milage);
            b.Ignition = byte.Parse(s: "1");
            b.MsgType = "";
            b.NumberOfSatellite = byte.Parse(s: "0");
            b.Parameter1 = "";
            b.Parameter2 = "";
            b.Speed = decimal.Parse(speed.ToString());
            b.TransCode = 0;
            b.UnitID = cihazId;

            adresistenen.Text = adra++.ToString();
            var arm = new armoli();
            var adrrr = arm.ArmoliGeoCodeReverse(lat.ToString().Replace(",", "."), lon.ToString().Replace(",", "."));
            adresislenen.Text = adri++.ToString();

            b.Street = adrrr.ToString();
            b.Status = " ";
            b.Quarter = " ";
            b.District = " ";
            b.City = " ";
            b.Country = " ";
            b.RolantiSuresi = 0;
            b.ipNumber = " ";

            var d = new TBL_ARCHIVEDAL();
            d.obj = b;
            d.Save();




        }

        /// <summary>
        /// Alarm durumu kaydet
        /// </summary>
        /// <param name="alarm"></param>
        /// <param name="archiveId"></param>
        /// <param name="terminalId"></param>
        /// <param name="dt"></param>
        /// <param name="kartno"></param>
        public void AlarmKaydet(int alarm, int archiveId, int terminalId, DateTime dt, string kartno)
        {
            var b = new TBL_ALARM();
            b.ArchiveID = archiveId;
            b.TerminalID = terminalId;
            b.ContactType = 0;
            b.AlarmType = alarm;
            b.ContactNumber = "";
            b.SpecCode = "";
            b.TelemetryValue = "";
            b.AlarmDate = dt;
            b.LocalAlarmDate = DateTime.Now;
            b.AlarmMore = "";
            var d = new TBL_ALARMDAL();
            d.obj = b;
            d.Save();

            if (kartno != "")
            {
                var kart = new TBL_READ_IDENTITYDAL();
                var db = new AracTakipDb();
                var con = new SqlConnection(db.Database.Connection.ConnectionString);
                int yon = 1;
                int rfidKart = kart.GetRfidCardId(kartno);
                try
                {
                    con.Open();
                    string query = "select top 1 isnull(Yonu,0) Yonu from TBL_READ_IDENTITY where [RfidCardId] = " + rfidKart + " order by ReadDate desc";
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        yon = int.Parse(cmd.ExecuteScalar().ToString());
                    }
                    catch (Exception)
                    {
                        yon = 0;
                    }

                    if (yon == 1)
                        yon = 0;
                    else yon = 1;

                    kart.obj = new TBL_READ_IDENTITY();
                    kart.obj.RfidCardId = rfidKart;
                    kart.obj.ReadDate = b.AlarmDate;
                    kart.obj.TerminalId = terminalId;
                    kart.obj.Yonu = byte.Parse(yon.ToString());
                    kart.obj.ArchiveId = b.ArchiveID;
                    kart.Save();
                }
                catch (Exception ex)
                {
                    string hata = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        /// <summary>
        /// Cihazdan gelen '@L' mesajlarıdır
        /// </summary>
        /// <param name="gelendata">Cihazdan gelen veri</param>
        /// <param name="ip">Cihazın ip numarası</param>
        public void PeriyodikMesaj(string gelendata, string ip)
        {
            vistenen.Text = va++.ToString();
            //@L 040863071010245816950410160907130382027510270863600010800D00000000CA1670000E080F0097 !
            gelendata = gelendata.TrimStart('@').TrimStart('L').ToString().TrimEnd('!').ToString();
            //       imei           tarih  saat     enlem    boylam     durum   hız  mesafe yön alarm alarm ek ve ekbilgi sensör vesaire -->
            //040 86307101024581695 041016 090713 0 38202751 027086360 0010800D 0000 0000CA 167 000   0E080F0097
            string imei = gelendata.Substring(3, 17);
            string mesajNo = "@L" + gelendata.Substring(0, 3);
            byte[] statuss = new byte[32];

            string date = gelendata.Substring(20, 6);
            int dd = int.Parse(date.Substring(0, 2));
            int mo = int.Parse(date.Substring(2, 2));
            int yy = int.Parse(DateTime.Now.Year.ToString().Substring(0, 2) + date.Substring(4, 2));

            string saat = gelendata.Substring(26, 6);
            int hh = int.Parse(saat.Substring(0, 2));
            int mm = int.Parse(saat.Substring(2, 2));
            int sec = int.Parse(saat.Substring(4, 2));
            DateTime dt = new DateTime(yy, mo, dd, hh, mm, sec);
            string gpsdurum = gelendata.Substring(32, 1);

            string degree = gelendata.Substring(33, 8).Substring(0, 2);
            string min = gelendata.Substring(33, 8).Substring(2, 2);
            string sec1 = gelendata.Substring(33, 8).Substring(4);
            decimal la = decimal.Parse(degree) + decimal.Parse(string.Format("{0},{1}", min, sec1)) / 60;
            string lat = la.ToString();

            degree = gelendata.Substring(41, 9).Substring(0, 3);
            min = gelendata.Substring(41, 9).Substring(3, 2);
            sec1 = gelendata.Substring(41, 9).Substring(5);
            decimal lo = decimal.Parse(degree) + decimal.Parse(string.Format("{0},{1}", min, sec1)) / 60;

            string lon = lo.ToString();
            if (lon.Length > 9)
            {
                lon = lon.Substring(0, 10);
            }



            string status = "";
            if (gelendata.Substring(50, 8) != null)
            {
                status = Convert.ToString(Convert.ToInt32(gelendata.Substring(50, 8), 16), 2);//hex to binary convert yapıldı
            }
            char[] arr = status.ToCharArray();
            int j = 0;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                statuss[j] = byte.Parse(arr[i].ToString());
                j++;
            }
            string ignition = statuss[3].ToString();

            if (ignition == "1")
                ignition = "0";
            else
                ignition = "1";

            decimal hiz = (decimal.Parse("1,852") * decimal.Parse(System.Convert.ToInt32(gelendata.Substring(58, 4), 16).ToString())) / 100; //hex to decimal to kmh
            decimal mesafe = (decimal.Parse(System.Convert.ToInt32(gelendata.Substring(62, 6), 16).ToString())); //hex to decimal
            mesafe = (mesafe / 16) * decimal.Parse("1,852");
            decimal yon = decimal.Parse(System.Convert.ToInt32(gelendata.Substring(68, 3), 16).ToString());
            decimal alarm = decimal.Parse(System.Convert.ToInt32(gelendata.Substring(71, 3), 16).ToString());
            string depokapak = "0";
            if (alarm == 48)
            {
                string a = gelendata.Substring(77, 1);
                depokapak = a;
            }
            string kartno = "";
            decimal rolantisuresi = 0;
            if (gelendata.Length > 74 && gelendata.Substring(74, 2) == "05")
                rolantisuresi = decimal.Parse(System.Convert.ToInt32(gelendata.Substring(76, 6), 16).ToString());
            decimal calisma = 0;
            // string g;= "gelmedi";
            if (gelendata.Length > 74 && gelendata.Substring(74, 2) == "08")
            {
                calisma = decimal.Parse(System.Convert.ToInt32(gelendata.Substring(80, 8), 16).ToString());
                // g = "geldi";
            }
            if (gelendata.Length > 71)
            {
                if (gelendata.Substring(71, 3) == "041" || gelendata.Substring(71, 3) == "042" || gelendata.Substring(71, 3) == "002")
                {
                    kartno = gelendata.Substring(74, 10);
                }
            }

            decimal yukseklik = 0;
            if (gelendata.Length > 74)
            {
                try
                {
                    yukseklik = decimal.Parse(System.Convert.ToInt32(gelendata.Substring(74, 2), 16).ToString());
                }
                catch (Exception)
                {
                    yukseklik = 0;
                }
            }

            TBL_ARCHIVE b = new TBL_ARCHIVE();
            b.Altitude = int.Parse(yukseklik.ToString());
            b.CoordinateX = decimal.Parse(lon);
            b.CoordinateY = decimal.Parse(lat);
            b.DateTimeGps = dt.AddHours(3);
            b.DateTimeLocal = DateTime.Now;
            b.Direction = yon;
            b.Distance = mesafe;
            b.Ignition = byte.Parse(ignition);
            b.MsgType = status;
            b.NumberOfSatellite = byte.Parse(gpsdurum);
            b.Parameter1 = depokapak;
            b.Parameter2 = mesajNo;
            b.Speed = decimal.Parse(hiz.ToString());
            b.TransCode = 0;
            b.UnitID = imei;

            adresistenen.Text = adra++.ToString();
            var arm = new armoli();
            var adrrr = arm.ArmoliGeoCodeReverse(lat.ToString().Replace(",", "."), lon.ToString().Replace(",", "."));
            adresislenen.Text = adri++.ToString();

            b.Street = adrrr.ToString();
            b.Status = " ";
            b.Quarter = " ";
            b.District = " ";
            b.City = " ";
            b.Country = " ";

            b.NumberOfSatellite = byte.Parse(gpsdurum);
            b.RolantiSuresi = Convert.ToInt32(rolantisuresi);
            b.ipNumber = ip;

            TBL_ARCHIVEDAL d = new TBL_ARCHIVEDAL();
            d.obj = b;
            d.Save();
            vislenen.Text = vi++.ToString();
            DbHelper dh = new DbHelper();
            int terminalId = dh.GetTerminalId(imei);
            if (alarm > 0)
            {
                if (alarm == 48)
                {
                    if (b.Parameter1 == "1")
                    {
                        AlarmKaydet(67, d.obj.ArchiveID, terminalId, b.DateTimeGps, "");
                        dh.SendSMS(terminalId, "Depo Kapagi Acildi");
                    }
                    else
                    {
                        AlarmKaydet(68, d.obj.ArchiveID, terminalId, b.DateTimeGps, "");
                        dh.SendSMS(terminalId, "Depo Kapagi Kapandi");
                    }
                }
                else
                {
                    if (alarm == 4 || alarm == 12 || alarm == 13)
                    {
                    }
                    else
                    {
                        AlarmKaydet(int.Parse(alarm.ToString()), d.obj.ArchiveID, terminalId, b.DateTimeGps, kartno);
                    }
                }
            }

            //DateTime dtbas = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 0);
            //DateTime dtbit = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            //if (DateTime.Now >= dtbas && DateTime.Now <= dtbit)
            //{
            //    if (!terminals.Contains(terminalId))
            //    {
            //        terminals.Add(terminalId);
            //        AlarmKaydet(13, d.obj.ArchiveID, terminalId, b.DateTimeGps, "");
            //        AlarmKaydet(12, d.obj.ArchiveID, terminalId, b.DateTimeGps, "");
            //    }
            //}
        }

        /// <summary>
        /// Cihazdan gelen sefer bilgisidir
        /// </summary>
        /// <param name="gelendata">Cihazdan gelen veri</param>
        public void SeferBilgisi(string gelendata)
        {
            string mesaj = gelendata;
            mesaj = mesaj.TrimStart('@').TrimStart('S').TrimStart(';').TrimEnd('!').ToString();

            string date = "", saat = "0", degree = "0", min = "0", sec1 = "0", baslat = "0", baslon = "0", bitlat = "0", bitlon = "0";
            int dd = 0, mo = 0, yy = 0, hh = 0, mm = 0, sec = 0;
            DateTime dtbaslama = DateTime.Now;
            DateTime dtbitme = DateTime.Now;
            string[] info = mesaj.Split(';');
            string imei = info[0];
            string acilis = info[1]; //A,220514,155334.00,3827.1228,N,02710.7324,E
            string[] acilisbilgisi = acilis.Split(',');
            if (acilisbilgisi[0] != "R")
            {
                date = acilisbilgisi[1];
                dd = int.Parse(date.Substring(0, 2));
                mo = int.Parse(date.Substring(2, 2));
                yy = int.Parse(DateTime.Now.Year.ToString().Substring(0, 2) + date.Substring(4, 2));

                saat = acilisbilgisi[2];
                hh = int.Parse(saat.Substring(0, 2));
                mm = int.Parse(saat.Substring(2, 2));
                sec = int.Parse(saat.Substring(4, 2));

                dtbaslama = new DateTime(yy, mo, dd, hh, mm, sec);

                degree = acilisbilgisi[3].Substring(0, 2);
                min = acilisbilgisi[3].Substring(2, 2);
                sec1 = acilisbilgisi[3].Substring(5);
                decimal la = decimal.Parse(degree) + decimal.Parse(string.Format("{0},{1}", min, sec1)) / 60;
                baslat = la.ToString();

                degree = acilisbilgisi[5].Substring(0, 3); ;
                min = acilisbilgisi[5].Substring(3, 2);
                sec1 = acilisbilgisi[5].Substring(6);
                decimal lo = decimal.Parse(degree) + decimal.Parse(string.Format("{0},{1}", min, sec1)) / 60;
                baslon = lo.ToString();
            }
            string kapanis = info[2];
            string[] kapanisbilgisi = kapanis.Split(',');
            if (kapanisbilgisi[0] != "R")
            {
                date = kapanisbilgisi[1];
                dd = int.Parse(date.Substring(0, 2));
                mo = int.Parse(date.Substring(2, 2));
                yy = int.Parse(DateTime.Now.Year.ToString().Substring(0, 2) + date.Substring(4, 2));

                saat = kapanisbilgisi[2];
                hh = int.Parse(saat.Substring(0, 2));
                mm = int.Parse(saat.Substring(2, 2));
                sec = int.Parse(saat.Substring(4, 2));

                dtbitme = new DateTime(yy, mo, dd, hh, mm, sec);

                degree = kapanisbilgisi[3].Substring(0, 2);
                min = kapanisbilgisi[3].Substring(2, 2);
                sec1 = kapanisbilgisi[3].Substring(5);
                decimal la = decimal.Parse(degree) + decimal.Parse(min.ToString() + "," + sec1.ToString()) / 60;
                bitlat = la.ToString();

                degree = kapanisbilgisi[5].Substring(0, 3);
                min = kapanisbilgisi[5].Substring(3, 2);
                sec1 = kapanisbilgisi[5].Substring(6);
                decimal lo = decimal.Parse(degree) + decimal.Parse(min.ToString() + "," + sec1.ToString()) / 60;
                bitlon = lo.ToString();
            }

            string gidilenyol = info[3];
            decimal mesafe = (decimal.Parse(System.Convert.ToInt32(gidilenyol, 16).ToString())); //hex to decimal
            mesafe = (mesafe / 16) * decimal.Parse("1,852"); //convert to km
            decimal maxHiz = decimal.Parse(info[4].Replace('.', ','));
            decimal sefersuresi = decimal.Parse(info[5]) / 60;
            decimal maxhizsuresi = decimal.Parse(info[6]) / 60;
            string[] rolantiler = info[7].Split(',');
            decimal ilkhareketrolantisuresi = int.Parse(rolantiler[0]);
            decimal DurmaRolantiSuresi = int.Parse(rolantiler[1]);
            decimal SonRolantiSuresi = int.Parse(rolantiler[2]);

            DbHelper dh = new DbHelper();
            int terminalId = dh.GetTerminalId(imei);

            TBL_SEFER_PAKETI sefer = new TBL_SEFER_PAKETI();

            sefer.BaslangicBoylam = decimal.Parse(baslon);
            sefer.BaslangicEnlem = decimal.Parse(baslat);
            sefer.BitisBoylam = decimal.Parse(bitlon);
            sefer.BitisEnlem = decimal.Parse(bitlat);
            sefer.GidilenYol = mesafe;
            sefer.GpsVarmı = acilisbilgisi[0];
            sefer.SeferBaslama = dtbaslama.AddHours(3);
            sefer.SeferBitme = dtbitme.AddHours(3);
            sefer.MaximumHiz = maxHiz;
            sefer.SeferSuresi = sefersuresi;
            sefer.MaximumHizSuresi = maxhizsuresi;
            sefer.IlkHareketeKadarRolanti = ilkhareketrolantisuresi;
            sefer.SonRolantiSuresi = SonRolantiSuresi;
            sefer.SurucuBilgisi = "";
            sefer.TerminalId = terminalId;
            sefer.DurmaRolantiSuresi = DurmaRolantiSuresi;

            var arm = new armoli();
            adresistenen.Text = adra++.ToString();
            var adrrr = arm.ArmoliGeoCodeReverse(baslat.ToString().Replace(",", "."), baslon.ToString().Replace(",", "."));
            adresislenen.Text = adri++.ToString();

            sefer.BasStreet = adrrr.ToString();
            sefer.BasQuarter = " ";
            sefer.BasDistrict = " ";
            sefer.BasCity = " ";
            sefer.BasCountry = " ";

            adresistenen.Text = adra++.ToString();
            var adrrr2 = arm.ArmoliGeoCodeReverse(baslat.ToString().Replace(",", "."), baslon.ToString().Replace(",", "."));
            adresislenen.Text = adri++.ToString();


            sefer.BitStreet = adrrr2.ToString();


            sefer.BitQuarter = " ";
            sefer.BitDistrict = " ";
            sefer.BitCity = " ";
            sefer.BitCountry = " ";

            TBL_SEFER_PAKETIDAL dal = new TBL_SEFER_PAKETIDAL();
            dal.obj = new TBL_SEFER_PAKETI();
            dal.obj = sefer;
            dal.Save();
        }

        /// <summary>
        /// Cihazın sisteme ilk geldiği zaman gelen data
        /// </summary>
        /// <param name="gelendata">Cihazdan gelen veri</param>
        public void RegisterBilgisi(string gelendata, string ip)
        {
            //string mesaj = "@C;86307101317909595;M5A;286034240187848;01011439ort-2400!";

            string[] arr = gelendata.Split(';');

            string unitId = arr[1];
            string modelName = arr[2];
            string imsi = arr[3];
            string diger = arr[4];

            DbHelper dh = new DbHelper();
            int terminalId = dh.GetTerminalId(unitId);

            TBL_TERMINAL b = new TBL_TERMINAL();
            TBL_TERMINALDAL dal = new TBL_TERMINALDAL();
            dal.obj = new TBL_TERMINAL();

            if (terminalId == 0)
            {
                b.AccessoryType = "";
                b.AccStatus = true;
                b.ArchiveId = 1;
                b.Battery = byte.Parse("1");
                b.City = "";
                b.CompanyID = 0;
                b.Country = "";
                b.CreateDate = DateTime.Now;
                b.CurrentTankValue = 0;
                b.DefaultTankValue = 0;
                b.District = "";
                b.HistoryID = 0;
                b.IsActive = true;
                b.LastPositionX = 0;
                b.LastPositionY = 0;
                b.OfflineDate = DateTime.Now;
                b.OnlineDate = DateTime.Now;
                b.PlateNo = "";
                b.PositionDate = DateTime.Now;
                b.Quarter = "";
                b.Street = "";
                b.TerminalStatus = true;
                b.UnitId = unitId;
                b.UnitIpAddress = ip;
                b.UnitModel = modelName;
                b.UserID = 0;
                b.VehicleDefaultDistance = 0;
                b.GsmNumber = "";
                dal.obj = b;
                string s = dal.Save();
            }
            else
            {
                dal.obj.TerminalID = terminalId;
                dal.Read();
                dal.obj.UnitIpAddress = ip;
                dal.Update();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="durum"></param>
        public void MotorBlokajDurumuGuncelle(string unitId, int durum)
        {
            DbHelper dh = new DbHelper();
            TBL_TERMINALDAL dal = new TBL_TERMINALDAL();
            dal.obj = new TBL_TERMINAL();
            int terminalId = dh.GetTerminalId(unitId);
            dal.obj.TerminalID = terminalId;
            dal.Read();
            dal.obj.MotorBlokajDurumu = false;
            if (durum == 99)
            {
                dal.obj.MotorBlokajDurumu = true;
            }
            dal.Update();

            AracTakipDb db = new AracTakipDb();
            SqlConnection con = new SqlConnection(db.Database.Connection.ConnectionString);
            try
            {
                con.Open();
                string query = string.Format("update tbl_komut set komutDurum = 1 where komutDurum = 0 and Komut = '#ID;1234;{0};1234!' and terminalId = {1}", durum, terminalId);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            try
            {
                con.Open();
                string query2 = string.Format("update tbl_komut set sms=1 where terminalId = {0}", terminalId);
                SqlCommand cmd = new SqlCommand(query2, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int terminalIDbul(string imei)
        {
            int term = 0;
            AracTakipDb db = new AracTakipDb();
            SqlConnection sqlBaglantim = new SqlConnection(db.Database.Connection.ConnectionString);
            string query = string.Format(@"SELECT TerminalID FROM TBL_TERMINAL WHERE UnitId = '{0}'", imei);
            SqlCommand sqlSorgu = new SqlCommand(query, sqlBaglantim);

            try
            {
                if (sqlBaglantim != null)
                {
                    sqlBaglantim.Close();
                }
                sqlBaglantim.Open();
                SqlDataReader sqlOku = sqlSorgu.ExecuteReader();
                while (sqlOku.Read())
                {
                    term = Convert.ToInt32(sqlOku["TerminalID"]);
                }
                //kayitVarmi = sqlOku.Read();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlBaglantim.Close();
            }
            return term;
        }

        public bool cihazArmoli(int TerminalID)
        {
            AracTakipDb db = new AracTakipDb();
            SqlConnection sqlBaglantim = new SqlConnection(db.Database.Connection.ConnectionString);
            bool kayitVarmi = false;
            SqlCommand sqlSorgu = new SqlCommand("SELECT [TerminalID],[UnitModel] FROM [dbo].[TBL_TERMINAL] WHERE [UnitModel]='Y30' and [TerminalID]=@TerminalID", sqlBaglantim);
            sqlSorgu.Parameters.AddWithValue("@TerminalID", TerminalID);

            if (sqlBaglantim != null)
            {
                sqlBaglantim.Close();
            }
            sqlBaglantim.Open();
            SqlDataReader sqlOku = sqlSorgu.ExecuteReader();
            kayitVarmi = sqlOku.Read();
            sqlBaglantim.Close();
            return kayitVarmi;
        }

        private void komutzamanlayici_Tick(object sender, EventArgs e)
        {
            komutuygula();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gunluk.Text = "";
        }
    }
}
