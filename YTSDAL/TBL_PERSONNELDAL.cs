
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;namespace YTSDAL
{
    public class TBL_PERSONNELDAL : IOperation
    {
        private TBL_PERSONNEL _obj = new TBL_PERSONNEL();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_PERSONNEL obj
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

        public TBL_PERSONNELDAL()
        {
            this._obj = new TBL_PERSONNEL();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_PERSONNEL original = this._db.TBL_PERSONNEL.SingleOrDefault<TBL_PERSONNEL>((TBL_PERSONNEL x) => x.PersonnelID == this._obj.PersonnelID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_PERSONNEL.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_PERSONNEL> query =
                from u in this._db.TBL_PERSONNEL.AsEnumerable<TBL_PERSONNEL>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_PERSONNEL>(query));
        }

        public string GetPersonelTakip(int terminalId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetPersonelTakipByTerminalId(terminalId, dtbaslama, dtbitme);
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
            TBL_PERSONNEL original = this._db.TBL_PERSONNEL.SingleOrDefault<TBL_PERSONNEL>((TBL_PERSONNEL x) => x.PersonnelID == this._obj.PersonnelID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.PersonnelID = original.PersonnelID;
                this._obj.CompanyID = original.CompanyID;
                this._obj.RoleID = original.RoleID;
                this._obj.FirstName = original.FirstName;
                this._obj.LastName = original.LastName;
                this._obj.RfidCardId = original.RfidCardId;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_PERSONNEL _instance = new TBL_PERSONNEL()
            {
                PersonnelID = this._obj.PersonnelID,
                CompanyID = this._obj.CompanyID,
                RoleID = this._obj.RoleID,
                FirstName = this._obj.FirstName,
                LastName = this._obj.LastName,
                RfidCardId = this._obj.RfidCardId
            };
            try
            {
                this._db.TBL_PERSONNEL.Add(_instance);
                this._db.SaveChanges();
                this._obj.PersonnelID = _instance.PersonnelID;
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
            TBL_PERSONNEL original = this._db.TBL_PERSONNEL.SingleOrDefault<TBL_PERSONNEL>((TBL_PERSONNEL x) => x.PersonnelID == this._obj.PersonnelID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.PersonnelID = this._obj.PersonnelID;
                original.CompanyID = this._obj.CompanyID;
                original.RoleID = this._obj.RoleID;
                original.FirstName = this._obj.FirstName;
                original.LastName = this._obj.LastName;
                original.RfidCardId = this._obj.RfidCardId;
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