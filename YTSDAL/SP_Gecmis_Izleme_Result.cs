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
    
    public partial class SP_Gecmis_Izleme_Result
    {
        public System.DateTime DateTimeGps { get; set; }
        public System.DateTime DateTimeLocal { get; set; }
        public Nullable<int> TerminalID { get; set; }
        public decimal CoordinateX { get; set; }
        public decimal CoordinateY { get; set; }
        public decimal Direction { get; set; }
        public int ArchiveID { get; set; }
        public string UnitID { get; set; }
        public byte Ignition { get; set; }
        public decimal Speed { get; set; }
    }
}
