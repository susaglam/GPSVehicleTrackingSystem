
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;namespace YTSDAL
{
    public class TBL_TERMINALDAL : IOperation
    {
        private TBL_TERMINAL _obj = new TBL_TERMINAL();

        private AracTakipDb _db = new AracTakipDb();

        private DbHelper dh = new DbHelper();

        private string _query;

        public TBL_TERMINAL obj
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

        public TBL_TERMINALDAL()
        {
            this._obj = new TBL_TERMINAL();
        }

        public string Delete()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_TERMINAL original = this._db.TBL_TERMINAL.SingleOrDefault<TBL_TERMINAL>((TBL_TERMINAL x) => x.TerminalID == this._obj.TerminalID);
            if (original == null)
            {
                str = ReturnMessage.DeleteHatasi.ToString();
            }
            else
            {
                this._db.TBL_TERMINAL.Remove(original);
                this._db.SaveChanges();
                str = ReturnMessage.DeleteEdildi.ToString();
            }
            return str;
        }

        public string GecmisIzleme()
        {
            DateTime bas = new DateTime(2014, 7, 11, 0, 0, 0);
            DateTime bit = new DateTime(2014, 7, 12, 23, 59, 59);
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetAktiviteRaporuByTerminalId(3, bas, bit);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetAktiviteRaporuByTerminalId(int terminalId, DateTime bas, DateTime bit)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetAktiviteRaporuByTerminalId(terminalId, bas, bit);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetAllData()
        {
            Kullanici k = this.dh.GetKullaniciBilgileri();
            DataSet ds = new DataSet();
            string _result = "";
            if (k.role == "Normal")
            {
                ds = this.dh.GetAracListesi(k.CompanyId, 1);
                if (ds.Tables.Count > 0)
                {
                    _result = JsonConvert.SerializeObject(ds);
                }
            }
            if (k.role == "Ana")
            {
                ds = this.dh.GetAracListesi(k.UpperCompanyId, 2);
                if (ds.Tables.Count > 0)
                {
                    _result = JsonConvert.SerializeObject(ds);
                }
            }
            return _result;
        }

        public string GetAnimatedData(int terminalId, int archiveId)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.PopDataBaseName(terminalId, archiveId);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
                _result = string.Concat(_result, "_", ds.Tables.Count);
            }
            return _result;
        }

        public string GetAracInfo(int terminalId)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetAracInfo(terminalId);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetAracListesi(int durum)
        {
            Kullanici b = this.dh.GetKullaniciBilgileri();
            DataSet ds = new DataSet();
            string _result = "";
            if (b.role != "Normal")
            {
                b.CompanyId = b.UpperCompanyId;
            }
            ds = this.dh.GetAracListesi(b.CompanyId, durum);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetCalismaRaporu(int companyId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetCalismaRaporuByCompanyId(companyId, dtbaslama, dtbitme);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetCalismaTerminal(int TerminalId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetCalismaRaporuByTerminalId(TerminalId, dtbaslama, dtbitme);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public DataSet GetDataSet(int terminalId, int archiveId)
        {
            DataSet ds = new DataSet();
            return this.dh.PopDataBaseName(terminalId, archiveId);
        }

        public string GetDetayliMesafeRaporu(int terminalId, DateTime dtBaslama, DateTime dtBitme)
        {
            string _result = "";
            List<TBL_SEFER_PAKETI> ds = this.dh.GetMesafeRaporuByTerminalId(terminalId, dtBaslama, dtBitme);
            if (ds.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetDurmaRaporu(int companyId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetDurmaRaporuByCompanyId(companyId, dtbaslama, dtbitme);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetDurmaTerminal(int TerminalId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetDurmaRaporuByTerminalId(TerminalId, dtbaslama, dtbitme);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetGecmisIzleme(int terminalId, string baslamazamani, string bitmezamani)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GecmisIzleme(terminalId, baslamazamani, bitmezamani);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetIhlalListesiByCompanyId(int companyId)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetIhlalListesiByCompanylId(companyId);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetIhlalListesiByTerminalId(int terminalId, DateTime bas, DateTime bit)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetIhlalListesiByTerminalId(terminalId, bas, bit);
            if (ds.Tables.Count > 0)
            {
                _result = JsonConvert.SerializeObject(ds);
            }
            return _result;
        }

        public string GetRolantiRaporu(int companyId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            string _result = "";
            ds = this.dh.GetRolantiRaporuByCompanyId(companyId, dtbaslama, dtbitme);
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
            TBL_TERMINAL original = this._db.TBL_TERMINAL.SingleOrDefault<TBL_TERMINAL>((TBL_TERMINAL x) => x.TerminalID == this._obj.TerminalID);
            if (original == null)
            {
                str = ReturnMessage.OkumaHatasi.ToString();
            }
            else
            {
                this._obj.TerminalID = original.TerminalID;
                this._obj.ArchiveId = original.ArchiveId;
                this._obj.UnitId = original.UnitId;
                this._obj.UnitModel = original.UnitModel;
                this._obj.UnitIpAddress = original.UnitIpAddress;
                this._obj.AccessoryType = original.AccessoryType;
                this._obj.LastPositionX = original.LastPositionX;
                this._obj.LastPositionY = original.LastPositionY;
                this._obj.CompanyID = original.CompanyID;
                this._obj.HistoryID = original.HistoryID;
                this._obj.CreateDate = original.CreateDate;
                this._obj.TerminalStatus = original.TerminalStatus;
                this._obj.IsActive = original.IsActive;
                this._obj.Battery = original.Battery;
                this._obj.AccStatus = original.AccStatus;
                this._obj.UserID = original.UserID;
                this._obj.OnlineDate = original.OnlineDate;
                this._obj.OfflineDate = original.OfflineDate;
                this._obj.PositionDate = original.PositionDate;
                this._obj.VehicleDefaultDistance = original.VehicleDefaultDistance;
                this._obj.DefaultTankValue = original.DefaultTankValue;
                this._obj.CurrentTankValue = original.CurrentTankValue;
                this._obj.PlateNo = original.PlateNo;
                this._obj.City = original.City;
                this._obj.Country = original.Country;
                this._obj.District = original.District;
                this._obj.Quarter = original.Quarter;
                this._obj.Street = original.Street;
                this._obj.GsmNumber = original.GsmNumber;
                this._obj.MotorBlokajDurumu = original.MotorBlokajDurumu;
                this._obj.KontakDurumu = original.KontakDurumu;
                str = ReturnMessage.OkumaBasarili.ToString();
            }
            return str;
        }

        public string Save()
        {
            string str;
            this._db = new AracTakipDb();
            TBL_TERMINAL _instance = new TBL_TERMINAL()
            {
                TerminalID = this._obj.TerminalID,
                ArchiveId = this._obj.ArchiveId,
                UnitId = this._obj.UnitId,
                UnitModel = this._obj.UnitModel,
                UnitIpAddress = this._obj.UnitIpAddress,
                AccessoryType = this._obj.AccessoryType,
                LastPositionX = this._obj.LastPositionX,
                LastPositionY = this._obj.LastPositionY,
                CompanyID = this._obj.CompanyID,
                HistoryID = this._obj.HistoryID,
                CreateDate = this._obj.CreateDate,
                TerminalStatus = this._obj.TerminalStatus,
                IsActive = this._obj.IsActive,
                Battery = this._obj.Battery,
                AccStatus = this._obj.AccStatus,
                UserID = this._obj.UserID,
                OnlineDate = this._obj.OnlineDate,
                OfflineDate = this._obj.OfflineDate,
                PositionDate = this._obj.PositionDate,
                VehicleDefaultDistance = this._obj.VehicleDefaultDistance,
                DefaultTankValue = this._obj.DefaultTankValue,
                CurrentTankValue = this._obj.CurrentTankValue,
                PlateNo = this._obj.PlateNo,
                City = this._obj.City,
                Country = this._obj.Country,
                District = this._obj.District,
                Quarter = this._obj.Quarter,
                Street = this._obj.Street,
                GsmNumber = this._obj.GsmNumber,
                MotorBlokajDurumu = this._obj.MotorBlokajDurumu,
                KontakDurumu = this._obj.KontakDurumu
            };
            try
            {
                this._db.TBL_TERMINAL.Add(_instance);
                this._db.SaveChanges();
                this._obj.ArchiveId = _instance.ArchiveId;
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
            TBL_TERMINAL original = this._db.TBL_TERMINAL.SingleOrDefault<TBL_TERMINAL>((TBL_TERMINAL x) => x.TerminalID == this._obj.TerminalID);
            if (original == null)
            {
                str = ReturnMessage.UpdateHatasi.ToString();
            }
            else
            {
                original.TerminalID = this._obj.TerminalID;
                original.ArchiveId = this._obj.ArchiveId;
                original.UnitId = this._obj.UnitId;
                original.UnitModel = this._obj.UnitModel;
                original.UnitIpAddress = this._obj.UnitIpAddress;
                original.AccessoryType = this._obj.AccessoryType;
                original.LastPositionX = this._obj.LastPositionX;
                original.LastPositionY = this._obj.LastPositionY;
                original.CompanyID = this._obj.CompanyID;
                original.HistoryID = this._obj.HistoryID;
                original.CreateDate = this._obj.CreateDate;
                original.TerminalStatus = this._obj.TerminalStatus;
                original.IsActive = this._obj.IsActive;
                original.Battery = this._obj.Battery;
                original.AccStatus = this._obj.AccStatus;
                original.UserID = this._obj.UserID;
                original.OnlineDate = this._obj.OnlineDate;
                original.OfflineDate = this._obj.OfflineDate;
                original.PositionDate = this._obj.PositionDate;
                original.VehicleDefaultDistance = this._obj.VehicleDefaultDistance;
                original.DefaultTankValue = this._obj.DefaultTankValue;
                original.CurrentTankValue = this._obj.CurrentTankValue;
                original.PlateNo = this._obj.PlateNo;
                original.City = this._obj.City;
                original.Country = this._obj.Country;
                original.District = this._obj.District;
                original.Quarter = this._obj.Quarter;
                original.Street = this._obj.Street;
                original.GsmNumber = this._obj.GsmNumber;
                original.MotorBlokajDurumu = this._obj.MotorBlokajDurumu;
                original.KontakDurumu = this._obj.KontakDurumu;
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