
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_PROG_ANSWERDAL : IOperation
    {
        private TBL_PROG_ANSWER _obj = new TBL_PROG_ANSWER();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_PROG_ANSWER obj
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

        public TBL_PROG_ANSWERDAL()
        {
            this._obj = new TBL_PROG_ANSWER();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_PROG_ANSWER original = this._db.TBL_PROG_ANSWER.SingleOrDefault<TBL_PROG_ANSWER>((TBL_PROG_ANSWER x) => x.AnswerID == this._obj.AnswerID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_PROG_ANSWER.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_PROG_ANSWER> query =
                from u in this._db.TBL_PROG_ANSWER.AsEnumerable<TBL_PROG_ANSWER>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_PROG_ANSWER>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_PROG_ANSWER original = this._db.TBL_PROG_ANSWER.SingleOrDefault<TBL_PROG_ANSWER>((TBL_PROG_ANSWER x) => x.AnswerID == this._obj.AnswerID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.AnswerID = original.AnswerID;
                this._obj.MsgType = original.MsgType;
                this._obj.TransCode = original.TransCode;
                this._obj.UnitID = original.UnitID;
                this._obj.CommandanData = original.CommandanData;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_PROG_ANSWER _instance = new TBL_PROG_ANSWER()
            {
                AnswerID = this._obj.AnswerID,
                MsgType = this._obj.MsgType,
                TransCode = this._obj.TransCode,
                UnitID = this._obj.UnitID,
                CommandanData = this._obj.CommandanData
            };
            try
            {
                this._db.TBL_PROG_ANSWER.Add(_instance);
                this._db.SaveChanges();
                this._obj.AnswerID = _instance.AnswerID;
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
            TBL_PROG_ANSWER original = this._db.TBL_PROG_ANSWER.SingleOrDefault<TBL_PROG_ANSWER>((TBL_PROG_ANSWER x) => x.AnswerID == this._obj.AnswerID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.AnswerID = this._obj.AnswerID;
                original.MsgType = this._obj.MsgType;
                original.TransCode = this._obj.TransCode;
                original.UnitID = this._obj.UnitID;
                original.CommandanData = this._obj.CommandanData;
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