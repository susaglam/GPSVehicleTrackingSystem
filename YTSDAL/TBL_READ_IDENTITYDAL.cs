
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;namespace YTSDAL
{
    public class TBL_READ_IDENTITYDAL : IOperation
    {
        private TBL_READ_IDENTITY _obj = new TBL_READ_IDENTITY();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_READ_IDENTITY obj
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

        public TBL_READ_IDENTITYDAL()
        {
            this._obj = new TBL_READ_IDENTITY();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_READ_IDENTITY original = this._db.TBL_READ_IDENTITY.SingleOrDefault<TBL_READ_IDENTITY>((TBL_READ_IDENTITY x) => x.InfoID == this._obj.InfoID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_READ_IDENTITY.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_READ_IDENTITY> query =
                from u in this._db.TBL_READ_IDENTITY.AsEnumerable<TBL_READ_IDENTITY>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_READ_IDENTITY>(query));
        }

        public int GetRfidCardId(string kartno)
        {
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            int kartId = -1;
            int ınt32 = Convert.ToInt32(kartno.Substring(2), 16);
            decimal num = decimal.Parse(ınt32.ToString());
            string kartnumber = num.ToString();
            try
            {
                try
                {
                    con.Open();
                    string query = string.Concat("Declare @kartId as int Set @kartId = -1 select @kartId = isnull([RfidCardId],-1) from [dbo].[TBL_RFIDCARD] where [RfidCardNumber] = '", kartnumber, "' select @kartId");
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text
                    };
                    kartId = int.Parse(cmd.ExecuteScalar().ToString());
                    if (kartId == -1)
                    {
                        query = string.Concat("insert into [dbo].[TBL_RFIDCARD]([RfidCardNumber]) OUTPUT INSERTED.RfidCardId values ('", kartnumber, "')");
                        cmd = new SqlCommand(query, con);
                        kartId = int.Parse(cmd.ExecuteScalar().ToString());
                    }
                }
                catch (Exception exception)
                {
                    string hata = exception.Message;
                }
            }
            finally
            {
                con.Close();
            }
            return kartId;
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_READ_IDENTITY original = this._db.TBL_READ_IDENTITY.SingleOrDefault<TBL_READ_IDENTITY>((TBL_READ_IDENTITY x) => x.InfoID == this._obj.InfoID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.InfoID = original.InfoID;
                this._obj.TerminalId = original.TerminalId;
                this._obj.Yonu = original.Yonu;
                this._obj.RfidCardId = original.RfidCardId;
                this._obj.ReadDate = original.ReadDate;
                this._obj.ArchiveId = original.ArchiveId;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_READ_IDENTITY _instance = new TBL_READ_IDENTITY()
            {
                InfoID = this._obj.InfoID,
                TerminalId = this._obj.TerminalId,
                Yonu = this._obj.Yonu,
                RfidCardId = this._obj.RfidCardId,
                ReadDate = this._obj.ReadDate,
                ArchiveId = this._obj.ArchiveId
            };
            try
            {
                this._db.TBL_READ_IDENTITY.Add(_instance);
                this._db.SaveChanges();
                this._obj.InfoID = _instance.InfoID;
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
            TBL_READ_IDENTITY original = this._db.TBL_READ_IDENTITY.SingleOrDefault<TBL_READ_IDENTITY>((TBL_READ_IDENTITY x) => x.InfoID == this._obj.InfoID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.InfoID = this._obj.InfoID;
                original.TerminalId = this._obj.TerminalId;
                original.Yonu = this._obj.Yonu;
                original.RfidCardId = this._obj.RfidCardId;
                original.ReadDate = this._obj.ReadDate;
                original.ArchiveId = this._obj.ArchiveId;
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