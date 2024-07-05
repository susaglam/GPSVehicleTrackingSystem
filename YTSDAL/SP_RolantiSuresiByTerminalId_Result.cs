//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YTSDAL
{
    using System;
    
    public partial class SP_RolantiSuresiByTerminalId_Result
    {
        public int rolantisuresi { get; set; }
        public int TerminalID { get; set; }
        public int ArchiveId { get; set; }
        public string UnitId { get; set; }
        public string UnitModel { get; set; }
        public string UnitIpAddress { get; set; }
        public string AccessoryType { get; set; }
        public decimal LastPositionX { get; set; }
        public decimal LastPositionY { get; set; }
        public int CompanyID { get; set; }
        public int HistoryID { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool TerminalStatus { get; set; }
        public bool IsActive { get; set; }
        public byte Battery { get; set; }
        public bool AccStatus { get; set; }
        public int UserID { get; set; }
        public System.DateTime OnlineDate { get; set; }
        public System.DateTime OfflineDate { get; set; }
        public System.DateTime PositionDate { get; set; }
        public decimal VehicleDefaultDistance { get; set; }
        public decimal DefaultTankValue { get; set; }
        public decimal CurrentTankValue { get; set; }
        public string PlateNo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string District { get; set; }
        public string Quarter { get; set; }
        public string Street { get; set; }
        public string GsmNumber { get; set; }
        public Nullable<bool> MotorBlokajDurumu { get; set; }
        public Nullable<int> KontakDurumu { get; set; }
    }
}
