using System;
namespace YTSDAL
{
    [Flags]
    public enum ReturnMessage
    {
        KayitBasarili,
        KayitHatasi,
        UpdateEdildi,
        UpdateHatasi,
        DeleteEdildi,
        DeleteHatasi,
        OkumaHatasi,
        OkumaBasarili
    }
}