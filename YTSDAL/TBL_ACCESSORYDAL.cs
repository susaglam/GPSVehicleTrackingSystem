
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_ACCESSORYDAL : IOperation
    {
        private TBL_ACCESSORY _obj = new TBL_ACCESSORY();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_ACCESSORY obj
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

        public TBL_ACCESSORYDAL()
        {
            this._obj = new TBL_ACCESSORY();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ACCESSORY original = this._db.TBL_ACCESSORY.SingleOrDefault<TBL_ACCESSORY>((TBL_ACCESSORY x) => x.AccessoryTypeId == this._obj.AccessoryTypeId);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_ACCESSORY.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_ACCESSORY> query =
                from u in this._db.TBL_ACCESSORY.AsEnumerable<TBL_ACCESSORY>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_ACCESSORY>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ACCESSORY original = this._db.TBL_ACCESSORY.SingleOrDefault<TBL_ACCESSORY>((TBL_ACCESSORY x) => x.AccessoryTypeId == this._obj.AccessoryTypeId);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.AccessoryTypeId = original.AccessoryTypeId;
                this._obj.AccessoryDetail = original.AccessoryDetail;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ACCESSORY _instance = new TBL_ACCESSORY()
            {
                AccessoryTypeId = this._obj.AccessoryTypeId,
                AccessoryDetail = this._obj.AccessoryDetail
            };
            try
            {
                this._db.TBL_ACCESSORY.Add(_instance);
                this._db.SaveChanges();
                this._obj.AccessoryTypeId = _instance.AccessoryTypeId;
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
            TBL_ACCESSORY original = this._db.TBL_ACCESSORY.SingleOrDefault<TBL_ACCESSORY>((TBL_ACCESSORY x) => x.AccessoryTypeId == this._obj.AccessoryTypeId);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.AccessoryTypeId = this._obj.AccessoryTypeId;
                original.AccessoryDetail = this._obj.AccessoryDetail;
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