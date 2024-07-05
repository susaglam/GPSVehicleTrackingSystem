
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_INDEKS_DETAILSDAL : IOperation
    {
        private TBL_INDEKS_DETAILS _obj = new TBL_INDEKS_DETAILS();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_INDEKS_DETAILS obj
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

        public TBL_INDEKS_DETAILSDAL()
        {
            this._obj = new TBL_INDEKS_DETAILS();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_INDEKS_DETAILS original = this._db.TBL_INDEKS_DETAILS.SingleOrDefault<TBL_INDEKS_DETAILS>((TBL_INDEKS_DETAILS x) => x.IndeksDegerId == this._obj.IndeksDegerId);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_INDEKS_DETAILS.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_INDEKS_DETAILS> query =
                from u in this._db.TBL_INDEKS_DETAILS.AsEnumerable<TBL_INDEKS_DETAILS>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_INDEKS_DETAILS>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_INDEKS_DETAILS original = this._db.TBL_INDEKS_DETAILS.SingleOrDefault<TBL_INDEKS_DETAILS>((TBL_INDEKS_DETAILS x) => x.IndeksDegerId == this._obj.IndeksDegerId);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.IndeksDegerId = original.IndeksDegerId;
                this._obj.IndeksBilgi = original.IndeksBilgi;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_INDEKS_DETAILS _instance = new TBL_INDEKS_DETAILS()
            {
                IndeksDegerId = this._obj.IndeksDegerId,
                IndeksBilgi = this._obj.IndeksBilgi
            };
            try
            {
                this._db.TBL_INDEKS_DETAILS.Add(_instance);
                this._db.SaveChanges();
                this._obj.IndeksDegerId = _instance.IndeksDegerId;
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
            TBL_INDEKS_DETAILS original = this._db.TBL_INDEKS_DETAILS.SingleOrDefault<TBL_INDEKS_DETAILS>((TBL_INDEKS_DETAILS x) => x.IndeksDegerId == this._obj.IndeksDegerId);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.IndeksDegerId = this._obj.IndeksDegerId;
                original.IndeksBilgi = this._obj.IndeksBilgi;
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