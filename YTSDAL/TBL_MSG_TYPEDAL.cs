
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_MSG_TYPEDAL : IOperation
    {
        private TBL_MSG_TYPE _obj = new TBL_MSG_TYPE();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_MSG_TYPE obj
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

        public TBL_MSG_TYPEDAL()
        {
            this._obj = new TBL_MSG_TYPE();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_MSG_TYPE original = this._db.TBL_MSG_TYPE.SingleOrDefault<TBL_MSG_TYPE>((TBL_MSG_TYPE x) => x.MsgTypeId == this._obj.MsgTypeId);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_MSG_TYPE.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_MSG_TYPE> query =
                from u in this._db.TBL_MSG_TYPE.AsEnumerable<TBL_MSG_TYPE>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_MSG_TYPE>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_MSG_TYPE original = this._db.TBL_MSG_TYPE.SingleOrDefault<TBL_MSG_TYPE>((TBL_MSG_TYPE x) => x.MsgTypeId == this._obj.MsgTypeId);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.MsgTypeId = original.MsgTypeId;
                this._obj.Type = original.Type;
                this._obj.Detail = original.Detail;
                this._obj.Value = original.Value;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_MSG_TYPE _instance = new TBL_MSG_TYPE()
            {
                MsgTypeId = this._obj.MsgTypeId,
                Type = this._obj.Type,
                Detail = this._obj.Detail,
                Value = this._obj.Value
            };
            try
            {
                this._db.TBL_MSG_TYPE.Add(_instance);
                this._db.SaveChanges();
                this._obj.MsgTypeId = _instance.MsgTypeId;
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
            TBL_MSG_TYPE original = this._db.TBL_MSG_TYPE.SingleOrDefault<TBL_MSG_TYPE>((TBL_MSG_TYPE x) => x.MsgTypeId == this._obj.MsgTypeId);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.MsgTypeId = this._obj.MsgTypeId;
                original.Type = this._obj.Type;
                original.Detail = this._obj.Detail;
                original.Value = this._obj.Value;
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