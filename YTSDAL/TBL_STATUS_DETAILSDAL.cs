
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_STATUS_DETAILSDAL : IOperation
    {
        private TBL_STATUS_DETAILS _obj = new TBL_STATUS_DETAILS();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_STATUS_DETAILS obj
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

        public TBL_STATUS_DETAILSDAL()
        {
            this._obj = new TBL_STATUS_DETAILS();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_STATUS_DETAILS original = this._db.TBL_STATUS_DETAILS.SingleOrDefault<TBL_STATUS_DETAILS>((TBL_STATUS_DETAILS x) => x.StatusBitNo == this._obj.StatusBitNo);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_STATUS_DETAILS.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_STATUS_DETAILS> query =
                from u in this._db.TBL_STATUS_DETAILS.AsEnumerable<TBL_STATUS_DETAILS>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_STATUS_DETAILS>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_STATUS_DETAILS original = this._db.TBL_STATUS_DETAILS.SingleOrDefault<TBL_STATUS_DETAILS>((TBL_STATUS_DETAILS x) => x.StatusBitNo == this._obj.StatusBitNo);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.StatusBitNo = original.StatusBitNo;
                this._obj.StatusName = original.StatusName;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_STATUS_DETAILS _instance = new TBL_STATUS_DETAILS()
            {
                StatusBitNo = this._obj.StatusBitNo,
                StatusName = this._obj.StatusName
            };
            try
            {
                this._db.TBL_STATUS_DETAILS.Add(_instance);
                this._db.SaveChanges();
                this._obj.StatusBitNo = _instance.StatusBitNo;
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
            TBL_STATUS_DETAILS original = this._db.TBL_STATUS_DETAILS.SingleOrDefault<TBL_STATUS_DETAILS>((TBL_STATUS_DETAILS x) => x.StatusBitNo == this._obj.StatusBitNo);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.StatusBitNo = this._obj.StatusBitNo;
                original.StatusName = this._obj.StatusName;
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