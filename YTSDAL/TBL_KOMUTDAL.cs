
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;namespace YTSDAL
{
    public class TBL_KOMUTDAL : IOperation
    {
        private TBL_KOMUT _obj;

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_KOMUT obj
        {
            get
            {
                return this._obj;
            }
            set
            {
                if (this._obj == null)
                {
                    this._obj = new TBL_KOMUT();
                }
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

        public TBL_KOMUTDAL()
        {
            this._obj = new TBL_KOMUT();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_KOMUT original = this._db.TBL_KOMUT.SingleOrDefault<TBL_KOMUT>((TBL_KOMUT x) => x.KomutId == this._obj.KomutId);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_KOMUT.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_KOMUT> query =
                from u in this._db.TBL_KOMUT.AsEnumerable<TBL_KOMUT>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_KOMUT>(query));
        }

        public DataTable GetDataTableFromQuery()
        {
            this._db = new AracTakipDb();
            return this.dh.GetDataTableFromQuery(this.query);
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_KOMUT original = this._db.TBL_KOMUT.SingleOrDefault<TBL_KOMUT>((TBL_KOMUT x) => x.KomutId == this._obj.KomutId);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.KomutId = original.KomutId;
                this._obj.TerminalId = original.TerminalId;
                this._obj.Komut = original.Komut;
                this._obj.KomutTarihi = original.KomutTarihi;
                this._obj.KomutDurum = original.KomutDurum;
                this._obj.sms = original.sms;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_KOMUT _instance = new TBL_KOMUT()
            {
                KomutId = this._obj.KomutId,
                TerminalId = this._obj.TerminalId,
                Komut = this._obj.Komut,
                KomutTarihi = this._obj.KomutTarihi,
                KomutDurum = this._obj.KomutDurum,
                sms= this._obj.sms
            };
            this._db.TBL_KOMUT.Add(_instance);
            try
            {
                this._db.SaveChanges();
                this._obj.KomutId = _instance.KomutId;
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
            TBL_KOMUT original = this._db.TBL_KOMUT.SingleOrDefault<TBL_KOMUT>((TBL_KOMUT x) => x.KomutId == this._obj.KomutId);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.KomutId = this._obj.KomutId;
                original.TerminalId = this._obj.TerminalId;
                original.Komut = this._obj.Komut;
                original.KomutTarihi = this._obj.KomutTarihi;
                original.KomutDurum = this._obj.KomutDurum;
                original.sms = this._obj.sms;
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