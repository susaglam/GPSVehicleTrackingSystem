using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
namespace YTSDAL
{
    public class TBL_ARCHIVEDAL : IOperation
    {
        private TBL_ARCHIVE _obj = new TBL_ARCHIVE();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_ARCHIVE obj
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

        public TBL_ARCHIVEDAL()
        {
            this._obj = new TBL_ARCHIVE();
        }

        public void AlarmKaydet(int alarm, int archiveId, int terminalId, DateTime dt)
        {
            TBL_ALARM b = new TBL_ALARM()
            {
                ArchiveID = archiveId,
                TerminalID = terminalId,
                ContactType = 0,
                AlarmType = alarm,
                ContactNumber = "",
                SpecCode = "",
                TelemetryValue = "",
                AlarmDate = dt,
                LocalAlarmDate = DateTime.Now,
                AlarmMore = ""
            };
            (new TBL_ALARMDAL()
            {
                obj = b
            }).Save();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ARCHIVE original = this._db.TBL_ARCHIVE.SingleOrDefault<TBL_ARCHIVE>((TBL_ARCHIVE x) => x.ArchiveID == this._obj.ArchiveID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_ARCHIVE.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GetAllData()
        {
            IEnumerable<TBL_ARCHIVE> query =
                from u in this._db.TBL_ARCHIVE.AsEnumerable<TBL_ARCHIVE>()
                select u;
            return JsonConvert.SerializeObject(this.dh.ExecuteQueryToDataTable<TBL_ARCHIVE>(query));
        }

        public string Read()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ARCHIVE original = this._db.TBL_ARCHIVE.SingleOrDefault<TBL_ARCHIVE>((TBL_ARCHIVE x) => x.ArchiveID == this._obj.ArchiveID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.ArchiveID = original.ArchiveID;
                this._obj.TransCode = original.TransCode;
                this._obj.UnitID = original.UnitID;
                this._obj.DateTimeLocal = original.DateTimeLocal;
                this._obj.DateTimeGps = original.DateTimeGps;
                this._obj.CoordinateX = original.CoordinateX;
                this._obj.CoordinateY = original.CoordinateY;
                this._obj.Speed = original.Speed;
                this._obj.Direction = original.Direction;
                this._obj.Distance = original.Distance;
                this._obj.Altitude = original.Altitude;
                this._obj.Ignition = original.Ignition;
                this._obj.NumberOfSatellite = original.NumberOfSatellite;
                this._obj.Parameter1 = original.Parameter1;
                this._obj.Parameter2 = original.Parameter2;
                this._obj.MsgType = original.MsgType;
                this._obj.Status = original.Status;
                this._obj.City = original.City;
                this._obj.Country = original.Country;
                this._obj.District = original.District;
                this._obj.Quarter = original.Quarter;
                this._obj.Street = original.Street;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ARCHIVE _instance = new TBL_ARCHIVE()
            {
                ArchiveID = this._obj.ArchiveID,
                TransCode = this._obj.TransCode,
                UnitID = this._obj.UnitID,
                DateTimeLocal = this._obj.DateTimeLocal,
                DateTimeGps = this._obj.DateTimeGps,
                CoordinateX = this._obj.CoordinateX,
                CoordinateY = this._obj.CoordinateY,
                Speed = this._obj.Speed,
                Direction = this._obj.Direction,
                Distance = this._obj.Distance,
                Altitude = this._obj.Altitude,
                Ignition = this._obj.Ignition,
                NumberOfSatellite = this._obj.NumberOfSatellite,
                Parameter1 = this._obj.Parameter1,
                Parameter2 = this._obj.Parameter2,
                MsgType = this._obj.MsgType,
                Status = this._obj.Status,
                City = this._obj.City,
                Country = this._obj.Country,
                District = this._obj.District,
                Quarter = this._obj.Quarter,
                Street = this._obj.Street,
                RolantiSuresi = _obj.RolantiSuresi
            };
            try
            {
                this._db.TBL_ARCHIVE.Add(_instance);
                this._db.SaveChanges();
                this._obj.ArchiveID = _instance.ArchiveID;
                if (this._obj.ArchiveID > 0)
                {
                    int terminalId = this.dh.GetTerminalId(this._obj.UnitID);
                    TBL_TERMINAL terminal = new TBL_TERMINAL();
                    TBL_TERMINALDAL d = new TBL_TERMINALDAL()
                    {
                        obj = new TBL_TERMINAL()
                        {
                            TerminalID = terminalId
                        }
                    };
                    d.Read();
                    d.obj.ArchiveId = this._obj.ArchiveID;
                    d.obj.LastPositionX = this._obj.CoordinateX;
                    d.obj.LastPositionY = this._obj.CoordinateY;
                    d.obj.UnitIpAddress = this._obj.ipNumber;
                    d.obj.City = this._obj.City;
                    d.obj.Country = this._obj.Country;
                    d.obj.District = this._obj.District;
                    d.obj.Street = this._obj.Street;
                    d.obj.Quarter = this._obj.Quarter;
                    d.obj.PositionDate = DateTime.Now;
                    if (d.obj.KontakDurumu != _instance.Ignition)
                    {
                        d.obj.KontakDurumu = _instance.Ignition;
                        if (d.obj.KontakDurumu != 1)
                        {
                            this.AlarmKaydet(13, d.obj.ArchiveId, terminalId, _instance.DateTimeGps);
                        }
                        else
                        {
                            this.AlarmKaydet(12, d.obj.ArchiveId, terminalId, _instance.DateTimeGps);
                        }
                    }
                    if (terminalId != 0)
                    {
                        d.Update();
                    }
                    else
                    {
                        d.Save();
                    }
                }
                str = ReturnMessage.KayitBasarili.ToString();
            }
            catch (Exception e)
            {
                str = ReturnMessage.KayitHatasi.ToString();
                string h = e.Message.ToString();
            }
            return str;
        }

        public string Update()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_ARCHIVE original = this._db.TBL_ARCHIVE.SingleOrDefault<TBL_ARCHIVE>((TBL_ARCHIVE x) => x.ArchiveID == this._obj.ArchiveID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.ArchiveID = this._obj.ArchiveID;
                original.TransCode = this._obj.TransCode;
                original.UnitID = this._obj.UnitID;
                original.DateTimeLocal = this._obj.DateTimeLocal;
                original.DateTimeGps = this._obj.DateTimeGps;
                original.CoordinateX = this._obj.CoordinateX;
                original.CoordinateY = this._obj.CoordinateY;
                original.Speed = this._obj.Speed;
                original.Direction = this._obj.Direction;
                original.Distance = this._obj.Distance;
                original.Altitude = this._obj.Altitude;
                original.Ignition = this._obj.Ignition;
                original.NumberOfSatellite = this._obj.NumberOfSatellite;
                original.Parameter1 = this._obj.Parameter1;
                original.Parameter2 = this._obj.Parameter2;
                original.MsgType = this._obj.MsgType;
                original.Status = this._obj.Status;
                original.City = this._obj.City;
                original.Country = this._obj.Country;
                original.District = this._obj.District;
                original.Quarter = this._obj.Quarter;
                original.Street = this._obj.Street;
                original.RolantiSuresi = _obj.RolantiSuresi;
                //original.ipNumber = _obj.ipNumber;
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