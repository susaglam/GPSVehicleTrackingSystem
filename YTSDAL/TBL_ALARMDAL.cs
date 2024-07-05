
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;namespace YTSDAL
{
    public class TBL_ALARMDAL : IOperation
    {
        private TBL_ALARM _obj = new TBL_ALARM();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_ALARM obj
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

        public TBL_ALARMDAL()
        {
            this._obj = new TBL_ALARM();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ALARM original = this._db.TBL_ALARM.SingleOrDefault<TBL_ALARM>((TBL_ALARM x) => x.AlarmID == this._obj.AlarmID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_ALARM.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_ALARM> query =
                from u in this._db.TBL_ALARM.AsEnumerable<TBL_ALARM>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_ALARM>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ALARM original = this._db.TBL_ALARM.SingleOrDefault<TBL_ALARM>((TBL_ALARM x) => x.AlarmID == this._obj.AlarmID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.AlarmID = original.AlarmID;
                this._obj.ArchiveID = original.ArchiveID;
                this._obj.TerminalID = original.TerminalID;
                this._obj.AlarmType = original.AlarmType;
                this._obj.AlarmMore = original.AlarmMore;
                this._obj.AlarmDate = original.AlarmDate;
                this._obj.LocalAlarmDate = original.LocalAlarmDate;
                this._obj.ContactType = original.ContactType;
                this._obj.SpecCode = original.SpecCode;
                this._obj.ContactNumber = original.ContactNumber;
                this._obj.TelemetryValue = original.TelemetryValue;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ALARM _instance = new TBL_ALARM()
            {
                AlarmID = this._obj.AlarmID,
                ArchiveID = this._obj.ArchiveID,
                TerminalID = this._obj.TerminalID,
                AlarmType = this._obj.AlarmType,
                AlarmMore = this._obj.AlarmMore,
                AlarmDate = this._obj.AlarmDate,
                LocalAlarmDate = this._obj.LocalAlarmDate,
                ContactType = this._obj.ContactType,
                SpecCode = this._obj.SpecCode,
                ContactNumber = this._obj.ContactNumber,
                TelemetryValue = this._obj.TelemetryValue
            };
            try
            {
                this._db.TBL_ALARM.Add(_instance);
                this._db.SaveChanges();
                this._obj.ArchiveID = _instance.ArchiveID;
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
            TBL_ALARM original = this._db.TBL_ALARM.SingleOrDefault<TBL_ALARM>((TBL_ALARM x) => x.AlarmID == this._obj.AlarmID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.AlarmID = this._obj.AlarmID;
                original.ArchiveID = this._obj.ArchiveID;
                original.TerminalID = this._obj.TerminalID;
                original.AlarmType = this._obj.AlarmType;
                original.AlarmMore = this._obj.AlarmMore;
                original.AlarmDate = this._obj.AlarmDate;
                original.LocalAlarmDate = this._obj.LocalAlarmDate;
                original.ContactType = this._obj.ContactType;
                original.SpecCode = this._obj.SpecCode;
                original.ContactNumber = this._obj.ContactNumber;
                original.TelemetryValue = this._obj.TelemetryValue;
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