
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_INVOICEDAL : IOperation
    {
        private TBL_INVOICE _obj = new TBL_INVOICE();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_INVOICE obj
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

        public TBL_INVOICEDAL()
        {
            this._obj = new TBL_INVOICE();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_INVOICE original = this._db.TBL_INVOICE.SingleOrDefault<TBL_INVOICE>((TBL_INVOICE x) => x.InvoiceID == this._obj.InvoiceID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_INVOICE.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_INVOICE> query =
                from u in this._db.TBL_INVOICE.AsEnumerable<TBL_INVOICE>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_INVOICE>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_INVOICE original = this._db.TBL_INVOICE.SingleOrDefault<TBL_INVOICE>((TBL_INVOICE x) => x.InvoiceID == this._obj.InvoiceID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.InvoiceID = original.InvoiceID;
                this._obj.CompanyID = original.CompanyID;
                this._obj.InvoiceMonthofDate = original.InvoiceMonthofDate;
                this._obj.InvoicePeriod = original.InvoicePeriod;
                this._obj.InvoiceAmount = original.InvoiceAmount;
                this._obj.InvoiceName = original.InvoiceName;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_INVOICE _instance = new TBL_INVOICE()
            {
                InvoiceID = this._obj.InvoiceID,
                CompanyID = this._obj.CompanyID,
                InvoiceMonthofDate = this._obj.InvoiceMonthofDate,
                InvoicePeriod = this._obj.InvoicePeriod,
                InvoiceAmount = this._obj.InvoiceAmount,
                InvoiceName = this._obj.InvoiceName
            };
            try
            {
                this._db.TBL_INVOICE.Add(_instance);
                this._db.SaveChanges();
                this._obj.InvoiceID = _instance.InvoiceID;
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
            TBL_INVOICE original = this._db.TBL_INVOICE.SingleOrDefault<TBL_INVOICE>((TBL_INVOICE x) => x.InvoiceID == this._obj.InvoiceID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.InvoiceID = this._obj.InvoiceID;
                original.CompanyID = this._obj.CompanyID;
                original.InvoiceMonthofDate = this._obj.InvoiceMonthofDate;
                original.InvoicePeriod = this._obj.InvoicePeriod;
                original.InvoiceAmount = this._obj.InvoiceAmount;
                original.InvoiceName = this._obj.InvoiceName;
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