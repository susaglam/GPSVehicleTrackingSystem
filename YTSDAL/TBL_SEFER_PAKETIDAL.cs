
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;namespace YTSDAL
{
    public class TBL_SEFER_PAKETIDAL : IOperation
    {
        private TBL_SEFER_PAKETI _obj = new TBL_SEFER_PAKETI();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_SEFER_PAKETI obj
        {
            get
            {
                return this._obj;
            }
            set
            {
                this._obj = value;
            }
        }

        public string query
        {
            get
            {
                return this._query;
            }
            set
            {
                this._query = value;
            }
        }

        public TBL_SEFER_PAKETIDAL()
        {
            this._obj = new TBL_SEFER_PAKETI();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_SEFER_PAKETI original = this._db.TBL_SEFER_PAKETI.SingleOrDefault<TBL_SEFER_PAKETI>((TBL_SEFER_PAKETI x) => x.SeferId == this._obj.SeferId);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_SEFER_PAKETI.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_SEFER_PAKETI> query =
                from u in this._db.TBL_SEFER_PAKETI.AsEnumerable<TBL_SEFER_PAKETI>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_SEFER_PAKETI>(query));
        }

        public DateTime GetDate(string date)
        {
            DateTime convertDate = DateTime.Now;
            try
            {
                char[] chrArray = new char[] { ' ' };
                string[] d = date.Split(chrArray);
                string str = d[0];
                chrArray = new char[] { '.' };
                string[] t = str.Split(chrArray);
                string str1 = d[1];
                chrArray = new char[] { ':' };
                str1.Split(chrArray);
                int[] values = new int[(int) t.Length];
                for (int x = 0; x < (int) t.Length; x++)
                {
                    string str2 = t[x].ToString();
                    chrArray = new char[] { ' ' };
                    string str3 = str2.TrimStart(chrArray);
                    chrArray = new char[] { ' ' };
                    values[x] = Convert.ToInt32(str3.TrimEnd(chrArray).Trim());
                }
            }
            catch (FormatException formatException)
            {
            }
            return convertDate;
        }

        public string GetSeferPaketi(DateTime dtBaslama, DateTime dtBitme)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetSeferPaketi(dtBaslama, dtBitme);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_SEFER_PAKETI original = this._db.TBL_SEFER_PAKETI.SingleOrDefault<TBL_SEFER_PAKETI>((TBL_SEFER_PAKETI x) => x.SeferId == this._obj.SeferId);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.SeferId = original.SeferId;
                this._obj.TerminalId = original.TerminalId;
                this._obj.GpsVarmı = original.GpsVarmı;
                this._obj.SeferBaslama = original.SeferBaslama;
                this._obj.BaslangicEnlem = original.BaslangicEnlem;
                this._obj.BaslangicBoylam = original.BaslangicBoylam;
                this._obj.SeferBitme = original.SeferBitme;
                this._obj.BitisEnlem = original.BitisEnlem;
                this._obj.BitisBoylam = original.BitisBoylam;
                this._obj.GidilenYol = original.GidilenYol;
                this._obj.MaximumHiz = original.MaximumHiz;
                this._obj.SeferSuresi = original.SeferSuresi;
                this._obj.MaximumHizSuresi = original.MaximumHizSuresi;
                this._obj.IlkHareketeKadarRolanti = original.IlkHareketeKadarRolanti;
                this._obj.DurmaRolantiSuresi = original.DurmaRolantiSuresi;
                this._obj.SonRolantiSuresi = original.SonRolantiSuresi;
                this._obj.SurucuBilgisi = original.SurucuBilgisi;
                this._obj.BasCity = original.BasCity;
                this._obj.BasCountry = original.BasCountry;
                this._obj.BasDistrict = original.BasDistrict;
                this._obj.BasQuarter = original.BasQuarter;
                this._obj.BasStreet = original.BasStreet;
                this._obj.BitCity = original.BitCity;
                this._obj.BitCountry = original.BitCountry;
                this._obj.BitDistrict = original.BitDistrict;
                this._obj.BitQuarter = original.BitQuarter;
                this._obj.BitStreet = original.BitStreet;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_SEFER_PAKETI _instance = new TBL_SEFER_PAKETI()
            {
                SeferId = this._obj.SeferId,
                TerminalId = this._obj.TerminalId,
                GpsVarmı = this._obj.GpsVarmı,
                SeferBaslama = this._obj.SeferBaslama,
                BaslangicEnlem = this._obj.BaslangicEnlem,
                BaslangicBoylam = this._obj.BaslangicBoylam,
                SeferBitme = this._obj.SeferBitme,
                BitisEnlem = this._obj.BitisEnlem,
                BitisBoylam = this._obj.BitisBoylam,
                GidilenYol = this._obj.GidilenYol,
                MaximumHiz = this._obj.MaximumHiz,
                SeferSuresi = this._obj.SeferSuresi,
                MaximumHizSuresi = this._obj.MaximumHizSuresi,
                IlkHareketeKadarRolanti = this._obj.IlkHareketeKadarRolanti,
                DurmaRolantiSuresi = this._obj.DurmaRolantiSuresi,
                SonRolantiSuresi = this._obj.SonRolantiSuresi,
                SurucuBilgisi = this._obj.SurucuBilgisi,
                BasCity = this._obj.BasCity,
                BasCountry = this._obj.BasCountry,
                BasDistrict = this._obj.BasDistrict,
                BasQuarter = this._obj.BasQuarter,
                BasStreet = this._obj.BasStreet,
                BitCity = this._obj.BitCity,
                BitCountry = this._obj.BitCountry,
                BitDistrict = this._obj.BitDistrict,
                BitQuarter = this._obj.BitQuarter,
                BitStreet = this._obj.BitStreet
            };
            try
            {
                this._db.TBL_SEFER_PAKETI.Add(_instance);
                this._db.SaveChanges();
                this._obj.SeferId = _instance.SeferId;
                str = ReturnMessage.KayitBasarili.ToString();
            }
            catch (Exception exception)
            {
                str = ReturnMessage.KayitHatasi.ToString();
            }
            return str;
        }

        public string Update()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_SEFER_PAKETI original = this._db.TBL_SEFER_PAKETI.SingleOrDefault<TBL_SEFER_PAKETI>((TBL_SEFER_PAKETI x) => x.SeferId == this._obj.SeferId);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.SeferId = this._obj.SeferId;
                original.TerminalId = this._obj.TerminalId;
                original.GpsVarmı = this._obj.GpsVarmı;
                original.SeferBaslama = this._obj.SeferBaslama;
                original.BaslangicEnlem = this._obj.BaslangicEnlem;
                original.BaslangicBoylam = this._obj.BaslangicBoylam;
                original.SeferBitme = this._obj.SeferBitme;
                original.BitisEnlem = this._obj.BitisEnlem;
                original.BitisBoylam = this._obj.BitisBoylam;
                original.GidilenYol = this._obj.GidilenYol;
                original.MaximumHiz = this._obj.MaximumHiz;
                original.SeferSuresi = this._obj.SeferSuresi;
                original.MaximumHizSuresi = this._obj.MaximumHizSuresi;
                original.IlkHareketeKadarRolanti = this._obj.IlkHareketeKadarRolanti;
                original.DurmaRolantiSuresi = this._obj.DurmaRolantiSuresi;
                original.SonRolantiSuresi = this._obj.SonRolantiSuresi;
                original.SurucuBilgisi = this._obj.SurucuBilgisi;
                original.BasCity = this._obj.BasCity;
                original.BasCountry = this._obj.BasCountry;
                original.BasDistrict = this._obj.BasDistrict;
                original.BasQuarter = this._obj.BasQuarter;
                original.BasStreet = this._obj.BasStreet;
                original.BitCity = this._obj.BitCity;
                original.BitCountry = this._obj.BitCountry;
                original.BitDistrict = this._obj.BitDistrict;
                original.BitQuarter = this._obj.BitQuarter;
                original.BitStreet = this._obj.BitStreet;
                try
                {
                    this._db.SaveChanges();
                    str = ReturnMessage.UpdateEdildi.ToString();
                }
                catch (Exception exception)
                {
                    str = ReturnMessage.UpdateHatasi.ToString();
                }
            }
            return str;
        }
    }
}