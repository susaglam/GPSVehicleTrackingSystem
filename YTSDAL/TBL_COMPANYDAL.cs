
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_COMPANYDAL : IOperation
    {
        private TBL_COMPANY _obj = new TBL_COMPANY();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_COMPANY obj
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

        public TBL_COMPANYDAL()
        {
            this._obj = new TBL_COMPANY();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_COMPANY original = this._db.TBL_COMPANY.SingleOrDefault<TBL_COMPANY>((TBL_COMPANY x) => x.CompanyID == this._obj.CompanyID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_COMPANY.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_COMPANY> query =
                from u in this._db.TBL_COMPANY.AsEnumerable<TBL_COMPANY>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_COMPANY>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_COMPANY original = this._db.TBL_COMPANY.SingleOrDefault<TBL_COMPANY>((TBL_COMPANY x) => x.CompanyID == this._obj.CompanyID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.CompanyID = original.CompanyID;
                this._obj.UpperCompany = original.UpperCompany;
                this._obj.CompanyName = original.CompanyName;
                this._obj.ContactName = original.ContactName;
                this._obj.ContactPhone = original.ContactPhone;
                this._obj.ContactEmail = original.ContactEmail;
                this._obj.ContactAddress = original.ContactAddress;
                this._obj.Expire = original.Expire;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_COMPANY _instance = new TBL_COMPANY()
            {
                CompanyID = this._obj.CompanyID,
                UpperCompany = this._obj.UpperCompany,
                CompanyName = this._obj.CompanyName,
                ContactName = this._obj.ContactName,
                ContactPhone = this._obj.ContactPhone,
                ContactEmail = this._obj.ContactEmail,
                ContactAddress = this._obj.ContactAddress,
                Expire = this._obj.Expire
            };
            try
            {
                this._db.TBL_COMPANY.Add(_instance);
                this._db.SaveChanges();
                this._obj.CompanyID = _instance.CompanyID;
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
            TBL_COMPANY original = this._db.TBL_COMPANY.SingleOrDefault<TBL_COMPANY>((TBL_COMPANY x) => x.CompanyID == this._obj.CompanyID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.CompanyID = this._obj.CompanyID;
                original.UpperCompany = this._obj.UpperCompany;
                original.CompanyName = this._obj.CompanyName;
                original.ContactName = this._obj.ContactName;
                original.ContactPhone = this._obj.ContactPhone;
                original.ContactEmail = this._obj.ContactEmail;
                original.ContactAddress = this._obj.ContactAddress;
                original.Expire = this._obj.Expire;
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