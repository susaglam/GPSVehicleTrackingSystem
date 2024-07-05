
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_ALARM_DETAILSDAL : IOperation
    {
        private TBL_ALARM_DETAILS _obj = new TBL_ALARM_DETAILS();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_ALARM_DETAILS obj
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

        public TBL_ALARM_DETAILSDAL()
        {
            this._obj = new TBL_ALARM_DETAILS();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ALARM_DETAILS original = this._db.TBL_ALARM_DETAILS.SingleOrDefault<TBL_ALARM_DETAILS>((TBL_ALARM_DETAILS x) => x.AlarmTypeId == this._obj.AlarmTypeId);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_ALARM_DETAILS.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_ALARM_DETAILS> query =
                from u in this._db.TBL_ALARM_DETAILS.AsEnumerable<TBL_ALARM_DETAILS>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_ALARM_DETAILS>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ALARM_DETAILS original = this._db.TBL_ALARM_DETAILS.SingleOrDefault<TBL_ALARM_DETAILS>((TBL_ALARM_DETAILS x) => x.AlarmTypeId == this._obj.AlarmTypeId);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.AlarmTypeId = original.AlarmTypeId;
                this._obj.AlarmDetail = original.AlarmDetail;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ALARM_DETAILS _instance = new TBL_ALARM_DETAILS()
            {
                AlarmTypeId = this._obj.AlarmTypeId,
                AlarmDetail = this._obj.AlarmDetail
            };
            try
            {
                this._db.TBL_ALARM_DETAILS.Add(_instance);
                this._db.SaveChanges();
                this._obj.AlarmTypeId = _instance.AlarmTypeId;
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
            TBL_ALARM_DETAILS original = this._db.TBL_ALARM_DETAILS.SingleOrDefault<TBL_ALARM_DETAILS>((TBL_ALARM_DETAILS x) => x.AlarmTypeId == this._obj.AlarmTypeId);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.AlarmTypeId = this._obj.AlarmTypeId;
                original.AlarmDetail = this._obj.AlarmDetail;
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