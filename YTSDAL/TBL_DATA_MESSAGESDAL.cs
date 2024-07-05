
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_DATA_MESSAGESDAL : IOperation
    {
        private TBL_DATA_MESSAGES _obj = new TBL_DATA_MESSAGES();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_DATA_MESSAGES obj
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

        public TBL_DATA_MESSAGESDAL()
        {
            this._obj = new TBL_DATA_MESSAGES();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_DATA_MESSAGES original = this._db.TBL_DATA_MESSAGES.SingleOrDefault<TBL_DATA_MESSAGES>((TBL_DATA_MESSAGES x) => x.MessagesID == this._obj.MessagesID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_DATA_MESSAGES.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_DATA_MESSAGES> query =
                from u in this._db.TBL_DATA_MESSAGES.AsEnumerable<TBL_DATA_MESSAGES>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_DATA_MESSAGES>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_DATA_MESSAGES original = this._db.TBL_DATA_MESSAGES.SingleOrDefault<TBL_DATA_MESSAGES>((TBL_DATA_MESSAGES x) => x.MessagesID == this._obj.MessagesID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.MessagesID = original.MessagesID;
                this._obj.TerminalId = original.TerminalId;
                this._obj.Komut = original.Komut;
                this._obj.Durum = original.Durum;
                this._obj.IstekId = original.IstekId;
                this._obj.MsgType = original.MsgType;
                this._obj.TransCode = original.TransCode;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_DATA_MESSAGES _instance = new TBL_DATA_MESSAGES()
            {
                MessagesID = this._obj.MessagesID,
                TerminalId = this._obj.TerminalId,
                Komut = this._obj.Komut,
                Durum = this._obj.Durum,
                IstekId = this._obj.IstekId,
                MsgType = this._obj.MsgType,
                TransCode = this._obj.TransCode
            };
            try
            {
                this._db.TBL_DATA_MESSAGES.Add(_instance);
                this._db.SaveChanges();
                this._obj.MessagesID = _instance.MessagesID;
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
            TBL_DATA_MESSAGES original = this._db.TBL_DATA_MESSAGES.SingleOrDefault<TBL_DATA_MESSAGES>((TBL_DATA_MESSAGES x) => x.MessagesID == this._obj.MessagesID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.MessagesID = this._obj.MessagesID;
                original.TerminalId = this._obj.TerminalId;
                original.Komut = this._obj.Komut;
                original.Durum = this._obj.Durum;
                original.IstekId = this._obj.IstekId;
                original.MsgType = this._obj.MsgType;
                original.TransCode = this._obj.TransCode;
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