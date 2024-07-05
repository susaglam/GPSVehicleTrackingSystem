
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_USERDAL : IOperation
    {
        private TBL_USER _obj = new TBL_USER();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_USER obj
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

        public TBL_USERDAL()
        {
            this._obj = new TBL_USER();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_USER original = this._db.TBL_USER.SingleOrDefault<TBL_USER>((TBL_USER x) => x.UserID == this._obj.UserID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_USER.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_USER> query =
                from u in this._db.TBL_USER.AsEnumerable<TBL_USER>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_USER>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_USER original = this._db.TBL_USER.SingleOrDefault<TBL_USER>((TBL_USER x) => x.UserID == this._obj.UserID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.UserID = original.UserID;
                this._obj.CompanyID = original.CompanyID;
                this._obj.IsActive = original.IsActive;
                this._obj.CreateDate = original.CreateDate;
                this._obj.ExpireDate = original.ExpireDate;
                this._obj.LastConnectionDate = original.LastConnectionDate;
                this._obj.FirstName = original.FirstName;
                this._obj.LastName = original.LastName;
                this._obj.UserName = original.UserName;
                this._obj.Password = original.Password;
                this._obj.Email = original.Email;
                this._obj.PhoneHome = original.PhoneHome;
                this._obj.PhoneWork = original.PhoneWork;
                this._obj.PhoneGsm = original.PhoneGsm;
                this._obj.Address = original.Address;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_USER _instance = new TBL_USER()
            {
                UserID = this._obj.UserID,
                CompanyID = this._obj.CompanyID,
                IsActive = this._obj.IsActive,
                CreateDate = this._obj.CreateDate,
                ExpireDate = this._obj.ExpireDate,
                LastConnectionDate = this._obj.LastConnectionDate,
                FirstName = this._obj.FirstName,
                LastName = this._obj.LastName,
                UserName = this._obj.UserName,
                Password = this._obj.Password,
                Email = this._obj.Email,
                PhoneHome = this._obj.PhoneHome,
                PhoneWork = this._obj.PhoneWork,
                PhoneGsm = this._obj.PhoneGsm,
                Address = this._obj.Address
            };
            try
            {
                this._db.TBL_USER.Add(_instance);
                this._db.SaveChanges();
                this._obj.UserID = _instance.UserID;
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
            TBL_USER original = this._db.TBL_USER.SingleOrDefault<TBL_USER>((TBL_USER x) => x.UserID == this._obj.UserID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.UserID = this._obj.UserID;
                original.CompanyID = this._obj.CompanyID;
                original.IsActive = this._obj.IsActive;
                original.CreateDate = this._obj.CreateDate;
                original.ExpireDate = this._obj.ExpireDate;
                original.LastConnectionDate = this._obj.LastConnectionDate;
                original.FirstName = this._obj.FirstName;
                original.LastName = this._obj.LastName;
                original.UserName = this._obj.UserName;
                original.Password = this._obj.Password;
                original.Email = this._obj.Email;
                original.PhoneHome = this._obj.PhoneHome;
                original.PhoneWork = this._obj.PhoneWork;
                original.PhoneGsm = this._obj.PhoneGsm;
                original.Address = this._obj.Address;
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