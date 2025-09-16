namespace RMS.Models.Common;

public class EnumForEntity
{
    public enum NoeAmalyatKhakiAbnie
    {
        KhakBardari = 1,
        KhakRizi = 2,
        PayKani = 3,
    }

    public enum NoeAmalyatKhaki
    {
        LajanBardari = 10,
        KhakBardariNabati = 11,
        KhakBardariDarZaminNarm = 12,
        KhakBardariDarZaminDoj = 13,
        KhakBardariDarZaminSangi = 14,
        KhakBardariDarZaminRaza = 15,
        KhakBardariDarZaminMonbasetShavande = 16,
    }

    public enum NoeFehrestBaha
    {
        RahDari = 232,
        Abnie = 233,
        RahoBand = 234
    }

    public enum NoeBarAvord
    {
        WithoutProject = 1,
        WithProject = 2
    }
}
